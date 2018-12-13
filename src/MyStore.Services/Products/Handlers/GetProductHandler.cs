using System.Threading.Tasks;
using MyStore.Core.Domain;
using MyStore.Core.Repositories;
using MyStore.Services.Products.Dto;
using MyStore.Services.Products.Queries;

namespace MyStore.Services.Products.Handlers
{
    public class GetProductHandler : IQueryHandler<GetProduct, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<ProductDto> HandleAsync(GetProduct query)
            => Map(await _productRepository.GetAsync(query.Id));
        
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