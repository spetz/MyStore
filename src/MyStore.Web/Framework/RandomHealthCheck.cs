using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MyStore.Web.Framework
{
    public class RandomHealthCheck : IHealthCheck
    {
        private readonly Random _random = new Random();
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await Task.CompletedTask;

            return _random.Next(0, 2) == 0 ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
        }
    }
}