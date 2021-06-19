using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Bilicra.API.Application.IntegrationTest
{
    public class ProductCatalogControllerTest: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ProductCatalogControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/productcatalog/getproductcatalogs")]
        public async Task TestGetProductCatalogs(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            client.Dispose();
            response.Content.Dispose();
        }
    }
}
