using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidLAMap.Services.Interfaces
{
    public interface IGeoService
    {
        Task<List<Point>> SearchLocation(string location);
    }
}