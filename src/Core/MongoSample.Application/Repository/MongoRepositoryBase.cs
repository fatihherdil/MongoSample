using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoSample.Application.Interfaces;
using MongoSample.Domain.Interfaces;
using MongoSample.Persistence;

namespace MongoSample.Application.Repository
{
    public abstract class MongoRepositoryBase<T> : UpdateDefinition<T>, IMongoRepository<T> where T : IEntity, new()
    {
        private readonly IMongoCollection<T> _collection;
        public MongoRepositoryBase(MongoContext context)
        {
            _collection = context.GetCollection<T>();
        }

        public T Add(T entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public Task<T> AddAsync(T entity)
        {
            _collection.InsertOneAsync(entity);
            return Task.FromResult<T>(entity);
        }

        public int AddRange(IEnumerable<T> entities)
        {
            _collection.InsertMany(entities);
            return 0;
        }

        public Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            _collection.InsertManyAsync(entities);
            return Task.FromResult<int>(0);
        }

        public T Delete(T entity)
        {
            var deleted =_collection.FindOneAndDelete(e => e.Id == entity.Id);
            return deleted;
        }

        public Task<T> DeleteAsync(T entity)
        {
            var deleted = _collection.FindOneAndDeleteAsync(e => e.Id == entity.Id);
            return deleted;
        }

        public T DeleteById(ObjectId id)
        {
            var deleted = _collection.FindOneAndDelete(e => e.Id == id);
            return deleted;
        }

        public Task<T> DeleteByIdAsync(ObjectId id)
        {
            var deleted = _collection.FindOneAndDeleteAsync(e => e.Id == id);
            return deleted;
        }

        public ICollection<T> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public Task<IAsyncCursor<T>> GetAllAsync()
        {
            return _collection.FindAsync(_ => true);
        }

        public ICollection<T> GetAllBy(Expression<Func<T, bool>> match)
        {
            return _collection.Find(match).ToList();
        }

        public Task<IAsyncCursor<T>> GetAllByAsync(Expression<Func<T, bool>> match)
        {
            return _collection.FindAsync(match);
        }

        public T GetBy(Expression<Func<T, bool>> match)
        {
            return _collection.Find(match).FirstOrDefault();
        }

        public Task<T> GetByAsync(Expression<Func<T, bool>> match)
        {
            var entities = _collection.FindAsync(match);
            entities.Wait();
            return entities.Result.FirstOrDefaultAsync();
        }

        public T GetById(ObjectId id)
        {
            return _collection.Find(e => e.Id == id).FirstOrDefault();
        }

        public Task<T> GetByIdAsync(ObjectId id)
        {
            var entities = _collection.FindAsync(e => e.Id == id);
            entities.Wait();
            return entities.Result.FirstOrDefaultAsync();
        }

        public T Update(T entity)
        {
            return _collection.FindOneAndUpdate(e => e.Id == entity.Id, this);
        }

        public Task<T> UpdateAsync(T entity)
        {
            return _collection.FindOneAndUpdateAsync(e => e.Id == entity.Id, this);
        }
    }
}
