using CovidLAMap.Core.DTOs;
using CovidLAMap.Core.Models;
using CovidLAMap.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Data.Repositories
{
    public class EthEventRepository : Repository<EthEventDTO>, IEthEventRepository
    {
        public EthEventRepository(CovidDbContext context) : base(context)
        {

        }
    }
}
