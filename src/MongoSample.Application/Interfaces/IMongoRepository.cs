using MongoDB.Bson;
using MongoDB.Driver;
using MongoSample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoSample.Application.Interfaces
{
    public interface IMongoRepository<T> where T : IEntity, new()
    {
        ICollection<T> GetAll();
        T GetById(ObjectId id);
        T GetBy(Expression<Func<T, bool>> match);
        ICollection<T> GetAllBy(Expression<Func<T, bool>> match);

        Task<IAsyncCursor<T>> GetAllAsync();
        Task<T> GetByIdAsync(ObjectId id);
        Task<T> GetByAsync(Expression<Func<T, bool>> match);
        Task<IAsyncCursor<T>> GetAllByAsync(Expression<Func<T, bool>> match);

        T Delete(T entity);
        Task<T> DeleteAsync(T entity);

        T DeleteById(ObjectId id);
        Task<T> DeleteByIdAsync(ObjectId id);

        T Update(T entity);
        Task<T> UpdateAsync(T entity);

        T Add(T entity);
        Task<T> AddAsync(T entity);

        int AddRange(IEnumerable<T> entities);
        Task<int> AddRangeAsync(IEnumerable<T> entities);
    }
}
