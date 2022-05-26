using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Database.Helpers;
using Warehouse.Database.Helpers.ExtensionMethods;
using Warehouse.Database.Interfaces;

namespace Warehouse.Database.Repositories
{
    public abstract class BaseRepository<TEntity> : DatabaseContext, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly IMongoCollection<TEntity> _collection;
        protected readonly AppSettings _appSettings;

        public BaseRepository(IConfiguration configuration,
            AppSettings appSettings) : base(configuration)
        {
            _collection = _db.GetCollection<TEntity>(typeof(TEntity).Name);
            _appSettings = appSettings;
        }

        public async Task Add(TEntity entity)
        {
            entity.UserId = _appSettings.UserId;
            entity.CreatedDate = DateTime.Now;
            entity.InitHistory();
            await _collection.InsertOneAsync(entity);
        }

        public async Task Delete(ObjectId id)
        {
            var entity = await (await _collection.FindAsync(e => e.Id == id)).FirstOrDefaultAsync();
            if (entity.UserId == _appSettings.UserId)
            {
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
        }

        public async Task<TEntity> Recover(ObjectId id)
        {
            var entity = await (await _collection.FindAsync(e => e.Id == id)).FirstOrDefaultAsync();
            if (entity.UserId == _appSettings.UserId)
            {
                entity.Version = ++entity.Version;
                entity.IsDeleted = false;
                entity.IsDeleted_history = entity.IsDeleted_history.Append(new()
                {
                    UpdatedDate = DateTime.Now,
                    Value = false,
                    Version = entity.Version
                });
                await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id), entity);
            }
            return entity;
        }

        public async Task<TEntity> Get(ObjectId id)
            => await (await _collection.FindAsync(e => e.Id == id && e.UserId == _appSettings.UserId)).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> GetActive()
            => await (await _collection.FindAsync(e => !e.IsDeleted && e.UserId == _appSettings.UserId)).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetDeleted()
            => await (await _collection.FindAsync(e => e.IsDeleted && e.UserId == _appSettings.UserId)).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAll()
            => await (await _collection.FindAsync(e => e.UserId == _appSettings.UserId)).ToListAsync();

        public async Task Update(TEntity entity)
        {
            var oldEntity = await (await _collection.FindAsync(e => e.Id == entity.Id)).FirstOrDefaultAsync();
            if (oldEntity.UserId == _appSettings.UserId)
            {
                entity.Version = ++oldEntity.Version;
                oldEntity.UpdateHistoryForChanges(entity);
                entity.UpdatedDate = DateTime.Now;

                await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id), entity);
            }
        }

        public async Task UpdateWithoutHistory(TEntity entity)
            => await _collection.ReplaceOneAsync(e => e.Id == entity.Id && e.UserId == _appSettings.UserId, entity);
    }
}
