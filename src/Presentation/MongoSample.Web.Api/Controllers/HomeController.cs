using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoSample.Application.Repository;

namespace MongoSample.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public HomeController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //return Json(await _userRepository.GetAllAsync());
            return null;
        }
    }
}