using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Auth.Infrastructure.User.Command;
using Shop.Shared.API;

namespace Shop.Auth.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebApi<UserRegisterCommand>();
            services.AddSwagger<Startup>(Configuration);
            services.AddRepository<Startup>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop.Auth.API v1"));
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseProblemDetails();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
