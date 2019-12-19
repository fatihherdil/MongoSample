using System;
using System.Net;
using System.Runtime.Serialization;

namespace MongoSample.Domain.Exceptions
{
    [Serializable]
    public abstract class ExceptionBase : Exception
    {
        public abstract HttpStatusCode StatusCode { get; }

        protected ExceptionBase()
        {

        }
        protected ExceptionBase(string message) : base(message)
        {

        }
        protected ExceptionBase(string message, Exception innerException) : base(message, innerException)
        {

        }
        protected ExceptionBase(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {

        }
    }
}
