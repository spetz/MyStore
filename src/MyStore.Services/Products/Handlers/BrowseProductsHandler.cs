using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStore.Core.Domain;
using MyStore.Core.Repositories;
using MyStore.Services.Products.Dto;
using MyStore.Services.Products.Queries;

namespace MyStore.Services.Products.Handlers
{
    public class BrowseProductsHandler : IQueryHandler<BrowseProducts, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public BrowseProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> HandleAsync(BrowseProducts query)
        {
            var products = await _productRepository.GetAllAsync();
            var filteredProducts = string.IsNullOrWhiteSpace(query.Name)
                ? products
                : products.Where(p => p.Name.Contains(query.Name));

            return filteredProducts.Select(Map);
        }

        private static ProductDto Map(Product product)
            => product == null
                ? null
                : new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category
                };
    }
}