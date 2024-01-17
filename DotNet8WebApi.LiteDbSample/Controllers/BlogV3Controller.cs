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
        private readonly LiteDbV3Service _liteDbV3Service;
        private readonly string _tableName = "Blog";

        public BlogV3Controller(LiteDbV3Service liteDbV3Service)
        {
            _liteDbV3Service = liteDbV3Service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lst = _liteDbV3Service.List<BlogModel>(_tableName);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var item = _liteDbV3Service.GetById<BlogModel>(x => x.BlogId == id, _tableName);
            if (item == null)
            {
                return NotFound("No Data Found.");
            }
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
            _liteDbV3Service.Add(newBlog, _tableName);
            return Ok(newBlog);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, BlogRequestModel reqModel)
        {
            var item = _liteDbV3Service.GetById<BlogModel>(x => x.BlogId == id, _tableName);

            item.BlogTitle = reqModel.BlogTitle;
            item.BlogAuthor = reqModel.BlogAuthor;
            item.BlogContent = reqModel.BlogContent;

            var result = _liteDbV3Service.Update(item, _tableName);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(string id, BlogRequestModel reqModel)
        {
            var item = _liteDbV3Service.GetById<BlogModel>(x => x.BlogId == id, _tableName);

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

            var result = _liteDbV3Service.Update(item , _tableName);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var item = _liteDbV3Service.GetById<BlogModel>(x => x.BlogId == id, _tableName);
            var result = _liteDbV3Service.Delete<BlogModel>(item.Id!, _tableName);
            return Ok(result);
        }
    }
}
