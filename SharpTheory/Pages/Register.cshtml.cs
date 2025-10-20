using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SharpTheory.Pages;

public class Register : PageModel
{
    private readonly ILogger<Register> _logger;
    private readonly IConfiguration _config;
    private readonly HttpClient _client;

    public Register(ILogger<Register> logger ,IConfiguration config, IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _config = config;
        _client = clientFactory.CreateClient("Default");
    }
    
    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        public DateTime Birthday { get; set; }
        
    }
    
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("Invalid input");
            return Page();
        }

        var baseUrl = _config["Microservice:BaseUrl"];
        var url = $"{baseUrl}/api/profile";

        var json = JsonSerializer.Serialize(Input);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync(url, content);
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Server returned error");
            ModelState.AddModelError(string.Empty, "Registration failed.");
            return Page();
        }
        
        _logger.LogInformation("Registration succeeded");
        return RedirectToPage("/Index");
    }
}