using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CovidLAMap.API.ApiModels;
using CovidLAMap.Core.Extensions;
using CovidLAMap.Core.Models;
using CovidLAMap.Services.Interfaces;
using CsvHelper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CovidLAMap.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        private readonly ILogger<CredentialsController> _logger;
        private readonly ICredentialService _credentialService;
        private readonly IDistributedCache _cache;

        public CredentialsController(ILogger<CredentialsController> logger,
            ICredentialService credentialService, IDistributedCache cache)
        {
            _logger = logger;
            _credentialService = credentialService;
            _cache = cache;
        }

        [HttpGet("ByCountry")]
        [ResponseCache(Duration = 60)] //Todo
        public async Task<ActionResult> ByCountry()
        {
            return await _cache.GetOrSetAsync("byCountry", async () =>
            {
                try
                {
                    var byCountry = await _credentialService.GetByCountryAsync();
                    return Ok(byCountry);
                }
                catch (Exception e)
                {
                    var guid = Guid.NewGuid();
                    _logger.LogError(e, $"Error on Post. Id: {guid}", null);
                    return StatusCode(500, $"Error Id {guid}");
                }
            }, TimeSpan.FromSeconds(60));
        }

        [HttpGet("ByCountryCsv")]
        [ResponseCache(Duration = 60)] //TODO config
        public async Task<ActionResult> ByCountryCsv()
        {
            var csv = await _cache.GetOrSetAsync<string>("byCountryCsv", async () =>
            {
                try
                {
                    var byCountryEnumerable = await _credentialService.GetByCountryAsync();
                    var finalCsv = ToCsv(byCountryEnumerable);
                    return finalCsv;
                }
                catch (Exception e)
                {
                    var guid = Guid.NewGuid();
                    _logger.LogError(e, $"Error on Post. Id: {guid}", null);
                    return $"Error Id {guid}";
                }
            }, TimeSpan.FromSeconds(60)); //TODO config
            return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", "data.csv");
        }

        [HttpPost("query")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult> Query(QueryCredentials queryCredentials)
        {
            return await QueryCredentials(queryCredentials); //TODO cache 
        }

        private async Task<ActionResult> QueryCredentials(QueryCredentials queryCredentials)
        {
            try
            {

                if (queryCredentials.Aggregated.HasValue && queryCredentials.Aggregated.Value == true &&
                    queryCredentials.ClusterRadius.HasValue && queryCredentials.ClusterRadius.Value > 0)
                {
                    var aggregatedList = await _credentialService.ClusteredCredentials(queryCredentials.Lat.GetValueOrDefault(),
                    queryCredentials.Lon.GetValueOrDefault(), queryCredentials.Radius.GetValueOrDefault(), queryCredentials.ClusterRadius.GetValueOrDefault(),
                    queryCredentials.Filter?.AgeToTuple(), queryCredentials.Filter?.Sex);
                    return Ok(aggregatedList);
                }

                if (queryCredentials.Aggregated.HasValue && queryCredentials.Aggregated.Value == true)
                {
                    var aggregatedList = await _credentialService.GetPointsInCircleAggregated(queryCredentials.Lat.GetValueOrDefault(),
                    queryCredentials.Lon.GetValueOrDefault(), queryCredentials.Radius.GetValueOrDefault(), queryCredentials.Filter?.Country,
                     queryCredentials.Filter?.State, queryCredentials.Filter?.AgeToTuple(), queryCredentials.Filter?.Sex);
                    return Ok(aggregatedList);
                }

                if (queryCredentials.Lat.HasValue && queryCredentials.Lon.HasValue && queryCredentials.Radius.HasValue)
                {
                    if (queryCredentials.Radius.Value <= 0) return StatusCode(422, "Radius cannot be 0 or less");
                    if (queryCredentials.Filter != null)
                    {
                        var list = await _credentialService.GetPointsInCircle(queryCredentials.Lat.Value,
                        queryCredentials.Lon.Value, queryCredentials.Radius.Value, queryCredentials.Filter.Country,
                         queryCredentials.Filter.State, queryCredentials.Filter.AgeToTuple(), queryCredentials.Filter.Sex);
                        return Ok(list);
                    }
                    else
                    {
                        var list = await _credentialService.GetPointsInCircle(queryCredentials.Lat.Value,
                        queryCredentials.Lon.Value, queryCredentials.Radius.Value);
                        return Ok(list);
                    }
                }

                return NotFound();
            }
            catch (Exception e)
            {
                var guid = Guid.NewGuid();
                _logger.LogError(e, $"Error on Post. Id: {guid}", null);
                return StatusCode(500, $"Error Id {guid}");
            }
        }

        [HttpPost("querycsv")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult> QueryCsv(QueryCredentials queryCredentials)
        {
            try //TODO Cache
            {

                if (queryCredentials.Aggregated.HasValue && queryCredentials.Aggregated.Value == true &&
                   queryCredentials.ClusterRadius.HasValue && queryCredentials.ClusterRadius.Value > 0)
                {
                    var aggregatedList = await _credentialService.ClusteredCredentials(queryCredentials.Lat.GetValueOrDefault(),
                    queryCredentials.Lon.GetValueOrDefault(), queryCredentials.Radius.GetValueOrDefault(), queryCredentials.ClusterRadius.GetValueOrDefault(),
                    queryCredentials.Filter?.AgeToTuple(), queryCredentials.Filter?.Sex);
                    string csv = ToCsv(aggregatedList);
                    return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", "data.csv");
                }

                if (queryCredentials.Aggregated.HasValue && queryCredentials.Aggregated.Value == true)
                {
                    var aggregatedList = await _credentialService.GetPointsInCircleAggregated(queryCredentials.Lat.Value,
                    queryCredentials.Lon.Value, queryCredentials.Radius.Value, queryCredentials.Filter?.Country,
                     queryCredentials.Filter?.State, queryCredentials.Filter?.AgeToTuple(), queryCredentials.Filter?.Sex);
                    string csv = ToCsv(aggregatedList);
                    return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", "data.csv");
                }

                if (queryCredentials.Lat.HasValue && queryCredentials.Lon.HasValue && queryCredentials.Radius.HasValue)
                {
                    if (queryCredentials.Radius.Value <= 0) return StatusCode(422, "Radius cannot be 0 or less");

                    if(queryCredentials.Filter != null)
                    {
                        var list = await _credentialService.GetPointsInCircle(queryCredentials.Lat.Value,
                        queryCredentials.Lon.Value, queryCredentials.Radius.Value, queryCredentials.Filter.Country,
                         queryCredentials.Filter.State, queryCredentials.Filter.AgeToTuple(), queryCredentials.Filter.Sex);
                        string finalCsv = ToCsv(list);
                        return File(System.Text.Encoding.UTF8.GetBytes(finalCsv), "text/csv", "data.csv");
                    }
                    else
                    {
                        var list = await _credentialService.GetPointsInCircle(queryCredentials.Lat.Value,
                        queryCredentials.Lon.Value, queryCredentials.Radius.Value);
                        string finalCsv = ToCsv(list);
                        return File(System.Text.Encoding.UTF8.GetBytes(finalCsv), "text/csv", "data.csv");
                    }
                }

                return NotFound();
            }
            catch (Exception e)
            {
                var guid = Guid.NewGuid();
                _logger.LogError(e, $"Error on Post. Id: {guid}", null);
                return StatusCode(500, $"Error Id {guid}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">Infected = 0,
        /// Healthy = 1,
        /// Symtomps = 2,
        /// Recovered = 3</param>
        /// <param name="country"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet("ByHealthStatus")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult> ByStatus(HealthStatus status, string country, string state)
        {
            var result = await _credentialService.GetByTypeAsync(status, country, state);
            return Ok(result);
        }

        private static string ToCsv(IEnumerable<RegisteredCredential> list)
        {
            var csvEnumerable = list.Select(x => CsvAgregationsByCountry.From(x));
            using var writer = new StringWriter();
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Configuration.RegisterClassMap<CsvAgregationsByCountryMap>();
            csv.WriteRecords(csvEnumerable);
            return writer.ToString();
        }

        private static string ToCsv(IEnumerable<AgregationsByCountry> list)
        {
            var csvEnumerable = list.Select(x => CsvAgregationsByCountry.From(x));
            using var writer = new StringWriter();
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Configuration.RegisterClassMap<CsvAgregationsByCountryMap>();
            csv.WriteRecords(csvEnumerable);
            return writer.ToString();
        }
    }
}