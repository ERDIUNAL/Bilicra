using Bilicra.API.Domain.Models;
using Bilicra.Infrastructure.Processes;
using Bilicra.Persistence.Processes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bilicra.API.Services
{
    public static class ApiService
    {
        public static List<ProductCatolog> GetProductCatalogs()
        {
            return new PersistenceProcess(new LoggingProcess()).GetProductCatalogs();
        }

        public static ProductCatolog GetProductCatalog(int Id)
        {
            return new PersistenceProcess(new LoggingProcess()).GetProductCatalog(Id);
        }

        public static ProductCatolog GetProductCatalog(string Code)
        {
            return new PersistenceProcess(new LoggingProcess()).GetProductCatalog(Code);
        }

        public static void SaveProductCatalog(ProductCatolog productCatolog)
        {
            new PersistenceProcess(new LoggingProcess()).SaveProductCatalog(productCatolog);
        }

        public static void DeleteProductCatalog(int Id)
        {
            new PersistenceProcess(new LoggingProcess()).DeleteProductCatalog(Id);
        }
    }
}
