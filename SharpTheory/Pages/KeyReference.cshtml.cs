using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using System.Text.Json;

namespace SharpTheory.Pages
{
    /// <summary>
    /// PageModel for the Key Reference page, loads and displays key signatures and descriptions.
    /// </summary>
    public class KeyReferenceModel : PageModel
    {
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
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            Description = root?.Description;
            Keys = root?.Keys;
        }
    }
}
