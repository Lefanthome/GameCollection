using Nest;
using System;

namespace GameCollection.ElasticSearch.Services
{
    public class ElasticSearchClientBase
    {
        public readonly string INDEX;
        public readonly Uri SERVER_URI;
        public readonly ElasticClient elasticClient;

        public ElasticSearchClientBase(string index, string urlServer)
        {
            INDEX = index;
            SERVER_URI = new Uri(urlServer);
            elasticClient = GetClient();
        }

        private ElasticClient GetClient()
        {
            var settings = new ConnectionSettings(SERVER_URI).DefaultIndex(INDEX);

            var client = new ElasticClient(settings);
            return client;
        }

    }
}
