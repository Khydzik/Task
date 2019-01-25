using LearningProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace LearningProject.Controllers
{
    [Authorize(Roles = "admin")]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class UserController:Controller
    {
        private ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            _context = context;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task GetUsers()
        {
            var users = _context.Users.Include(u=>u.Role).Select(user => new { id = user.Id, userName = user.UserName, role = user.Role.Name });             
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(users, new JsonSerializerSettings { Formatting = Formatting.Indented }));

        }
    }
}
