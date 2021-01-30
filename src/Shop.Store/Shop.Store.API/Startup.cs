using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Book.API.SeedWork;
using Shop.Shared.API;

namespace Shop.Book.API
{
    public class Startup
    {
        private readonly StartupConfig _config;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _config = new StartupConfig(Configuration);
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebApi<Startup>();
            services.AddSwagger<Startup>(Configuration, true);
            services.AddJwt(_config.AuthSettings);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop.Book.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}