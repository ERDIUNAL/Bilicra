using Bilicra.API.Domain.Models;
using Bilicra.API.Processes;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bilicra.API.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCatalogController : ControllerBase
    {
        [HttpGet]
        [Route("GetProductCatalogs")]
        public List<ProductCatolog> GetProductCatalogs()
        {
            return new ApiProcess().GetProductCatalogs();
        }

        [HttpGet]
        [Route("GetProductCatalogsAsExcel")]
        public IActionResult GetProductCatalogsAsExcel()
        {
            byte[] excelFile;

            var pruductCatalogs = new ApiProcess().GetProductCatalogs();

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

            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [HttpGet]
        [Route("GetProductCatalogById")]
        public ProductCatolog GetProductCatalogById(int Id)
        {
            return new ApiProcess().GetProductCatalog(Id);
        }

        [HttpGet]
        [Route("GetProductCatalogByCode")]
        public ProductCatolog GetProductCatalogByCode(string Code)
        {
            return new ApiProcess().GetProductCatalog(Code);
        }

        [HttpPost]
        [Route("SaveProductCatalog")]
        public IActionResult SaveProductCatalog([FromBody] ProductCatolog productCatolog)
        {
            try
            {
                productCatolog.LastUpdated = DateTime.Now;

                new ApiProcess().SaveProductCatalog(productCatolog);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("DeleteProductCatalog")]
        public void DeleteProductCatalog(int Id)
        {
            new ApiProcess().DeleteProductCatalog(Id);
        }
    }
}
