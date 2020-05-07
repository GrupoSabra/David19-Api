using CovidLAMap.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Data.Configurations
{
    public class AgregationsByTypeConfiguration : IEntityTypeConfiguration<AgregationsByType>
    {
        public void Configure(EntityTypeBuilder<AgregationsByType> entity)
        {
            entity.HasNoKey();

            entity.ToTable("agregations_by_types");

            entity.Property(e => e.Centroid).HasColumnName("centroid");

            entity.Property(e => e.Female1318).HasColumnName("female1318");

            entity.Property(e => e.Female1830).HasColumnName("female1830");

            entity.Property(e => e.Female3040).HasColumnName("female3040");

            entity.Property(e => e.Female4065).HasColumnName("female4065");

            entity.Property(e => e.Female66).HasColumnName("female66");

            entity.Property(e => e.Male1318).HasColumnName("male1318");

            entity.Property(e => e.Male1830).HasColumnName("male1830");

            entity.Property(e => e.Male3040).HasColumnName("male3040");

            entity.Property(e => e.Male4065).HasColumnName("male4065");

            entity.Property(e => e.Male66).HasColumnName("male66");

            entity.Property(e => e.NameEn)
                .HasColumnName("name_en")
                .HasMaxLength(44);

            entity.Property(e => e.NameEs)
                .HasColumnName("name_es")
                .HasMaxLength(44);

            entity.Property(e => e.NamePt)
                .HasColumnName("name_pt")
                .HasMaxLength(43);

            entity.Property(e => e.Othersex1318).HasColumnName("othersex1318");

            entity.Property(e => e.Othersex1830).HasColumnName("othersex1830");

            entity.Property(e => e.Othersex3040).HasColumnName("othersex3040");

            entity.Property(e => e.Othersex4065).HasColumnName("othersex4065");

            entity.Property(e => e.Othersex66).HasColumnName("othersex66");

            entity.Property(e => e.Unspecifiedsex1318).HasColumnName("unspecifiedsex1318");

            entity.Property(e => e.Unspecifiedsex1830).HasColumnName("unspecifiedsex1830");

            entity.Property(e => e.Unspecifiedsex3040).HasColumnName("unspecifiedsex3040");

            entity.Property(e => e.Unspecifiedsex4065).HasColumnName("unspecifiedsex4065");

            entity.Property(e => e.Unspecifiedsex66).HasColumnName("unspecifiedsex66");

        }
    }
}
