using CovidLAMap.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Core.Repositories
{
    public interface IEthEventRepository : IRepository<EthEventDTO>
    {
        Task<EthEventDTO> GetFullById(string id);
    }
}
