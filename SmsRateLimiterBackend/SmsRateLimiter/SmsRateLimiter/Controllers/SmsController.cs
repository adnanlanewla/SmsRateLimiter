using Microsoft.AspNetCore.Mvc;
using SmsRateLimiterBackend.Services;
using System.Threading.Tasks;

namespace SmsRateLimiterBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly RateLimitingService _rateLimitingService;

        public SmsController(RateLimitingService rateLimitingService)
        {
            _rateLimitingService = rateLimitingService;
        }

        [HttpPost("send/{accountId}/{phoneNumber}")]
        public async Task<IActionResult> SendSms(string accountId, string phoneNumber, [FromBody] SmsRequest request)
        {
            if (await _rateLimitingService.CanSendSmsAsync(accountId, phoneNumber))
            {
                // Simulate sending an SMS
                return Ok(new { Message = $"SMS sent to {phoneNumber}: {request.Message}" });
            }
            else
            {
                return StatusCode(429, "Rate limit exceeded. Please try again later.");
            }
        }
    }

    public class SmsRequest
    {
        public string Message { get; set; }
    }
}