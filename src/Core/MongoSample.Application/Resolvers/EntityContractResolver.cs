using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoSample.Application.Resolvers
{
    public class EntityContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            var ignoreAttributes = new object[] { new JsonIgnoreAttribute(), new IgnoreDataMemberAttribute(), new BsonIgnoreAttribute() };

            var doesHaveIgnoreAttrs = member.GetCustomAttributes(true).Any(attr => ignoreAttributes.Contains(attr));

            prop.Ignored = doesHaveIgnoreAttrs;
            prop.ShouldSerialize = (propInstance) =>
            {
                return !doesHaveIgnoreAttrs;
            };
            return prop;
        }
    }
}
