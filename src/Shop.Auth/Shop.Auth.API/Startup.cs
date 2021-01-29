using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Shop.Auth.API.Issues;
using Shop.Auth.API.Services;
using Shop.Shared.API;
using System.Text;
using Shop.Auth.Application.Context;
using Shop.Auth.Application.Domain;
using Shop.Auth.Application.Security.Jwt;
using Shop.Auth.Application.Security.Jwt.Interfaces;
using Shop.Auth.Application.Security.Model;
using Shop.Auth.Application.User.Command;

namespace Shop.Auth.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        private IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebApi<UserRegisterCommand>();
            services.AddSwagger<Startup>(Configuration);
            services.AddRepository<Startup>();
            services.AddEfCore<IdentityAccountContext>(Configuration, "Identity");
            services.AddIdentity<ShopUser, ShopRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 1;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<IdentityAccountContext>()
                .AddTokenProvider(TokenProviderNames.LoginProvider, typeof(DataProtectorTokenProvider<ShopUser>));
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var authSettings = Configuration.GetOptions<AuthSettings>("AuthSettings");
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.SecretKey)), SecurityAlgorithms.HmacSha256);
            });
            services.AddSingleton<IJwtTokenValidator, JwtTokenValidator>();
            services.AddSingleton<IJwtTokenHandler, JwtTokenHandler>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddSingleton<ITokenFactory, TokenFactory>();
            services.AddSingleton(authSettings);
            services.AddHostedService<DbInitializerService>();
            services.AddProblemDetails(x => x.Map<AuthException>(ex => new AuthProblemDetails(ex)));
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
