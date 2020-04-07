using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidLAMap.Web.ApiModels
{
    public class EthEvent
    {
        public List<EthValue> IndexedParameters { get; set; }
        public List<EthValue> NonIndexedParameters { get; set; }
        public string Name { get; set; }
        public string FilterId { get; set; }
        public string NodeName { get; set; }
        public string TransactionHash { get; set; }
        public int LogIndex { get; set; }
        public int BlockNumber { get; set; }
        public string BlockHash { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string EventSpecificationSignature { get; set; }
        public string NetworkName { get; set; }
        public string Id { get; set; }
    }

    public class EthValue
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
