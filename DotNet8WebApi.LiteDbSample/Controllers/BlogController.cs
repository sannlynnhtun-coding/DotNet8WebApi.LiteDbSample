using DotNet8WebApi.LiteDbSample.Models;
using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.LiteDbSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly string _filePath;
        private readonly string _folderPath;
        public BlogController()
        {
            _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LiteDb");
            Directory.CreateDirectory(_folderPath);

            _filePath = Path.Combine(_folderPath, "Blog.db");
        }

        [Route("Get")]
        [HttpGet]
        public IActionResult Get()
        {
            var db = new LiteDatabase(_filePath);
            var collection = db.GetCollection<BlogModel>("Blog");
            var lst = collection.FindAll().ToList();
            db.Dispose();
            return Ok(lst);
        }
        [HttpGet("Id")]
        public IActionResult GetById(string id)
        {
            var db = new LiteDatabase(_filePath);
            var collection = db.GetCollection<BlogModel>("Blog");
            var item = collection.Find(x => x.BlogId == id).FirstOrDefault();
            db.Dispose();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create()
        {
            var db = new LiteDatabase(_filePath);
            var collection = db.GetCollection<BlogModel>("Blog");
            var newBlog = new BlogModel
            {
                BlogId = Ulid.NewUlid().ToString(),
                BlogTitle = "LiteDb",
                BlogAuthor = "LiteDb",
                BlogContent = "LiteDb",
            };
            collection.Insert(newBlog);
            db.Dispose();
            return Ok(newBlog);
        }

        [HttpPut]
        public IActionResult Put(string id, BlogModel reqModel)
        {

            var db = new LiteDatabase(_filePath);
            var collection = db.GetCollection<BlogModel>("Blog");
            var item = collection.Find(x => x.BlogId == id).FirstOrDefault();

            item.BlogTitle = reqModel.BlogTitle;
            item.BlogAuthor = reqModel.BlogAuthor;
            item.BlogContent = reqModel.BlogContent;

            var result = collection.Update(item);
            db.Dispose();
            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch(string id, BlogRequestModel reqModel)
        {
            var db = new LiteDatabase(_filePath);
            var collection = db.GetCollection<BlogModel>("Blog");
            var item = collection.Find(x => x.BlogId == id).FirstOrDefault();
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

            var result = collection.Update(item);
            db.Dispose();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var db = new LiteDatabase(_filePath);
            var collection = db.GetCollection<BlogModel>("Blog");
            var item = collection.Find(x => x.BlogId == id).FirstOrDefault();
            var result = collection.Delete(item.Id);
            db.Dispose();
            return Ok();
        }
    }
}
