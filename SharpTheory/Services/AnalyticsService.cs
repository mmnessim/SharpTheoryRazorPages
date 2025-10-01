using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharpTheory.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        private const string AnalyticsUrl = "http://192.168.68.4:8080/api/analytics/track";

        public AnalyticsService(IHttpClientFactory httpClientFactory, ILogger<AnalyticsService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task SendEventAsync(string eventType, object? payload = null, string? userId = null)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var analyticsEvent = new
                {
                    ID = 0,
                    AppName = "SharpTheory",
                    EventType = eventType,
                    UserId = userId,
                    Payload = payload != null ? JsonSerializer.Serialize(payload) : null,
                    Timestamp = DateTime.UtcNow
                };

                var json = JsonSerializer.Serialize(analyticsEvent);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(AnalyticsUrl, content);
                response.EnsureSuccessStatusCode();

                _logger.LogInformation("Analytics event sent: {EventType}", eventType);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to send analytics event: {EventType}", eventType);
            }
        }
    }
}
