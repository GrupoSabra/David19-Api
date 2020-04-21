using CovidLAMap.Core.Models;
using CovidLAMap.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Data.Repositories
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(CovidDbContext context) : base(context)
        {

        }
    }
}
