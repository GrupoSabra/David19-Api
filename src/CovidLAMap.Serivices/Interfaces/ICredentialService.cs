using CovidLAMap.Core.DTOs;
using CovidLAMap.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Services.Interfaces
{
    public interface ICredentialService
    {
        Task<IEnumerable<RegisteredCredential>> GetAllAsync();
        Task<List<RegisteredCredentialFat>> GetAllByCountryAsync();
        Task ImportAsync(EthEventDTO ethEvent);
    }
}
