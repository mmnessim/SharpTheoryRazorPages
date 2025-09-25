using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using System.Text.Json;

namespace SharpTheory.Pages
{
    public class ScaleQuizModel : PageModel
    {
        private readonly ILogger<ScaleQuizModel> _logger;
        public List<TheoryInteger>? Integers { get; set; }
        public List<int> RawInts { get; set; } = [];
        public TheoryScale? Scale { get; set; }

        public ScaleQuizModel(ILogger<ScaleQuizModel> logger)
        {
            _logger = logger;
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

            var random = new Random();
            var majorScales = root.Scales.Where(s => s.Major != null).ToList();
            Scale = majorScales[random.Next(majorScales.Count)];
            if (Scale != null && Scale.Major != null)
            {
                foreach (var i in Scale.Major.Integers)
                {
                    RawInts.Add(i);
                }
            }  
        }
    }
}
