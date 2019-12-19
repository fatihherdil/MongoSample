using MongoSample.Application.Interfaces;
using System.Net;

namespace MongoSample.Application.Abstraction
{
    public abstract class BaseResponse : IResponse
    {
        public virtual string Status { get; set; }
        public virtual int ResponseCode { get; set; }
        public virtual object Response { get; set; }

        protected BaseResponse()
        {

        }

        public BaseResponse(string status, int responseCode, object response)
        {
            Status = status;
            ResponseCode = responseCode;
            Response = response;
        }

        public BaseResponse(string status, object response)
        {
            Status = status;
            ResponseCode = 500;
            Response = response;
        }

        public BaseResponse(int responseCode, object response)
        {
            Status = "OK";
            ResponseCode = responseCode;
            Response = response;
        }

        public BaseResponse(object response)
        {
            Status = "OK";
            ResponseCode = 200;
            Response = response;
        }

        public BaseResponse(HttpStatusCode statusCode, object response)
        {
            Status = "OK";
            ResponseCode = (int)statusCode;
            Response = response;
        }

        public BaseResponse(string status, HttpStatusCode statusCode, object response)
        {
            Status = status;
            ResponseCode = (int)statusCode;
            Response = response;
        }
    }
}
