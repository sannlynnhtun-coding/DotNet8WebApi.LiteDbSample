using LiteDB;

namespace DotNet8WebApi.LiteDbSample.Models
{
    public class BlogModel
    {
        [BsonId]
        public ObjectId? Id { get; set; }
        public string? BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
    }
}
