using CovidLAMap.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Data.Configurations
{
    public class AgregationsByCountryConfiguration : IEntityTypeConfiguration<AgregationsByCountry>
    {
        public void Configure(EntityTypeBuilder<AgregationsByCountry> builder)
        {
            builder.HasNoKey();

            builder.ToView("agregations_by_country");

            builder.Property(e => e.Assistpeople).HasColumnName("assistpeople");

            builder.Property(e => e.Attendancehealthcenter).HasColumnName("attendancehealthcenter");

            builder.Property(e => e.Centroid).HasColumnName("centroid");

            builder.Property(e => e.Commutingfinancial).HasColumnName("commutingfinancial");

            builder.Property(e => e.Commutingwork).HasColumnName("commutingwork");

            builder.Property(e => e.Confinement).HasColumnName("confinement");

            builder.Property(e => e.Female).HasColumnName("female");

            builder.Property(e => e.Forcemajeure).HasColumnName("forcemajeure");

            builder.Property(e => e.Infection).HasColumnName("infection");

            builder.Property(e => e.Interruption).HasColumnName("interruption");

            builder.Property(e => e.Male).HasColumnName("male");

            builder.Property(e => e.NameEn)
                .HasColumnName("name_en")
                .HasMaxLength(44);

            builder.Property(e => e.NameEs)
                .HasColumnName("name_es")
                .HasMaxLength(44);

            builder.Property(e => e.NamePt)
                .HasColumnName("name_pt")
                .HasMaxLength(43);

            builder.Property(e => e.Other).HasColumnName("other");

            builder.Property(e => e.Purchase).HasColumnName("purchase");

            builder.Property(e => e.Recovery).HasColumnName("recovery");

            builder.Property(e => e.Returnresidence).HasColumnName("returnresidence");

            builder.Property(e => e.Symptoms).HasColumnName("symptoms");

            builder.Property(e => e.Total).HasColumnName("total");

            builder.Property(e => e.Unspecified).HasColumnName("unspecified");
        }
    }
}
