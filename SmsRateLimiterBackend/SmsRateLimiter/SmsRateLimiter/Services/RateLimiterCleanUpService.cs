using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmsRateLimiterService.Services;
using System.Threading;
using System.Threading.Tasks;
using SmsRateLimiterBackend.Services;

namespace SmsRateLimiterService.Services
{
    public class RateLimiterCleanupService : BackgroundService
    {
        private readonly RateLimitingService _rateLimitingService;

        public RateLimiterCleanupService(RateLimitingService rateLimitingService)
        {
            _rateLimitingService = rateLimitingService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _rateLimitingService.Cleanup();
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Clean up every 5 minutes
            }
        }
    }
}