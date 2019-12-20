using MongoSample.Application.Resolvers;
using Newtonsoft.Json;

namespace MongoSample.Application.SerializerSettings
{
    public class EntitySerializerSettings : JsonSerializerSettings
    {
        private static EntitySerializerSettings _instance = new EntitySerializerSettings();
        public static EntitySerializerSettings Instance => _instance;
        public EntitySerializerSettings()
        {
            this.ContractResolver = new EntityContractResolver();
        }
    }
}
