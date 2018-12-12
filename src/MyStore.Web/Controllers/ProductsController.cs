using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStore.Web.Framework;
using MyStore.Web.Models;

namespace MyStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ProductsManager _productsManager;

        public ProductsController(ProductsManager productsManager)
        {
            _productsManager = productsManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
            => await Task.FromResult(_productsManager.Products);

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            var product = _productsManager.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return await Task.FromResult(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Product product)
        {
            product.Id = Guid.NewGuid();
            _productsManager.Products.Add(product);
            await Task.CompletedTask;

            return CreatedAtAction(nameof(Get), new {id = product.Id}, null);
        }
    }
}