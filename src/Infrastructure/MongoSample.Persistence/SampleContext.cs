using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoSample.Domain.Entities;
using MongoSample.Persistence.Models;

namespace MongoSample.Persistence
{
    public class SampleContext : MongoContext
    {
        public IMongoCollection<User> User { get; set; }
        public SampleContext(IOptions<MongoDbOptions> mongoDbOptions) : base(mongoDbOptions)
        {
        }

    }

}
