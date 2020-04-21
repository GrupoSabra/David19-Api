using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidLAMap.Core.DTOs;
using CovidLAMap.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CovidLAMap.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EthEventController : ControllerBase
    {

        private readonly ILogger<EthEventController> _logger;
        private readonly ICredentialService _credentialService;

        public EthEventController(ILogger<EthEventController> logger, 
            ICredentialService credentialService)
        {
            _logger = logger;
            _credentialService = credentialService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(EthEventDTO eventDTO)
        {
            try
            {
                var name = eventDTO.Name.ToLowerInvariant();
                if (name == "credentialregistered")
                {
                    await _credentialService.ImportAsync(eventDTO);
                    return Ok();
                }
                else if (name == "credentialrevoked")
                {
                    await _credentialService.RevokeCredential(eventDTO);
                    return Ok();
                }

                return StatusCode(500, $"The event {name} is not supported");
            }
            catch (Exception e)
            {
                var guid = Guid.NewGuid();
                _logger.LogError(e, $"Error on Post. Id: {guid}", eventDTO);
                return StatusCode(500, $"Error Id {guid}");
            }
            
            
        }
    }
}
