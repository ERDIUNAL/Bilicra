using Bilicra.API.Domain.Interfaces;
using Bilicra.API.Domain.Models;
using Bilicra.API.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bilicra.API.Processes
{
    public class ApiProcess : IApiProcess
    {
        public List<ProductCatolog> GetProductCatalogs()
        {
            return ApiService.GetProductCatalogs();
        }

        public ProductCatolog GetProductCatalog(int Id)
        {
            return ApiService.GetProductCatalog(Id);
        }

        public ProductCatolog GetProductCatalog(string Code)
        {
            return ApiService.GetProductCatalog(Code);
        }

        public void SaveProductCatalog(ProductCatolog productCatolog)
        {
            ApiService.SaveProductCatalog(productCatolog);
        }

        public void DeleteProductCatalog(int Id)
        {
            ApiService.DeleteProductCatalog(Id);
        }
    }
}
