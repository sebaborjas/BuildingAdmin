using IServices;
using Domain;
using IDataAccess;

namespace Services;

public class UserService : IUserServices
{
    private IGenericRepository<Administrator> _adminRepository;
    private IGenericRepository<MaintenanceOperator> _operatorRepository;
    private IGenericRepository<Manager> _managerRepository;
    private IGenericRepository<CompanyAdministrator> _companyRepository;
    private ISessionService _sessionService;

    public UserService(IGenericRepository<Administrator> adminRepository, IGenericRepository<MaintenanceOperator> operatorRepository, IGenericRepository<Manager> managerRepository, ISessionService sessionService, IGenericRepository<CompanyAdministrator> companyRepository)
    {
        _adminRepository = adminRepository;
        _operatorRepository = operatorRepository;
        _managerRepository = managerRepository;
        _sessionService = sessionService;
        _companyRepository = companyRepository;
    }

    public Administrator CreateAdministrator(Administrator administrator)
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
        if (userAlreadyExist == null)
        {
            userAlreadyExist = _companyRepository.GetByCondition(user => user.Email == administrator.Email);
        }

        if (userAlreadyExist != null)
        {
            throw new ArgumentException("User already exist");
        }
        try
        {
            _adminRepository.Insert(administrator);
            return administrator;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error creating administrator", e);
        }
    }

    public MaintenanceOperator CreateMaintenanceOperator(MaintenanceOperator maintenanceOperator)
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
        if (userAlreadyExist == null)
        {
            userAlreadyExist = _companyRepository.GetByCondition(user => user.Email == maintenanceOperator.Email);
        }

        if (userAlreadyExist != null)
        {
            throw new ArgumentException("User already exist");
        }

        List<Building> buildings = new List<Building>();
        foreach (var buildingToAdd in maintenanceOperator.Buildings)
        {
            var operatorBuilding = ((Manager)_sessionService.GetCurrentUser()).Buildings.Find(building => building.Id == buildingToAdd.Id);
            if (operatorBuilding == null)
            {
                throw new ArgumentException("Invalid building");
            };
            buildings.Add(operatorBuilding);
        }
        try
        {
            maintenanceOperator.Buildings = buildings;
            _operatorRepository.Insert(maintenanceOperator);
            return maintenanceOperator;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error creating maintenance operator", e);
        }
    }

    public void DeleteManager(int id)
    {

        Manager managerToDelete = _managerRepository.Get(id);

        if (managerToDelete == null)
        {
            throw new ArgumentNullException("Manager not found");
        }
        try
        {
            _managerRepository.Delete(managerToDelete);

        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error deleting manager", e);
        }
    }

    public CompanyAdministrator CreateCompanyAdministrator(CompanyAdministrator companyAdministrator)
    {
        var currentUser = _sessionService.GetCurrentUser() as CompanyAdministrator;
        if (currentUser == null)
        {
            throw new InvalidOperationException("Current user is not authorized to create a company administrator");
        }
        if (!IsNewCompanyAdministratorValid(companyAdministrator))
        {
            throw new ArgumentException("Invalid company administrator data");
        }
        if (IsUserAlreadyExist(companyAdministrator))
        {
            throw new ArgumentException("User already exist");
        }
        if (currentUser.ConstructionCompany == null)
        {
            throw new InvalidOperationException("Current user does not have a construction company");
        }
        try
        {
            companyAdministrator.ConstructionCompany = currentUser.ConstructionCompany;
            _companyRepository.Insert(companyAdministrator);
            return companyAdministrator;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error creating company administrator", e);
        }
    }

    public List<Manager> GetManagers()
    {
        return _managerRepository.GetAll<Manager>().ToList();
    }

    public List<CompanyAdministrator> GetCompanyAdministrators()
    {
        var currentUser = _sessionService.GetCurrentUser() as CompanyAdministrator;
        if (currentUser.ConstructionCompany == null)
        {
            throw new InvalidOperationException("Current user does not have a construction company");
        }
        var userCompany = currentUser.ConstructionCompany;
        var companyAdministrators = _companyRepository.GetAll<CompanyAdministrator>().ToList();
        return companyAdministrators.Where(companyAdministrator => companyAdministrator.ConstructionCompany.Id == userCompany.Id).ToList();
    }

    public List<MaintenanceOperator> GetMaintenanceOperators()
    {
        var currentUser = _sessionService.GetCurrentUser() as Manager;
        if (currentUser == null)
        {
            throw new InvalidOperationException("Current user is not a manager");
        }
        var buildings = currentUser.Buildings;
        if (buildings.Count == 0)
        {
            throw new InvalidOperationException("Manager does not have buildings");
        }
        var maintenanceOperators = _operatorRepository.GetAll<MaintenanceOperator>().ToList();
        
        var result = new List<MaintenanceOperator>();
        foreach (var maintenanceOperator in maintenanceOperators)
        {
            foreach (var building in buildings)
            {
                if (maintenanceOperator.Buildings.Contains(building))
                {
                    result.Add(maintenanceOperator);
                    break;
                }
            }
        }
        maintenanceOperators = maintenanceOperators.Where(maintenanceOperator => maintenanceOperator.Buildings.Any(building => buildings.Any(managerBuilding => managerBuilding.Id == building.Id))).ToList();


        return result;
    }

    private bool IsNewAdministratorValid(Administrator administrator)
    {
        return administrator != null && administrator.Name != null && administrator.LastName != null && administrator.Email != null && administrator.Password != null;
    }

    private bool IsNewMaintenanceOperatorValid(MaintenanceOperator maintenanceOperator)
    {
        return maintenanceOperator != null && maintenanceOperator.Name != null && maintenanceOperator.LastName != null && maintenanceOperator.Email != null && maintenanceOperator.Password != null;
    }
    private bool IsNewCompanyAdministratorValid(CompanyAdministrator companyAdministrator)
    {
        return companyAdministrator != null && companyAdministrator.Name != null && companyAdministrator.LastName != null && companyAdministrator.Email != null && companyAdministrator.Password != null;
    }

    private bool IsUserAlreadyExist(User newUser)
    {
        User userAlreadyExist = null;
        userAlreadyExist = _adminRepository.GetByCondition(user => user.Email == newUser.Email);
        if (userAlreadyExist == null)
        {
            userAlreadyExist = _managerRepository.GetByCondition(user => user.Email == newUser.Email);
        }
        if (userAlreadyExist == null)
        {
            userAlreadyExist = _operatorRepository.GetByCondition(user => user.Email == newUser.Email);
        }
        if (userAlreadyExist == null)
        {
            userAlreadyExist = _companyRepository.GetByCondition(user => user.Email == newUser.Email);
        }
        return userAlreadyExist != null;
    }
}

