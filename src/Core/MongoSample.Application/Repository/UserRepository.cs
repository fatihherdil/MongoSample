using MongoSample.Domain.Entities;
using MongoSample.Persistence;

namespace MongoSample.Application.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(MongoContext context) : base(context)
        {
        }


    }
}
