using System;
using MyStore.Services.Products.Dto;

namespace MyStore.Services.Products.Queries
{
    public class GetProduct : IQuery<ProductDto>
    {
        public Guid Id { get; set; }
    }
}