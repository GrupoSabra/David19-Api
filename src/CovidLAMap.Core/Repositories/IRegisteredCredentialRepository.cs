using CovidLAMap.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Core.Repositories
{
    public interface IRegisteredCredentialRepository : IRepository<RegisteredCredential>
    {
        Task<List<RegisteredCredentialFat>> GetCountAllByCountryAsync();
    }
}
