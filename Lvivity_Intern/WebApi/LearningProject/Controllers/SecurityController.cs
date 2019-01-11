using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using LearningProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningProject.Controllers
{
   // [EnableCors(origins: "*",headers:"*",methods:"*")]
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {        
        [HttpPost]
        public IActionResult Authorization([FromBody] UserLogin userLogin)
        {
            if(userLogin.UserName == "admin" && userLogin.Password == "admin")
                return StatusCode(200);
            else
            {
                return StatusCode(401);
            }
        }
    }
}