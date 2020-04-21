using CovidLAMap.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Data.Configurations
{
    public class RegisteredCredentialConfiguration : IEntityTypeConfiguration<RegisteredCredential>
    {
        public void Configure(EntityTypeBuilder<RegisteredCredential> builder)
        {
            builder.HasKey(e => e.HashId);

            builder.HasIndex(e => e.CountryGid)
                .HasName("fki_InCountry");

            builder.HasIndex(e => e.StateGid)
                .HasName("fki_InState");

            builder.HasIndex(e => e.SubjectHashId)
                .HasName("SubjectIdIdx")
                .HasOperators(new[] { "text_pattern_ops" });

            builder.Property(e => e.CountryGid).HasColumnName("CountryGId");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Location).HasColumnType("geometry(Point)");

            builder.Property(e => e.StateGid).HasColumnName("StateGId");

            builder.Property(e => e.SubjectHashId).IsRequired();

            builder.HasOne(d => d.Country)
                .WithMany(p => p.RegisteredCredentials)
                .HasForeignKey(d => d.CountryGid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("InCountry");

            builder.HasOne(d => d.State)
                .WithMany(p => p.RegisteredCredentials)
                .HasForeignKey(d => d.StateGid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("InState");


            builder.ToTable("RegisteredCredentials");
        }
    }
}
