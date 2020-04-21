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
        public long? Total { get; set; }
        public long? Male { get; set; }
        public long? Female { get; set; }
        public long? Unspecified { get; set; }
        public long? Other { get; set; }
        public long? Confinement { get; set; }
        public long? Interruption { get; set; }
        public long? Symptoms { get; set; }
        public long? Infection { get; set; }
        public long? Recovery { get; set; }
        public long? Purchase { get; set; }
        public long? Attendancehealthcenter { get; set; }
        public long? Commutingwork { get; set; }
        public long? Returnresidence { get; set; }
        public long? Assistpeople { get; set; }
        public long? Commutingfinancial { get; set; }
        public long? Forcemajeure { get; set; }
    }
}
