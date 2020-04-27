using CovidLAMap.Data;
using CovidLAMap.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Services
{
    public static class DependenciesSetup
    {
    
        public static void SetupServicesDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupDataDependencies(configuration);
            services.AddHttpClient<IGeoService, GeoService>();
            services.AddTransient<ICredentialService, CredentialService>();
            services.AddTransient<IFailedEthEventsService, FailedEthEventsService>();
        }
    }
}
