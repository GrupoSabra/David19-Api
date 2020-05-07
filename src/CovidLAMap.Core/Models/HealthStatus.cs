using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CovidLAMap.Core.Models
{
    public enum HealthStatus
    {
        /// <summary>
        /// Infected
        /// </summary>
        Infected = 0,
        /// <summary>
        /// Healthy
        /// </summary>
        Healthy = 1,
        /// <summary>
        /// With Symtomps
        /// </summary>
        Symtomps = 2,
        /// <summary>
        /// Subject recoverd from Covid
        /// </summary>
        Recovered = 3
    }

    public static class HealthStatusExtensions 
    {
        public static CredentialType ToCredentialType(this HealthStatus status)
        {
            return status switch
            {
                HealthStatus.Infected => CredentialType.Infection,
                HealthStatus.Healthy => CredentialType.Confinement,
                HealthStatus.Symtomps => CredentialType.Symptoms,
                HealthStatus.Recovered => CredentialType.Recovery,
                _ => CredentialType.Confinement,
            };
        }
    }
}
