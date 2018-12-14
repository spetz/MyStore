using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using MyStore.Core.Domain;
using MyStore.Core.Repositories;
using MyStore.Services.Products;
using Xunit;

namespace MyStore.Tests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task given_valid_id_get_should_return_product_dto()
        {
            var id = Guid.NewGuid();
            var product = new Product(Guid.NewGuid(), "iphone", 1, "phones");
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetAsync(id)).ReturnsAsync(product);

            var productService = new ProductService(productRepositoryMock.Object);

            var dto = await productService.GetAsync(id);

            dto.Should().NotBeNull();
            dto.Id.Should().Be(product.Id);
            dto.Name.Should().Be(product.Name);
            dto.Category.Should().Be(product.Category);
            dto.Price.Should().Be(product.Price);
            productRepositoryMock.Verify(x => x.GetAsync(id), Times.Once);
        }
    }
}