using DotNet8WebApi.LiteDbSample.Models.Blog;
using LiteDB;

namespace DotNet8WebApi.LiteDbSample.Services
{
    public class LiteDbV2Service:IDisposable
    {
        private readonly LiteDatabase _liteDatabase;

        public LiteDbV2Service(LiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public ILiteCollection<BlogModel> Blog => _liteDatabase.GetCollection<BlogModel>("Blog");

        public void Dispose() => _liteDatabase.Dispose();
    }
}
