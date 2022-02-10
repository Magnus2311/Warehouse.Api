using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Database.Interfaces;
using Warehouse.Database.Repositories;

namespace Warehouse.Api.Services
{
    public abstract class BaseService
    {
        private readonly BaseRepository<IEntity> _baseRepository;

        public BaseService(BaseRepository<IEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public virtual async Task Add(IEntity entity) 
            => await _baseRepository.Add(entity);

        public virtual async Task Delete(string id)
            => await _baseRepository.Delete(id);

        public virtual async Task<IEntity> Get(string id)
            => await _baseRepository.Get(id);

        public virtual async Task<IEnumerable<IEntity>> GetAll()
            => await _baseRepository.GetAll();

        public virtual async Task Update(IEntity entity)
            => await _baseRepository.Update(entity);

    }
}
