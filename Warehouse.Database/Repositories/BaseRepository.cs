using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Database.Helpers.ExtensionMethods;
using Warehouse.Database.Interfaces;

namespace Warehouse.Database.Repositories
{
    public abstract class BaseRepository<TEntity> : DatabaseContext, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly IMongoCollection<TEntity> _collection;

        public BaseRepository(IConfiguration configuration) : base(configuration)
        {
            _collection = _db.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task Add(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.InitHistory();
            await _collection.InsertOneAsync(entity);
        }

        public async Task Delete(ObjectId id)
        {
            var entity = await (await _collection.FindAsync(e => e.Id == id)).FirstOrDefaultAsync();
            entity.Version = ++entity.Version;
            entity.IsDeleted = true;
            entity.IsDeleted_history = entity.IsDeleted_history.Append(new()
            {
                UpdatedDate = DateTime.Now,
                Value = true,
                Version = entity.Version
            });
            await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id), entity);
        }

        public async Task<TEntity> Get(ObjectId id)
            => await (await _collection.FindAsync(e => e.Id == id)).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> GetActive()
            => await (await _collection.FindAsync(Builders<TEntity>.Filter.Eq(e => e.IsDeleted, false))).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetDeleted()
            => await (await _collection.FindAsync(Builders<TEntity>.Filter.Eq(e => e.IsDeleted, true))).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAll()
            => await (await _collection.FindAsync(Builders<TEntity>.Filter.Empty)).ToListAsync();

        public async Task Update(TEntity entity)
        {
            var oldEntity = await (await _collection.FindAsync(e => e.Id == entity.Id)).FirstOrDefaultAsync();
            entity.Version = ++oldEntity.Version;
            oldEntity.UpdateHistoryForChanges(entity);
            entity.UpdatedDate = DateTime.Now;

            await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id), entity);
        }

        public async Task UpdateWithoutHistory(TEntity entity)
            => await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id), entity);
    }
}
