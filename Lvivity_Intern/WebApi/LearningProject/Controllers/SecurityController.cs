using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using LearningProject.Application;
using LearningProject.Data.Models;
using LearningProject.Web.Models;
using System.Threading.Tasks;

namespace LearningProject.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly IUserService _objUserService;

        public SecurityController(IUserService userService)
        {
            _objUserService = userService;
        }

        [HttpPost]
        public async Task<ResponseLogin<Role>> Authorization([FromBody] LoginModel login)
        {
            var user = await _objUserService.GetUser(login);

            var identity = _objUserService.GetIdentity(user);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER, audience: AuthOptions.AUDIENCE, notBefore: now, claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)), signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new ResponseLogin<Role>
            {
                Id = user.Id,
                Username = identity.Name,
                Access_token = encodedJwt,
                Role = new Role { Name = user.Role.Name }
            };

            return response;
        }
    }
}