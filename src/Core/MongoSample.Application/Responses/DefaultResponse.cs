using MongoSample.Application.Abstraction;
using System.Net;

namespace MongoSample.Application.Responses
{
    public class DefaultResponse : BaseResponse
    {
        public DefaultResponse():base()
        {

        }
        public DefaultResponse(string status, int responseCode, object response):base(status, responseCode, response)
        {
        }
        public DefaultResponse(string status, object response):base(status, response)
        {
        }
        public DefaultResponse(int responseCode, object response):base(responseCode, response)
        {
        }
        public DefaultResponse(object response):base(response)
        {
        }
        public DefaultResponse(HttpStatusCode statusCode, object response):base(statusCode, response)
        {
        }
        public DefaultResponse(string status, HttpStatusCode statusCode, object response):base(status, statusCode, response)
        {
        }
    }
}
