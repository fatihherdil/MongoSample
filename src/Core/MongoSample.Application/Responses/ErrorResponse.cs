using MongoSample.Application.Abstraction;
using System.Net;

namespace MongoSample.Application.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public ErrorResponse(HttpStatusCode statusCode, object response)
        {
            Status = statusCode.ToString();
            ResponseCode = (int)statusCode;
            Response = response;
        }
    }
}
