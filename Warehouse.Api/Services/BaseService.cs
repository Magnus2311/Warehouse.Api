using MongoDB.Bson;
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

        public virtual async Task Delete(ObjectId id)
            => await _baseRepository.Delete(id);

        public virtual async Task<IEntity> Get(ObjectId id)
            => await _baseRepository.Get(id);

        public virtual async Task<IEnumerable<IEntity>> GetActive()
            => await _baseRepository.GetActive();

        public virtual async Task Update(IEntity entity)
            => await _baseRepository.Update(entity);

    }
}
