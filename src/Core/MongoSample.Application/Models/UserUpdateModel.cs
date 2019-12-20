using MongoSample.Domain.Entities;

namespace MongoSample.Application.Models
{
    public class UserUpdateModel
    {
        public string Id { get; set; }

        public User User { get; set; }
    }
}
