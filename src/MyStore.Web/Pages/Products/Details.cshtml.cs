using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore.Services.Products;
using MyStore.Services.Products.Dto;
using MyStore.Web.Framework;
using MyStore.Web.Models;

namespace MyStore.Web.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;
        
        public ProductDto Product { get; private set; }

        public DetailsModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Product = await _productService.GetAsync(id);
            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
