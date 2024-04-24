namespace Services;
using IServices;
using Domain;

public class AdministratorService : IService<Administrator>
{
  public int Create(Administrator entity)
  {
    return 3;
  }
}
