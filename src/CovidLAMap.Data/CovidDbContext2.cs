using CovidLAMap.Core.Models;
using CovidLAMap.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Data
{
    public class CovidDbContext2 : DbContext
    {
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<RegisteredCredential> RegisteredCredentials { get; set; }
        public virtual DbSet<State> States { get; set; }

        public CovidDbContext2(DbContextOptions<CovidDbContext2> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.ApplyConfiguration(new RegisteredCredentialConfiguration());
            modelBuilder.ApplyConfiguration(new CountriesConfiguration());
            modelBuilder.ApplyConfiguration(new StatesConfiguration());
            modelBuilder.ApplyConfiguration(new AgregationsByCountryConfiguration());
            modelBuilder.ApplyConfiguration(new EthEventConfiguration());
        }  
    }
}
