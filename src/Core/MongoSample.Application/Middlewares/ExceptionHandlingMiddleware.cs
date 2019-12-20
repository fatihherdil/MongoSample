using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoSample.Application.Responses;
using MongoSample.Domain.Exceptions;
using Utf8Json;

namespace MongoSample.Application.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            const HttpStatusCode code = HttpStatusCode.InternalServerError;

            var exception = ex as ExceptionBase;

            var responseCode = exception != null ? (int)exception.StatusCode : (int)code;

            //var result = JsonConvert.SerializeObject(new ErrorResponse((HttpStatusCode)responseCode, ex.Message),
            //    new JsonSerializerSettings()
            //    {
            //        ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //        Formatting = Formatting.Indented
            //    });

            using (var memoryStream = new MemoryStream())
            {
                var errorResponse = new ErrorResponse((HttpStatusCode)responseCode, ex.Message);
                JsonSerializer.SerializeAsync(memoryStream, errorResponse);
                var result = JsonSerializer.PrettyPrint(memoryStream.ToArray());
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = responseCode;

                return context.Response.WriteAsync(result);
            }
        }
    }
}
