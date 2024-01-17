using LiteDB;

namespace DotNet8WebApi.LiteDbSample.Models.Catalog
{
    public class CatalogModel
    {
        [BsonId]
        public ObjectId? Id { get; set; }
        public string CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string CatalogDescription { get; set; }
        public int CatalogPrice { get; set; }
    }
}
