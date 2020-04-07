using CovidLAMap.Core.Models;
using CovidLAMap.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Data
{
    public class CovidDbContext : DbContext
    {
        public DbSet<RegisteredCredential> Credentials { get; set; }

        public CovidDbContext(DbContextOptions<CovidDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.ApplyConfiguration(new RegisteredCredentialConfiguration());
        }
    }
}
