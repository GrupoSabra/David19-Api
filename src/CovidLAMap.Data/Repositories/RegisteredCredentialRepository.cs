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

        public async Task<List<AgregationsByCountry>> GetPointsInCircleAgregated(double lat, double lon, double radiusKms,
            string country = "", string state = "", (double, double)? ageRange = null, Sex? sex = null)
        {
            var query = "SELECT c.name_es,\r\n    c.name_en,\r\n    c.name_pt,\r\n    c.centroid,\r\n    count(DISTINCT rc.\"SubjectHashId\") AS userstotal,\r\n    count(*) AS transactioncount,\r\n    count(*) FILTER (WHERE rc.\"HasNoSymptoms\" IS NOT NULL AND rc.\"HasNoSymptoms\" = true AND NOT (rc.\"SubjectHashId\" IN ( SELECT rc2.\"SubjectHashId\"\r\n           FROM \"RegisteredCredentials\" rc2\r\n          WHERE rc.\"SubjectHashId\" = rc2.\"SubjectHashId\" AND (rc2.\"IsRevoked\" IS NULL OR rc2.\"IsRevoked\" = false) AND (rc2.\"CredentialType\" = 3 OR rc2.\"CredentialType\" = 4))) OR (rc.\"SubjectHashId\" IN ( SELECT rc_1.\"SubjectHashId\"\r\n           FROM \"RegisteredCredentials\" rc_1\r\n          GROUP BY rc_1.\"SubjectHashId\"\r\n         HAVING sum(\r\n                CASE\r\n                    WHEN rc_1.\"CredentialType\" = 0 AND rc_1.\"IsRevoked\" = false THEN 1\r\n\t\t\t        WHEN rc_1.\"CredentialType\" = 1 AND rc_1.\"IsRevoked\" = false THEN 1\r\n                    WHEN rc_1.\"CredentialType\" <> 0 AND rc_1.\"CredentialType\" <> 1 AND rc_1.\"IsRevoked\" = false THEN \'-1\'::integer\r\n                    ELSE 0\r\n                END) = 1))) AS healthy,\r\n    count(*) FILTER (WHERE rc.\"HasNoSymptoms\" IS NOT NULL AND rc.\"HasNoSymptoms\" = true) AS nosymptoms,\r\n    count(*) FILTER (WHERE rc.\"HasSymptoms\" IS NOT NULL AND rc.\"HasSymptoms\" = true) AS symptoms,\r\n    count(*) FILTER (WHERE rc.\"HasFever\" IS NOT NULL AND rc.\"HasFever\" = true) AS fever,\r\n    count(*) FILTER (WHERE rc.\"HasCought\" IS NOT NULL AND rc.\"HasCought\" = true) AS cought,\r\n    count(*) FILTER (WHERE rc.\"HasBreathingIssues\" IS NOT NULL AND rc.\"HasBreathingIssues\" = true) AS breathingissues,\r\n    count(*) FILTER (WHERE rc.\"HasLossSmell\" IS NOT NULL AND rc.\"HasLossSmell\" = true) AS losssmell,\r\n    count(*) FILTER (WHERE rc.\"HasHeadache\" IS NOT NULL AND rc.\"HasHeadache\" = true) AS headache,\r\n    count(*) FILTER (WHERE rc.\"HasMusclePain\" IS NOT NULL AND rc.\"HasMusclePain\" = true) AS hasmusclepain,\r\n    count(*) FILTER (WHERE rc.\"HasSoreThroat\" IS NOT NULL AND rc.\"HasSoreThroat\" = true) AS sorethroat,\r\n    count(*) FILTER (WHERE rc.\"CredentialType\" = 3) AS infection,\r\n    count(*) FILTER (WHERE rc.\"CredentialType\" = 4) AS recovery,\r\n    count(*) FILTER (WHERE rc.\"CredentialType\" = 0 OR rc.\"CredentialType\" = 1 AND date_part(\'day\'::text, now() - rc.\"StartDate\"::timestamp with time zone) > 1::double precision) AS confinement,\r\n    count(*) FILTER (WHERE rc.\"CredentialType\" = 1 AND date_part(\'day\'::text, now() - rc.\"StartDate\"::timestamp with time zone) <= 1::double precision) AS interruption,\r\n    count(*) FILTER (WHERE rc.\"Reason\" = 0) AS food,\r\n    count(*) FILTER (WHERE rc.\"Reason\" = 1) AS work,\r\n    count(*) FILTER (WHERE rc.\"Reason\" = 2) AS medicines,\r\n    count(*) FILTER (WHERE rc.\"Reason\" = 3) AS doctor,\r\n    count(*) FILTER (WHERE rc.\"Reason\" = 4) AS moving,\r\n    count(*) FILTER (WHERE rc.\"Reason\" = 5) AS assist,\r\n    count(*) FILTER (WHERE rc.\"Reason\" = 6) AS financial,\r\n    count(*) FILTER (WHERE rc.\"Reason\" = 7) AS force,\r\n    count(*) FILTER (WHERE rc.\"Reason\" = 8) AS pets,\r\n    count(*) FILTER (WHERE rc.\"Sex\" = 0) AS male,\r\n    count(*) FILTER (WHERE rc.\"Sex\" = 1) AS female,\r\n    count(*) FILTER (WHERE rc.\"Sex\" = 2) AS unspecifiedsex,\r\n    count(*) FILTER (WHERE rc.\"Sex\" = 3) AS othersex,\r\n    avg(rc.\"Age\") FILTER (WHERE rc.\"Age\" IS NOT NULL) AS age,\r\n    count(*) FILTER (WHERE rc.\"Age\" <= 18) AS form1318count,\r\n    count(*) FILTER (WHERE rc.\"Age\" > 18 AND rc.\"Age\" <= 30) AS form1930count,\r\n    count(*) FILTER (WHERE rc.\"Age\" > 30 AND rc.\"Age\" <= 40) AS form3140count,\r\n    count(*) FILTER (WHERE rc.\"Age\" > 40 AND rc.\"Age\" <= 65) AS form4165count,\r\n    count(*) FILTER (WHERE rc.\"Age\" > 65) AS form66count\r\n   FROM \"RegisteredCredentials\" rc\r\n     JOIN \"Countries\" c ON c.gid = rc.\"CountryGId\"\r\n ";
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
            //if (!string.IsNullOrEmpty(state)) TODO no lo soporta
            //{
            //    query += $"and s.iso_3166_2 = {{{nextCounter}}} ";
            //    nextCounter++;
            //    argsList.Add(state);
            //}
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

            query += "GROUP BY c.gid, c.name_es, c.name_en, c.name_pt, c.centroid;";

            return await context.CountryAgregations.FromSqlRaw(query, argsList.ToArray()).ToListAsync();
        }
    } 
}