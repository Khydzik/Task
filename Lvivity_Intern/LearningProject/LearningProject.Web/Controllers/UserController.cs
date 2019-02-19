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
    public class UserController : Controller
    {
        private readonly IUserService _objUserService;

        public UserController(IUserService userService)
        {
            _objUserService = userService;
        }

        [HttpPost]
        public async Task<List<User>> GetUsers([FromBody] PaginationModel paginationModel)
        {
            var users = await _objUserService.GetUsersItem(paginationModel);

            return users;
        }       
    }
}
