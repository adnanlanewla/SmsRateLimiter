using System.Threading.RateLimiting;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using SmsRateLimiterBackend.Services;
using Xunit;

namespace SmsRateLimiterService.Tests
{
    public class RateLimitingServiceTests
    {
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly RateLimitingService _rateLimitingService;

        public RateLimitingServiceTests()
        {
            _mockMemoryCache = new Mock<IMemoryCache>();
            _rateLimitingService = new RateLimitingService();
        }

        [Fact]
        public async Task CanSendSmsAsync_WithinLimits_ReturnsTrue()
        {
            // Arrange
            var phoneNumber = "1234567890";
            var account = "test1";

            // Act
            var result = await _rateLimitingService.CanSendSmsAsync(phoneNumber, account);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CanSendSmsAsync_ExceedsPerNumberLimit_ReturnsFalse()
        {
            // Arrange
            var phoneNumber = "1234567890";
            var account = "test1";

            // Exceed the per-number limit (5 requests per second)
            for (int i = 0; i < 5; i++)
            {
                await _rateLimitingService.CanSendSmsAsync(phoneNumber, account);
            }

            // Act
            var result = await _rateLimitingService.CanSendSmsAsync(phoneNumber, account);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CanSendSmsAsync_ExceedsAccountLimit_ReturnsFalse()
        {
            // Arrange
            var phoneNumber1 = "1234567890";
            var phoneNumber2 = "0987654321";
            var account1 = "test1";
            var account2 = "test2";

            // Exceed the account-wide limit (10 requests per second)
            for (int i = 0; i < 10; i++)
            {
                await _rateLimitingService.CanSendSmsAsync(phoneNumber1, account1);
            }

            // Act
            var result = await _rateLimitingService.CanSendSmsAsync(phoneNumber2, account2);

            // Assert
            Assert.False(result);
        }

    }
}