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

            exception = Record.Exception(() => model.OnGet("c-minor"));

            Assert.Null(exception);
            Assert.Equal("C minor", model.Key?.Name);
            Assert.Equal("minor", model.Key?.Mode);
            Assert.NotNull(model.Key);
            Assert.NotNull(model.Scale);
            Assert.NotNull(model.NatMinor);
        }

        [Fact]
        public void OnGet_WithInvalidId()
        {
            var model = new KeyDetailsModel();

            var exception = Record.Exception(() => model.OnGet("INVALID"));
            Assert.NotNull(exception);
            Console.WriteLine(exception.Message);
            Assert.Null(model.Key);
            Assert.Null(model.Scale);
            Assert.Null(model.NatMinor);
        }
    }
}
