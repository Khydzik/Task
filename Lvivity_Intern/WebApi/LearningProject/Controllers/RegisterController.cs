using LearningProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Web.Http;

namespace LearningProject.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        ApplicationContext _context;

        public RegisterController(ApplicationContext context)
        {
            _context = context;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task Registration([Microsoft.AspNetCore.Mvc.FromBody]Register registerUser)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == registerUser.UserName);
            Role role = await _context.Roles.FirstOrDefaultAsync(t => t.Name == "user");
            if (role != null)
            {
                if (user == null)
                {
                    user = new User
                    {
                        UserName = registerUser.UserName,
                        Password = registerUser.Password
                    };

                    user.Role = role;

                    _context.Users.Add(user);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    Response.StatusCode = 400;
                    await Response.WriteAsync("Such user exist");
                }
            }         
        }
    }
}
