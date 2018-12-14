using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MyStore.Core.Domain;
using MyStore.Infrastructure.EF;
using MyStore.Services.Products.Dto;
using MyStore.Web;
using Xunit;

namespace MyStore.Tests.Integration.Controllers
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ProductsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(b =>
            {
                b.UseEnvironment("test");
//                b.ConfigureServices(s =>
//                {
//                    using (var scope = s.BuildServiceProvider().CreateScope())
//                    {
//                        var dbContext = scope.ServiceProvider.GetService<MyStoreContext>();
//                        dbContext.Database.EnsureCreated();
//                        dbContext.Products.Add(new Product(Guid.NewGuid(), "ihpone", 4000, "phones"));
//                        dbContext.SaveChanges();
//                    }
//                });
            });
        }
        
        // Init products
        [Fact]
        public async Task given_valid_query_get_should_return_products()
        {
            var httpClient = _factory.CreateClient();

            var response = await httpClient.GetAsync("/api/products");

            response.EnsureSuccessStatusCode();
            var products = await response.Content
                .ReadAsAsync<IEnumerable<ProductDto>>();

//            products.Should().NotBeEmpty();
        }
    }
}