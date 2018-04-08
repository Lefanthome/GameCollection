using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCollection.Contrat.Dto
{
    public class SearchResult<T>
    {
        public long Total { get; set; }
        public IEnumerable<T> Results {get;set;}
        public long ElapsedMilliseconds { get; set; }
    }
}
