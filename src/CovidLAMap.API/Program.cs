using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CovidLAMap.Core.DTOs;
using CovidLAMap.Services;
using CovidLAMap.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;
using Serilog.Sinks.PostgreSQL;

namespace CovidLAMap.API
{
    public class Program
    {
        private const string CONFIG_ENV_VAR_PREFIX = "DAVID19_API_";
        private static IConfigurationRoot config;

        public static int Main(string[] args)
        {
            CreateConfig(args);
            CreateLogger();
            try
            {
                var section = config.GetSection("ConsumerMode");
                if(section.GetValue("Enabled", false))
                {
                    ConsumerServer(section.GetValue<string>("Address"));

                }
                else
                {
                    Log.Information("Starting web host");
                    CreateHostBuilder(args).Build().Run();
                }
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseSerilog()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables(CONFIG_ENV_VAR_PREFIX);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(options =>
                    {
                        options.AddServerHeader = false;
                    });
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateLogger()
        {
            var elasticSection = config.GetSection("Elastic");
            var elasticUri = new Uri(elasticSection.GetValue<string>("url"));
            var user = elasticSection.GetValue<string>("user");
            var pass = elasticSection.GetValue<string>("password");
            var index = elasticSection.GetValue<string>("index");

            Log.Logger = new LoggerConfiguration()
           .Enrich.FromLogContext()
           .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticUri)
           {
               ModifyConnectionSettings = x=> x.BasicAuthentication(user, pass),
               IndexFormat= index,
               TemplateName= "DavidTemplate",
               AutoRegisterTemplate = true,
               AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
               FailureCallback = e => { 
                   Console.WriteLine("Unable to submit event " + e.MessageTemplate); 
               },
               
               EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback,
               FailureSink = new FileSink("./failures.txt", new JsonFormatter(), null),
               MinimumLogEventLevel = LogEventLevel.Information
           })
           .WriteTo.Console(LogEventLevel.Information)
           .CreateLogger();

            Log.Information($"Elastic uri:  {elasticUri}  - user: {user} - index: {index}");
        }

        private static void CreateConfig(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            env = string.IsNullOrEmpty(env) ? string.Empty : env;
            config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env}.json", optional: true)
                            .AddEnvironmentVariables(CONFIG_ENV_VAR_PREFIX)
                            .AddCommandLine(args)
                            .Build();
        }

        private static void ConsumerServer(string server)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "postgres-Ingestion",
                BootstrapServers = server,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
            c.Subscribe("contract-events");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            try
            {
                var services = new ServiceCollection();
                services.SetupServicesDependencies(config);
                var container = services.BuildServiceProvider();
                var credService = container.GetService<ICredentialService>();
                Log.Information("Consumer starting...");
                while (true)
                {
                    try
                    {
                        var cr = c.Consume(cts.Token);
                        ConsumeMessage(cr.Message.Value, credService);
                    }
                    catch (ConsumeException e)
                    {
                        Log.Error(e, "Consume Exception");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                Log.Information("Closing by demand...");
                c.Close();
            }
            catch(Exception e)
            {
                Log.Error(e,"Catchall: ");
            }
        }

        private static void ConsumeMessage(string message, ICredentialService credService)
        {
            try
            {
                var contractEvent = JsonConvert.DeserializeObject<QueueMessage<EthEventDTO>>(message).Details;
                if (contractEvent == null || contractEvent.Status != "CONFIRMED") return;

                var name = contractEvent.Name.ToLowerInvariant();
                if (name == "credentialregistered")
                {
                    credService.ImportAsync(contractEvent).Wait();
                }
                else if (name == "credentialrevoked")
                {
                    credService.RevokeCredential(contractEvent).Wait();
                }
            }
            catch(Exception e)
            {
                Log.Error(e, "Error consuming message: " + message);
            }
        }
    }
}
