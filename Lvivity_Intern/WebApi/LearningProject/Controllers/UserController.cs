using LearningProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace LearningProject.Controllers
{
    [Authorize(Roles = "admin")]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class UserController : Controller
    {
        public const int DefaultPageRecordCount = 3;
        public const int DefaultPages = 1;
        private ApplicationContext _context;

        public UserController(ApplicationContext context)
        {

            _context = context;
        }

        private List<User> GetUsersItem(PaginationModel paginationModel)
        {
            var takePage = paginationModel.CurrentPage == 0 ? DefaultPages : paginationModel.CurrentPage;
            var takeCount = paginationModel.PerPage == 0 ? DefaultPageRecordCount : paginationModel.PerPage;

            var users = new List<User>();
            users = _context.Users.Include(x => x.Role.Name).Select(user => new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role
            })
                .Skip((takePage - 1) * takeCount)
                .Take(takeCount)
                .ToList();           

            return users;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<object> Users([Microsoft.AspNetCore.Mvc.FromBody] PaginationModel paginationModel )
        {
            var users = GetUsersItem(paginationModel);
            var response = new
            {
                user = users
            };
            return response;
         }
    }
}
