using Dapper;
using GameCollection.Contrat.Dto;
using GameCollection.DALL.Repositories.Interface;
using GameCollection.DALL.Repositories.Queries;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GameCollection.DALL.Repositories.Implementation
{
    public class GameRepository : ICrudRepository<GameDto>
    {
        private readonly string _cnx;

        public GameRepository(string connectionString, string providerName = "System.Data.SqlClient")
        {
            _cnx = connectionString; //DbFactory.GetConnection(connectionString, providerName, false);
        }

        public bool Insert(GameDto entity)
        {
            using (SqlConnection connection = new SqlConnection(_cnx))
            {
                int rowsAffected = connection.Execute(GameQueries.INSERT, new { Name = entity.Name, Developper = entity.Developper, Console = entity.Console, Genre = entity.Genre });
                return rowsAffected > 0 ? true : false;
            }
        }

        public bool Update(GameDto entity)
        {
            using (SqlConnection connection = new SqlConnection(_cnx))
            {
                int rowsAffected = connection.Execute(GameQueries.UPDATE, new { Identifier = entity.Identifier, Name = entity.Name, Developper = entity.Developper, Console = entity.Console, Genre = entity.Genre });
                return rowsAffected > 0 ? true : false;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_cnx))
            {
                int rowsAffected = connection.Execute(GameQueries.DELETE, new { Identifier = id });

                return rowsAffected > 0 ? true : false;
            }
        }

        public GameDto GetById(int identifier)
        {
            using (SqlConnection connection = new SqlConnection(_cnx))
            {
                var result = connection.QueryFirstOrDefault<GameDto>(GameQueries.GET_ALL);
                return result;
            }
        }

        public IEnumerable<GameDto> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_cnx))
            {
                //connection.Open();
                var resultList = connection.Query<GameDto>(GameQueries.GET_ALL);
                return resultList;
            }

        }
    }
}
