using System;
using System.ComponentModel.DataAnnotations;

namespace MyStore.Web.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required] [Range(1, 100000)] public decimal Price { get; set; }

        [Required] public string Category { get; set; }
    }
}