using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidLAMap.Core.DTOs;
using CovidLAMap.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovidLAMap.Web.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [ApiController]
    [Route("[controller]")]
    public class EthEventController : ControllerBase
    {
        private readonly ICredentialService credentialService;

        public EthEventController(ICredentialService credentialService)
        {
            this.credentialService = credentialService;
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(EthEventDTO eventDTO)
        {
            try
            {
                await credentialService.ImportAsync(eventDTO);
                return Ok();
            }
            catch (Exception e)
            {
                //this.logger.LogError(e, "Error on Action Import");
                return StatusCode(504, "Server Error: " + e.Message);
            }
        }
    }
}