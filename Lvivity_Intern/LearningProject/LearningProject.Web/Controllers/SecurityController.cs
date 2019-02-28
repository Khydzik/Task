using Microsoft.AspNetCore.Mvc;
using System;
using LearningProject.Application;
using LearningProject.Data.Models;
using System.Threading.Tasks;
using LearningProject.Web.Token;

namespace LearningProject.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenFormation _token;

        public SecurityController(IUserService userService, ITokenFormation token)
        {
            _userService = userService;
            _token = token;
        }

        [HttpPost]
        public async Task<AuthorizationDto> Authorization([FromBody] LoginModel input)
        {
            var user = await _userService.GetUser(input.UserName);

            if (user == null)
            {
                throw new Exception("Invalid username or password");
            }
            
            var jwt = _token.GetToken(user);

            return new AuthorizationDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = jwt,
                Role = user.Role.Name
            };
        }
    }
}