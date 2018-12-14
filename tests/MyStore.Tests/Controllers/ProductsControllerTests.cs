using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using MyStore.Services;
using MyStore.Services.Products.Dto;
using MyStore.Services.Products.Queries;
using MyStore.Web.Controllers;
using Xunit;

namespace MyStore.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Fixture _fixture;
        
        public ProductsControllerTests()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public async Task given_valid_query_get_should_return_products()
        {
            var productsDtos = _fixture.CreateMany<ProductDto>();
            var query = new BrowseProducts();
            var dispatcherMock = new Mock<IDispatcher>();
            dispatcherMock.Setup(x => x.QueryAsync(query)).ReturnsAsync(productsDtos);
            
            var controller = new ProductsController(dispatcherMock.Object);

            var response = await controller.Get(query);

            response.Should().NotBeNull();
            response.Value.Should().NotBeEmpty();
            response.Value.Should().BeSameAs(productsDtos);
            dispatcherMock.Verify(x => x.QueryAsync(query), Times.Once);
        }
    }
}