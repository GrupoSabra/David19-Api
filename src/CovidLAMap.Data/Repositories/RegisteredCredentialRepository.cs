using CovidLAMap.Core.Models;
using CovidLAMap.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Data.Repositories
{
    public class RegisteredCredentialRepository : Repository<RegisteredCredential>, IRegisteredCredentialRepository
    {
        private readonly CovidDbContext context;

        public RegisteredCredentialRepository(CovidDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<RegisteredCredentialFat>> GetCountAllByCountryAsync()
        {
            var list = await this.context.CredentialsAggregated.FromSqlInterpolated(
                $"SELECT rc.*, c.name_es as CountryName FROM public.\"Countries\" as c join \"RegisteredCredentials\" as rc on ST_Contains(c.geom, rc.\"Location\")"
                ).ToListAsync();
            return list;
        }
    }
}

