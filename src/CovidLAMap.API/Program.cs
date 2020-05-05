using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
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

            Log.Logger = new LoggerConfiguration()
           .Enrich.FromLogContext()
           .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticUri)
           {
               ModifyConnectionSettings = x=> 
               x.BasicAuthentication(elasticSection.GetValue<string>("user"), elasticSection.GetValue<string>("password")),
               IndexFormat= elasticSection.GetValue<string>("index"),
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
    }
}
