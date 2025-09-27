using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Services;

namespace SharpTheory.Pages.Partials
{
    public class _KeyListPartialModel : PageModel
    {
        private readonly ILogger<_KeyListPartialModel> _logger;
        private readonly IAnalyticsService _analyticsService;
        public _KeyListPartialModel(ILogger<_KeyListPartialModel> logger, IAnalyticsService analyticsService)
        {
            _logger = logger;
            _analyticsService = analyticsService;
        }

        public void OnGet()
        {
            var CurrentTime = DateTime.Now;

            _logger.LogInformation("Index page loaded at {Time}", CurrentTime);
            _ = _analyticsService.SendEventAsync("PageView");
        }
    }
}
