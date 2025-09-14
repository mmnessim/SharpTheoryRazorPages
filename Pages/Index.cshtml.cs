using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using System.Text.Json;

namespace SharpTheory.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public TheoryDescription? Description { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            using var client = new HttpClient { BaseAddress = new Uri("https://localhost:8081/") };
            var response = await client.GetAsync("api/description");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Description = JsonSerializer.Deserialize<TheoryDescription>(json);
            }
        }
    }
}
