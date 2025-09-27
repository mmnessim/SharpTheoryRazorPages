using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using SharpTheory.Services;
using System.Text.Json;

namespace SharpTheory.Pages
{
    public class KeyDetailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAnalyticsService _analyticsService;
        public KeyDetailsModel(ILogger<IndexModel> logger, IAnalyticsService analyticsService)
        {
            _logger = logger;
            _analyticsService = analyticsService;
        }

        public TheoryKey? Key { get; set; }
        public TheoryScaleType? Scale { get; set; }
        public TheoryScaleType? NatMinor { get; set; }
        public void OnGet(string id)
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            var noteName = id.Split("-")[0];
            var mode = id.Split("-")[1];
            id = id.Replace("-", " ");
            Key = root?.Keys.FirstOrDefault(k => k.Name.ToLower() == id && k.Mode.Contains(mode));

            var scales = root?.Scales.FirstOrDefault(s => s.Integer == Key?.Integer && s.Tonic.ToLower().Contains(noteName));
            if (mode == "major")
            {
                Scale = scales?.Major;
            }
            else
            {
                Scale = scales?.HarmonicMinor;
                NatMinor = scales?.NaturalMinor;
            }
            var now = DateTime.Now;
            _logger.LogInformation("KeyDetails page loaded at {Time}", now);
            _ = _analyticsService.SendEventAsync("KeyDetailsPageView");
        }
    }
}
