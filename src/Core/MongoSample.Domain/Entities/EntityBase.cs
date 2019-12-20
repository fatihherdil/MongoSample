using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoSample.Domain.Interfaces;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace MongoSample.Domain.Entities
{
    public abstract class EntityBase : IEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [JsonIgnore]
        [BsonIgnore]
        [IgnoreDataMember]
        public virtual bool IsNull { get; set; }

        public abstract IEntity GetNullInstance();
    }
}
