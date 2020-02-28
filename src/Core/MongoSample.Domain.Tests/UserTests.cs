using System;
using MongoDB.Bson;
using MongoSample.Domain.Entities;
using NUnit.Framework;

namespace MongoSample.Domain.Tests
{
    public class UserTests
    {
        [Test]
        public void User_Creation_Password_Test()
        {
            var pwd = "Test";
            var email = "Test";
            var user = new User();
            Assert.Throws(typeof(NullReferenceException), () => { user.Password = string.Empty; });
            Assert.DoesNotThrow(() =>
            {
                user.Email = email;
                user.Password = pwd;
            });
            Assert.AreNotEqual(pwd, user.Password);
            user.Id = ObjectId.Parse("012345678901234567890123");
            user.Password = pwd;
            Assert.AreEqual(pwd, user.Password);
        }
        
        [Test]
        public void User_Nullable_Test()
        {
            var user = new User().GetNullInstance();
            Assert.IsTrue(user.IsNull);

            user = new User();
            Assert.IsFalse(user.IsNull);
        }
    }
}
