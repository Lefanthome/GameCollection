using GameCollection.Contrat.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCollection.ElasticSearch.Interfaces
{
    public interface ISearchService<T>
    {
        SearchResult<T> Search(string query, int page, int pageSize);
    }
}
