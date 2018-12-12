using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore.Web.Framework;
using MyStore.Web.Models;

namespace MyStore.Web.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductsManager _productsManager;

        public IEnumerable<Product> Products => _productsManager.Products;

        public IEnumerable<SelectListItem> Categories => new List<SelectListItem>
        {
            new SelectListItem("Phones", "Phones"),
            new SelectListItem("Computers", "Computers"),
        };
        
        [BindProperty]
        public Product Product { get; set; } = new Product();

        public IndexModel(ProductsManager productsManager)
        {
            _productsManager = productsManager;
        }
        
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await Task.CompletedTask;
            Product.Id = Guid.NewGuid();
            _productsManager.Products.Add(Product);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            _productsManager.Products.Remove(Products.SingleOrDefault(p => p.Id == id));
            await Task.CompletedTask;

            return RedirectToPage();
        }
    }
}
