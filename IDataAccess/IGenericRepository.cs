namespace IDataAcess;

public interface IGenericRepository<T> where T : class
{
  void Insert(T entity);

  void Update(T entity);

  T Get(int id);

  IEnumerable<U> GetAll<U>() where U : class;

  public void Save();

  public void Delete(T entity);

  public bool CheckConnection();
}