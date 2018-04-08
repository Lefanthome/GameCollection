using GameCollection.Contrat.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCollection.ElasticSearch.Services
{
    public class EsGameDocumentService : EsDocumentService<GameDto>
    {
        public EsGameDocumentService(string index, string urlServer) 
            : base(index, urlServer)
        {
        }

        public GameDto SeachBy(int identifier)
        {

            var response = elasticClient.Search<GameDto>(s => s
            .Index(INDEX)
            .Type(typeof(GameDto))
            .Query(q =>
                q.Match(mq => mq.Field(f => f.Identifier).Query(identifier.ToString()))
                )
            );

            if (response.Hits.Count == 1)
            {
                var list = response.Hits.Select(x => x.Source);
                return list.FirstOrDefault();
            }

            return null;
        }
    }
}
