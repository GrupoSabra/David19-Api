using CovidLAMap.Core;
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
        private RegisteredCredentialRepository credentialsRepository;

        public IRegisteredCredentialRepository Credentials => credentialsRepository ??= new RegisteredCredentialRepository(context);

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
