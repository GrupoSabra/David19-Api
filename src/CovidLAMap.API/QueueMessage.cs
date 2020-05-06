using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidLAMap.API
{
    public class QueueMessage<T>
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public T Details { get; set; }
        public int Retries { get; set; }
    }
}
