using System;
using System.Collections.Generic;
using MyStore.Web.Models;

namespace MyStore.Web.Framework
{
    public class ProductsManager
    {
        public List<Product> Products { get; } = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Iphone X",
                Category = "Phones",
                Price = 5000
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung S9",
                Category = "Phones",
                Price = 4000
            }
        };
    }
}