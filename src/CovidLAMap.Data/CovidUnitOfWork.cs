﻿using CovidLAMap.Core;
using CovidLAMap.Core.Repositories;
using CovidLAMap.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Data
{
    public class CovidUnitOfWork : ICovidUnitOfWork
    {
        private readonly CovidDbContext context;
        private IAgregationsByTypeRepository typeAgregations;
        private IRegisteredCredentialRepository credentialsRepository;
        private ICountryRepository countryRepository;
        private IStateRepository stateRepository;
        private IAgregationsByCountryRepository agregationsByCountry;
        private IEthEventRepository ethEventRepository;

        public IRegisteredCredentialRepository Credentials => credentialsRepository ??= new RegisteredCredentialRepository(context);
        public ICountryRepository Countries => countryRepository ??= new CountryRepository(context);
        public IStateRepository States => stateRepository ??= new StateRepository(context);
        public IAgregationsByCountryRepository CountryAgregations => agregationsByCountry ??= new AgregationsByCountryRepository(context);
        public IEthEventRepository EthEvents => ethEventRepository ??= new EthEventRepository(context);
        public IAgregationsByTypeRepository TypeAgregations => typeAgregations ??= new AgregationsByTypeRepository(context); 

        public CovidUnitOfWork(CovidDbContext context)
        {
            this.context = context;
        }
        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
