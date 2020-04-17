using CovidLAMap.Services.Interfaces;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CovidLAMap.Services
{
    public class GeoService : IGeoService
    {
        //more info: https://nominatim.org/release-docs/develop/api/Reverse/
        private const string LOCATION_API_URL = "https://nominatim.openstreetmap.org/search?format=json&limit=3&q=";
        private readonly HttpClient httpClient;

        public GeoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<Point>> SearchLocation(string location)
        {
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("f1ana.Nominatim.API", Assembly.GetExecutingAssembly().GetName().Version.ToString()));
            Thread.Sleep(1000); //Supports only one request per second TODO improve 
            using var response = await httpClient.GetAsync(LOCATION_API_URL + location);
            try
            {
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                var posibleLocations = JsonConvert.DeserializeObject<List<dynamic>>(result, new ExpandoObjectConverter());
                if (posibleLocations.Count <= 0) throw new Exception("Cannot convert location. Could not get find any location");

                var list = posibleLocations.Select(x =>
                    new Point(double.Parse(x.lat.ToString()), double.Parse(x.lon.ToString()))).ToList();
                return list;
            }
            catch (Exception)
            {
                return new List<Point>();
            }
        }
    }
}
