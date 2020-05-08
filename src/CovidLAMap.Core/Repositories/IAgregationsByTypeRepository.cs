using CovidLAMap.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidLAMap.Core.Repositories
{
    public interface IAgregationsByTypeRepository : IRepository<AgregationsByType>
    {
        Task<List<AgregationsByType>> GetByStatus(HealthStatus status, string country = "", string state = "");
    }
}