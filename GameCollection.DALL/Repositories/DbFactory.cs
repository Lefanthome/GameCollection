using System;
using System.Data;
using System.Data.Common;

namespace GameCollection.DALL.Repositories
{

    public class DbFactory
    {

        public static IDbConnection GetConnection(string connectionString, string providerName, bool isWithOpen)
        {
            var _provider = DbProviderFactories.GetFactory(providerName);
            var _connection = _provider.CreateConnection();
            if (_connection == null)
                throw new Exception(string.Format("Failed to create a connection using the connection string named '{0}' in app/web.config.", connectionString));

            _connection.ConnectionString = connectionString;

            if (isWithOpen)
            {
                _connection.Open();
            }

            return _connection;
        }

    }
}
