using CovidLAMap.Core.Models;
using CovidLAMap.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Data.Repositories
{
    public class AgregationsByTypeRepository : Repository<AgregationsByType>, IAgregationsByTypeRepository
    {
        private readonly CovidDbContext context;

        public AgregationsByTypeRepository(CovidDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<AgregationsByType>> GetByStatus(HealthStatus status,
            string country = "", string state = "", (double, double)? ageRange = null, Sex? sex = null)
        {
            var credType = status.ToCredentialType();
            var statusp = new NpgsqlParameter("htype", (int)credType) { DbType = DbType.Int32 };
            var ageMinp = new NpgsqlParameter("ageMin", ageRange.HasValue ? (object)ageRange.Value.Item1 : DBNull.Value) { IsNullable = true, DbType = DbType.Int32 };
            var ageMaxp = new NpgsqlParameter("ageMax", ageRange.HasValue ? (object)ageRange.Value.Item2 : DBNull.Value) { IsNullable = true, DbType = DbType.Int32 };
            var countryp = new NpgsqlParameter("country", string.IsNullOrEmpty(country) ? DBNull.Value : (object)country) { IsNullable = true, DbType = DbType.String };
            var statep = new NpgsqlParameter("province", string.IsNullOrEmpty(state) ? DBNull.Value : (object)state) { IsNullable = true, DbType = DbType.String };
            var sexp = new NpgsqlParameter("sex", sex == null ? DBNull.Value : (object)((int)sex)) { IsNullable = true, DbType = DbType.Int32 };
            return await context.TypeAgregations.FromSqlRaw
                ("select * from credentials_by_type({0}, {1}, {2}, {3}, {4}, {5})", statusp, countryp, statep, ageMinp, ageMaxp, sexp)
                .ToListAsync();
        }

        
    }
}
