using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace TestDataAccess;

public abstract class GenericRepository<T> where T : class
{
    protected DbContext Context { get; set; }

    IEnumerable<U> GetAll<U>() where U : class{
      return null;
    }
    
    void Insert(T entity){}

    void Update(T entity){}

    T Get(int id){
      return null;
    }
    void Save(){}

    void Delete(T entity){}

    bool CheckConnection(){
      return false;
    }
}