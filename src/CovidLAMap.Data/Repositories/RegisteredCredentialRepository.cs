using CovidLAMap.Core.Models;
using CovidLAMap.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NetTopologySuite.Geometries;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
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
             "select * from \"RegisteredCredentials\" as rc where ST_Distance(ST_Transform(ST_SetSRID(\"Location\", 4326), 26986)," +
                "ST_Transform(ST_SetSRID(ST_POINT({0}, {1}), 4326), 26986)) / 1000 < {2} and (rc.\"IsRevoked\" IS NULL OR rc.\"IsRevoked\" = false)",
                 lat, lon, radiusKms
            ).ToListAsync();
        }

        public async Task<List<RegisteredCredential>> GetPointsInCircle(double lat, double lon, double radiusKms,
            string country = "", string state = "", (double, double)? ageRange = null, Sex? sex = null)
        {
            var query = "select * from \"RegisteredCredentials\" as rc ";

            if (!string.IsNullOrEmpty(country))
            {
                query += "join \"Countries\" as c on c.gid = \"CountryGId\" ";
            }
            if (!string.IsNullOrEmpty(state))
            {
                query += "join \"States\" as s on s.gid = \"StateGId\" ";
            }
            query += "where ST_Distance(ST_Transform(ST_SetSRID(\"Location\", 4326), 26986)," +
                "ST_Transform(ST_SetSRID(ST_POINT({0}, {1}), 4326), 26986)) / 1000 < {2} and rc.\"IsRevoked\" = false ";
            List<object> argsList = new List<object>() { lat, lon, radiusKms };
            var nextCounter = 3;
            if (!string.IsNullOrEmpty(country))
            {
                query += $"and c.iso_n3 = {{{nextCounter}}} ";
                nextCounter++;
                argsList.Add(country);
            }
            if (!string.IsNullOrEmpty(state))
            {
                query += $"and s.iso_3166_2 = {{{nextCounter}}} ";
                nextCounter++;
                argsList.Add(state);
            }
            if (sex != null)
            {
                query += $"and \"Sex\" = {{{nextCounter}}} ";
                nextCounter++;
                argsList.Add((int)sex);
            }
            if (ageRange.HasValue)
            {
                query += $"and \"Age\" >= {{{nextCounter}}} and \"Age\" < {{{++nextCounter}}} ";
                argsList.Add(ageRange.Value.Item1);
                argsList.Add(ageRange.Value.Item2);
            }

            return await context.RegisteredCredentials.FromSqlRaw(query, argsList.ToArray()).ToListAsync();
        }

        public async Task<List<AgregationsByCountry>> GetPointsInCircleAgregated(double? lat, double? lon, double? radiusKms,
            string country = "", string state = "", (double, double)? ageRange = null, Sex? sex = null)
        {
            var ageMinp = new NpgsqlParameter("ageMin", ageRange.HasValue ? (object)ageRange.Value.Item1 : DBNull.Value) { IsNullable = true, DbType = DbType.Int32};
            var ageMaxp = new NpgsqlParameter("ageMax", ageRange.HasValue ? (object)ageRange.Value.Item2 : DBNull.Value) { IsNullable = true, DbType = DbType.Int32 };
            var latp = new NpgsqlParameter("lat", lat == 0 || lat == null ? DBNull.Value : (object)lat) { IsNullable = true, DbType = DbType.Double };
            var lonp = new NpgsqlParameter("lon", lon == 0 || lon == null ? DBNull.Value : (object)lon) { IsNullable = true, DbType = DbType.Double };
            var radiusP = new NpgsqlParameter("radiusKms", radiusKms == 0 || radiusKms == null ? DBNull.Value : (object)radiusKms) { IsNullable = true, DbType = DbType.Int32 };
            var countryp = new NpgsqlParameter("country", string.IsNullOrEmpty(country) ? DBNull.Value : (object)country) { IsNullable = true, DbType = DbType.String}; 
            var statep = new NpgsqlParameter("province", string.IsNullOrEmpty(state) ? DBNull.Value : (object)state) { IsNullable = true, DbType = DbType.String };
            var sexp = new NpgsqlParameter("sex", sex == null ? DBNull.Value : (object)((int)sex)) { IsNullable = true, DbType = DbType.Int32 };
            return await context.CountryAgregations.FromSqlRaw
                ("select * from agregated_credentials({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} )",
                latp, lonp, radiusP, countryp, statep, ageMinp, ageMaxp, sexp)
                .ToListAsync();
        }

        public async Task<List<AgregationsByCountry>> ClusteredCredentials(double lat, double lon, double radiusKms,
            double clusterRaidusKms, (double, double)? ageRange = null, Sex? sex = null)
        {
            var ageMinp = new NpgsqlParameter("ageMin", ageRange.HasValue ? (object)ageRange.Value.Item1 : DBNull.Value) { IsNullable = true, DbType = DbType.Int32 };
            var ageMaxp = new NpgsqlParameter("ageMax", ageRange.HasValue ? (object)ageRange.Value.Item2 : DBNull.Value) { IsNullable = true, DbType = DbType.Int32 };
            var latp = new NpgsqlParameter("lat", (object)lat) {  DbType = DbType.Double };
            var lonp = new NpgsqlParameter("lon", (object)lon) {  DbType = DbType.Double };
            var radiusP = new NpgsqlParameter("radiusKms", (object)radiusKms) { DbType = DbType.Int32 };
            var clusterRadisup = new NpgsqlParameter("clusterRadius", (object)clusterRaidusKms) { DbType = DbType.Int32 };
            var sexp = new NpgsqlParameter("sex", sex == null ? DBNull.Value : (object)((int)sex)) { IsNullable = true, DbType = DbType.Int32 };
            return await context.CountryAgregations.FromSqlRaw
                ("select * from clustered_credentials({0}, {1}, {2}, {3}, {4}, {5}, {6} )",
                latp, lonp, radiusP, clusterRadisup, ageMinp, ageMaxp, sexp)
                .ToListAsync();
        }
    } 
}