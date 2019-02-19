using LearningProject.Application;
using LearningProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearningProject.Web.Controllers
{
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
        public async Task<Role> ChangeRole([FromBody] EditModel model)
        {
            return await _userService.ChangeUserRole(model.userId, model.roleId);
        }
    }
}
