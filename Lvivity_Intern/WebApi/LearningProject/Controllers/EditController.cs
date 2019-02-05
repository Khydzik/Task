using LearningProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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
        public async Task<object> Edit([FromBody] EditModel model)
        {
            User user = GetUser(model);
            Role newRole = GetRole(model);

            if (user != null)
            {
                user.Role = newRole;
                db.SaveChanges();

              var response = new
                {
                    name = user.Role.Name
                };

                return response;
            }   
            else
            { 
                throw new Exception("Not exist user");
            }            
        }

        private Role GetRole(EditModel model)
        {
            return db.Roles.FirstOrDefault(r => r.Name == model.Role.Name);
        }
    }
}
