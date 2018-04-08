using GameCollection.Contrat.Dto;
using GameCollection.ElasticSearch.Interface;
using Nest;
using System;

namespace GameCollection.ElasticSearch.Services
{
    public class EsIndexService : ElasticSearchClientBase, IIndex
    {
        public EsIndexService(string index, string urlServer)
            : base(index, urlServer)
        {

        }

        public void CreateIndex()
        {
            if (!elasticClient.IndexExists(INDEX).Exists)
            {
                var settings = new IndexSettings { NumberOfReplicas = 0, NumberOfShards = 2 };
                var indexConfig = new IndexState
                {
                    Settings = settings
                };

                //create with settings and mappings
                elasticClient.CreateIndex(INDEX, c => c
                    .InitializeUsing(indexConfig)
                    .Mappings(m => m.Map<GameDto>(mp => mp.AutoMap()))
                    );
            }
        }

        public void DeleteIndex()
        {
            elasticClient.DeleteIndex(INDEX);
        }

        public void ReIndex(string src, string dest)
        {
            throw new NotImplementedException();
        }
    }
}
