using MongoSample.Domain.Entities;
using MongoSample.Persistence;

namespace MongoSample.Application.Repositories
{
    public class UserRepository : MongoRepositoryBase<User>
    {
        public UserRepository(MongoContext context) : base(context)
        {
        }
    }
}
