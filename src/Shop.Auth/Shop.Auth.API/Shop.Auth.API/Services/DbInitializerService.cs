using System;
using Microsoft.Extensions.Hosting;
using Shop.Auth.Infrastructure.Context;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Auth.API.Services
{
    public class DbInitializerService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public DbInitializerService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _serviceProvider.GetRequiredService<IConfiguration>();
            using var scope = _serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<IdentityAccountContext>().Database.EnsureCreatedAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
