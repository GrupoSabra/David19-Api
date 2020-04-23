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
        public void Configure(EntityTypeBuilder<RegisteredCredential> entity)
        {
            entity.HasKey(e => e.HashId);

            entity.HasIndex(e => e.CountryGid)
                .HasName("fki_InCountry");

            entity.HasIndex(e => e.StateGid)
                .HasName("fki_InState");

            entity.HasIndex(e => new { e.IsRevoked, e.SubjectHashId })
                .HasName("SubjectIdIdx")
                .HasCollation(new[] { null, "C.UTF-8" })
                .HasOperators(new[] { null, "varchar_ops" });

            entity.Property(e => e.CountryGid).HasColumnName("CountryGId");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Location).HasColumnType("geometry(Point)");

            entity.Property(e => e.StateGid).HasColumnName("StateGId");

            entity.Property(e => e.SubjectHashId).IsRequired();

            entity.HasOne(d => d.Country)
                .WithMany(p => p.RegisteredCredentials)
                .HasForeignKey(d => d.CountryGid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("InCountry");

            entity.HasOne(d => d.State)
                .WithMany(p => p.RegisteredCredentials)
                .HasForeignKey(d => d.StateGid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("InState");


            entity.ToTable("RegisteredCredentials");
        }
    }
}
