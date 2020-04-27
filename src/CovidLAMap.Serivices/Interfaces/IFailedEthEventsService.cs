using CovidLAMap.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Services.Interfaces
{
    public interface IFailedEthEventsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventDto"></param>
        /// <param name="maxTimes"></param>
        /// <returns>True if it has reached the maxTimes permitted</returns>
        Task<bool> CheckMaxFailedTimes(EthEventDTO eventDto, int maxTimes = 3);
    }
}
