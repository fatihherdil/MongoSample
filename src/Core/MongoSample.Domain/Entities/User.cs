using System;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoSample.Domain.Interfaces;

namespace MongoSample.Domain.Entities
{
    [BsonIgnoreExtraElements]
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
                if (Id == null || Id == ObjectId.Empty)
                    _password = GetHashedPassword(value);
                else
                    _password = value;
            }
        }

        public string Name { get; set; }
        public string Surname { get; set; }

        public override IEntity GetNullInstance()
        {
            return new User
            {
                Email = string.Empty,
                Id = ObjectId.Empty,
                IsNull = true,
                Name = string.Empty,
                Surname = string.Empty,
                _password = string.Empty
            };
        }

        protected string GetHashedPassword(string password)
        {
            //var saltBytes = new byte[16];
            //var provider = new RNGCryptoServiceProvider();
            //provider.GetNonZeroBytes(saltBytes);
            //var salt = Convert.ToBase64String(saltBytes);

            //var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            //var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            //return hashPassword; //TODO: Store Salt with User or Use the Code Below.


            byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
            byte[] salt = Encoding.UTF8.GetBytes(Email);
            byte[] saltedPassword = new byte[pwdBytes.Length + salt.Length];

            Buffer.BlockCopy(pwdBytes, 0, saltedPassword, 0, pwdBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, pwdBytes.Length, salt.Length);

            SHA1 sha = SHA1.Create();

            return Convert.ToBase64String(sha.ComputeHash(saltedPassword));
        }
    }
}