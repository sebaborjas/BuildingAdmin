using IServices;
using Domain;
using IDataAccess;

namespace Services;

public class UserService : IUserServices
{
    private IGenericRepository<Administrator> _adminRepository;
    private IGenericRepository<MaintenanceOperator> _operatorRepository;
    private IGenericRepository<Manager> _managerRepository;
    private ISessionService _sessionService;

    public UserService(IGenericRepository<Administrator> adminRepository, IGenericRepository<MaintenanceOperator> operatorRepository, IGenericRepository<Manager> managerRepository, ISessionService sessionService)
    {
        _adminRepository = adminRepository;
        _operatorRepository = operatorRepository;
        _managerRepository = managerRepository;
        _sessionService = sessionService;
    }

    public Administrator CreateAdministrator(Administrator administrator)
    {
        try
        {
            if (!IsNewAdministratorValid(administrator))
            {
                throw new ArgumentException("Invalid administrator data");
            }
            User userAlreadyExist = null;
            userAlreadyExist = _adminRepository.GetByCondition(user => user.Email == administrator.Email);
            if (userAlreadyExist == null)
            {
                userAlreadyExist = _managerRepository.GetByCondition(user => user.Email == administrator.Email);
            }
            if (userAlreadyExist == null)
            {
                userAlreadyExist = _operatorRepository.GetByCondition(user => user.Email == administrator.Email);
            }

            if (userAlreadyExist != null)
            {
                throw new ArgumentException("User already exist");
            }

            _adminRepository.Insert(administrator);
            return administrator;
        }
        catch (Exception e)
        {
            throw new Exception("Error creating administrator", e);
        }
    }

    public MaintenanceOperator CreateMaintenanceOperator(MaintenanceOperator maintenanceOperator)
    {
        try
        {
            if (!IsNewMaintenanceOperatorValid(maintenanceOperator))
            {
                throw new ArgumentException("Invalid maintenance operator data");
            }
            User userAlreadyExist = null;
            userAlreadyExist = _adminRepository.GetByCondition(user => user.Email == maintenanceOperator.Email);
            if (userAlreadyExist == null)
            {
                userAlreadyExist = _managerRepository.GetByCondition(user => user.Email == maintenanceOperator.Email);
            }
            if (userAlreadyExist == null)
            {
                userAlreadyExist = _operatorRepository.GetByCondition(user => user.Email == maintenanceOperator.Email);
            }


            if (userAlreadyExist != null)
            {
                throw new ArgumentException("User already exist");
            }
            var operatorBuilding = ((Manager)_sessionService.GetCurrentUser()).Buildings.Find(building => building.Id == maintenanceOperator.Building.Id);
            if (operatorBuilding == null)
            {
                throw new ArgumentException("Invalid building");
            };

            maintenanceOperator.Building = operatorBuilding;
            _operatorRepository.Insert(maintenanceOperator);
            return maintenanceOperator;
        }
        catch (Exception e)
        {
            throw new Exception("Error creating maintenance operator", e);
        }

    }

    public void DeleteManager(int id)
    {
        try
        {
            Manager managerToDelete = _managerRepository.Get(id);

            if (managerToDelete == null)
            {
                throw new ArgumentNullException("Manager not found");
            }
            _managerRepository.Delete(managerToDelete);

        }
        catch (Exception e)
        {
            throw new Exception("Error deleting manager", e);
        }
    }

    private bool IsNewAdministratorValid(Administrator administrator)
    {
        return administrator != null && administrator.Name != null && administrator.LastName != null && administrator.Email != null && administrator.Password != null;
    }

    private bool IsNewMaintenanceOperatorValid(MaintenanceOperator maintenanceOperator)
    {
        return maintenanceOperator != null && maintenanceOperator.Name != null && maintenanceOperator.LastName != null && maintenanceOperator.Email != null && maintenanceOperator.Password != null;
    }
}
