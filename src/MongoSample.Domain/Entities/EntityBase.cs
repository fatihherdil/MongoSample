using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoSample.Domain.Interfaces;

namespace MongoSample.Domain.Entities
{
    public abstract class EntityBase : IEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
