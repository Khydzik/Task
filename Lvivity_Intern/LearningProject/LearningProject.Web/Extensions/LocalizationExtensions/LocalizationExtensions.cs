using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace LearningProject.Web.Extensions.LocalizationExtensions
{
    public static class LocalizationExtensions
    {
        public static void AddLocalizationConfig(this IApplicationBuilder app)
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
        }

        public static void AddLocalizationService(this IServiceCollection services)
        {
            services.AddLocalization(o => o.ResourcesPath = "Localization");
        }
    }
}
