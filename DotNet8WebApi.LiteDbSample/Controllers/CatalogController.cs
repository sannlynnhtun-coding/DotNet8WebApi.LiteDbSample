using DotNet8WebApi.LiteDbSample.Models.Catalog;
using DotNet8WebApi.LiteDbSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.LiteDbSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly LiteDbV2Service _liteDbV2Service;

        public CatalogController(LiteDbV2Service liteDbV2Service)
        {
            _liteDbV2Service = liteDbV2Service;
        }

        [Route("Get")]
        [HttpGet]
        public IActionResult Get()
        {
            var lst = _liteDbV2Service.Catalog.FindAll().ToList();
            //_liteDbService.Dispose();
            return Ok(lst);
        }

        [HttpGet("Id")]
        public IActionResult GetById(string id)
        {
            var item = _liteDbV2Service.Catalog.Find(x => x.CatalogId == id).FirstOrDefault();
            //_liteDbService.Dispose();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create()
        {
            var catalog = new CatalogModel
            {
                CatalogId = Ulid.NewUlid().ToString(),
                CatalogName = "Product ABC",
                CatalogDescription  = "Description of Product ABC",
                CatalogPrice = 10
            };
            _liteDbV2Service.Catalog.Insert(catalog);
            //_liteDbService.Dispose();
            return Ok(catalog);
        }

        [HttpPut]
        public IActionResult Put(string id, CatalogModel reqModel)
        {
            var item = _liteDbV2Service.Catalog.Find(x => x.CatalogId == id).FirstOrDefault();

            item.CatalogName = reqModel.CatalogName;
            item.CatalogDescription = reqModel.CatalogDescription;
            item.CatalogPrice = reqModel.CatalogPrice;

            var result = _liteDbV2Service.Catalog.Update(item);
            //_liteDbService.Dispose();
            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch(string id, CatalogRequestModel reqModel)
        {
            var item = _liteDbV2Service.Catalog.Find(x => x.CatalogId == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(reqModel.CatalogName))
            {
                item.CatalogName = reqModel.CatalogName;
            }

            var result = _liteDbV2Service.Catalog.Update(item);
            //_liteDbService.Dispose();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var item = _liteDbV2Service.Catalog.Find(x => x.CatalogId == id).FirstOrDefault();
            var result = _liteDbV2Service.Catalog.Delete(item.Id);
            //_liteDbService.Dispose();
            return Ok();
        }
    }
}
