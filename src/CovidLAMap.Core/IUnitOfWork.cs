using CovidLAMap.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidLAMap.Core
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
    }
}
