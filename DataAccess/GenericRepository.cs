using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public abstract class GenericRepository<T> where T : class
{
    protected DbContext Context { get; set; }

    public IEnumerable<U> GetAll<U>() where U : class{
      return null;
    }
    
    public void Insert(T entity){}

    public void Update(T entity){}

    public T Get(int id){
      return null;
    }
    public void Save(){}

    public void Delete(T entity){}

    public bool CheckConnection(){
      return false;
    }
}