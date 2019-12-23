using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoSample.Application.Abstraction;
using MongoSample.Application.Interfaces;
using MongoSample.Application.Models;
using MongoSample.Application.Responses;
using MongoSample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoSample.Web.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IMongoRepository<User> _userRepository;
        public UserController(IMongoRepository<User> userRepository)
        {
            if (userRepository == null) throw new ArgumentException($"Api Cannot be Build without Repository({nameof(userRepository)})");
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Json(new DefaultResponse((await _userRepository.GetAllAsync()).ToEnumerable()));
        }

        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser(User user)
        {
            if (!ModelState.IsValid) throw new ArgumentException($"{nameof(user)} is Invalid");
            var u = await _userRepository.AddAsync(user);
            return Json(new DefaultResponse(System.Net.HttpStatusCode.Created, u));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser(UserUpdateModel user)
        {
            if (!ModelState.IsValid) throw new ArgumentException($"{nameof(user)} is Invalid");
            ObjectId id = ObjectId.Empty;
            if (!ObjectId.TryParse(user.Id, out id)) throw new FormatException($"{nameof(user.Id)} is in Incorrect Format For ObjectId(MongoDb)");
            if (id == ObjectId.Empty) throw new ArgumentException($"{nameof(user.Id)} Cannot Be Null Or Empty");

            user.User.Id = id;

            var u = _userRepository.UpdateAsync(user.User);

            return Json(new DefaultResponse(System.Net.HttpStatusCode.NoContent, user.User));
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id.Trim())) throw new ArgumentNullException($"{nameof(id)} Cannot Be Null Or Empty");
            ObjectId objId = ObjectId.Empty;
            if (!ObjectId.TryParse(id, out objId)) throw new FormatException($"{nameof(id)} is in Incorrect Format For ObjectId(MongoDb)");
            if (objId == ObjectId.Empty) throw new ArgumentException($"{nameof(id)} Cannot Be Null Or Empty");

            return Json(new DefaultResponse(System.Net.HttpStatusCode.NoContent, await _userRepository.DeleteByIdAsync(objId)));
        }

        [HttpPost("delete")]
        [ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUserPost(Dictionary<string, string> id)
        {

            if (!id.ContainsKey("id")) throw new ArgumentNullException($"{nameof(id)} Cannot Be Null Or Empty");

            var Id = id["id"].Trim();
            return await DeleteUser(Id);
        }

    }
}
