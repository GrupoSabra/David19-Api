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
    public class EthController : ControllerBase
    {

        private readonly ILogger<EthController> _logger;
        private readonly ICredentialService _credentialService;

        public EthController(ILogger<EthController> logger, 
            ICredentialService credentialService)
        {
            _logger = logger;
            _credentialService = credentialService;
        }

        [HttpPost("event")]
        public async Task<ActionResult> Post(EthEventDTO eventDTO)
        {
            if (eventDTO == null || eventDTO.Status != "CONFIRMED") return Ok();
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

        [HttpPost("all-events")]
        public ActionResult AllEvents()
        {
            return Ok();
        }

        [HttpPost("all-blocks")]
        public ActionResult AllBlocks()
        {
            return Ok();
        }
    }
}
