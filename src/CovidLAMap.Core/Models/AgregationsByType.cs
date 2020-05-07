using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace CovidLAMap.Core.Models
{
    public partial class AgregationsByType
    {
        public string NameEs { get; set; }
        public string NameEn { get; set; }
        public string NamePt { get; set; }
        public Geometry Centroid { get; set; }
        public long? Male1318 { get; set; }
        public long? Female1318 { get; set; }
        public long? Unspecifiedsex1318 { get; set; }
        public long? Othersex1318 { get; set; }
        public long? Male1830 { get; set; }
        public long? Female1830 { get; set; }
        public long? Unspecifiedsex1830 { get; set; }
        public long? Othersex1830 { get; set; }
        public long? Male3040 { get; set; }
        public long? Female3040 { get; set; }
        public long? Unspecifiedsex3040 { get; set; }
        public long? Othersex3040 { get; set; }
        public long? Male4065 { get; set; }
        public long? Female4065 { get; set; }
        public long? Unspecifiedsex4065 { get; set; }
        public long? Othersex4065 { get; set; }
        public long? Male66 { get; set; }
        public long? Female66 { get; set; }
        public long? Unspecifiedsex66 { get; set; }
        public long? Othersex66 { get; set; }
    }
}
