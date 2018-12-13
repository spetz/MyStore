using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore.Services.Products;
using MyStore.Services.Products.Dto;
using MyStore.Web.Framework;
using MyStore.Web.Models;

namespace MyStore.Web.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IEnumerable<ProductDto> Products { get; private set; }

        public IEnumerable<SelectListItem> Categories => new List<SelectListItem>
        {
            new SelectListItem("Phones", "Phones"),
            new SelectListItem("Computers", "Computers"),
        };
        
        [BindProperty]
        public ProductDto Product { get; set; } = new ProductDto();

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        
        public async Task OnGetAsync()
        {
            Products = await _productService.BrowseAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product.Id = Guid.NewGuid();
            await _productService.AddAsync(Product);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _productService.DeleteAsync(id);
            
            return RedirectToPage();
        }
    }
}
