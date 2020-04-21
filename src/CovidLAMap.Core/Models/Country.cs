using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace CovidLAMap.Core.Models
{
    public partial class Country
    {
        public Country()
        {
            RegisteredCredentials = new HashSet<RegisteredCredential>();
        }

        public int Gid { get; set; }
        public string Featurecla { get; set; }
        public short? Scalerank { get; set; }
        public short? Labelrank { get; set; }
        public string Sovereignt { get; set; }
        public string SovA3 { get; set; }
        public short? Adm0Dif { get; set; }
        public short? Level { get; set; }
        public string Type { get; set; }
        public string Admin { get; set; }
        public string Adm0A3 { get; set; }
        public short? GeouDif { get; set; }
        public string Geounit { get; set; }
        public string GuA3 { get; set; }
        public short? SuDif { get; set; }
        public string Subunit { get; set; }
        public string SuA3 { get; set; }
        public short? BrkDiff { get; set; }
        public string Name { get; set; }
        public string NameLong { get; set; }
        public string BrkA3 { get; set; }
        public string BrkName { get; set; }
        public string BrkGroup { get; set; }
        public string Abbrev { get; set; }
        public string Postal { get; set; }
        public string FormalEn { get; set; }
        public string FormalFr { get; set; }
        public string NameCiawf { get; set; }
        public string NoteAdm0 { get; set; }
        public string NoteBrk { get; set; }
        public string NameSort { get; set; }
        public string NameAlt { get; set; }
        public short? Mapcolor7 { get; set; }
        public short? Mapcolor8 { get; set; }
        public short? Mapcolor9 { get; set; }
        public short? Mapcolor13 { get; set; }
        public decimal? PopEst { get; set; }
        public short? PopRank { get; set; }
        public double? GdpMdEst { get; set; }
        public short? PopYear { get; set; }
        public short? Lastcensus { get; set; }
        public short? GdpYear { get; set; }
        public string Economy { get; set; }
        public string IncomeGrp { get; set; }
        public short? Wikipedia { get; set; }
        public string Fips10 { get; set; }
        public string IsoA2 { get; set; }
        public string IsoA3 { get; set; }
        public string IsoA3Eh { get; set; }
        public string IsoN3 { get; set; }
        public string UnA3 { get; set; }
        public string WbA2 { get; set; }
        public string WbA3 { get; set; }
        public int? WoeId { get; set; }
        public int? WoeIdEh { get; set; }
        public string WoeNote { get; set; }
        public string Adm0A3Is { get; set; }
        public string Adm0A3Us { get; set; }
        public short? Adm0A3Un { get; set; }
        public short? Adm0A3Wb { get; set; }
        public string Continent { get; set; }
        public string RegionUn { get; set; }
        public string Subregion { get; set; }
        public string RegionWb { get; set; }
        public short? NameLen { get; set; }
        public short? LongLen { get; set; }
        public short? AbbrevLen { get; set; }
        public short? Tiny { get; set; }
        public short? Homepart { get; set; }
        public double? MinZoom { get; set; }
        public double? MinLabel { get; set; }
        public double? MaxLabel { get; set; }
        public decimal? NeId { get; set; }
        public string Wikidataid { get; set; }
        public string NameAr { get; set; }
        public string NameBn { get; set; }
        public string NameDe { get; set; }
        public string NameEn { get; set; }
        public string NameEs { get; set; }
        public string NameFr { get; set; }
        public string NameEl { get; set; }
        public string NameHi { get; set; }
        public string NameHu { get; set; }
        public string NameId { get; set; }
        public string NameIt { get; set; }
        public string NameJa { get; set; }
        public string NameKo { get; set; }
        public string NameNl { get; set; }
        public string NamePl { get; set; }
        public string NamePt { get; set; }
        public string NameRu { get; set; }
        public string NameSv { get; set; }
        public string NameTr { get; set; }
        public string NameVi { get; set; }
        public string NameZh { get; set; }
        public MultiPolygon Geom { get; set; }
        public Geometry Centroid { get; set; }

        public virtual ICollection<RegisteredCredential> RegisteredCredentials { get; set; }
    }
}
