using LearningProject.Application;
using LearningProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LearningProject.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<User> Registration([FromBody]Register input)
        {
            var user = await _userService.CreateUser(input.UserName, input.Password);          

            return user;
        }
    }
}
