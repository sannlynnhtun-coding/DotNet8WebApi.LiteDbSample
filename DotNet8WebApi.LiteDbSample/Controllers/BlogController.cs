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
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult Create()
        {
            var db = new LiteDatabase(_filePath);
            var collection = db.GetCollection<BlogModel>("Blog");
            var result = collection.Insert(new BlogModel
            {
                BlogId = Ulid.NewUlid().ToString(),
                BlogTitle = "LiteDb",
                BlogAuthor = "LiteDb",
                BlogContent = "LiteDb",
            });

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
