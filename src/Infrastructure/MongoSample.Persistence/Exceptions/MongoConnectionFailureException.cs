using System;

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
