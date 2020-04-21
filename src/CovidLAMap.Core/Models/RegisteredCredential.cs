using CovidLAMap.Core.DTOs;
using Geohash;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
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

        public bool? IsRevoked { get; set; }
        public int CountryGid { get; set; }
        public int StateGid { get; set; }

        public virtual Country Country { get; set; }
        public virtual State State { get; set; }

        public static RegisteredCredential From(EthEventDTO eventDto)
        {
            var hasher = new Geohasher();
            var decoded = hasher.Decode(eventDto.NonIndexedParameters[5].Value.ToString());
            var ret = new RegisteredCredential()
            {
                HashId = eventDto.IndexedParameters[0].Value.ToString(),
                CitizenAddress = eventDto.NonIndexedParameters[0].Value.ToString(),
                SubjectHashId = eventDto.NonIndexedParameters[1].Value.ToString(),
                StartDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(eventDto.NonIndexedParameters[2].Value.ToString())).DateTime,
                CredentialCreation = DateTimeOffset.FromUnixTimeSeconds(long.Parse(eventDto.NonIndexedParameters[3].Value.ToString())).DateTime,
                Sex = Enum.Parse<Sex>(eventDto.NonIndexedParameters[4].Value.ToString()),
                CredentialType = Enum.Parse<CredentialType>(eventDto.NonIndexedParameters[6].Value.ToString()),
                Reason = Enum.Parse<InterruptionReason>(eventDto.NonIndexedParameters[7].Value.ToString())
            };

            ret.Location = new Point(decoded.Item2, decoded.Item1);

            ret.Lat = ret.Location.Coordinate.X;
            ret.Lon = ret.Location.Coordinate.Y;
            return ret;
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
        None = 0,
        Purchase = 1,
        AttendanceHealthCenter = 2,
        CommutingWork = 3,
        ReturnResidence = 4,
        AssistPeople = 5,
        CommutingFinancial = 6,
        ForceMajeure = 7
    }

}
