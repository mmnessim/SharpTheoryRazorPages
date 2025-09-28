using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using SharpTheory.Services;
using System.Text.Json;

namespace SharpTheory.Pages
{
    public class ScaleQuizModel : PageModel
    {
        private readonly ILogger<ScaleQuizModel> _logger;
        private readonly IAnalyticsService _analyticsService;
        public List<TheoryInteger>? Integers { get; set; }
        public List<int> RawInts { get; set; } = [];
        public TheoryScale? Scale { get; set; }
        public bool ShowNotes { get; set; } = true;
        [BindProperty]
        public int ScaleInt { get; set; } = 0;

        public ScaleQuizModel(ILogger<ScaleQuizModel> logger, IAnalyticsService analyticsService)
        {
            _logger = logger;
            _analyticsService = analyticsService;
        }
        public void OnGet()
        {
            _logger.LogInformation("ScaleQuiz OnGet called.");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            if (root == null)
            {
                return;
            }
            Integers = root.Integers.ToList();

            var majorScales = root.Scales.Where(s => s.Major != null).ToList();
            Scale = majorScales[0];
            if (Scale != null && Scale.Major != null)
            {
                foreach (var i in Scale.Major.Integers)
                {
                    RawInts.Add(i);
                }
            }
            _logger.LogInformation("ScaleQuiz page loaded at {Time}", DateTime.Now);
            _analyticsService.SendEventAsync("PageView");
        }

        public void OnPost()
        {
            _logger.LogInformation("ScaleQuiz OnPost called.");
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            if (root == null)
            {
                return;
            }
            Integers = root.Integers.ToList();

            var majorScales = root.Scales.Where(s => s.Major != null).ToList();
            _logger.LogInformation($"ScaleInt = {ScaleInt}");
            Scale = majorScales.FirstOrDefault(s => s.Integer.Equals(ScaleInt));

            if (Scale != null && Scale.Major != null)
            {
                foreach (var i in Scale.Major.Integers)
                {
                    RawInts.Add(i);
                }
            }
            _logger.LogInformation("ScaleQuiz page loaded at {Time}", DateTime.Now);
            _analyticsService.SendEventAsync("PageView");
        }
    }
}
