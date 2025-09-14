using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using System.Text.Json;

namespace SharpTheory.Pages
{
    public class KeyDetailsModel : PageModel
    {
        public TheoryKey? Key { get; set; }
        public TheoryScaleType? Scale { get; set; }
        public void OnGet(string id)
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            var parts = id.Split("-");
            var tonic = parts[0];
            var mode = parts[1];
            Key = root?.Keys.FirstOrDefault(k => k.Name.ToLower().Contains(tonic) && k.Mode.Contains(mode));

            var scales = root?.Scales.FirstOrDefault(s => s.Integer == Key?.Integer);
            if (mode == "major")
            {
                Scale = scales?.Major;
            } else
            {
                Scale = scales?.NaturalMinor;
            }
        }
    }
}
