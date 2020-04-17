using CovidLAMap.Core.Models;
using CovidLAMap.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Data
{
    public class CovidDbContext : DbContext
    {
        public DbSet<RegisteredCredential> Credentials { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<RegisteredCredentialFat> CredentialsAggregated { get; set; }

        public CovidDbContext(DbContextOptions<CovidDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.ApplyConfiguration(new RegisteredCredentialConfiguration());
            modelBuilder.ApplyConfiguration(new CountriesConfiguration());

            RegisterQueries(modelBuilder);
        }

        private void RegisterQueries(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisteredCredentialFat>().HasNoKey();
        }
    }
}
