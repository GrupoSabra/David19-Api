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
            var nextCounter = 3;
            if (!string.IsNullOrEmpty(country))
            {
                query += $"and c.iso_n3 = '{nextCounter}' ";
                nextCounter++;
                argsList.Add(country);
            }
            if (!string.IsNullOrEmpty(state))
            {
                query += $"and s.iso_3166_2 = '{nextCounter}' ";
                nextCounter++;
                argsList.Add(state);
            }
            if(sex != null)
            {
                query += $"and \"Sex\" = {nextCounter} ";
                nextCounter++;
                argsList.Add((int)sex);
            }
            if (ageRange.HasValue)
            {
                query += $"and \"Age\" >= {nextCounter} and \"Age\" < {nextCounter++}";
                argsList.Add(ageRange.Value.Item1);
                argsList.Add(ageRange.Value.Item2);
            }

            return await context.RegisteredCredentials.FromSqlRaw(query, argsList.ToArray()).ToListAsync();
        }

        public async Task<List<AgregationsByCountry>> GetPointsInCircleAgregated(double lat, double lon, double radiusKms)
        {
            var raw = "SELECT c.name_es,    c.name_en,    c.name_pt,    c.centroid,    count(DISTINCT rc.\"SubjectHashId\") AS userstotal,    count(*) AS transactioncount,	count(*) FILTER (WHERE rc.\"HasNoSymptoms\" IS NOT NULL AND rc.\"HasNoSymptoms\" = true and 					\"SubjectHashId\" not in (select \"SubjectHashId\" from \"RegisteredCredentials\" as rc2 where 											(rc2.\"CredentialType\" = 3 OR rc2.\"CredentialType\" = 4)					and (rc2.\"IsRevoked\" IS NULL OR rc2.\"IsRevoked\" = false) and rc.\"SubjectHashId\" = rc2.\"SubjectHashId\" )					) AS healthy,    count(*) FILTER (WHERE rc.\"HasNoSymptoms\" IS NOT NULL AND rc.\"HasNoSymptoms\" = true) AS nosymptoms,    count(*) FILTER (WHERE rc.\"HasSymptoms\" IS NOT NULL AND rc.\"HasSymptoms\" = true) AS symptoms,    count(*) FILTER (WHERE rc.\"HasFever\" IS NOT NULL AND rc.\"HasFever\" = true) AS fever,    count(*) FILTER (WHERE rc.\"HasCought\" IS NOT NULL AND rc.\"HasCought\" = true) AS cought,    count(*) FILTER (WHERE rc.\"HasBreathingIssues\" IS NOT NULL AND rc.\"HasBreathingIssues\" = true) AS breathingissues,    count(*) FILTER (WHERE rc.\"HasLossSmell\" IS NOT NULL AND rc.\"HasLossSmell\" = true) AS losssmell,    count(*) FILTER (WHERE rc.\"HasHeadache\" IS NOT NULL AND rc.\"HasHeadache\" = true) AS headache,    count(*) FILTER (WHERE rc.\"HasMusclePain\" IS NOT NULL AND rc.\"HasMusclePain\" = true) AS hasmusclepain,    count(*) FILTER (WHERE rc.\"HasSoreThroat\" IS NOT NULL AND rc.\"HasSoreThroat\" = true) AS sorethroat,    count(*) FILTER (WHERE rc.\"CredentialType\" = 3) AS infection,    count(*) FILTER (WHERE rc.\"CredentialType\" = 4) AS recovery,    count(*) FILTER (WHERE rc.\"CredentialType\" = 0 OR rc.\"CredentialType\" = 1 AND date_part('day'::text, now() - rc.\"StartDate\"::timestamp with time zone) > 1::double precision) AS confinement,    count(*) FILTER (WHERE rc.\"CredentialType\" = 1 AND date_part('day'::text, now() - rc.\"StartDate\"::timestamp with time zone) <= 1::double precision) AS interruption,    count(*) FILTER (WHERE rc.\"Reason\" = 0) AS food,    count(*) FILTER (WHERE rc.\"Reason\" = 1) AS work,    count(*) FILTER (WHERE rc.\"Reason\" = 2) AS medicines,    count(*) FILTER (WHERE rc.\"Reason\" = 3) AS doctor,    count(*) FILTER (WHERE rc.\"Reason\" = 4) AS moving,    count(*) FILTER (WHERE rc.\"Reason\" = 5) AS assist,    count(*) FILTER (WHERE rc.\"Reason\" = 6) AS financial,    count(*) FILTER (WHERE rc.\"Reason\" = 7) AS force,    count(*) FILTER (WHERE rc.\"Reason\" = 8) AS pets,    count(*) FILTER (WHERE rc.\"Sex\" = 0) AS male,    count(*) FILTER (WHERE rc.\"Sex\" = 1) AS female,    count(*) FILTER (WHERE rc.\"Sex\" = 2) AS unspecifiedsex,    count(*) FILTER (WHERE rc.\"Sex\" = 3) AS othersex,    avg(rc.\"Age\") FILTER (WHERE rc.\"Age\" IS NOT NULL) AS age,    count(*) FILTER (WHERE rc.\"Age\" <= 18) AS form1318count,    count(*) FILTER (WHERE rc.\"Age\" > 18 AND rc.\"Age\" <= 30) AS form1930count,    count(*) FILTER (WHERE rc.\"Age\" > 30 AND rc.\"Age\" <= 40) AS form3140count,    count(*) FILTER (WHERE rc.\"Age\" > 40 AND rc.\"Age\" <= 65) AS form4165count,    count(*) FILTER (WHERE rc.\"Age\" > 65) AS form66count   FROM \"RegisteredCredentials\" rc     JOIN \"Countries\" c ON c.gid = rc.\"CountryGId\"  WHERE rc.\"IsRevoked\" IS NULL OR rc.\"IsRevoked\" = false and  ST_Distance(ST_Transform(ST_SetSRID(\"Location\", 4326), 26986),                ST_Transform(ST_SetSRID(ST_POINT({0}, {1}), 4326), 26986)) / 1000 < {2}  GROUP BY rc.\"CountryGId\", c.name_es, c.name_en, c.name_pt, c.centroid";
            
            return await context.CountryAgregations.FromSqlRaw( raw, lat, lon, radiusKms).ToListAsync();
        }
    }
}