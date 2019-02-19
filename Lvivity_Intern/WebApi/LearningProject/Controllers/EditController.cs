using LearningProject.Application;
using LearningProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearningProject.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EditController : ControllerBase
    {
        private readonly IUserService _objUserService;

        public EditController(IUserService userService)
        {
            _objUserService = userService;
        }

        [HttpPatch]
        public async Task<string> Edit([FromBody] EditModel model)
        {
            var user = await _objUserService.EditUser(model);
            return user;
        }
    }
}
