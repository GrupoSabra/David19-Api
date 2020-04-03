using Microsoft.AspNetCore.SpaServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using System.Net.Http;

namespace CovidLAMap.Extensions
{
    public static class SpaBuilderExtensions
    {
        /// <summary>
        /// Checks if the angular server is running and use it. If it is not running it runs npm start
        /// </summary>
        /// <param name="spa">the extension ISpaBuilder </param>
        /// <param name="url">the url to check if the angular server is running/param>
        /// <returns></returns>
        public static ISpaBuilder CheckAndStartAngular(this ISpaBuilder spa, string url = "http://localhost:4200/")
        {
            try
            {
                using HttpClient client = new HttpClient();
                var response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
                spa.UseProxyToSpaDevelopmentServer(url);
            }
            catch (Exception)
            {
                spa.UseAngularCliServer(npmScript: "start");
            }

            return spa;
        }
    }
}
