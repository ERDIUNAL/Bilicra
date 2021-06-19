using Bilicra.API.Domain.Models;
using Bilicra.Infrastructure.Processes;
using Bilicra.Persistence.Processes;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static int SaveProductCatalog(ProductCatolog productCatolog)
        {
            return new PersistenceProcess(new LoggingProcess()).SaveProductCatalog(productCatolog);
        }

        public static int DeleteProductCatalog(int Id)
        {
            return new PersistenceProcess(new LoggingProcess()).DeleteProductCatalog(Id);
        }

        public static byte[] GetProductCatalogsAsExcel(List<ProductCatolog> pruductCatalogs)
        {
            byte[] excelFile;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sayfa 1");

                worksheet.Cell(1, 1).SetValue("Id");
                worksheet.Cell(1, 2).SetValue("Code");
                worksheet.Cell(1, 3).SetValue("Name");
                worksheet.Cell(1, 4).SetValue("Price");
                worksheet.Cell(1, 5).SetValue("LastUpdated");

                worksheet.Row(1).CellsUsed().Style.Font.SetBold();
                worksheet.Row(1).CellsUsed().Style.Font.SetFontSize(12);
                worksheet.Row(1).CellsUsed().Style.Fill.SetBackgroundColor(XLColor.LightGray);

                worksheet.Cell(2, 1).InsertData(pruductCatalogs);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);

                    excelFile = memoryStream.ToArray();
                }
            }

            return excelFile;
        }
    }
}
