using CovidLAMap.Core.Models;
using CovidLAMap.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<List<RegisteredCredential>> GetPointsInCircle(double lat, double lon, double radiusKms)
        {
            return await context.RegisteredCredentials.FromSqlRaw(
             "select * from \"RegisteredCredentials\" where ST_Distance(ST_Transform(ST_SetSRID(\"Location\", 4326), 26986)," +
                "ST_Transform(ST_SetSRID(ST_POINT({0}, {1}), 4326), 26986)) / 1000 < {2}",
                 lat, lon, radiusKms
            ).ToListAsync();
        }

        public async Task<List<RegisteredCredential>> GetPointsInCircle(double lat, double lon, double radiusKms,
            string country = "", string state = "", (double, double)? ageRange = null, Sex? sex = null)
        {
            var query = "select * from \"RegisteredCredentials\" ";

            if (!string.IsNullOrEmpty(country))
            {
                query += "join \"Countries\" as c on c.gid = \"CountryGId\" ";
            }
            if (!string.IsNullOrEmpty(state))
            {
                query += "join \"States\" as s on s.gid = \"StateGId\" ";
            }
            query += "where ST_Distance(ST_Transform(ST_SetSRID(\"Location\", 4326), 26986)," +
                "ST_Transform(ST_SetSRID(ST_POINT({0}, {1}), 4326), 26986)) / 1000 < {2} ";
            List<object> argsList = new List<object>() { lat, lon, radiusKms };
            if (!string.IsNullOrEmpty(country))
            {
                query += "and c.iso_n3 = '{country}' ";
                argsList.Add(country);
            }
            if (!string.IsNullOrEmpty(state))
            {
                query += "and s.iso_3166_2 = '{state}' ";
                argsList.Add(state);
            }
            if(sex != null)
            {
                query += "and \"Sex\" = {sex} ";
                argsList.Add((int)sex);
            }
            if (ageRange.HasValue)
            {
                query += "and \"Age\" >= {min} and \"Age\" < {max}";
                argsList.Add(ageRange.Value.Item1);
                argsList.Add(ageRange.Value.Item2);
            }

            return await context.RegisteredCredentials.FromSqlRaw(query, argsList.ToArray()).ToListAsync();
        }
    }
}