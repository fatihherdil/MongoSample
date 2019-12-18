using MongoSample.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MongoSample.Persistence.Exceptions
{
    public class MongoConnectionFailureException : MongoConnectionException
    {
        public MongoConnectionFailureException() :base("Mongo Client Cannot Connect To Server")
        {

        }
        public MongoConnectionFailureException(Exception innerException):base("Mongo Client Cannot Connect To Server", innerException)
        {

        }
    }
}
