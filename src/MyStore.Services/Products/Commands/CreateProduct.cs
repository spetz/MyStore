using System;
using Newtonsoft.Json;

namespace MyStore.Services.Products.Commands
{
    public class CreateProduct : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public string Category { get; }

        [JsonConstructor]
        public CreateProduct(Guid id, string name, decimal price, string category)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Name = name;
            Price = price;
            Category = category;
        }
    }
}