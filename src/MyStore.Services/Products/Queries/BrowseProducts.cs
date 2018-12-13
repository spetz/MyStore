using System.Collections.Generic;
using MyStore.Services.Products.Dto;

namespace MyStore.Services.Products.Queries
{
    public class BrowseProducts : IQuery<IEnumerable<ProductDto>>
    {
        public string Name { get; set; }
    }
}