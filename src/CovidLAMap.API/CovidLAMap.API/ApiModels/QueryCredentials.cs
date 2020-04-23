using CovidLAMap.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidLAMap.API.ApiModels
{
    public class QueryCredentials
    {
        public FilterCredentials Filter { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        /// <summary>
        /// In Kms
        /// </summary>
        public double? Radius { get; set; }
    }

    public class FilterCredentials
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Age { get; set; }
        public Sex Sex { get; set; }
    }
}
