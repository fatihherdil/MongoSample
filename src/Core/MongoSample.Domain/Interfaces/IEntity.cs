using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace MongoSample.Domain.Interfaces
{
    public interface IEntity : INullableObject<IEntity>
    {
        ObjectId Id { get; set; }
    }
}
