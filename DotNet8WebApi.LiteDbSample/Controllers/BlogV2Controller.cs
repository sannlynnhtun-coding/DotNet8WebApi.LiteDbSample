using DotNet8WebApi.LiteDbSample.Models.Blog;
using DotNet8WebApi.LiteDbSample.Services;
using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.LiteDbSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogV2Controller : ControllerBase
    {
        private readonly LiteDbV2Service _liteDbService;

        public BlogV2Controller(LiteDbV2Service liteDbService)
        {
            _liteDbService = liteDbService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lst = _liteDbService.Blog.FindAll().ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var item = _liteDbService.Blog.Find(x => x.BlogId == id).FirstOrDefault();
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
            _liteDbService.Blog.Insert(newBlog);
            return Ok(newBlog);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, BlogModel reqModel)
        {
            var item = _liteDbService.Blog.Find(x => x.BlogId == id).FirstOrDefault();

            item.BlogTitle = reqModel.BlogTitle;
            item.BlogAuthor = reqModel.BlogAuthor;
            item.BlogContent = reqModel.BlogContent;

            var result = _liteDbService.Blog.Update(item);
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(string id, BlogRequestModel reqModel)
        {
            var item = _liteDbService.Blog.Find(x => x.BlogId == id).FirstOrDefault();
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

            var result = _liteDbService.Blog.Update(item);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var item = _liteDbService.Blog.Find(x => x.BlogId == id).FirstOrDefault();
            var result = _liteDbService.Blog.Delete(item.Id);
            return Ok();
        }
    }
}
