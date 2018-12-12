using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyStore.Web.Framework
{
    public class UsersProcessorHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<UsersProcessorHostedService> _logger;

        public UsersProcessorHostedService(ILogger<UsersProcessorHostedService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Working...");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var client = scope.ServiceProvider.GetService<IReqResClient>();
                    var users = await client.GetUsersAsync();
                    _logger.LogInformation("Processing users: " +
                                           $"{string.Join(", ", users.Select(u => u.FirstName))}");
                }
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}