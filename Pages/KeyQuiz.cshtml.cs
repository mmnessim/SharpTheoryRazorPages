using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using System.Text.Json;

namespace SharpTheory.Pages
{
    public class KeyQuizModel : PageModel
    {
        public TheoryKey? Key { get; set; }
        public int? NumSharps { get; set; }
        public int? NumFlats { get; set; }

        [BindProperty]
        public string? SelectedKeyName { get; set; }

        [BindProperty]
        public int? UserSharps { get; set; }
        [BindProperty]
        public int? UserFlats { get; set; }
        public string? ResultMessage { get; set; }

        public void OnGet()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);

            var random = new Random();
            Key = root?.Keys[random.Next(root.Keys.Count)];
            NumSharps = Key?.NumSharps;
            NumFlats = Key?.NumFlats;
            SelectedKeyName = Key?.Name;
        }

        public void OnPost()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);

            if (root != null && !string.IsNullOrEmpty(SelectedKeyName))
            {
                Key = root.Keys.FirstOrDefault(k => k.Name == SelectedKeyName);
                NumSharps = Key?.NumSharps ?? 0;
                NumFlats = Key?.NumFlats ?? 0;
            }

            if (UserSharps.HasValue && Key != null && UserFlats.HasValue)
            {
                ResultMessage = UserSharps == NumSharps && UserFlats == NumFlats
                    ? $"Correct! {NumSharps} Sharps and {NumFlats} Flats"
                    : $"Incorrect. The correct answer is {NumSharps} sharps and {NumFlats} flats.";
            }
        }

        public IActionResult OnPostNewQuestion()
        {
            return RedirectToPage();
        }
    }
}
