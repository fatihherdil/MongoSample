using MongoSample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MongoSample.Domain.Exceptions
{
    public class EntityNotFoundException<T> : EntityNotFoundException where T : class, IEntity, new()
    {

        public EntityNotFoundException() : base($"{typeof(T).Name} Entity Not Found !", new T())
        {

        }
    }

    public class EntityNotFoundException : ExceptionBase
    {
        public object Entity { get; private set; }

        public EntityNotFoundException(string message, object entity) : base(message)
        {
            Entity = entity;
        }

        public EntityNotFoundException(object entity) : base($"{entity.GetType().Name} Entity Not Found !")
        {
            Entity = entity;
        }

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
