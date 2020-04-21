using CovidLAMap.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidLAMap.Core
{
    public interface ICovidUnitOfWork : IUnitOfWork
    {
        IRegisteredCredentialRepository Credentials { get; }
        ICountryRepository Countries { get; }
        IStateRepository States { get; }
        IAgregationsByCountryRepository CountryAgregations { get; }
    }
}
