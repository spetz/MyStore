using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStore.Core.Domain;
using MyStore.Core.Repositories;
using MyStore.Services.Products.Dto;

namespace MyStore.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> GetAsync(Guid id)
            => Map(await _productRepository.GetAsync(id));

        public async Task<IEnumerable<ProductDto>> BrowseAsync(string name = "")
        {
            var products = await _productRepository.GetAllAsync();
            var filteredProducts =
                string.IsNullOrWhiteSpace(name) ? products : products.Where(p => p.Name.Contains(name));

            return filteredProducts.Select(Map);
        }

        public async Task AddAsync(ProductDto dto)
            => await _productRepository.AddAsync(new Product(dto.Id, dto.Name, dto.Price, dto.Category));

        public async Task DeleteAsync(Guid id)
            => await _productRepository.DeleteAsync(id);

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