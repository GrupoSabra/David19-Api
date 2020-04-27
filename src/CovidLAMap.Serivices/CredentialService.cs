using CovidLAMap.Core;
using CovidLAMap.Core.DTOs;
using CovidLAMap.Core.Models;
using CovidLAMap.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
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
                var country = await this.covidUnitOfWork.Countries.SingleOrDefaultAsync(x => x.Geom.Contains(credential.Location));
                var state = await this.covidUnitOfWork.States.SingleOrDefaultAsync(x => x.Geom.Contains(credential.Location));
                credential.CountryGid = country.Gid;
                credential.StateGid = state.Gid;
                await covidUnitOfWork.Credentials.AddAsync(credential);
                await covidUnitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Error on EthEvent transaction hash: {ethEvent.TransactionHash}", e);
            }
        }

        public async Task RevokeCredential(EthEventDTO ethEvent)
        {
            var hash = ethEvent.IndexedParameters[0].Value.ToString();
            var credential = await covidUnitOfWork.Credentials
                .SingleOrDefaultAsync(x => x.HashId == hash);
            if (credential != null)
            {
                credential.IsRevoked = true;
            }
            covidUnitOfWork.Credentials.UpdateAsync(credential);
            await covidUnitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<AgregationsByCountry>> GetByCountryAsync()
        {
            return await covidUnitOfWork.CountryAgregations.GetAllAsync();
        }

        public async Task<IEnumerable<RegisteredCredential>> GetAllAsync()
        {
            return await covidUnitOfWork.Credentials.GetAllAsync();
        }

        public async Task<List<RegisteredCredential>> GetPointsInCircle(double lat, double lon, double radiusKms)
        {
            return await covidUnitOfWork.Credentials.GetPointsInCircle(lat, lon, radiusKms);
        }

        public async Task<List<RegisteredCredential>> GetPointsInCircle(double lat, double lon, double radiusKms,
            string country = "", string state = "", (double, double)? ageRange = null, Sex? sex = null)
        {
            if (string.IsNullOrEmpty(country) && string.IsNullOrEmpty(country) && !ageRange.HasValue && !sex.HasValue)
                return await GetPointsInCircle(lat, lon, radiusKms);
            return await covidUnitOfWork.Credentials
                .GetPointsInCircle(lat, lon, radiusKms, country, state, ageRange, sex);
        }


    }
}
