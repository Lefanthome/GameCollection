using GameCollection.Contrat.Dto;
using GameCollection.DALL.Repositories.Implementation;
using System.Collections.Generic;

namespace GameCollection.Business
{
    public class GameService : IGameService
    {
        private readonly GameRepository _repo;
        public GameService(string connectionString)
        {
            _repo = new GameRepository(connectionString);
        }

        public bool Insert(GameDto entity)
        {
            return _repo.Insert(entity);
        }

        public bool Update(GameDto entity)
        {
            return _repo.Update(entity);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public GameDto GetById(int identifier)
        {
            return _repo.GetById(identifier);
        }

        public IEnumerable<GameDto> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
