using CovidLAMap.Core.DTOs;
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
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<RegisteredCredential> RegisteredCredentials { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<AgregationsByCountry> CountryAgregations { get; set; }
        public virtual DbSet<AgregationsByType> TypeAgregations { get; set; }
        public virtual DbSet<EthEventDTO> EthEvents { get; set; }

        public CovidDbContext(DbContextOptions<CovidDbContext> options) : base(options)
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
            modelBuilder.ApplyConfiguration(new AgregationsByTypeConfiguration());
        }  
    }
}
