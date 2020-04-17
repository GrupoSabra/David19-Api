using CovidLAMap.Core;
using CovidLAMap.Core.DTOs;
using CovidLAMap.Core.Models;
using CovidLAMap.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidLAMap.Services
{
    public class CredentialService : ICredentialService
    {
        private readonly ICovidUnitOfWork covidUnitOfWork;
        public CredentialService(ICovidUnitOfWork covidUnitOfWork)
        {
            this.covidUnitOfWork = covidUnitOfWork;
        }

        public async Task ImportAsync(EthEventDTO ethEvent)
        {
            var credential = RegisteredCredential.From(ethEvent);
            try 
            { 
                await covidUnitOfWork.Credentials.AddAsync(credential);
                await covidUnitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Error on EthEvent transaction hash: {ethEvent.TransactionHash}", e);
            }
        }

        public async Task<List<RegisteredCredentialFat>> GetAllByCountryAsync()
        {
            return await covidUnitOfWork.Credentials.GetCountAllByCountryAsync();
        }

        public async Task<IEnumerable<RegisteredCredential>> GetAllAsync()
        {
            return await covidUnitOfWork.Credentials.GetAllAsync();
        }
    
    }
}
