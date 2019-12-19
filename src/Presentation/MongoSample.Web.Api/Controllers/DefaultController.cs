using Microsoft.AspNetCore.Mvc;
using MongoSample.Application.Abstraction;
using MongoSample.Persistence;
using System;
using System.Threading.Tasks;

namespace MongoSample.Web.Api.Controllers
{
    public class DefaultController : ApiControllerBase
    {
        private readonly MongoContext _context;
        public DefaultController(MongoContext mongoContext)
        {
            _context = mongoContext;
        }

        [HttpGet]
        public async Task<IAsyncResult> Index()
        {
            //return Json()
            return null;
        }
    }
}
