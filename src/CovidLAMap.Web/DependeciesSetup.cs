using CovidLAMap.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidLAMap.Web
{
    public static class DependeciesSetup
    {
        public static void SetupDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupServicesDependencies(configuration);
        }
    }
}
