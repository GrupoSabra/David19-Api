using CovidLAMap.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Data.Configurations
{
    public class StatesConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(e => e.Gid)
                .HasName("ne_10m_admin_1_states_provinces_pkey");

            builder.HasIndex(e => e.Geom)
                .HasName("ne_10m_admin_1_states_provinces_geom_idx")
                .HasMethod("gist");

            builder.Property(e => e.Gid)
                .HasColumnName("gid")
                .HasDefaultValueSql("nextval('ne_10m_admin_1_states_provinces_gid_seq'::regclass)");

            builder.Property(e => e.Abbrev)
                .HasColumnName("abbrev")
                .HasMaxLength(9);

            builder.Property(e => e.Adm0A3)
                .HasColumnName("adm0_a3")
                .HasMaxLength(3);

            builder.Property(e => e.Adm0Label).HasColumnName("adm0_label");

            builder.Property(e => e.Adm0Sr).HasColumnName("adm0_sr");

            builder.Property(e => e.Adm1Code)
                .HasColumnName("adm1_code")
                .HasMaxLength(9);

            builder.Property(e => e.Admin)
                .HasColumnName("admin")
                .HasMaxLength(36);

            builder.Property(e => e.AreaSqkm).HasColumnName("area_sqkm");

            builder.Property(e => e.Centroid).HasColumnName("centroid");

            builder.Property(e => e.CheckMe).HasColumnName("check_me");

            builder.Property(e => e.CodeHasc)
                .HasColumnName("code_hasc")
                .HasMaxLength(8);

            builder.Property(e => e.CodeLocal)
                .HasColumnName("code_local")
                .HasMaxLength(5);

            builder.Property(e => e.Datarank).HasColumnName("datarank");

            builder.Property(e => e.DissMe).HasColumnName("diss_me");

            builder.Property(e => e.Featurecla)
                .HasColumnName("featurecla")
                .HasMaxLength(20);

            builder.Property(e => e.Fips)
                .HasColumnName("fips")
                .HasMaxLength(5);

            builder.Property(e => e.FipsAlt)
                .HasColumnName("fips_alt")
                .HasMaxLength(9);

            builder.Property(e => e.GadmLevel).HasColumnName("gadm_level");

            builder.Property(e => e.Geom)
                .HasColumnName("geom")
                .HasColumnType("geometry(MultiPolygon)");

            builder.Property(e => e.Geonunit)
                .HasColumnName("geonunit")
                .HasMaxLength(40);

            builder.Property(e => e.GnA1Code)
                .HasColumnName("gn_a1_code")
                .HasMaxLength(10);

            builder.Property(e => e.GnId).HasColumnName("gn_id");

            builder.Property(e => e.GnLevel).HasColumnName("gn_level");

            builder.Property(e => e.GnName)
                .HasColumnName("gn_name")
                .HasMaxLength(72);

            builder.Property(e => e.GnRegion)
                .HasColumnName("gn_region")
                .HasMaxLength(1);

            builder.Property(e => e.GnsAdm1)
                .HasColumnName("gns_adm1")
                .HasMaxLength(4);

            builder.Property(e => e.GnsId).HasColumnName("gns_id");

            builder.Property(e => e.GnsLang)
                .HasColumnName("gns_lang")
                .HasMaxLength(3);

            builder.Property(e => e.GnsLevel).HasColumnName("gns_level");

            builder.Property(e => e.GnsName)
                .HasColumnName("gns_name")
                .HasMaxLength(80);

            builder.Property(e => e.GnsRegion)
                .HasColumnName("gns_region")
                .HasMaxLength(4);

            builder.Property(e => e.GuA3)
                .HasColumnName("gu_a3")
                .HasMaxLength(3);

            builder.Property(e => e.HascMaybe)
                .HasColumnName("hasc_maybe")
                .HasMaxLength(13);

            builder.Property(e => e.Iso31662)
                .HasColumnName("iso_3166_2")
                .HasMaxLength(8);

            builder.Property(e => e.IsoA2)
                .HasColumnName("iso_a2")
                .HasMaxLength(2);

            builder.Property(e => e.Labelrank).HasColumnName("labelrank");

            builder.Property(e => e.Latitude).HasColumnName("latitude");

            builder.Property(e => e.Longitude).HasColumnName("longitude");

            builder.Property(e => e.Mapcolor13).HasColumnName("mapcolor13");

            builder.Property(e => e.Mapcolor9).HasColumnName("mapcolor9");

            builder.Property(e => e.MaxLabel).HasColumnName("max_label");

            builder.Property(e => e.MinLabel).HasColumnName("min_label");

            builder.Property(e => e.MinZoom).HasColumnName("min_zoom");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(44);

            builder.Property(e => e.NameAlt)
                .HasColumnName("name_alt")
                .HasMaxLength(129);

            builder.Property(e => e.NameAr)
                .HasColumnName("name_ar")
                .HasMaxLength(85);

            builder.Property(e => e.NameBn)
                .HasColumnName("name_bn")
                .HasMaxLength(128);

            builder.Property(e => e.NameDe)
                .HasColumnName("name_de")
                .HasMaxLength(51);

            builder.Property(e => e.NameEl)
                .HasColumnName("name_el")
                .HasMaxLength(82);

            builder.Property(e => e.NameEn)
                .HasColumnName("name_en")
                .HasMaxLength(47);

            builder.Property(e => e.NameEs)
                .HasColumnName("name_es")
                .HasMaxLength(52);

            builder.Property(e => e.NameFr)
                .HasColumnName("name_fr")
                .HasMaxLength(52);

            builder.Property(e => e.NameHi)
                .HasColumnName("name_hi")
                .HasMaxLength(134);

            builder.Property(e => e.NameHu)
                .HasColumnName("name_hu")
                .HasMaxLength(41);

            builder.Property(e => e.NameId)
                .HasColumnName("name_id")
                .HasMaxLength(45);

            builder.Property(e => e.NameIt)
                .HasColumnName("name_it")
                .HasMaxLength(49);

            builder.Property(e => e.NameJa)
                .HasColumnName("name_ja")
                .HasMaxLength(93);

            builder.Property(e => e.NameKo)
                .HasColumnName("name_ko")
                .HasMaxLength(58);

            builder.Property(e => e.NameLen).HasColumnName("name_len");

            builder.Property(e => e.NameLocal)
                .HasColumnName("name_local")
                .HasMaxLength(66);

            builder.Property(e => e.NameNl)
                .HasColumnName("name_nl")
                .HasMaxLength(44);

            builder.Property(e => e.NamePl)
                .HasColumnName("name_pl")
                .HasMaxLength(45);

            builder.Property(e => e.NamePt)
                .HasColumnName("name_pt")
                .HasMaxLength(43);

            builder.Property(e => e.NameRu)
                .HasColumnName("name_ru")
                .HasMaxLength(91);

            builder.Property(e => e.NameSv)
                .HasColumnName("name_sv")
                .HasMaxLength(48);

            builder.Property(e => e.NameTr)
                .HasColumnName("name_tr")
                .HasMaxLength(44);

            builder.Property(e => e.NameVi)
                .HasColumnName("name_vi")
                .HasMaxLength(49);

            builder.Property(e => e.NameZh)
                .HasColumnName("name_zh")
                .HasMaxLength(61);

            builder.Property(e => e.NeId).HasColumnName("ne_id");

            builder.Property(e => e.Note)
                .HasColumnName("note")
                .HasMaxLength(114);

            builder.Property(e => e.Postal)
                .HasColumnName("postal")
                .HasMaxLength(3);

            builder.Property(e => e.ProvnumNe).HasColumnName("provnum_ne");

            builder.Property(e => e.Region)
                .HasColumnName("region")
                .HasMaxLength(43);

            builder.Property(e => e.RegionCod)
                .HasColumnName("region_cod")
                .HasMaxLength(15);

            builder.Property(e => e.RegionSub)
                .HasColumnName("region_sub")
                .HasMaxLength(41);

            builder.Property(e => e.Sameascity).HasColumnName("sameascity");

            builder.Property(e => e.Scalerank).HasColumnName("scalerank");

            builder.Property(e => e.SovA3)
                .HasColumnName("sov_a3")
                .HasMaxLength(3);

            builder.Property(e => e.SubCode)
                .HasColumnName("sub_code")
                .HasMaxLength(5);

            builder.Property(e => e.Type)
                .HasColumnName("type")
                .HasMaxLength(38);

            builder.Property(e => e.TypeEn)
                .HasColumnName("type_en")
                .HasMaxLength(27);

            builder.Property(e => e.Wikidataid)
                .HasColumnName("wikidataid")
                .HasMaxLength(9);

            builder.Property(e => e.Wikipedia)
                .HasColumnName("wikipedia")
                .HasMaxLength(84);

            builder.Property(e => e.WoeId).HasColumnName("woe_id");

            builder.Property(e => e.WoeLabel)
                .HasColumnName("woe_label")
                .HasMaxLength(64);

            builder.Property(e => e.WoeName)
                .HasColumnName("woe_name")
                .HasMaxLength(44);

            builder.ToTable("States");
        }
    }
}
