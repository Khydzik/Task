using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using LearningProject.Application;
using LearningProject.Data.Models;
using System.Threading.Tasks;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace LearningProject.Web.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<List<User>> GetUsers([FromBody] PaginationModel paginationModel)
        {
            var users = await _userService.GetUsersItem(paginationModel.Skip, paginationModel.Take);

            if(users == null) { throw new Exception("No users."); }

            return users;
        }       
    }
}
