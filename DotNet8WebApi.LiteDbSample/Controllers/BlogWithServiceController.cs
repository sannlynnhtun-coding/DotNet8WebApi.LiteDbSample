using DotNet8WebApi.LiteDbSample.Models;
using DotNet8WebApi.LiteDbSample.Services;
using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.LiteDbSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogWithServiceController : ControllerBase
    {
        private readonly LiteDbService _liteDbService;

        public BlogWithServiceController(LiteDbService liteDbService)
        {
            _liteDbService = liteDbService;
        }

        [Route("Get")]
        [HttpGet]
        public IActionResult Get()
        {
            var lst = _liteDbService.Blog.FindAll().ToList();
            _liteDbService.Dispose();
            return Ok(lst);
        }

        [HttpGet("Id")]
        public IActionResult GetById(string id)
        {
            var item = _liteDbService.Blog.Find(x => x.BlogId == id).FirstOrDefault();
            _liteDbService.Dispose();
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
            _liteDbService.Dispose();
            return Ok(newBlog);
        }

        [HttpPut]
        public IActionResult Put(string id, BlogModel reqModel)
        {
            var item = _liteDbService.Blog.Find(x => x.BlogId == id).FirstOrDefault();

            item.BlogTitle = reqModel.BlogTitle;
            item.BlogAuthor = reqModel.BlogAuthor;
            item.BlogContent = reqModel.BlogContent;

            var result = _liteDbService.Blog.Update(item);
            _liteDbService.Dispose();
            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch(string id, BlogRequestModel reqModel)
        {
            var item = _liteDbService.Blog.Find(x => x.BlogId == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(reqModel.BlogTitle))
            {
                item.BlogTitle = reqModel.BlogTitle;
            }

            //if (!string.IsNullOrEmpty(reqModel.BlogAuthor))
            //{
            //    item.BlogAuthor = reqModel.BlogAuthor;
            //}

            //if (!string.IsNullOrEmpty(reqModel.BlogContent))
            //{
            //    item.BlogContent = reqModel.BlogContent;
            //}

            var result = _liteDbService.Blog.Update(item);
            _liteDbService.Dispose();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var item = _liteDbService.Blog.Find(x => x.BlogId == id).FirstOrDefault();
            var result = _liteDbService.Blog.Delete(item.Id);
            _liteDbService.Dispose();
            return Ok();
        }
    }
}
