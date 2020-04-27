using CovidLAMap.Core.Models;
using CovidLAMap.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Data.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(CovidDbContext context) : base(context)
        {

        }
    }
}
