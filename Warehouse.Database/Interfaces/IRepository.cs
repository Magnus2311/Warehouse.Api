using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Database.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(string id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(string id);
    }
}
