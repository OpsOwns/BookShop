using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Auth.Infrastructure.Context;
using Shop.Auth.Infrastructure.User.Model;
using Shop.Shared.API;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Auth.API.Services
{
    public class DbInitializerService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public DbInitializerService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var result = _serviceProvider.GetRequiredService<IConfiguration>();
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IdentityAccountContext>();
            if (!await context.Database.EnsureCreatedAsync(cancellationToken))
                return;
            await InitializeDbData(result, scope);
        }
        private async Task InitializeDbData(IConfiguration result, IServiceScope scope)
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ShopRole>>();
            var adminManager = scope.ServiceProvider.GetRequiredService<UserManager<ShopUser>>();
            var startupRoles = result.GetOptions<List<ShopRole>>("StartupRole");
            var startupUser = result.GetOptions<ShopUser>("StartupUser");
            foreach (var role in startupRoles)
                await roleManager.CreateAsync(role);
            await adminManager.CreateAsync(startupUser, result.GetValue<string>("StartupUser:Password"));
            await adminManager.AddToRoleAsync(await adminManager.FindByNameAsync(startupUser.UserName),
                startupRoles.Find(x => x.Name == "Admin")?.Name);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
