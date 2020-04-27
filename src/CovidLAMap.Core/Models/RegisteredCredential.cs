using CovidLAMap.Core.DTOs;
using CovidLAMap.Core.Tools;
using Geohash;
using NetTopologySuite.Geometries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CovidLAMap.Core.Models
{
    public class RegisteredCredential
    {
        public long Id { get; set; }
        public string HashId { get; set; }
        public string CitizenAddress { get; set; }
        public string SubjectHashId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CredentialCreation { get; set; }
        public Sex Sex { get; set; }
        public Point Location { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public CredentialType CredentialType { get; set; }
        public InterruptionReason Reason { get; set; }
        public bool IsRevoked { get; set; }
        public short Age { get; set; }
        public bool? HasNoSymptoms { get; set; }
        public bool? HasSymptoms { get; set; }
        public bool? HasFever { get; set; }
        public bool? HasCought { get; set; }
        public bool? HasBreathingIssues { get; set; }
        public bool? HasLossSmell { get; set; }
        public bool? HasHeadache { get; set; }
        public bool? HasMusclePain { get; set; }
        public bool? HasSoreThroat { get; set; }
        public int CountryGid { get; set; }
        public int StateGid { get; set; }

        public virtual Country Country { get; set; }
        public virtual State State { get; set; }

        public static RegisteredCredential From(EthEventDTO eventDto)
        {
            var ret = new RegisteredCredential()
            {
                IsRevoked = false,
                HashId = eventDto.IndexedParameters[0].Value,
                CitizenAddress = eventDto.NonIndexedParameters[0].Value,
                SubjectHashId = eventDto.NonIndexedParameters[1].Value,
                StartDate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(eventDto.NonIndexedParameters[2].Value)).DateTime.ToUniversalTime(),
                CredentialCreation = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(eventDto.NonIndexedParameters[3].Value)).DateTime.ToUniversalTime(),
                Sex = Enum.Parse<Sex>(eventDto.NonIndexedParameters[4].Value),
                Age = short.Parse(eventDto.NonIndexedParameters[5].Value),
                CredentialType = Enum.Parse<CredentialType>(eventDto.NonIndexedParameters[7].Value),
                Reason = Enum.Parse<InterruptionReason>(eventDto.NonIndexedParameters[8].Value)
            };

            var hasher = new Geohasher();
            var hash = Hex.HexToString(eventDto.NonIndexedParameters[6].Value);
            var decoded = hasher.Decode(hash);
            ret.Location = new Point(decoded.Item2, decoded.Item1);
            ret.Lat = ret.Location.Coordinate.X;
            ret.Lon = ret.Location.Coordinate.Y;
            SetSymptoms(ret, eventDto.NonIndexedParameters[9].Value);
            return ret;
        }

        private static void SetSymptoms(RegisteredCredential ret, string hex)
        {
            if (ret.CredentialType != CredentialType.Symptoms) return;
            var byteR = Hex.HexToBinary(hex).ToCharArray();
            if(byteR[0] == '0')
            {
                ret.HasNoSymptoms = true;
                return;
            }
            else
            {
                ret.HasSymptoms = true;
            }
            if (byteR[1] == '1') ret.HasFever = true;
            if (byteR[2] == '1') ret.HasCought = true;
            if (byteR[3] == '1') ret.HasBreathingIssues = true;
            if (byteR[4] == '1') ret.HasLossSmell = true;
            if (byteR[5] == '1') ret.HasHeadache = true;
            if (byteR[6] == '1') ret.HasMusclePain = true;
            if (byteR[7] == '1') ret.HasSoreThroat = true;
        }
    }

    public enum Sex
    {
        Male = 0,
        Female = 1,
        Unspecified = 2,
        Other = 3
    }

    public enum CredentialType
    {
        Confinement = 0,
        Interruption = 1,
        Symptoms = 2,
        Infection = 3,
        Recovery = 4
    }

    public enum InterruptionReason
    {
        Food = 0,
        Work = 1,
        Medicines =2,
        Doctor =3,
        Moving =4,
        Assist=5,
        Financial =6,
        Force=7,
        Pets=8,
        Other = 9
    }

}
