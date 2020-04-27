using CovidLAMap.Core.Models;
using CovidLAMap.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Data.Repositories
{
    public class AgregationsByCountryRepository : Repository<AgregationsByCountry>, IAgregationsByCountryRepository
    {
        public AgregationsByCountryRepository(CovidDbContext2 context) : base(context)
        {

        }
    }
}
