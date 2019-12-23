using MongoSample.Application.Interfaces;
using MongoSample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoSample.Application
{
    public class LoginManager
    {
        private readonly IMongoRepository<User> _userRepository;
        public LoginManager(IMongoRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException($"{nameof(email)} cannot be Null or Empty");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException($"{nameof(password)} cannot be Null or Empty");

            var user = new User();
            user.Email = email;
            user.Password = password;

            var mongoUser = _userRepository.GetBy(u => u.Email == user.Email);

            if (mongoUser.Password != user.Password)
                return false;

            return true;
        }
    }
}
