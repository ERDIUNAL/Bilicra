using Bilicra.API.Domain.Interfaces;
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
        private readonly IApiProcess _apiProcess;

        public ProductCatalogController(IApiProcess apiProcess)
        {
            _apiProcess = apiProcess;
        }

        [HttpGet]
        [Route("GetProductCatalogs")]
        public List<ProductCatolog> GetProductCatalogs()
        {
            return _apiProcess.GetProductCatalogs();
        }

        [HttpGet]
        [Route("GetProductCatalogsAsExcel")]
        public IActionResult GetProductCatalogsAsExcel()
        {
            var excelFile = _apiProcess.GetProductCatalogsAsExcel(_apiProcess.GetProductCatalogs());

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
        [Route("CreateProductCatalog")]
        public IActionResult CreateProductCatalog([FromBody] ProductCatolog productCatolog)
        {
            try
            {
                productCatolog.LastUpdated = DateTime.Now;

                var affectedRowCount = new ApiProcess().SaveProductCatalog(productCatolog);

                return Ok(affectedRowCount.ToString() + (affectedRowCount > 1 ? " rows " : " row ") + "affected");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateProductCatalog")]
        public IActionResult UpdateProductCatalog([FromBody] ProductCatolog productCatolog)
        {
            try
            {
                productCatolog.LastUpdated = DateTime.Now;

                var affectedRowCount = new ApiProcess().SaveProductCatalog(productCatolog);

                return Ok(affectedRowCount.ToString() + (affectedRowCount > 1 ? " rows " : " row ") + "affected");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("DeleteProductCatalog")]
        public IActionResult DeleteProductCatalog(int Id)
        {
            try
            {
                var affectedRowCount = new ApiProcess().DeleteProductCatalog(Id);

                return Ok(affectedRowCount.ToString() + (affectedRowCount > 1 ? " rows " : " row ") + "affected");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
