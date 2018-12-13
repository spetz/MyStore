using System;

namespace MyStore.Core.Domain
{
    public class Product
    {
        public AggregateId Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Category { get; private set; }

        private Product()
        {
        }
        
        public Product(Guid id, string name, decimal price, string category)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid product name.", nameof(name));
            }
            
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }
    }
}