using CovidLAMap.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Data.Configurations
{
    public class RegisteredCredentialConfiguration : IEntityTypeConfiguration<RegisteredCredential>
    {
        public void Configure(EntityTypeBuilder<RegisteredCredential> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.ToTable("RegisteredCredentials");
        }
    }
}
