using MongoSample.Domain.Exceptions;
using System.Net;

namespace MongoSample.Application.Exceptions
{
    public class InvalidModelException : ExceptionBase
    {
        public InvalidModelException() : base("Submitted Model Is Not Valid !")
        {

        }

        public InvalidModelException(string message) : base(message)
        {

        }

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}
