using GameCollection.Contrat.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCollection.DALL.Repositories.Interface
{
    public interface ICrudRepository<T> where T : class
    {
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(int id);
        T GetById(int identifier);
        IEnumerable<T> GetAll();
    }
}
