using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoClients
{
    public class QueryResult<T> where T : class
    {
        public IEnumerable<T>? Items{get;set; }
        public int Count { get; set; }
    }
}
