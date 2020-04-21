using CovidLAMap.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Data.Configurations
{
    public class CountriesConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(e => e.Gid)
                .HasName("countries_pkey");

            builder.HasIndex(e => e.Geom)
                .HasName("countries_geom_idx")
                .HasMethod("gist");

            builder.Property(e => e.Gid)
                .HasColumnName("gid")
                .HasDefaultValueSql("nextval('countries_gid_seq'::regclass)");

            builder.Property(e => e.Abbrev)
                .HasColumnName("abbrev")
                .HasMaxLength(13);

            builder.Property(e => e.AbbrevLen).HasColumnName("abbrev_len");

            builder.Property(e => e.Adm0A3)
                .HasColumnName("adm0_a3")
                .HasMaxLength(3);

            builder.Property(e => e.Adm0A3Is)
                .HasColumnName("adm0_a3_is")
                .HasMaxLength(3);

            builder.Property(e => e.Adm0A3Un).HasColumnName("adm0_a3_un");

            builder.Property(e => e.Adm0A3Us)
                .HasColumnName("adm0_a3_us")
                .HasMaxLength(3);

            builder.Property(e => e.Adm0A3Wb).HasColumnName("adm0_a3_wb");

            builder.Property(e => e.Adm0Dif).HasColumnName("adm0_dif");

            builder.Property(e => e.Admin)
                .HasColumnName("admin")
                .HasMaxLength(36);

            builder.Property(e => e.BrkA3)
                .HasColumnName("brk_a3")
                .HasMaxLength(3);

            builder.Property(e => e.BrkDiff).HasColumnName("brk_diff");

            builder.Property(e => e.BrkGroup)
                .HasColumnName("brk_group")
                .HasMaxLength(17);

            builder.Property(e => e.BrkName)
                .HasColumnName("brk_name")
                .HasMaxLength(32);

            builder.Property(e => e.Centroid).HasColumnName("centroid");

            builder.Property(e => e.Continent)
                .HasColumnName("continent")
                .HasMaxLength(23);

            builder.Property(e => e.Economy)
                .HasColumnName("economy")
                .HasMaxLength(26);

            builder.Property(e => e.Featurecla)
                .HasColumnName("featurecla")
                .HasMaxLength(15);

            builder.Property(e => e.Fips10)
                .HasColumnName("fips_10_")
                .HasMaxLength(3);

            builder.Property(e => e.FormalEn)
                .HasColumnName("formal_en")
                .HasMaxLength(52);

            builder.Property(e => e.FormalFr)
                .HasColumnName("formal_fr")
                .HasMaxLength(35);

            builder.Property(e => e.GdpMdEst).HasColumnName("gdp_md_est");

            builder.Property(e => e.GdpYear).HasColumnName("gdp_year");

            builder.Property(e => e.Geom)
                .HasColumnName("geom")
                .HasColumnType("geometry(MultiPolygon)");

            builder.Property(e => e.GeouDif).HasColumnName("geou_dif");

            builder.Property(e => e.Geounit)
                .HasColumnName("geounit")
                .HasMaxLength(36);

            builder.Property(e => e.GuA3)
                .HasColumnName("gu_a3")
                .HasMaxLength(3);

            builder.Property(e => e.Homepart).HasColumnName("homepart");

            builder.Property(e => e.IncomeGrp)
                .HasColumnName("income_grp")
                .HasMaxLength(23);

            builder.Property(e => e.IsoA2)
                .HasColumnName("iso_a2")
                .HasMaxLength(3);

            builder.Property(e => e.IsoA3)
                .HasColumnName("iso_a3")
                .HasMaxLength(3);

            builder.Property(e => e.IsoA3Eh)
                .HasColumnName("iso_a3_eh")
                .HasMaxLength(3);

            builder.Property(e => e.IsoN3)
                .HasColumnName("iso_n3")
                .HasMaxLength(3);

            builder.Property(e => e.Labelrank).HasColumnName("labelrank");

            builder.Property(e => e.Lastcensus).HasColumnName("lastcensus");

            builder.Property(e => e.Level).HasColumnName("level");

            builder.Property(e => e.LongLen).HasColumnName("long_len");

            builder.Property(e => e.Mapcolor13).HasColumnName("mapcolor13");

            builder.Property(e => e.Mapcolor7).HasColumnName("mapcolor7");

            builder.Property(e => e.Mapcolor8).HasColumnName("mapcolor8");

            builder.Property(e => e.Mapcolor9).HasColumnName("mapcolor9");

            builder.Property(e => e.MaxLabel).HasColumnName("max_label");

            builder.Property(e => e.MinLabel).HasColumnName("min_label");

            builder.Property(e => e.MinZoom).HasColumnName("min_zoom");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(25);

            builder.Property(e => e.NameAlt)
                .HasColumnName("name_alt")
                .HasMaxLength(19);

            builder.Property(e => e.NameAr)
                .HasColumnName("name_ar")
                .HasMaxLength(72);

            builder.Property(e => e.NameBn)
                .HasColumnName("name_bn")
                .HasMaxLength(148);

            builder.Property(e => e.NameCiawf)
                .HasColumnName("name_ciawf")
                .HasMaxLength(45);

            builder.Property(e => e.NameDe)
                .HasColumnName("name_de")
                .HasMaxLength(46);

            builder.Property(e => e.NameEl)
                .HasColumnName("name_el")
                .HasMaxLength(88);

            builder.Property(e => e.NameEn)
                .HasColumnName("name_en")
                .HasMaxLength(44);

            builder.Property(e => e.NameEs)
                .HasColumnName("name_es")
                .HasMaxLength(44);

            builder.Property(e => e.NameFr)
                .HasColumnName("name_fr")
                .HasMaxLength(54);

            builder.Property(e => e.NameHi)
                .HasColumnName("name_hi")
                .HasMaxLength(126);

            builder.Property(e => e.NameHu)
                .HasColumnName("name_hu")
                .HasMaxLength(52);

            builder.Property(e => e.NameId)
                .HasColumnName("name_id")
                .HasMaxLength(46);

            builder.Property(e => e.NameIt)
                .HasColumnName("name_it")
                .HasMaxLength(48);

            builder.Property(e => e.NameJa)
                .HasColumnName("name_ja")
                .HasMaxLength(63);

            builder.Property(e => e.NameKo)
                .HasColumnName("name_ko")
                .HasMaxLength(47);

            builder.Property(e => e.NameLen).HasColumnName("name_len");

            builder.Property(e => e.NameLong)
                .HasColumnName("name_long")
                .HasMaxLength(36);

            builder.Property(e => e.NameNl)
                .HasColumnName("name_nl")
                .HasMaxLength(49);

            builder.Property(e => e.NamePl)
                .HasColumnName("name_pl")
                .HasMaxLength(47);

            builder.Property(e => e.NamePt)
                .HasColumnName("name_pt")
                .HasMaxLength(43);

            builder.Property(e => e.NameRu)
                .HasColumnName("name_ru")
                .HasMaxLength(86);

            builder.Property(e => e.NameSort)
                .HasColumnName("name_sort")
                .HasMaxLength(36);

            builder.Property(e => e.NameSv)
                .HasColumnName("name_sv")
                .HasMaxLength(57);

            builder.Property(e => e.NameTr)
                .HasColumnName("name_tr")
                .HasMaxLength(42);

            builder.Property(e => e.NameVi)
                .HasColumnName("name_vi")
                .HasMaxLength(56);

            builder.Property(e => e.NameZh)
                .HasColumnName("name_zh")
                .HasMaxLength(36);

            builder.Property(e => e.NeId)
                .HasColumnName("ne_id")
                .HasColumnType("numeric(10,0)");

            builder.Property(e => e.NoteAdm0)
                .HasColumnName("note_adm0")
                .HasMaxLength(22);

            builder.Property(e => e.NoteBrk)
                .HasColumnName("note_brk")
                .HasMaxLength(63);

            builder.Property(e => e.PopEst)
                .HasColumnName("pop_est")
                .HasColumnType("numeric(10,0)");

            builder.Property(e => e.PopRank).HasColumnName("pop_rank");

            builder.Property(e => e.PopYear).HasColumnName("pop_year");

            builder.Property(e => e.Postal)
                .HasColumnName("postal")
                .HasMaxLength(4);

            builder.Property(e => e.RegionUn)
                .HasColumnName("region_un")
                .HasMaxLength(23);

            builder.Property(e => e.RegionWb)
                .HasColumnName("region_wb")
                .HasMaxLength(26);

            builder.Property(e => e.Scalerank).HasColumnName("scalerank");

            builder.Property(e => e.SovA3)
                .HasColumnName("sov_a3")
                .HasMaxLength(3);

            builder.Property(e => e.Sovereignt)
                .HasColumnName("sovereignt")
                .HasMaxLength(32);

            builder.Property(e => e.SuA3)
                .HasColumnName("su_a3")
                .HasMaxLength(3);

            builder.Property(e => e.SuDif).HasColumnName("su_dif");

            builder.Property(e => e.Subregion)
                .HasColumnName("subregion")
                .HasMaxLength(25);

            builder.Property(e => e.Subunit)
                .HasColumnName("subunit")
                .HasMaxLength(36);

            builder.Property(e => e.Tiny).HasColumnName("tiny");

            builder.Property(e => e.Type)
                .HasColumnName("type")
                .HasMaxLength(17);

            builder.Property(e => e.UnA3)
                .HasColumnName("un_a3")
                .HasMaxLength(4);

            builder.Property(e => e.WbA2)
                .HasColumnName("wb_a2")
                .HasMaxLength(3);

            builder.Property(e => e.WbA3)
                .HasColumnName("wb_a3")
                .HasMaxLength(3);

            builder.Property(e => e.Wikidataid)
                .HasColumnName("wikidataid")
                .HasMaxLength(8);

            builder.Property(e => e.Wikipedia).HasColumnName("wikipedia");

            builder.Property(e => e.WoeId).HasColumnName("woe_id");

            builder.Property(e => e.WoeIdEh).HasColumnName("woe_id_eh");

            builder.Property(e => e.WoeNote)
                .HasColumnName("woe_note")
                .HasMaxLength(167);

            builder.ToTable("Countries");

        }
    }
}
