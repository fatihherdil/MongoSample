using Microsoft.AspNetCore.Mvc;
using MongoSample.Application;
using MongoSample.Application.Abstraction;
using MongoSample.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoSample.Web.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly LoginManager _loginManager;
        public LoginController(LoginManager loginManager)
        {
            _loginManager = loginManager ?? throw new ArgumentNullException($"Api Cannot be Build without Repository({nameof(loginManager)})");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Dictionary<string, string> userInfo)
        {
            if (!userInfo.ContainsKey("email"))
                throw new ArgumentNullException($"For User Login Email Should be Given.");
            if (!userInfo.ContainsKey("password"))
                throw new ArgumentNullException($"For User Login Password Should be Given.");

            var result = _loginManager.Login(userInfo["email"], userInfo["password"]);

            return Json(new DefaultResponse(result));
        }
    }
}
