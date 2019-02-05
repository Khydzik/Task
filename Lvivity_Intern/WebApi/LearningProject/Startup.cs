using LearningProject.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Threading.Tasks;
using System.Buffers;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace LearningProject
{
    public class Startup
    {
        public const int ValidationError = 1;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {           
            services.AddLocalization(o => o.ResourcesPath = "Resources");
            services.AddCors();
            services.AddMvc(options => {
                options.OutputFormatters.RemoveType(typeof(JsonOutputFormatter));
             
                options.OutputFormatters.Add(new CustomJsonOutputFormatter());
                  }).AddDataAnnotationsLocalization().AddViewLocalization();
            services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
            options.InvalidModelStateResponseFactory = ctx => new ValidationResult());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My Api", Version = "v1" });
            }
            );
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });
        }    

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
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
                                Id = ValidationError,
                                Message = ex.Error.Message
                            }
                        };
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                    }
                });                
            });

            var supportedCultures = new[]
            {
                new CultureInfo("de"),
                new CultureInfo("ru"),
                new CultureInfo("uk"),
                new CultureInfo("en")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });          
             
            app.UseAuthentication();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api V1");
            });

            app.UseMvc();
        }
    }   

}
