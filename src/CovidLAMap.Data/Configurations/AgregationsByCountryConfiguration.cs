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
        public void Configure(EntityTypeBuilder<AgregationsByCountry> entity)
        {
            entity.HasNoKey();

            entity.ToView("agregations_by_country");

            entity.Property(e => e.Age)
                .HasColumnName("age")
                .HasColumnType("numeric");

            entity.Property(e => e.Assist).HasColumnName("assist");

            entity.Property(e => e.Breathingissues).HasColumnName("breathingissues");

            entity.Property(e => e.Centroid).HasColumnName("centroid");

            entity.Property(e => e.Confinement).HasColumnName("confinement");

            entity.Property(e => e.Cought).HasColumnName("cought");

            entity.Property(e => e.Doctor).HasColumnName("doctor");

            entity.Property(e => e.Female).HasColumnName("female");

            entity.Property(e => e.Fever).HasColumnName("fever");

            entity.Property(e => e.Financial).HasColumnName("financial");

            entity.Property(e => e.Force).HasColumnName("force");

            entity.Property(e => e.Form1318count).HasColumnName("form1318count");

            entity.Property(e => e.Form1930count).HasColumnName("form1930count");

            entity.Property(e => e.Form3140count).HasColumnName("form3140count");

            entity.Property(e => e.Form4165count).HasColumnName("form4165count");

            entity.Property(e => e.Form66count).HasColumnName("form66count");

            entity.Property(e => e.Hasmusclepain).HasColumnName("hasmusclepain");

            entity.Property(e => e.Headache).HasColumnName("headache");

            entity.Property(e => e.Infection).HasColumnName("infection");

            entity.Property(e => e.Interruption).HasColumnName("interruption");
            entity.Property(e => e.food).HasColumnName("food");

            entity.Property(e => e.Losssmell).HasColumnName("losssmell");

            entity.Property(e => e.Male).HasColumnName("male");

            entity.Property(e => e.Medicines).HasColumnName("medicines");

            entity.Property(e => e.Moving).HasColumnName("moving");

            entity.Property(e => e.NameEn)
                .HasColumnName("name_en")
                .HasMaxLength(44);

            entity.Property(e => e.NameEs)
                .HasColumnName("name_es")
                .HasMaxLength(44);

            entity.Property(e => e.NamePt)
                .HasColumnName("name_pt")
                .HasMaxLength(43);

            entity.Property(e => e.Nosymptoms).HasColumnName("nosymptoms");
            entity.Property(e => e.Healthy).HasColumnName("healthy");

            entity.Property(e => e.Othersex).HasColumnName("othersex");

            entity.Property(e => e.Pets).HasColumnName("pets");

            entity.Property(e => e.Recovery).HasColumnName("recovery");

            entity.Property(e => e.Sorethroat).HasColumnName("sorethroat");

            entity.Property(e => e.Symptoms).HasColumnName("symptoms");

            entity.Property(e => e.Transactioncount).HasColumnName("transactioncount");

            entity.Property(e => e.Unspecifiedsex).HasColumnName("unspecifiedsex");

            entity.Property(e => e.Userstotal).HasColumnName("userstotal");

            entity.Property(e => e.Work).HasColumnName("work");
        }
    }
}
