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
        public string OriginalLocation { get; set; }
        public CredentialType CredintialType { get; set; }
        public InterruptionReason Reason { get; set; }
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
