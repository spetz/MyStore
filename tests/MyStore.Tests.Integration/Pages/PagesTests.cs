using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using MyStore.Web;
using Xunit;

namespace MyStore.Tests.Integration.Pages
{
    public class PagesTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public PagesTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/products")]
        public async Task get_index_should_return_page(string url)
        {
            // Arrange
            var httpClient = _factory.CreateClient();
            
            // Act
            var response = await httpClient.GetAsync(url);
            
            // Assert 200-299
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString()
                .Should().Be("text/html; charset=utf-8");
        }
    }
}