using System.Collections.Concurrent;
using System.Threading.RateLimiting;

namespace SmsRateLimiterBackend.Services
{
    public class RateLimitingService
    {
        private readonly RateLimiter _perNumberRateLimiter;
        private readonly RateLimiter _perAccountRateLimiter;
        private readonly ConcurrentDictionary<string, int> _accountMetrics = new();
        private readonly ConcurrentDictionary<string, int> _numberMetrics = new();

        public RateLimitingService()
        {
            // Configure per-number rate limiter
            _perNumberRateLimiter = new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5, // Max 5 messages per second per phone number
                Window = TimeSpan.FromSeconds(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0 // No queuing
            });

            // Configure per-account rate limiter
            _perAccountRateLimiter = new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10, // Max 10 messages per second per account
                Window = TimeSpan.FromSeconds(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0 // No queuing
            });
        }

        public async Task<bool> CanSendSmsAsync(string accountId, string phoneNumber)
        {
            // Check per-number rate limit
            var perNumberLease = await _perNumberRateLimiter.AcquireAsync(1);
            if (!perNumberLease.IsAcquired)
            {
                return false;
            }

            // Check per-account rate limit
            var perAccountLease = await _perAccountRateLimiter.AcquireAsync(1);
            if (!perAccountLease.IsAcquired)
            {
                return false;
            }

            // Track metrics
            _accountMetrics.AddOrUpdate(accountId, 1, (_, count) => count + 1);
            _numberMetrics.AddOrUpdate(phoneNumber, 1, (_, count) => count + 1);

            return true;
        }

        public int GetAccountMetrics(string accountId)
        {
            return _accountMetrics.GetOrAdd(accountId, 0);
        }

        public int GetNumberMetrics(string phoneNumber)
        {
            return _numberMetrics.GetOrAdd(phoneNumber, 0);
        }

        public void Cleanup()
        {
            // Clean up unused rate limiter partitions
            _perNumberRateLimiter.Dispose();
            _perAccountRateLimiter.Dispose();
        }
    }
}