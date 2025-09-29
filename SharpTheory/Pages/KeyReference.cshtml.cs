using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using System.Text.Json;
using SharpTheory.Services;

namespace SharpTheory.Pages
{
    /// <summary>
    /// PageModel for the Key Reference page, loads and displays key signatures and descriptions.
    /// </summary>
    public class KeyReferenceModel : PageModel
    {
        private readonly ILogger<KeyReferenceModel> _logger;
        private readonly IAnalyticsService _analyticsService;
        private readonly TheoryDataService _dataService;
        public KeyReferenceModel(ILogger<KeyReferenceModel> logger, IAnalyticsService analyticsService, TheoryDataService dataService)
        {
            _logger = logger;
            _analyticsService = analyticsService;
            _dataService = dataService;
        }
        /// <summary>
        /// Description of API
        /// </summary>
        public TheoryDescription? Description { get; set; }

        /// <summary>
        /// List of all keys from data.json
        /// </summary>
        public List<TheoryKey>? Keys { get; set; }

        /// <summary>
        /// Handles GET requests to load key data from JSON file.
        /// </summary>
        public void OnGet()
        {
            _logger.LogInformation("KeyReference page loaded");
            _ = _analyticsService.SendEventAsync("PageView");
            var root = _dataService.Root;
            Description = root?.Description;
            Keys = root?.Keys;
        }
    }
}
