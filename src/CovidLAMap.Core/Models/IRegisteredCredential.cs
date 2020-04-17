using NetTopologySuite.Geometries;
using System;

namespace CovidLAMap.Core.Models
{
    public interface IRegisteredCredential
    {
        string CitizenAddress { get; set; }
        DateTime CredentialCreation { get; set; }
        CredentialType CredintialType { get; set; }
        string HashId { get; set; }
        long Id { get; set; }
        double Lat { get; set; }
        Point Location { get; set; }
        double Lon { get; set; }
        InterruptionReason Reason { get; set; }
        Sex Sex { get; set; }
        DateTime StartDate { get; set; }
        string SubjectHashId { get; set; }
    }
}