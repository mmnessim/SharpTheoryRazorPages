using SharpTheory.Models;
using SharpTheory.Pages;
using System.Text.Json;
using Xunit;

namespace SharpTheoryTest
{
    public class KeyDetailsModelsTests
    {
        [Fact]
        public void OnGet_WithValidId_SetsKeyAndScale()
        {
            var model = new KeyDetailsModel();

            var exception = Record.Exception(() => model.OnGet("c-major"));

            Assert.Null(exception);
            Assert.Equal("C Major", model.Key?.Name);
            Assert.Equal("major", model.Key?.Mode);
            Assert.NotNull(model.Key);
            Assert.NotNull(model.Scale);
            Assert.Null(model.NatMinor);
        }
    }
}
