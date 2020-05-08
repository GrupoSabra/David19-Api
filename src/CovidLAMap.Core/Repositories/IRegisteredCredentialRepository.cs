using CovidLAMap.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Core.Repositories
{
    public interface IRegisteredCredentialRepository : IRepository<RegisteredCredential>
    {
        Task<List<AgregationsByCountry>> ClusteredCredentials(double lat, double lon, double radiusKms, double clusterRaidusKms, (double, double)? ageRange = null, Sex? sex = null);
        Task<List<RegisteredCredential>> GetPointsInCircle(double lat, double lon, double radiusKms);
        Task<List<RegisteredCredential>> GetPointsInCircle(double lat, double lon, double radiusKms, string country = "", string state = "", (double, double)? ageRange = null, Sex? sex = null);
        Task<List<AgregationsByCountry>> GetPointsInCircleAgregated(double? lat, double? lon, double? radiusKms, string country = "", string state = "", (double, double)? ageRange = null, Sex? sex = null);
    }
}
