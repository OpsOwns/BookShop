using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Shared.API;
using Shop.Store.API.SeedWork;
using Shop.Store.API.Services;
using Shop.Store.Application.Command.Book;
using Shop.Store.Infrastructure.Db;

namespace Shop.Store.API
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
            services.AddWebApi<BookContext>();
            services.AddDomainEvents();
            services.AddSwagger<Startup>(Configuration, true);
            services.AddEfCore<BookContext>(Configuration, "StoreDB").AddHostedService<SeedService>();
            services.AddJwt(_config.AuthSettings);
            services.AddMediatR(typeof(CreateBookCommand).Assembly);
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