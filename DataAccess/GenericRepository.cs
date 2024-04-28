using IDataAcess;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess;

[ExcludeFromCodeCoverage]
public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected DbContext Context { get; set; }
    
    public void Insert(T entity)
    {
        Context.Set<T>().Add(entity);
        Save();
    }

    public void Update(T entity)
    {
        Context.Set<T>().Update(entity);
    }

    public T Get(int id){
      return Context.Set<T>().Find(id);
    }

    public IEnumerable<U> GetAll<U>() where U : class{
      return Context.Set<U>().ToList();
    }

    public void Save()
    {
        Context.SaveChanges();
    }

    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
        Save();
    }

    public bool CheckConnection(){
      return Context.Database.CanConnect();
    }
}