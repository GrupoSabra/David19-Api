using CovidLAMap.Core.Models;
using CsvHelper.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace CovidLAMap.API.ApiModels
{
    public class CsvAgregationsByCountry
    {
        public string type { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public long usersCount { get; set; }
        public long transactionCount { get; set; }
        public long healthyCount { get; set; }
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
                lat = byCountry.Centroid.Coordinates[0].Y,
                lon = byCountry.Centroid.Coordinates[0].X,
                usersCount = byCountry.Userstotal.GetValueOrDefault(),
                transactionCount = byCountry.Transactioncount.GetValueOrDefault(),
                healthyCount = byCountry.Healthy.GetValueOrDefault(),
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
                purchaseFoodCount = byCountry.food.GetValueOrDefault(),
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

        public static CsvAgregationsByCountry From(RegisteredCredential credential)
        {
            return new CsvAgregationsByCountry()
            {
                type = "point",
                lat = credential.Lat,
                lon = credential.Lon,
                usersCount = 1,
                transactionCount = 1, //TODO
                noSymptomsCount = credential.HasNoSymptoms.GetValueOrDefault() ? 1 : 0,
                symptomsCount = credential.HasSymptoms.GetValueOrDefault() ? 1 : 0,
                healthyCount = credential.HasNoSymptoms.GetValueOrDefault() ? 1 : 0, //TODO
                feverCount = credential.HasFever.GetValueOrDefault() ? 1 : 0,
                coughCount = credential.HasCought.GetValueOrDefault() ? 1 : 0,
                breathingIssuesCount = credential.HasBreathingIssues.GetValueOrDefault() ? 1 : 0,
                lossSmellCount = credential.HasLossSmell.GetValueOrDefault() ? 1 : 0,
                headacheCount = credential.HasHeadache.GetValueOrDefault() ? 1 : 0,
                musclePainCount = credential.HasMusclePain.GetValueOrDefault() ? 1 : 0,
                soreThroatCount = credential.HasSoreThroat.GetValueOrDefault() ? 1 : 0,
                infectedCount = credential.CredentialType == CredentialType.Infection ? 1 : 0,
                recoveryCount = credential.CredentialType == CredentialType.Recovery ? 1 : 0,
                confinedCount = credential.CredentialType == CredentialType.Confinement ? 1 : 0,
                confinementInterruptionCount = credential.CredentialType == CredentialType.Interruption ? 1 : 0,
                purchaseFoodCount = credential.Reason == InterruptionReason.Food ? 1 : 0,
                workCount = credential.Reason == InterruptionReason.Work ? 1 : 0,
                medicinesCount = credential.Reason == InterruptionReason.Medicines ? 1 : 0,
                doctorCount = credential.Reason == InterruptionReason.Doctor ? 1 : 0,
                movingCount = credential.Reason == InterruptionReason.Moving ? 1 : 0,
                assistCount = credential.Reason == InterruptionReason.Assist ? 1 : 0,
                financialCount = credential.Reason == InterruptionReason.Financial ? 1 : 0,
                forceCount = credential.Reason == InterruptionReason.Force ? 1 : 0,
                petsCount = credential.Reason == InterruptionReason.Pets ? 1 : 0,
                maleCount = credential.Sex == Sex.Male ? 1 : 0,
                femaleCount = credential.Sex == Sex.Female ? 1 : 0,
                otherSexCount = credential.Sex == Sex.Other ? 1 : 0,
                unspecifiedSexCount = credential.Sex == Sex.Unspecified ? 1 : 0,
                age = credential.Age,
                form1318Count = credential.Age <= 18 ? 1 : 0,
                form1930Count = credential.Age > 18 || credential.Age <= 30 ? 1 : 0,
                form3140Count = credential.Age > 30 || credential.Age <= 40 ? 1 : 0,
                form4165Count = credential.Age > 40 || credential.Age <= 65 ? 1 : 0,
                form66Count = credential.Age > 65 ? 1 : 0,
                hash = credential.HashId,
                subjectId = credential.SubjectHashId
            };
        }
    }

    public sealed class CsvAgregationsByCountryMap : ClassMap<CsvAgregationsByCountry>
    {
        public CsvAgregationsByCountryMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.lon).Name("long");
        }
    }
}
