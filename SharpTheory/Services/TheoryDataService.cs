using SharpTheory.Models;
using System.Text.Json;

namespace SharpTheory.Services
{
    public class TheoryDataService
    {
        private readonly TheoryRoot? _root;

        public TheoryDataService()
        {
            var json = System.IO.File.ReadAllText("Data/data.json");
            _root = JsonSerializer.Deserialize<TheoryRoot>(json);
        }

        public TheoryRoot? Root => _root;
    }
}
