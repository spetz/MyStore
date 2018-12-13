using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyStore.Services.Products.Dto;

namespace MyStore.Services.Products
{
    public interface IProductService
    {
        Task<ProductDto> GetAsync(Guid id);
        Task<IEnumerable<ProductDto>> BrowseAsync(string name = "");
        Task AddAsync(ProductDto dto);
        Task DeleteAsync(Guid id);
    }
}