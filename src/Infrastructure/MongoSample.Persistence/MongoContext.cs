using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoSample.Persistence.Exceptions;
using MongoSample.Persistence.Models;
using System;

namespace MongoSample.Persistence
{
    public class MongoContext : IDisposable
    {
        private readonly string _connectionString;
        private readonly string _databaseName;
        private MongoClient _client;
        private IMongoDatabase _db;
        private bool _isDisposed = false;
        private bool _isConnectionOpen = false;
        /*public MongoContext(string connectionString, string databaseName)
        {
            if (string.IsNullOrEmpty(connectionString.Trim()))
                throw new NullReferenceException($"{nameof(connectionString)} Cannot be Null or Empty");
            if (string.IsNullOrEmpty(databaseName.Trim()))
                throw new NullReferenceException($"{nameof(databaseName)} Cannot be Null or Empty");

            _connectionString = connectionString;
            _databaseName = databaseName;

            OpenConnection();
            GetDatabase();
        }*/

        public MongoContext(IOptions<MongoDbOptions> mongoDbOptions)
        {
            if (mongoDbOptions.Value == null)
                throw new ArgumentNullException($"{nameof(mongoDbOptions)} Cannot be Null for MongoContext to Build");
            if (string.IsNullOrEmpty(mongoDbOptions.Value.ConnectionString.Trim()))
                throw new NullReferenceException($"{nameof(mongoDbOptions)} {nameof(mongoDbOptions.Value.ConnectionString)} Cannot be Null or Empty");
            if (string.IsNullOrEmpty(mongoDbOptions.Value.DatabaseName.Trim()))
                throw new NullReferenceException($"{nameof(mongoDbOptions)} {nameof(mongoDbOptions.Value.DatabaseName)} Cannot be Null or Empty");

            _connectionString = mongoDbOptions.Value.ConnectionString;
            _databaseName = mongoDbOptions.Value.DatabaseName;

            OpenConnection();
            GetDatabase();
        }

        protected void OpenConnection()
        {
            CheckDisposed();
            try
            {
                _client = new MongoClient(_connectionString);
                _isConnectionOpen = true;
            }
            catch (Exception e)
            {
                _isConnectionOpen = false;
                throw new MongoConnectionFailureException(e);
            }
        }

        public IMongoDatabase GetDatabase()
        {
            CheckDisposed();
            if (!_isConnectionOpen || _client == null)
                OpenConnection();

            if (string.IsNullOrEmpty(_databaseName.Trim()))
                throw new MongoDatabaseException("DatabaseName cannot be Null or Empty");

            try
            {
                var db = _client.GetDatabase(_databaseName);
                _db = db;
                return db;
            }
            catch (Exception e)
            {
                throw new MongoDatabaseException("Cannot Retrieve Database", e);
            }
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            CheckDisposed();
            if (!_isConnectionOpen || _client == null)
                OpenConnection();
            if (_db == null)
                throw new MongoDatabaseException("Database Not Found");

            try
            {
                var collectionName = typeof(T).Name;
                var collection = _db.GetCollection<T>(collectionName);
                return collection;
            }
            catch (Exception e)
            {
                throw new MongoCollectionException("Cannot Retrieve Collection", e);
            }
        }

        protected void CheckDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(MongoContext), $"{nameof(MongoContext)} is Disposed");
        }

        public void Dispose()
        {
            _client = null;
            _db = null;
            _isConnectionOpen = false;
            _isDisposed = true;
        }
    }
}
