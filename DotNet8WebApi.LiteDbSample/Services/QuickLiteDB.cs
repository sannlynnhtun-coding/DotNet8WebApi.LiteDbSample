using LiteDB;
using System.Linq.Expressions;

namespace DotNet8WebApi.LiteDbSample.Services;

public class QuickLiteDB
{
    //public static LiteDatabase db = null;
    ///private string DBPath;
    private readonly LiteDatabase _db;

    public QuickLiteDB(LiteDatabase db)
    {
        _db = db;
    }

    /// <summary>
    /// https://csharp.hotexamples.com/examples/LiteDB/LiteDatabase/-/php-litedatabase-class-examples.html <br/>
    /// https://www.litedb.org/docs/getting-started/
    /// </summary>
    /// <returns></returns>
    /// 
    //public QuickLiteDB(string l_FileName = null, string l_FolderPath = null)
    //{
    //    if (l_FileName == null)
    //        l_FileName = "logs_" + DateTime.Now.ToString("yyyy-MM-dd");
    //    if (db == null)
    //        db = OpenOrCreateDBContext(l_FileName, l_FolderPath);
    //}

    //public LiteDatabase OpenOrCreateDBContext(string l_FileName, string l_FolderPath = null)
    //{
    //    DBPath = l_FolderPath;
    //    if (DBPath != null)
    //        Directory.CreateDirectory(DBPath);
    //    DBPath = Path.Combine(DBPath, l_FileName + ".litedb");
    //    return new LiteDatabase($"Filename={DBPath}; Connection=shared");
    //}

    public BsonValue Add<T>(T reqModel, string l_TableNameorClassName = null)
    {
        if (l_TableNameorClassName == null)
            l_TableNameorClassName = typeof(T).Name;
       return _db.GetCollection<T>(l_TableNameorClassName).Insert(reqModel);
    }

    public bool Update<T>(T reqModel, string l_TableNameorClassName = null)
    {
        if (l_TableNameorClassName == null)
            l_TableNameorClassName = typeof(T).Name;
        return _db.GetCollection<T>(l_TableNameorClassName).Update(reqModel);

    }

    public bool Delete<T>(ObjectId Id, string l_TableNameorClassName = null)
    {
        if (l_TableNameorClassName == null)
            l_TableNameorClassName = typeof(T).Name;
       return _db.GetCollection<T>(l_TableNameorClassName).Delete(new BsonValue(Id));
    }

    public List<T> List<T>(string l_TableNameorClassName = null)
    {
        //if (l_TableNameorClassName == null)
        //    l_TableNameorClassName = typeof(T).Name;
        ILiteCollection<T> lst;
        if (l_TableNameorClassName != null)
            lst = _db.GetCollection<T>(l_TableNameorClassName);
        else
            lst = _db.GetCollection<T>();
        List<T> _list = lst.FindAll().ToList();
        return _list;
    }
    public T GetById<T>(Expression<Func<T, bool>> condition, string l_TableNameorClassName = null)
    {
        ILiteCollection<T> lst;
        if (l_TableNameorClassName != null)
            lst = _db.GetCollection<T>(l_TableNameorClassName);
        else
            lst = _db.GetCollection<T>();
        var item = lst.Find(condition).FirstOrDefault();
        return item;
    }
}

