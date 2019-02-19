using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using LearningProject.Web;
using LearningProject.Web.Extensions;
using LearningProject.Data;
using Microsoft.EntityFrameworkCore;
using LearningProject.Web.Formatters;

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
            services.AddLocalizationAndCorsService();
            services.AddValidaitonService();
            services.AddSwaggerService();
            services.AddRegisteService();
            services.AddAuthenticationService();            
        }    

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.ExceptionHandlerConfig();                                   
            app.UseAuthentication();
            app.LocalizationAndCorsConfig();
            app.SwaggerConfig();
            app.UseMvc();
        }
    }   
}
