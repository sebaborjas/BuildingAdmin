namespace IServices;

public interface IService<T> where T : class
{
  public T Create(T entity);
}
