using LearningProject.Application;
using LearningProject.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LearningProject.Web.Extensions.ExceptionHandleExtensions
{
    public static class ExceptionHandle
    {
        public static void AddExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var response = new Responce<object>()
                        {
                            Result = null,
                            Error = new Error()
                            {
                                Id = ErrorType.Validation,
                                Message = ex.Error.Message
                            }
                        };
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                    }
                });
            });
        }
    }
}
