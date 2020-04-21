using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidLAMap.Core.Models;
using CovidLAMap.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CovidLAMap.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {

        private readonly ILogger<EthEventController> _logger;
        private readonly ICredentialService _credentialService;

        public CredentialsController(ILogger<EthEventController> logger,
            ICredentialService credentialService)
        {
            _logger = logger;
            _credentialService = credentialService;
        }
        [HttpGet("ByCountry")]
        public async Task<ActionResult<IEnumerable<AgregationsByCountry>>> ByCountry()
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
        }
    }
}