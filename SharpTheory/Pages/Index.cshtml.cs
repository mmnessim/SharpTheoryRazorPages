using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using System.Text;
using System.Text.Json;

namespace SharpTheory.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public DateTime CurrentTime { get; set; }
        public TheoryDescription? Description { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            Description = root?.Description;

            CurrentTime = DateTime.Now;

            _logger.LogInformation("Index page loaded at {Time}", CurrentTime);
            _ = SendAnalytics("IndexPageView");
        }

        public async Task SendAnalytics(string eventType, object payload = null, string? userId = null)
        {
            var analyticsURL = "https://localhost:7109/api/analytics/track";
            try
            {
                var client = _httpClientFactory.CreateClient();

                var analyticsEvent = new
                {
                    ID = 0,
                    AppName = "SharpTheory", 
                    EventType = eventType,
                    UserId = userId,
                    Payload = JsonSerializer.Serialize(payload),
                    Timestamp = DateTime.UtcNow
                };

                var json = JsonSerializer.Serialize(analyticsEvent);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(analyticsURL, content);
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
