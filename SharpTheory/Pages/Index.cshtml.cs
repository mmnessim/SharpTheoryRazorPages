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
        private readonly IAnalyticsService _analyticsService;
        private readonly TheoryDataService _dataService;
        public DateTime CurrentTime { get; set; }
        public TheoryDescription? Description { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IAnalyticsService analyticsService, TheoryDataService dataService)
        {
            _logger = logger;
            _analyticsService = analyticsService;
            _dataService = dataService;
        }

        public void OnGet()
        {
            var root = _dataService.Root;
            Description = root?.Description;

            CurrentTime = DateTime.Now;

            _logger.LogInformation("Index page loaded at {Time}", CurrentTime);
            _ = _analyticsService.SendEventAsync("IndexPageView");
        }

    }
}
