﻿using DotNet8WebApi.LiteDbSample.Models.Blog;
using DotNet8WebApi.LiteDbSample.Models.Catalog;
using LiteDB;

namespace DotNet8WebApi.LiteDbSample.Services
{
    public class LiteDbV2Service:IDisposable
    {
        //private readonly LiteDatabase _liteDatabase;
        //private readonly string _filePath;
        //private readonly string _folderPath;
        //public LiteDbV2Service()
        //{
        //    _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LiteDb");
        //    Directory.CreateDirectory(_folderPath);

        //    _filePath = Path.Combine(_folderPath, "Blog.db");
        //    _liteDatabase = new LiteDatabase(_filePath);
        //}
        private readonly LiteDatabase _liteDatabase;

        public LiteDbV2Service(LiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public ILiteCollection<BlogModel> Blog => _liteDatabase.GetCollection<BlogModel>("Blog");
        public ILiteCollection<CatalogModel> Catalog => _liteDatabase.GetCollection<CatalogModel>("Catalog");

        public void Dispose() => _liteDatabase.Dispose();
    }
}