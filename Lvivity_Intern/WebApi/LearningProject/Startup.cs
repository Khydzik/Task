using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using LearningProject.Web.Models;
using LearningProject.Web;
using LearningProject.Web.Extensions;
using LearningProject.Data;
using Microsoft.EntityFrameworkCore;

namespace LearningProject
{
    public class Startup
    {
        IConfiguration Configuration { get; }
        public const int ValidationError = 1;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            string assemblyName = typeof(DataBaseContext).Namespace;
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connection, apt =>
            apt.MigrationsAssembly(assemblyName)));
            services.AddMvc(options => {
                options.OutputFormatters.RemoveType(typeof(JsonOutputFormatter));
                options.OutputFormatters.Add(new CustomJsonOutputFormatter());
            }).AddDataAnnotationsLocalization().AddViewLocalization();
            services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
            options.InvalidModelStateResponseFactory = ctx => new ValidationResult());
            services.AddLocalizationAndCorsService();
            services.SwaggerService();
            services.RegisteService();
            services.AuthenticationService();            
        }    

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.ExceptionHandlerConfig(ValidationError);            
                         
            app.UseAuthentication();
            app.LocalizationAndCorsConfig();
            app.SwaggerConfig();
            app.UseMvc();
        }
    }   
}
