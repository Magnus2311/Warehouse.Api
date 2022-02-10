using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Database.Interfaces;

namespace Warehouse.Database.Repositories
{
    public abstract class BaseRepository<TEntity> : DatabaseContext, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly IMongoCollection<TEntity> _collection;

        public BaseRepository()
        {
            _collection = _db.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task Add(TEntity entity)
            => await _collection.InsertOneAsync(entity);

        public async Task Delete(string id)
            => await _collection.FindOneAndDeleteAsync(Builders<TEntity>.Filter.Eq(e => e.Id, id));

        public async Task<TEntity> Get(string id)
            => await (await _collection.FindAsync(e => e.Id == id)).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> GetAll()
            => await (await _collection.FindAsync(Builders<TEntity>.Filter.Empty)).ToListAsync();

        public async Task Update(TEntity entity)
            => await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id), entity);
    }
}
