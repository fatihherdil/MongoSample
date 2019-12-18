using System;
using System.Net;
using MongoSample.Domain.Exceptions;

namespace MongoSample.Persistence.Exceptions
{
    public class MongoDatabaseException : ExceptionBase
    {
        public MongoDatabaseException(string message) : base(message)
        {

        }
        public MongoDatabaseException(string message, Exception innerException) : base(message, innerException)
        {

        }
        public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
    }
}
