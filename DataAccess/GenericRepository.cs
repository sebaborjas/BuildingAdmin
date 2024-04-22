using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public abstract class GenericRepository<T> where T : class
{
    protected DbContext Context { get; set; }
    
    public void Insert(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public void Update(T entity){}

    public T Get(int id){
      return Context.Set<T>().Find(id);
    }

    public IEnumerable<U> GetAll<U>() where U : class{
      return Context.Set<U>().ToList();
    }

    public void Save(){}

    public void Delete(T entity){}

    public bool CheckConnection(){
      return false;
    }
}