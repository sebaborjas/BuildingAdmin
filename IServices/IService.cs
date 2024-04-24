namespace IServices;

public interface IService<T> where T : class
{
  public int Create(T entity);
}
