using System.Threading.Tasks;
using MyStore.Core.Domain;
using MyStore.Core.Repositories;
using MyStore.Services.Products.Commands;

namespace MyStore.Services.Products.Handlers
{
    public class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task HandleAsync(CreateProduct command)
        {
            await _productRepository.AddAsync(new Product(command.Id,
                command.Name, command.Price, command.Category));
        }
    }
}