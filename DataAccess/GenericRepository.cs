using IDataAcess;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

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

    public virtual T GetByCondition(Expression<Func<T, bool>> searchCondition, List<string> includes = null)
    {
        IQueryable<T> query = Context.Set<T>();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return query.Where(searchCondition).Select(x => x)
            .FirstOrDefault();
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