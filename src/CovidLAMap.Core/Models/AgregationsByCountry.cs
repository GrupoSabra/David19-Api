using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace CovidLAMap.Core.Models
{
    public partial class AgregationsByCountry
    {
        public string NameEs { get; set; }
        public string NameEn { get; set; }
        public string NamePt { get; set; }
        public Geometry Centroid { get; set; }
        public long? Userstotal { get; set; }
        public long? Transactioncount { get; set; }
        public long? Healthy { get; set; }
        public long? Nosymptoms { get; set; }
        public long? Symptoms { get; set; }
        public long? Fever { get; set; }
        public long? Cought { get; set; }
        public long? Breathingissues { get; set; }
        public long? Losssmell { get; set; }
        public long? Headache { get; set; }
        public long? Hasmusclepain { get; set; }
        public long? Sorethroat { get; set; }
        public long? Infection { get; set; }
        public long? Recovery { get; set; }
        public long? Confinement { get; set; }
        public long? Interruption { get; set; }
        public long? food { get; set; }
        public long? Work { get; set; }
        public long? Medicines { get; set; }
        public long? Doctor { get; set; }
        public long? Moving { get; set; }
        public long? Assist { get; set; }
        public long? Financial { get; set; }
        public long? Force { get; set; }
        public long? Pets { get; set; }
        public long? Male { get; set; }
        public long? Female { get; set; }
        public long? Unspecifiedsex { get; set; }
        public long? Othersex { get; set; }
        public decimal? Age { get; set; }
        public long? Form1318count { get; set; }
        public long? Form1930count { get; set; }
        public long? Form3140count { get; set; }
        public long? Form4165count { get; set; }
        public long? Form66count { get; set; }
    }
}
