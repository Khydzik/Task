using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LearningProject.Web.Models
{
    public class ValidationResult : IActionResult
    {
        public const int ValidationError = 2;
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var modelState = context.ModelState;
            var errors = new List<string>();
            if (!modelState.IsValid)
            {               
                foreach (var state in modelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }                
            }

            context.HttpContext.Response.StatusCode = 400;
            var responce = new Responce<object>
            {
                Result = null,
                Error = new Error
                {   Id = ValidationError,
                    Message = errors.FirstOrDefault()
                }
            };
            
            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(responce,new JsonSerializerSettings { Formatting  = Formatting.Indented }));
        }
    }
}
