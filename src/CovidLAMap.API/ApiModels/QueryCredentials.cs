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
        public bool? Aggregated { get; set; }
        /// <summary>
        /// In Kms
        /// </summary>
        public double? Radius { get; set; }
    }

    public class FilterCredentials
    {
        public string Country { get; set; }
        public string State { get; set; }
        /// <summary>
        /// From lower to higher. Example from 20 years to 30 years: [20, 30]
        /// </summary>
        public int[] Age { get; set; }
        public Sex? Sex { get; set; }

        public (double, double)? AgeToTuple()
        {
            return Age != null && Age.Length > 1 ?
                        (Age[0], Age[1]) : default((double, double)?);
        }
    }
}
