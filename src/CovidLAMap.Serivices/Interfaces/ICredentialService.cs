﻿using CovidLAMap.Core.DTOs;
using CovidLAMap.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Services.Interfaces
{
    public interface ICredentialService
    {
        Task<IEnumerable<AgregationsByCountry>> ClusteredCredentials(double lat, double lon, double radiusKms, double clusterRaidusKms, (double, double)? ageRange = null, Sex? sex = null);
        Task<IEnumerable<RegisteredCredential>> GetAllAsync();
        Task<IEnumerable<AgregationsByCountry>> GetByCountryAsync();
        Task<IEnumerable<AgregationsByType>> GetByTypeAsync();
        Task<IEnumerable<AgregationsByType>> GetByTypeAsync(HealthStatus status, string country, string state);
        Task<List<RegisteredCredential>> GetPointsInCircle(double lat, double lon, double radiusKms);
        Task<List<RegisteredCredential>> GetPointsInCircle(double lat, double lon, double radiusKms, string country = "", string state = "", (double, double)? ageRange = null, Sex? sex = null);
        Task<IEnumerable<AgregationsByCountry>> GetPointsInCircleAggregated(double? lat, double? lon, double? radiusKms, string country = "", string state = "", (double, double)? ageRange = null, Sex? sex = null);
        Task ImportAsync(EthEventDTO ethEvent);
        Task RevokeCredential(EthEventDTO ethEvent);
    }
}
