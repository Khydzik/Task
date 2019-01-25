using LearningProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace LearningProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EditController : ControllerBase
    {
       
        ApplicationContext db;
         
        public EditController(ApplicationContext context )
        {
            
            this.db = context;
        }

        private User GetUser(EditModel model)
        {
            User user = db.Users.FirstOrDefault(r =>r.Id == model.Id);

            return user;
        }

        [HttpPatch]
        public async Task Edit([FromBody] EditModel model)
        {
            User user = GetUser(model);
            Role newRole = GetRole(model);

            if (user != null)
            {
                user.Role = newRole;
                db.SaveChanges();

                Response.StatusCode = 200;
                await Response.WriteAsync(JsonConvert.SerializeObject(new { name = newRole.Name }));                    
            }   
            else
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Not exist user");
                return;
            }
        }

        private Role GetRole(EditModel model)
        {
            return db.Roles.SingleOrDefault(r => r.Name == model.Role.Name);
        }
    }
}
