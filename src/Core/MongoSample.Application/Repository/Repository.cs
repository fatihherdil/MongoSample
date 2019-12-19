using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoSample.Domain.Interfaces;
using MongoSample.Persistence;

namespace MongoSample.Application.Repository
{
    public class Repository<T> : MongoRepositoryBase<T> where T : IEntity, new()
    {
        public Repository(MongoContext context) : base(context)
        {
        }

        public override BsonValue Render(IBsonSerializer<T> documentSerializer, IBsonSerializerRegistry serializerRegistry)
        {
            throw new NotImplementedException();
        }
    }
}
