using CovidLAMap.Core.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CovidLAMap.API.ApiModels
{
    public class CsvAgregationsByCountry
    {
        public string type { get; set; }
        public double lat { get; set; }
        public double longitud { get; set; }
        public long usersCount { get; set; }
        public long transactionCount { get; set; }
        public long noSymptomsCount { get; set; }
        public long symptomsCount { get; set; }
        public long feverCount { get; set; }
        public long coughCount { get; set; }
        public long breathingIssuesCount { get; set; }
        public long lossSmellCount { get; set; }
        public long headacheCount { get; set; }
        public long musclePainCount { get; set; }
        public long soreThroatCount { get; set; }
        public long infectedCount { get; set; }
        public long recoveryCount { get; set; }
        public long confinedCount { get; set; }
        public long confinementInterruptionCount { get; set; }
        public long purchaseFoodCount { get; set; }
        public long workCount { get; set; }
        public long medicinesCount { get; set; }
        public long doctorCount { get; set; }
        public long movingCount { get; set; }
        public long assistCount { get; set; }
        public long financialCount { get; set; }
        public long forceCount { get; set; }
        public long petsCount { get; set; }
        public long maleCount { get; set; }
        public long femaleCount { get; set; }
        public long otherSexCount { get; set; }
        public long unspecifiedSexCount { get; set; }
        public decimal age { get; set; }
        public long form1318Count { get; set; }
        public long form1930Count { get; set; }
        public long form3140Count { get; set; }
        public long form4165Count { get; set; }
        public long form66Count { get; set; }
        public string hash { get; set; }
        public string subjectId { get; set; }

        public static CsvAgregationsByCountry From(AgregationsByCountry byCountry)
        {
            var ret = new CsvAgregationsByCountry
            {
                type = "cluster",
                lat = byCountry.Centroid.Coordinates[0].X,
                longitud = byCountry.Centroid.Coordinates[0].Y,
                usersCount = byCountry.Userstotal.GetValueOrDefault(),
                transactionCount = byCountry.Transactioncount.GetValueOrDefault(),
                noSymptomsCount = byCountry.Nosymptoms.GetValueOrDefault(),
                symptomsCount = byCountry.Symptoms.GetValueOrDefault(),
                feverCount = byCountry.Fever.GetValueOrDefault(),
                coughCount = byCountry.Cought.GetValueOrDefault(),
                breathingIssuesCount = byCountry.Breathingissues.GetValueOrDefault(),
                lossSmellCount = byCountry.Losssmell.GetValueOrDefault(),
                headacheCount = byCountry.Headache.GetValueOrDefault(),
                musclePainCount = byCountry.Hasmusclepain.GetValueOrDefault(),
                soreThroatCount = byCountry.Sorethroat.GetValueOrDefault(),
                infectedCount = byCountry.Infection.GetValueOrDefault(),
                recoveryCount = byCountry.Recovery.GetValueOrDefault(),
                confinedCount = byCountry.Infection.GetValueOrDefault(),
                confinementInterruptionCount = byCountry.Interruption.GetValueOrDefault(),
                purchaseFoodCount = 0, //TODO missing
                workCount = byCountry.Work.GetValueOrDefault(),
                medicinesCount = byCountry.Medicines.GetValueOrDefault(),
                doctorCount = byCountry.Doctor.GetValueOrDefault(),
                movingCount = byCountry.Moving.GetValueOrDefault(),
                assistCount = byCountry.Assist.GetValueOrDefault(),
                financialCount = byCountry.Financial.GetValueOrDefault(),
                forceCount = byCountry.Force.GetValueOrDefault(),
                petsCount = byCountry.Pets.GetValueOrDefault(),
                maleCount = byCountry.Male.GetValueOrDefault(),
                femaleCount = byCountry.Female.GetValueOrDefault(),
                otherSexCount = byCountry.Othersex.GetValueOrDefault(),
                unspecifiedSexCount = byCountry.Unspecifiedsex.GetValueOrDefault(),
                age = byCountry.Age.GetValueOrDefault(),
                form1318Count = byCountry.Form1318count.GetValueOrDefault(),
                form1930Count = byCountry.Form1930count.GetValueOrDefault(),
                form3140Count = byCountry.Form3140count.GetValueOrDefault(),
                form4165Count =  byCountry.Form4165count.GetValueOrDefault(),
                form66Count = byCountry.Form66count.GetValueOrDefault(),
                hash = "0",
                subjectId = "0"
            };
            return ret;
        }
    }

    public sealed class CsvAgregationsByCountryMap : ClassMap<CsvAgregationsByCountry>
    {
        public CsvAgregationsByCountryMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.longitud).Name("long");
        }
    }
}
