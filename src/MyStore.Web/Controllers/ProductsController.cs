using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Products;
using MyStore.Services.Products.Dto;
using MyStore.Web.Framework;
using MyStore.Web.Models;

namespace MyStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get(string name)
            => Ok(await _productService.BrowseAsync(name));

        [HttpGet("{id:guid}")]
//        [ProducesResponseType(typeof(Product), 200)]
//        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductDto>> Get(Guid id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductDto product)
        {
            product.Id = Guid.NewGuid();
            await _productService.AddAsync(product);

            return CreatedAtAction(nameof(Get), new {id = product.Id}, null);
        }
    }
}