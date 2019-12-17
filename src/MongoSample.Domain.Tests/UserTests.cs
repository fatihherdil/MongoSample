using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
