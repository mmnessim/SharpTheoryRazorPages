using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpTheory.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace SharpTheory.Pages
{
    /// <summary>
    /// PageModel for the Key Quiz page, handles quiz logic and user interactions.
    /// </summary>
        public class KeyQuizModel : PageModel
    {
        // Variabels for selected key
        /// <summary>
        /// Pool of keys to select from, filtered based on user preferences.
        /// </summary>
        private List<TheoryKey>? KeyPool { get; set; }
        /// <summary>
        /// The currently selected key for the quiz.
        /// </summary>
        public TheoryKey? Key { get; set; }
        /// <summary>
        /// Gets or sets the number of sharps for the selected key.

        /// </summary>
        public int? NumSharps { get; set; }
        /// <summary>
        /// Gets or sets the number of flats for the selected key.
        /// </summary>
        public int? NumFlats { get; set; }

        /// <summary>
        /// Gets or sets the name of the selected key. This is bound to a hidden input to maintain state across requests.
        /// </summary>
        [BindProperty]
        public string? SelectedKeyName { get; set; }

        // User inputs
        /// <summary>
        /// Gets or sets the number of sharps input by the user.
        /// </summary>
        [BindProperty]
        public int? UserSharps { get; set; }

        /// <summary>
        /// Gets or sets the number of flats input by the user.
        /// </summary>
        [BindProperty]
        public int? UserFlats { get; set; }

        /// <summary>
        /// Gets or sets the result message to be displayed after user submission.
        /// </summary>
        public string? ResultMessage { get; set; }

        // Flags to change KeyPool
        /// <summary>
        /// Gets or sets sharps only flag for KeyPool.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public bool OnlySharps { get; set; } = false;

        /// <summary>
        /// Gets or sets flats only flag for KeyPool.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public bool OnlyFlats { get; set; } = false;

        // Answer history
        /// <summary>
        /// Gets or sets the count of correct answers, stored in session.
        /// </summary>
        public int RightCount
        {
            get => HttpContext.Session.GetInt32("RightCount") ?? 0;
            set => HttpContext.Session.SetInt32("RightCount", value);
        }

        /// <summary>
        /// Gets or sets the count of incorrect answers, stored in session.
        /// </summary>
        public int WrongCount
        {
            get => HttpContext.Session.GetInt32("WrongCount") ?? 0;
            set => HttpContext.Session.SetInt32("WrongCount", value);
        }

        /// <summary>
        /// Handles GET requests by selecting a random key to be rendered
        /// </summary>
        /// <remarks>
        /// First, OnGet() checks querystring for flags and sets flags accordingly. Then it reads data.json 
        /// sets KeyPool according to flags. Finally, it selects a random key and assigns it.
        /// </remarks>
        public void OnGet()
        {
            if (!Request.Query.ContainsKey("OnlySharps"))
            {
                OnlySharps = HttpContext.Session.GetInt32("OnlySharps") == 1;
                OnlyFlats = HttpContext.Session.GetInt32("OnlyFlats") == 1;
            } else
            {
                HttpContext.Session.SetInt32("OnlySharps", OnlySharps ? 1 : 0);
                HttpContext.Session.SetInt32("OnlyFlats", OnlyFlats ? 1 : 0);
            }
                
            var json = System.IO.File.ReadAllText("Data/data.json");
            var root = JsonSerializer.Deserialize<TheoryRoot>(json);
            if (OnlySharps)
            {
                KeyPool = root?.Keys.Where(k => k.NumSharps > 0).ToList();
            }
            else if (OnlyFlats)
            {
                KeyPool = root?.Keys.Where(k => k.NumFlats > 0).ToList();
            }
            else
            {
                KeyPool = root?.Keys;
            }

            var random = new Random();
            if (KeyPool != null)
            {
                Key = KeyPool[random.Next(KeyPool.Count)];
                NumSharps = Key?.NumSharps;
                NumFlats = Key?.NumFlats;
                SelectedKeyName = Key?.Name;
            }
            
        }

        /// <summary>
        /// Handles POST requests by validating user's submitted key and updating ResultMessage
        /// </summary>
        /// <remarks>
        /// Reads data.json and ensures that Key is set correctly.SelectedKey is bound to a hidden input 
        /// on KeyQuiz.cshtml that ensures persistence. UserSharps/UserFlats default to 0. Result message is
        /// set accordingly.
        /// </remarks>
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

            UserSharps ??= 0;
            UserFlats ??= 0;

            if (UserSharps == NumSharps && UserFlats == NumFlats)
            {
                ResultMessage = $"Correct! {NumSharps} Sharps and {NumFlats} Flats";
                RightCount++;
            }
            else
            {
                ResultMessage = $"Incorrect. The correct answer is {NumSharps} sharps and {NumFlats} flats.";
                WrongCount++;
            }
            
        }

        /// <summary>
        /// Refreshes page for a new question
        /// </summary>
        /// /// <returns>An <see cref="IActionResult"/> that redirects to the current page.</returns>
        public IActionResult OnPostNewQuestion()
        {
            return RedirectToPage();
        }

        /// <summary>
        /// Resets the counters for correct and incorrect answers to zero and redirects to the current page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> that redirects to the current page.</returns>
        public IActionResult OnPostClear()
        {
            RightCount = 0;
            WrongCount = 0;
            return RedirectToPage();
        }
    }
}
