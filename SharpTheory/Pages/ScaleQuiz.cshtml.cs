using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using System.Text.Json;

namespace SharpTheory.Pages
{
    public class ScaleQuizModel : PageModel
    {
        public List<TheoryInteger>? Integers { get; set; }
        public TheoryScale? Scale { get; set; }
        public void OnGet()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            if (root == null)
            {
                return;
            }
            Integers = root.Integers.ToList();

            var random = new Random();
            Scale = root.Scales[random.Next(root.Scales.Count)];
        }
    }
}
