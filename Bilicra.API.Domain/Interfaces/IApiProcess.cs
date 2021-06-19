using Bilicra.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bilicra.API.Domain.Interfaces
{
    public interface IApiProcess
    {
        List<ProductCatolog> GetProductCatalogs();
        ProductCatolog GetProductCatalog(int Id);
        ProductCatolog GetProductCatalog(string Code);
        int SaveProductCatalog(ProductCatolog productCatolog);
        int DeleteProductCatalog(int Id);
        public byte[] GetProductCatalogsAsExcel(List<ProductCatolog> pruductCatalogs);
    }
}
