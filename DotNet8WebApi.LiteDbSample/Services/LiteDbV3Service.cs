using LiteDB;
using System.Linq.Expressions;

namespace DotNet8WebApi.LiteDbSample.Services;

public class LiteDbV3Service
{
    private readonly LiteDatabase _db;

    public LiteDbV3Service(LiteDatabase db)
    {
        _db = db;
    }

    public BsonValue Add<T>(T reqModel, string tableOrClassName)
    {
        if (tableOrClassName == null)
            tableOrClassName = typeof(T).Name;
       return _db.GetCollection<T>(tableOrClassName).Insert(reqModel);
    }

    public bool Update<T>(T reqModel, string tableOrClassName)
    {
        if (tableOrClassName == null)
            tableOrClassName = typeof(T).Name;
        return _db.GetCollection<T>(tableOrClassName).Update(reqModel);

    }

    public bool Delete<T>(ObjectId Id, string tableOrClassName)
    {
        if (tableOrClassName == null)
            tableOrClassName = typeof(T).Name;
       return _db.GetCollection<T>(tableOrClassName).Delete(new BsonValue(Id));
    }

    public List<T> List<T>(string tableOrClassName)
    {
        if (tableOrClassName == null)
            tableOrClassName = typeof(T).Name;
        ILiteCollection<T> lst;
        if (tableOrClassName != null)
            lst = _db.GetCollection<T>(tableOrClassName);
        else
            lst = _db.GetCollection<T>();
        List<T> _list = lst.FindAll().ToList();
        return _list;
    }

    public T GetById<T>(Expression<Func<T, bool>> condition, string tableOrClassName)
    {
        ILiteCollection<T> lst;
        if (tableOrClassName != null)
            lst = _db.GetCollection<T>(tableOrClassName);
        else
            lst = _db.GetCollection<T>();
        var item = lst.Find(condition).FirstOrDefault();
        return item;
    }
}

