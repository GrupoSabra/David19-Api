using CovidLAMap.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Data
{
    public static class DependenciesSetup
    {
    
        public static void SetupDataDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICovidUnitOfWork, CovidUnitOfWork>();
            
            services.AddDbContext<CovidDbContext2>(options =>
               options.UseNpgsql(configuration.GetConnectionString("Default"),
              x => { 
                  x.MigrationsAssembly("CovidLAMap.Data");
                  x.UseNetTopologySuite();
              }));
        }
    }
}
