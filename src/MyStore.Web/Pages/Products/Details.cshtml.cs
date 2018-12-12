using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore.Web.Framework;
using MyStore.Web.Models;

namespace MyStore.Web.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ProductsManager _productsManager;
        
        public Product Product { get; private set; }

        public DetailsModel(ProductsManager productsManager)
        {
            _productsManager = productsManager;
        }

        public IActionResult OnGet(Guid id)
        {
            Product = _productsManager.Products.SingleOrDefault(p => p.Id == id);
            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
