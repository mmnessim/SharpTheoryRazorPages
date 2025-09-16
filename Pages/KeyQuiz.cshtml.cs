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

        [BindProperty]
        public string? SelectedKeyName { get; set; }

        [BindProperty]
        public int? UserAnswer { get; set; }
        public string? ResultMessage { get; set; }

        public void OnGet()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);

            var random = new Random();
            Key = root?.Keys[random.Next(root.Keys.Count)];
            NumSharps = Key?.NumSharps;
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
            }

            if (UserAnswer.HasValue && Key != null)
            {
                ResultMessage = UserAnswer == NumSharps
                    ? "Correct!"
                    : $"Incorrect. The correct answer is {NumSharps}.";
            }
        }
    }
}
