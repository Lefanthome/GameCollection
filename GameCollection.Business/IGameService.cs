using GameCollection.Contrat.Dto;

namespace GameCollection.Business
{
    public interface IGameService
    {
        bool Insert(GameDto entity);
        bool Update(GameDto entity);
        bool Delete(int id);
        GameDto GetById(int identifier);
    }
}
