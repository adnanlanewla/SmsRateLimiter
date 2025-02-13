using Microsoft.AspNetCore.Mvc;
using SmsRateLimiterBackend.Services;

namespace SmsRateLimiterBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonitoringController : ControllerBase
    {
        private readonly RateLimitingService _rateLimitingService;

        public MonitoringController(RateLimitingService rateLimitingService)
        {
            _rateLimitingService = rateLimitingService;
        }

        [HttpGet("account/{accountId}")]
        public IActionResult GetAccountMetrics(string accountId)
        {
            var metrics = _rateLimitingService.GetAccountMetrics(accountId);
            return Ok(new { MessagesSent = metrics });
        }

        [HttpGet("number/{phoneNumber}")]
        public IActionResult GetNumberMetrics(string phoneNumber)
        {
            var metrics = _rateLimitingService.GetNumberMetrics(phoneNumber);
            return Ok(new { MessagesSent = metrics });
        }
    }
}