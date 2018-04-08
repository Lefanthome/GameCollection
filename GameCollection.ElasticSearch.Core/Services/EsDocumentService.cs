using GameCollection.Contrat.Dto;
using Nest;
using System.Collections.Generic;
using System.Linq;

namespace GameCollection.ElasticSearch.Services
{
    public class EsDocumentService<TEntity> 
        : ElasticSearchClientBase
        where TEntity : class, IEntity
    {
        public EsDocumentService(string index, string urlServer)
            : base(index, urlServer)
        { }

        public virtual IBulkResponse BulkInsert(IEnumerable<TEntity> entities)
        {
            var request = new BulkDescriptor();

            foreach (var entity in entities)
            {
                request
                    .Index<TEntity>(op => op
                        .Id(entity.Identifier)
                        .Index(INDEX)
                        .Document(entity)
                        );
            }

            return elasticClient.Bulk(request);
        }

        public virtual void Insert(TEntity entity)
        {
            elasticClient.Index(entity,
                i => i
                    .Index(INDEX)
                    .Type(typeof(TEntity))
                    .Id(entity.Identifier)
                    .Refresh(Elasticsearch.Net.Refresh.True)
                    );
        }

        public virtual IUpdateResponse<TEntity> Update(TEntity entity)
        {
            return elasticClient.Update(DocumentPath<TEntity>
                .Id(entity.Identifier),
                u => u
                    .Index(INDEX)
                    .Type(typeof(TEntity))
                    .DocAsUpsert(true)
                    .Doc(entity));
        }

        public virtual IDeleteResponse Delete(TEntity entity)
        {
            return elasticClient.Delete<TEntity>(entity.Identifier,
                d => d
                .Index(INDEX)
                .Type(typeof(TEntity))
                );
        }

        public virtual SearchResult<TEntity> SearchAll(int from = 0, int size = 10000)
        {
            var response = elasticClient.Search<TEntity>(s => s
                            .From(from)
                            .Size(size)
                            .Index(INDEX)
                            .Type(typeof(TEntity))
                            .Query(q => q.MatchAll())
                           );

            if (response.Hits.Count > 0)
            {
                var list = response.Hits.Select(x => x.Source);

                return new SearchResult<TEntity>
                {
                    Total = response.Total,
                    ElapsedMilliseconds = response.Took,
                    Results = response.Documents

                };
            }

            return new SearchResult<TEntity>
            {
                Total = 0,
                ElapsedMilliseconds = 0,
                Results = null
            };
        }
    }

}
