using LearningProject.Application;
using LearningProject.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearningProject.Web.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserEditController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserEditController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPatch]
        public async Task<Role> ChangeRole([FromBody] EditModel input)
        {
            var role = await _userService.ChangeUserRole(input.userId, input.roleId);

            if(role == null){ throw new System.Exception("Role is not change!"); }

            return role;
        }
    }
}
