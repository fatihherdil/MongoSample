using System;
using MongoSample.Domain.Entities;
using NUnit.Framework;

namespace MongoSample.Domain.Tests
{
    public class UserTests
    {
        [Test]
        public void User_Creation_Password_Test()
        {
            var user = new User();
            Assert.Throws(typeof(NullReferenceException), () => { user.Password = string.Empty; });
            Assert.DoesNotThrow(() =>
            {
                user.Password = "Test";
            });
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
