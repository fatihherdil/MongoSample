using MongoSample.Domain.Exceptions;
using System;
using System.Net;

namespace MongoSample.Persistence.Exceptions
{
    public class MongoConnectionException : ExceptionBase
    {
        public MongoConnectionException(string message) : base(message)
        {

        }
        public MongoConnectionException(string message, Exception innerException) : base(message, innerException)
        {

        }
        public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

    }
}
