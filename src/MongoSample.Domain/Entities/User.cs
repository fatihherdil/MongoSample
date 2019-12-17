using System;
using System.Security.Cryptography;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoSample.Domain.Entities
{
    public class User : EntityBase
    {
        private string _password;
        [BsonRequired]
        public string Email { get; set; }
        [BsonRequired]
        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrEmpty(value.Trim()))
                    throw new NullReferenceException($"{nameof(Password)} cannot be Empty or Null");
                _password = GetHashedPassword(value);
            }
        }

        public string Name { get; set; }
        public string Surname { get; set; }

        protected string GetHashedPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var derivedBytes = new Rfc2898DeriveBytes(password, salt, 10000);

            var hash = derivedBytes.GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }
    }
}
