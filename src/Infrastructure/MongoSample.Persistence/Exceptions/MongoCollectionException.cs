using System;
using System.Net;
using MongoSample.Domain.Exceptions;

namespace MongoSample.Persistence.Exceptions
{
    public class MongoCollectionException : ExceptionBase
    {
        public MongoCollectionException(string message):base(message)
        {

        }

        public MongoCollectionException(string message, Exception innerException):base(message, innerException)
        {

        }
        public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
    }
}
