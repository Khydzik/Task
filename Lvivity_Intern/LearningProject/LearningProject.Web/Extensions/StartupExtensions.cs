using LearningProject.Application;
using LearningProject.Data;
using LearningProject.Web.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Globalization;

namespace LearningProject.Web.Extensions
{
    public static class StartupExtensions
    {
        public static void LocalizationAndCorsConfig(this IApplicationBuilder app)
        {
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

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

        }

        public static void SwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api V1");
            });
        }

        public static void ExceptionHandlerConfig(this IApplicationBuilder app)
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

        public static void AddAuthenticationService(this IServiceCollection services)
        {
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

        public static void AddRegisteService(this IServiceCollection services)
        {
            services.AddScoped<IUserDataService, UserDataStore>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostDataStore, PostDataStore>();
            services.AddScoped<IPostService, PostService>();
        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My Api", Version = "v1" });
            });
        }

        public static void AddValidaitonService(this IServiceCollection services)
        {
            services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = ctx => new ValidationResult());
        }

        

        public static void AddLocalizationAndCorsService(this IServiceCollection services)
        {
            services.AddLocalization(o => o.ResourcesPath = "Localization");
            services.AddCors();
        }
    }
}
