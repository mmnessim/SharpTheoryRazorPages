using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using System.Text.Json;

namespace SharpTheory.Pages
{
    public class KeyReferenceModel : PageModel
    {
        public TheoryDescription? Description { get; set; }
        public List<TheoryKey>? Keys { get; set; }
        public void OnGet()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            Description = root?.Description;
            Keys = root?.Keys;
        }
    }
}
