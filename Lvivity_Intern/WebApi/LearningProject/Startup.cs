using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LearningProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
         
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors(buidler => buidler.WithOrigins("*").AllowAnyHeader());

            app.UseMvc();
        }
    }
}
