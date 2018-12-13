using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services;
using MyStore.Services.Products;
using MyStore.Services.Products.Commands;
using MyStore.Services.Products.Dto;
using MyStore.Web.Framework;
using MyStore.Web.Models;

namespace MyStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public ProductsController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
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
        public async Task<ActionResult> Post(CreateProduct command)
        {
            await _commandDispatcher.SendAsync(command);

            return CreatedAtAction(nameof(Get), new {id = command.Id}, null);
        }
    }
}