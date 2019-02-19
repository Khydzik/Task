using Microsoft.AspNetCore.Mvc;
using System;
using LearningProject.Application;
using LearningProject.Data.Models;
using System.Threading.Tasks;

namespace LearningProject.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly IUserService _userService;

        public SecurityController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<AuthorizationDto> Authorization([FromBody] LoginModel input)
        {
            var user = await _userService.GetUser(input.UserName, input.Password);

            if (user == null)
            {
                throw new Exception("Invalid username or password");
            }

            ///TODO
            //var identity = _userService.GetIdentity(user);

            //var now = DateTime.UtcNow;

            //var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER, audience: AuthOptions.AUDIENCE, notBefore: now, claims: identity.Claims,
            //    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)), signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
            //    SecurityAlgorithms.HmacSha256));

            //var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthorizationDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = encodedJwt,
                Role = user.Role.Name
            };
        }
    }
}