using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using SharpTheory.Services;
using System.Text;
using System.Text.Json;

namespace SharpTheory.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAnalyticsService _analyticsService;
        public DateTime CurrentTime { get; set; }
        public TheoryDescription? Description { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory, IAnalyticsService analyticsService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _analyticsService = analyticsService;
        }

        public void OnGet()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            Description = root?.Description;

            CurrentTime = DateTime.Now;

            _logger.LogInformation("Index page loaded at {Time}", CurrentTime);
            _ = _analyticsService.SendEventAsync("IndexPageView");
        }

    }
}
