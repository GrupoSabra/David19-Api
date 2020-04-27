using CovidLAMap.Core.DTOs;
using CovidLAMap.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Data.Configurations
{
    public class EthEventConfiguration : IEntityTypeConfiguration<EthEventDTO>
    {

        public void Configure(EntityTypeBuilder<EthEventDTO> builder)
        {
            builder.HasKey(x => x.Id);


            builder.ToTable("EthEvents");
        }
    }
}
