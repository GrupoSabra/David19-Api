using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Core.Models
{
    public partial class State
    {
        public State()
        {
            RegisteredCredentials = new HashSet<RegisteredCredential>();
        }

        public int Gid { get; set; }
        public string Featurecla { get; set; }
        public short? Scalerank { get; set; }
        public string Adm1Code { get; set; }
        public int? DissMe { get; set; }
        public string Iso31662 { get; set; }
        public string Wikipedia { get; set; }
        public string IsoA2 { get; set; }
        public short? Adm0Sr { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string NameLocal { get; set; }
        public string Type { get; set; }
        public string TypeEn { get; set; }
        public string CodeLocal { get; set; }
        public string CodeHasc { get; set; }
        public string Note { get; set; }
        public string HascMaybe { get; set; }
        public string Region { get; set; }
        public string RegionCod { get; set; }
        public int? ProvnumNe { get; set; }
        public short? GadmLevel { get; set; }
        public short? CheckMe { get; set; }
        public short? Datarank { get; set; }
        public string Abbrev { get; set; }
        public string Postal { get; set; }
        public short? AreaSqkm { get; set; }
        public short? Sameascity { get; set; }
        public short? Labelrank { get; set; }
        public short? NameLen { get; set; }
        public short? Mapcolor9 { get; set; }
        public short? Mapcolor13 { get; set; }
        public string Fips { get; set; }
        public string FipsAlt { get; set; }
        public int? WoeId { get; set; }
        public string WoeLabel { get; set; }
        public string WoeName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string SovA3 { get; set; }
        public string Adm0A3 { get; set; }
        public short? Adm0Label { get; set; }
        public string Admin { get; set; }
        public string Geonunit { get; set; }
        public string GuA3 { get; set; }
        public int? GnId { get; set; }
        public string GnName { get; set; }
        public int? GnsId { get; set; }
        public string GnsName { get; set; }
        public short? GnLevel { get; set; }
        public string GnRegion { get; set; }
        public string GnA1Code { get; set; }
        public string RegionSub { get; set; }
        public string SubCode { get; set; }
        public short? GnsLevel { get; set; }
        public string GnsLang { get; set; }
        public string GnsAdm1 { get; set; }
        public string GnsRegion { get; set; }
        public double? MinLabel { get; set; }
        public double? MaxLabel { get; set; }
        public double? MinZoom { get; set; }
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
        public long? NeId { get; set; }
        public MultiPolygon Geom { get; set; }
        public Geometry Centroid { get; set; }

        public virtual ICollection<RegisteredCredential> RegisteredCredentials { get; set; }
    }
}
