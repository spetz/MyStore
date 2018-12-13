using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyStore.Core.Domain;

namespace MyStore.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}