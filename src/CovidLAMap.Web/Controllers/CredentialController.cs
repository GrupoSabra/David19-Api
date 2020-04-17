using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidLAMap.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovidLAMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialController : ControllerBase
    {
        private ICredentialService credentialService;

        public CredentialController(ICredentialService credentialService)
        {
            this.credentialService = credentialService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = (await credentialService.GetAllByCountryAsync()).ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                //this.logger.LogError(e, "Error on Action Import");
                return StatusCode(504, "Server Error: " + e.Message);
            }
        }

    }
}