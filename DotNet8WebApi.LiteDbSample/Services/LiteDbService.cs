using DotNet8WebApi.LiteDbSample.Models;
using LiteDB;

namespace DotNet8WebApi.LiteDbSample.Services
{
    public class LiteDbService
    {
        private readonly LiteDatabase _liteDatabase;
        private readonly string _filePath;
        private readonly string _folderPath;
        public LiteDbService()
        {
            _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LiteDb");
            Directory.CreateDirectory(_folderPath);

            _filePath = Path.Combine(_folderPath, "Blog.db");
            _liteDatabase = new LiteDatabase(_filePath);
        }

        public ILiteCollection<BlogModel> Blog => _liteDatabase.GetCollection<BlogModel>("Blog");

        public void Dispose() => _liteDatabase.Dispose();
    }
}
