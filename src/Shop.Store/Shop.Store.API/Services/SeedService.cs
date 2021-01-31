using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Store.Infrastructure.Db;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Store.API.Services
{
    public class SeedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public SeedService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _serviceProvider.GetRequiredService<IConfiguration>();
            using var scope = _serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<BookContext>().Database.EnsureCreatedAsync(cancellationToken);
        }
        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
