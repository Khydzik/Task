using LearningProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace LearningProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        public const int ValidationError = 3;
        ApplicationContext db;
 
        public SecurityController(ApplicationContext context)
        {
            this.db = context;
        }        

        [HttpPost]
        public async Task<object> Authorization([FromBody] LoginModel login)
        {
            var user = GetUser(login);
            var identity = GetIdentity(user);

            if (identity == null)
            {
                throw new Exception("Invalid username or password");
            }  

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER, audience: AuthOptions.AUDIENCE, notBefore: now, claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)), signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                id = user.Id,
                username = identity.Name,
                access_token = encodedJwt,                
                role = new { name = user.Role.Name }                
            };

            return response;
        }

        private User GetUser(LoginModel data)
        {
            User user = db.Users.Include(u => u.Role).FirstOrDefault(x => x.UserName == data.UserName && x.Password == data.Password);

            return user;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
                    
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Authorization", ClaimsIdentity.DefaultNameClaimType,ClaimsIdentity.DefaultRoleClaimType);
 
                return claimsIdentity;
            }
            return null;
        }
    }
}