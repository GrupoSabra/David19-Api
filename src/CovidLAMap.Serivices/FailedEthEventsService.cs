using CovidLAMap.Core;
using CovidLAMap.Core.DTOs;
using CovidLAMap.Services.Interfaces;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Operation.Overlay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Services
{
    public class FailedEthEventsService : IFailedEthEventsService
    {
        private readonly ICovidUnitOfWork covidUnitOfWork;
        private readonly ILogger logger;

        public FailedEthEventsService(ICovidUnitOfWork covidUnitOfWork, ILogger logger)
        {
            this.covidUnitOfWork = covidUnitOfWork;
            this.logger = logger;
        }

        public async Task<bool> CheckMaxFailedTimes(EthEventDTO eventDto, int maxTimes =3)
        {
            var eventDB = await covidUnitOfWork.EthEvents.GetByIdAsync(eventDto.Id);
            if(eventDB == null)
            {
                await covidUnitOfWork.EthEvents.AddAsync(eventDto);
                await covidUnitOfWork.CommitAsync();
                return false;
            }
            
            if(eventDB.IndexedParameters[0].Value == eventDto.IndexedParameters[0].Value)
            {
                if (eventDB.FailedTimes >= maxTimes) return true;

                eventDB.FailedTimes++;
                covidUnitOfWork.EthEvents.Update(eventDB);
                await covidUnitOfWork.CommitAsync();
                return false;
            }

            logger.LogCritical("The ids of the Events are not unique", eventDto, eventDB);
            return true;
        }
    }
}
