using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoSample.Application.Interfaces;
using MongoSample.Domain.Interfaces;
using MongoSample.Persistence;

namespace MongoSample.Application.Repositories
{
    public abstract class MongoRepositoryBase<T> : IMongoRepository<T> where T : IEntity, new()
    {
        protected readonly IMongoCollection<T> _collection;
        public MongoRepositoryBase(MongoContext context)
        {
            _collection = context.GetCollection<T>();
        }

        public virtual T Add(T entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public virtual Task<T> AddAsync(T entity)
        {
            _collection.InsertOneAsync(entity);
            return Task.FromResult<T>(entity);
        }

        public virtual int AddRange(IEnumerable<T> entities)
        {
            _collection.InsertMany(entities);
            return 0;
        }

        public virtual Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            _collection.InsertManyAsync(entities);
            return Task.FromResult<int>(0);
        }

        public virtual T Delete(T entity)
        {
            var deleted =_collection.FindOneAndDelete(e => e.Id == entity.Id);
            return deleted;
        }

        public virtual Task<T> DeleteAsync(T entity)
        {
            var deleted = _collection.FindOneAndDeleteAsync(e => e.Id == entity.Id);
            return deleted;
        }

        public virtual T DeleteById(ObjectId id)
        {
            var deleted = _collection.FindOneAndDelete(e => e.Id == id);
            return deleted;
        }

        public virtual Task<T> DeleteByIdAsync(ObjectId id)
        {
            var deleted = _collection.FindOneAndDeleteAsync(e => e.Id == id);
            return deleted;
        }

        public virtual ICollection<T> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public virtual Task<IAsyncCursor<T>> GetAllAsync()
        {
            return _collection.FindAsync(_ => true);
        }

        public virtual ICollection<T> GetAllBy(Expression<Func<T, bool>> match)
        {
            return _collection.Find(match).ToList();
        }

        public virtual Task<IAsyncCursor<T>> GetAllByAsync(Expression<Func<T, bool>> match)
        {
            return _collection.FindAsync(match);
        }

        public virtual T GetBy(Expression<Func<T, bool>> match)
        {
            return _collection.Find(match).FirstOrDefault();
        }

        public virtual Task<T> GetByAsync(Expression<Func<T, bool>> match)
        {
            var entities = _collection.FindAsync(match);
            entities.Wait();
            return entities.Result.FirstOrDefaultAsync();
        }

        public virtual T GetById(ObjectId id)
        {
            return _collection.Find(e => e.Id == id).FirstOrDefault();
        }

        public virtual Task<T> GetByIdAsync(ObjectId id)
        {
            var entities = _collection.FindAsync(e => e.Id == id);
            entities.Wait();
            return entities.Result.FirstOrDefaultAsync();
        }

        public virtual T Update(T entity)
        {
            return _collection.FindOneAndReplace<T>(e => e.Id == entity.Id, entity, new FindOneAndReplaceOptions<T, T>() { IsUpsert = false });
        }

        public virtual Task<T> UpdateAsync(T entity)
        {
            return _collection.FindOneAndReplaceAsync<T>(u => u.Id == entity.Id, entity, new FindOneAndReplaceOptions<T, T>() { IsUpsert = false });
        }
    }
}
