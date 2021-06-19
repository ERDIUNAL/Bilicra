using Bilicra.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bilicra.Persistence.Domain.Interfaces
{
    public interface IPersistenceProcess
    {
        void EstablishConnection(string server, string database, string username, string password);
        ProductCatolog GetProductCatalog(int Id);
        ProductCatolog GetProductCatalog(string Code);
        List<ProductCatolog> GetProductCatalogs();
        int SaveProductCatalog(ProductCatolog productCatolog);
        int DeleteProductCatalog(int Id);
    }
}
