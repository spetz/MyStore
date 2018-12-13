using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services;
using MyStore.Services.Products.Commands;
using MyStore.Services.Products.Dto;
using MyStore.Services.Products.Queries;

namespace MyStore.Web.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] BrowseProducts query)
            => Result(await QueryAsync(query));

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> Get(Guid id)
            => Result(await QueryAsync(new GetProduct {Id = id}));

        [HttpPost]
        [Authorize(Policy = "is-admin")]
        public async Task<ActionResult> Post(CreateProduct command)
        {
            await SendAsync(command);

            return CreatedAtAction(nameof(Get), new {id = command.Id}, null);
        }
    }
}