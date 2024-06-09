using IServices;
using Domain;
using IDataAccess;
using DTO.In;
using System;

namespace Services;

public class BuildingService : IBuildingService
{

    private IGenericRepository<Building> _buildingRepository;
    private ISessionService _sessionService;
    private IGenericRepository<Owner> _ownerRepository;
    private IGenericRepository<Manager> _managerRepository;
    private IGenericRepository<ConstructionCompany> _constructionCompany;

    public BuildingService(IGenericRepository<Building> buildingRepository, ISessionService sessionService, IGenericRepository<Owner> ownerRepository, IGenericRepository<Manager> managerRepository, IGenericRepository<ConstructionCompany> constructionCompany)
    {
        _buildingRepository = buildingRepository;
        _sessionService = sessionService;
        _ownerRepository = ownerRepository;
        _managerRepository = managerRepository;
        _constructionCompany = constructionCompany;

    }

    public Building CreateBuilding(Building building, string? managerEmail = null)
    {
        var currentUser = _sessionService.GetCurrentUser() as CompanyAdministrator;
        if (currentUser == null)
        {
            throw new InvalidOperationException("Current user is not authorized to create a building");
        }

        if (currentUser.ConstructionCompany == null)
        {
            throw new InvalidOperationException("Company administrator does not have a construction company");
        }

        var buildingAlreadyExist = _buildingRepository.GetByCondition(b => b.Name == building.Name || b.Address == building.Address || b.Location == building.Location);

        if (buildingAlreadyExist != null)
        {
            throw new ArgumentException("Building already exist");
        }
        if (!IsValidCreateBuilding(building))
        {
            throw new ArgumentException("Invalid building");
        }
        if (building.Apartments == null || building.Apartments.Count == 0)
        {
            throw new ArgumentException("Building must have at least one apartment");
        }

        try
        {
            building.ConstructionCompany = currentUser.ConstructionCompany;
            SetApartmentsExistingOwnersByEmail(building.Apartments);
            _buildingRepository.Insert(building);
            var manager = _managerRepository.GetByCondition(m => m.Email == managerEmail);
            if(manager != null)
            {
                manager.Buildings.Add(building);
                _managerRepository.Update(manager);
            }
            return building;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error creating building", e);
        }

    }

    public void DeleteBuilding(int buildingId)
    {
        var buildings = GetAllBuildingsForCCompany();
        var building = buildings.FirstOrDefault(b=>b.Id == buildingId);
        if (building == null)
        {
            throw new ArgumentNullException("Building not found");
        }

        try
        {
            _buildingRepository.Delete(building);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error deleting building");
        }

    }

    public Building ModifyBuilding(int buildingId, Building modifiedBuilding)
    {
        var currentUser = _sessionService.GetCurrentUser() as CompanyAdministrator;
        if (currentUser == null)
        {
            throw new InvalidOperationException("Current user is not a CompanyAdministrator");
        }

        var building = _buildingRepository.GetByCondition(b => b.Id == buildingId);

        if (building == null)
        {
            throw new ArgumentNullException("Building not found");
        }

        if (modifiedBuilding.Expenses > 0)
        {
            building.Expenses = modifiedBuilding.Expenses;
        }

        try
        {
            ModifyApartments(building.Apartments, modifiedBuilding.Apartments);
            _buildingRepository.Update(building);

            return building;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error modifying building");
        }
    }

    public List<Building> GetAllBuildingsForUser()
    {
        try
        {
            var currentUser = _sessionService.GetCurrentUser() as Manager;
            var buildings = currentUser.Buildings;
            return buildings;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error getting buildings");
        }
    }

    public List<Building> GetAllBuildingsForCCompany()
    {
        var currentUser = _sessionService.GetCurrentUser() as CompanyAdministrator;
        if (currentUser == null)
        {
            throw new InvalidOperationException("Current user is not a company administrator");
        }
        var constructionCompany = currentUser.ConstructionCompany;
        if (constructionCompany == null)
        {
            throw new InvalidOperationException("Current user does not have a construction company");
        }

        try
        {
            var allBuildings = _buildingRepository.GetAll<Building>();
            var buildings = allBuildings.Where(b => b.ConstructionCompany.Name == constructionCompany.Name).ToList();

            return buildings;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error getting buildings", e);
        }
    }

    public Building Get(int id)
    {
        var currentUser = _sessionService.GetCurrentUser() as CompanyAdministrator;
        if (currentUser == null)
        {
            throw new InvalidOperationException("Current user is not a company administrator");
        }

        try
        {
            var building = _buildingRepository.GetByCondition(b => b.Id == id);
            return building;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error getting building");
        }
    }

    public string? GetManagerName(int buildingId)
    {
        var manager = _managerRepository.GetByCondition(m => m.Buildings.Any(b => b.Id == buildingId));
        if (manager == null)
        {
            return null;
        }
        return manager.Name;
    }

    public void ChangeBuildingManager(int buildingId, int managerId)
    {
        var currentUser = _sessionService.GetCurrentUser() as CompanyAdministrator;
        if (currentUser == null)
        {
            throw new InvalidOperationException("Current user is not a company administrator");
        }

        var building = _buildingRepository.GetByCondition(b => b.Id == buildingId);
        if (building == null)
        {
            throw new ArgumentNullException("Building not found");
        }

        var manager = _managerRepository.GetByCondition(m => m.Id == managerId);
        if (manager == null)
        {
            throw new ArgumentNullException("Manager not found");
        }

        var lastManager = _managerRepository.GetByCondition(m => m.Buildings.Any(b => b.Id == buildingId));

        if (lastManager != null)
        {
            removeBuildingFromManager(building, lastManager);
        }
        assignBuildingToManager(building, manager);
    }

    private void ModifyApartments(List<Apartment> originalApartments, List<Apartment> modifiedApartments)
    {
        modifiedApartments.ForEach(modifiedApartment =>
        {
            var originalApartment = originalApartments.Find(apartment => apartment.Id == modifiedApartment.Id);
            if (originalApartment != null)
            {
                var ownerToModify = _ownerRepository.GetByCondition(owner => owner.Email == modifiedApartment.Owner.Email);
                if (ownerToModify == null)
                {
                    ownerToModify = modifiedApartment.Owner;
                    _ownerRepository.Insert(ownerToModify);
                }
                originalApartment.Owner = ownerToModify;
            }
        });
    }

    private void assignBuildingToManager(Building building, Manager manager)
    {
        try
        {
            manager.Buildings.Add(building);
            _managerRepository.Update(manager);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error assigning building to manager", e);
        }
    }

    private void removeBuildingFromManager(Building building, Manager manager)
    {
        try
        {
            manager.Buildings.Remove(building);
            _managerRepository.Update(manager);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error removing building from manager", e);
        }
    }

    private void SetApartmentsExistingOwnersByEmail(List<Apartment> apartments)
    {
        try
        {
            apartments.ForEach(apartment =>
            {
                var apartmentOwner = apartment.Owner;
                var existingOwner = _ownerRepository.GetByCondition(owner => owner.Email == apartmentOwner.Email);
                if (existingOwner != null)
                {
                    apartment.Owner = existingOwner;
                }
                else
                {
                    _ownerRepository.Insert(apartmentOwner);
                }
            });
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error setting existing owners", e);
        }
    }

    private bool IsValidCreateBuilding(Building building)
    {
        return building != null && !string.IsNullOrWhiteSpace(building.Name) && !string.IsNullOrWhiteSpace(building.Address) &&
                !string.IsNullOrWhiteSpace(building.Location) && building.Expenses >= 0;
    }
}