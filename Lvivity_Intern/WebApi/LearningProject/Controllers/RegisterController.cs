using LearningProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LearningProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        public const int ValidationError = 1;
        ApplicationContext _context;

        public RegisterController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<bool> Registration([FromBody]Register registerUser)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == registerUser.UserName);

            Role role = await _context.Roles.FirstOrDefaultAsync(t => t.Name == "user");

            if (role == null)
            {
                throw new System.Exception("Such role does not exist");
            }

            if (user != null)
            {
                throw  new System.Exception("Such user exist");
            }

            user = new User
            {
                UserName = registerUser.UserName,
                Password = registerUser.Password
            };

            user.Role = role;

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
