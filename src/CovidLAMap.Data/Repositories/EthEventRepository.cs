using CovidLAMap.Core.DTOs;
using CovidLAMap.Core.Models;
using CovidLAMap.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Data.Repositories
{
    public class EthEventRepository : Repository<EthEventDTO>, IEthEventRepository
    {
        private readonly CovidDbContext context;

        public EthEventRepository(CovidDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<EthEventDTO> GetFullById(string id)
        {
            return await this.context.EthEvents.Where(x => x.Id == id)
                .Include(x => x.IndexedParameters)
                .Include(x => x.NonIndexedParameters)
                .FirstOrDefaultAsync();
        }
    }
}
