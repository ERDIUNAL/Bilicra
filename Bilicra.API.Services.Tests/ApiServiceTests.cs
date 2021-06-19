using Bilicra.API.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Bilicra.API.Services.Tests
{
    public class ApiServiceTests
    {
        [Fact]
        public void TestGetProductCatalogsAsExcel()
        {
            var pruductCatalogs = new List<ProductCatolog>
            {
                new ProductCatolog
                {
                    Id = 1,
                    Code = "Code 1",
                    Name = "Name 1",
                    Price = 5,
                    LastUpdated = DateTime.Now
                },
                new ProductCatolog
                {
                    Id = 2,
                    Code = "Code 2",
                    Name = "Name 2",
                    Price = 7,
                    LastUpdated = DateTime.Now.AddDays(1)
                }
            };

            var result = ApiService.GetProductCatalogsAsExcel(pruductCatalogs);

            Assert.NotNull(result);
        }
    }
}
