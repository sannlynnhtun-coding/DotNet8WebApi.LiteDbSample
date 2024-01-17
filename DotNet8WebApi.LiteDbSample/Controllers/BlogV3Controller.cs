using DotNet8WebApi.LiteDbSample.Models.Blog;
using DotNet8WebApi.LiteDbSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.LiteDbSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogV3Controller : ControllerBase
    {
        private readonly QuickLiteDB _quickLiteDB;
        private readonly string _tablename = "Blog";

        public BlogV3Controller(QuickLiteDB quickLiteDB)
        {
            _quickLiteDB = quickLiteDB;
        }

        [Route("Get")]
        [HttpGet]
        public IActionResult Get()
        {
            var lst = _quickLiteDB.List<BlogModel>(_tablename);
            //_liteDbService.Dispose();
            return Ok(lst);
        }

        [HttpGet("Id")]
        public IActionResult GetById(string id)
        {
            var item = _quickLiteDB.GetById<BlogModel>(x => x.BlogId == id, _tablename);
            if (item == null)
            {
                return NotFound("No Data Found.");
            }
            //_liteDbService.Dispose();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create()
        {
            var newBlog = new BlogModel
            {
                BlogId = Ulid.NewUlid().ToString(),
                BlogTitle = "LiteDb",
                BlogAuthor = "LiteDb",
                BlogContent = "LiteDb",
            };
            _quickLiteDB.Add(newBlog, _tablename);
            //_liteDbService.Dispose();
            return Ok(newBlog);
        }

        [HttpPut]
        public IActionResult Put(string id, BlogRequestModel reqModel)
        {
            var item = _quickLiteDB.GetById<BlogModel>(x => x.BlogId == id, _tablename);

            item.BlogTitle = reqModel.BlogTitle;
            item.BlogAuthor = reqModel.BlogAuthor;
            item.BlogContent = reqModel.BlogContent;

            var result = _quickLiteDB.Update(item, _tablename);
            //_liteDbService.Dispose();
            return Ok(result);
        }

        [HttpPatch]
        public IActionResult Patch(string id, BlogRequestModel reqModel)
        {
            var item = _quickLiteDB.GetById<BlogModel>(x => x.BlogId == id, _tablename);

            if (!string.IsNullOrEmpty(reqModel.BlogTitle))
            {
                item.BlogTitle = reqModel.BlogTitle;
            }

            if (!string.IsNullOrEmpty(reqModel.BlogAuthor))
            {
                item.BlogAuthor = reqModel.BlogAuthor;
            }

            if (!string.IsNullOrEmpty(reqModel.BlogContent))
            {
                item.BlogContent = reqModel.BlogContent;
            }

            var result = _quickLiteDB.Update(item , _tablename);
            //_liteDbService.Dispose();

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var item = _quickLiteDB.GetById<BlogModel>(x => x.BlogId == id, _tablename);
            var result = _quickLiteDB.Delete<BlogModel>(item.Id!, _tablename);
            //_liteDbService.Dispose();
            return Ok(result);
        }
    }
}
