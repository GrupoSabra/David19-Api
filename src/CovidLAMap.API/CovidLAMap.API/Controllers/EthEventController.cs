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
    [ApiExplorerSettings(IgnoreApi = true)]
    public class EthController : ControllerBase
    {

        private readonly ILogger<EthController> _logger;
        private readonly ICredentialService _credentialService;
        private readonly IFailedEthEventsService _failedEthEventsService;

        public EthController(ILogger<EthController> logger, 
            ICredentialService credentialService, IFailedEthEventsService failedEthEventsService)
        {
            _logger = logger;
            _credentialService = credentialService;
            _failedEthEventsService = failedEthEventsService;
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
                ///Since Eventum if failed keep trying, no more than 3 times
                if (! await CheckEventIsNotInALoop(eventDTO))
                {
                    var guid = Guid.NewGuid();
                    _logger.LogError(e, $"Error on Post. Id: {guid}", eventDTO);
                    return StatusCode(500, $"Error Id {guid}");
                }
                {
                    _logger.LogCritical(e, "EthEvent is in a loop: ", eventDTO);
                    return Ok();
                }
            }
        }

        private async Task<bool> CheckEventIsNotInALoop(EthEventDTO eventDTO)
        {
            try
            {
                return await _failedEthEventsService.CheckMaxFailedTimes(eventDTO);
            }
            catch(Exception e)
            {
                _logger.LogCritical(e, "EthEvent is trap: ", eventDTO);
                return true;
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
