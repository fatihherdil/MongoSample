using MongoSample.Domain.Interfaces;

namespace MongoSample.Application.Interfaces
{
    public interface IDto<TEntity> where TEntity : IEntity, new()
    {
        TEntity ConvertToBo();
    }
}
