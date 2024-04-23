using Domain;
using IDataAcess;

namespace Services;

public class AdministratorService
{
  private readonly IGenericRepository<Administrator> _repository;

  public AdministratorService (IGenericRepository<Administrator> repository)
  {
    _repository = repository;
  }

  public void Insert(Administrator administrator)
  {
    _repository.Insert(administrator);
    _repository.Save();
  }
}