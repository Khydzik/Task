using LearningProject.Application;
using LearningProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearningProject.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _objUserService;

        public RegisterController(IUserService userService)
        {
            _objUserService = userService;
        }

        [HttpPost]
        public async Task<bool> Registration([FromBody]Register registerUser)
        {
            var register = await _objUserService.IsRegistration(registerUser);
            return register;
        }
    }
}
