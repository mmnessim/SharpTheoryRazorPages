using SharpTheory.Models;
using SharpTheory.Pages;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Xunit;
using Moq;
using SharpTheory.Services;
using Xunit.Abstractions;

namespace SharpTheoryTest
{
    public class KeyDetailsModelsTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public KeyDetailsModelsTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void OnGet_WithValidId_SetsKeyAndScale()
        {
            var mockLogger = new Mock<ILogger<KeyDetailsModel>>();
            var mockAnalyticsService = new Mock<IAnalyticsService>();
            var mockDataService = new Mock<TheoryDataService>();
            var model = new KeyDetailsModel(
                mockLogger.Object,
                mockAnalyticsService.Object,
                mockDataService.Object
            );

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
            var mockLogger = new Mock<ILogger<KeyDetailsModel>>();
            var mockAnalyticsService = new Mock<IAnalyticsService>();
            var mockDataService = new Mock<TheoryDataService>();
            var model = new KeyDetailsModel(
                mockLogger.Object,
                mockAnalyticsService.Object,
                mockDataService.Object
            );

            var exception = Record.Exception(() => model.OnGet("INVALID"));
            Assert.NotNull(exception);
            _testOutputHelper.WriteLine(exception.Message);
            Assert.Null(model.Key);
            Assert.Null(model.Scale);
            Assert.Null(model.NatMinor);
        }
    }
}
