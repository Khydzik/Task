using Microsoft.Extensions.DependencyInjection;
using LearningProject.Web.Models;

namespace LearningProject.Web.Extensions.ValidationExtensions
{
    public static class ValidationExtensions
    {
        public static void AddValidation(this IServiceCollection services)
        {
            services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = ctx => new ValidationResult());
        }
    }
}
