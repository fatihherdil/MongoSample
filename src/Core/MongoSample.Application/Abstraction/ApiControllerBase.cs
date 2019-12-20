using Microsoft.AspNetCore.Mvc;

namespace MongoSample.Application.Abstraction
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ApiControllerBase : Controller
    {
    }
}
