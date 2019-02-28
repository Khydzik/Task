using LearningProject.Application;
using LearningProject.Data;
using LearningProject.Data.Models;
using LearningProject.Web.Token;
using Microsoft.Extensions.DependencyInjection;

namespace LearningProject.Web.Extensions.RegisterExtensions
{
    public static class RegisterExtensions
    {
        public static void AddRegisterService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ITokenFormation, TokenFormation>();
            //services.AddScoped<IRepository<User>, UserRepository>();
            //services.AddScoped<IRepository<Role>, RoleRepository>();
            //services.AddScoped<IRepository<Post>, PostRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
