using LearningProject.Data;
using LearningProject.Web.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LearningProject.Web.Extensions.CorsExtensions;
using LearningProject.Web.Extensions.SwaggerExtensions;
using LearningProject.Web.Extensions.RegisterExtensions;
using LearningProject.Web.Extensions.LocalizationExtensions;
using LearningProject.Web.Extensions.ValidationExtensions;
using LearningProject.Web.Extensions.ExceptionHandleExtensions;
using LearningProject.Web.Extensions.AuthenticationExtensions;

namespace LearningProject
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(
                _configuration.GetConnectionString("DefaultConnection"),
                apt => apt.MigrationsAssembly(typeof(DataBaseContext).Namespace)));
            services.AddMvc(options => {
                options.OutputFormatters.RemoveType(typeof(JsonOutputFormatter));
                options.OutputFormatters.Add(new CustomJsonOutputFormatter());
            }).AddDataAnnotationsLocalization().AddViewLocalization();   
            services.AddLocalizationService();
            services.AddCorsService();
            services.AddValidation();
            services.AddSwaggerService();
            services.AddRegisterService();
            services.AddAuthenticationService();         
        }    

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.AddExceptionHandler();                                   
            app.UseAuthentication();
            app.AddLocalizationConfig();
            app.AddCorsConfig();
            app.AddSwaggerConfig();
            app.UseMvc();
        }
    }   
}
