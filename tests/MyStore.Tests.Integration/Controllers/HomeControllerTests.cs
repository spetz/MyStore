using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using MyStore.Web;
using Xunit;

namespace MyStore.Tests.Integration.Controllers
{
    public class HomeControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public HomeControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(b =>
            {
                b.UseEnvironment("test");
            });
        }

        [Fact]
        public async Task get_api_endpoint_should_return_application_name()
        {
            var httpClient = _factory.CreateClient();

            var response = await httpClient.GetAsync("/api");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be("MyStore [Test]");
        }
    }
}