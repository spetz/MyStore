using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyStore.Web.Models;

namespace MyStore.Web.Framework
{
    public class UsersProcessorHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<UsersProcessorHostedService> _logger;

        public UsersProcessorHostedService(ILogger<UsersProcessorHostedService> logger,
            IServiceProvider serviceProvider, IMemoryCache memoryCache)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _memoryCache = memoryCache;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Working...");
                using (var scope = _serviceProvider.CreateScope())
                {
//                    var users = _memoryCache.Get<IEnumerable<UserData>>("users");
                    var client = scope.ServiceProvider.GetService<IReqResClient>();
                    var users = await client.GetUsersAsync();
//                    _memoryCache.Set("users", users);
                    _logger.LogInformation("Processing users: " +
                                           $"{string.Join(", ", users.Select(u => u.FirstName))}");
                }
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}