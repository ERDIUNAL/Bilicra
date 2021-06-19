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

        public int SaveProductCatalog(ProductCatolog productCatolog)
        {
            return ApiService.SaveProductCatalog(productCatolog);
        }

        public int DeleteProductCatalog(int Id)
        {
            return ApiService.DeleteProductCatalog(Id);
        }

        public byte[] GetProductCatalogsAsExcel(List<ProductCatolog> pruductCatalogs)
        {
            return ApiService.GetProductCatalogsAsExcel(pruductCatalogs);
        }
    }
}
