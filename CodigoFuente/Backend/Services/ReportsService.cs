using IDataAccess;
using IServices;
using Domain;

namespace Services;

public class ReportsService : IReportServices
{
    private readonly IGenericRepository<Building> _buildingRepository;
    private readonly IGenericRepository<MaintenanceOperator> _maintenanceOperatorRepository;

    private readonly ISessionService _sessionService;

    public ReportsService(IGenericRepository<Building> buildingRepository, ISessionService sessionService, IGenericRepository<MaintenanceOperator> maintenanceOperatorRepository)
    {
        _buildingRepository = buildingRepository;
        _sessionService = sessionService;
        _maintenanceOperatorRepository = maintenanceOperatorRepository;
    }

    public ICollection<TicketByBuilding> GetTicketsByBuilding(string? name = null)
    {
        try
        {
            Manager manager = _sessionService.GetCurrentUser() as Manager;

            var buildings = manager.Buildings.ToList();

            var ticketDataList = new List<TicketByBuilding>();

            if (name != null)
            {
                buildings = buildings.Where(b => b.Name == name).ToList();
            }

            foreach (var building in buildings)
            {
                var ticketData = new TicketByBuilding
                {
                    BuildingName = building.Name,
                    TicketsOpen = building.Tickets.Count(t => t.Status == Domain.DataTypes.Status.Open),
                    TicketsInProgress = building.Tickets.Count(t => t.Status == Domain.DataTypes.Status.InProgress),
                    TicketsClosed = building.Tickets.Count(t => t.Status == Domain.DataTypes.Status.Closed)

                };

                ticketDataList.Add(ticketData);
            }
            return ticketDataList;

        }
        catch (Exception)
        {
            throw new InvalidOperationException("Error getting tickets by building");
        }
    }

    public ICollection<TicketsByMaintenanceOperator> GetTicketsByMaintenanceOperator(string buildingName, string? operatorName = null)
    {
        try
        {
            Manager manager = _sessionService.GetCurrentUser() as Manager;

            var buildings = manager.Buildings.ToList();

            var building = buildings.FirstOrDefault(b => b.Name == buildingName);

            var tickets = building.Tickets;
            
            var result = new List<TicketsByMaintenanceOperator>();

            if (operatorName != null)
            {
                var maintenanceOperator = _maintenanceOperatorRepository.GetByCondition(o => o.Name == operatorName);
                tickets = tickets.Where(t => t.IdOperatorAssigned != null && t.IdOperatorAssigned == maintenanceOperator.Id).ToList();

                var reportData = new TicketsByMaintenanceOperator
                {
                    OperatorName = operatorName,
                    TicketsOpen = tickets.Count(t => t.Status == Domain.DataTypes.Status.Open),
                    TicketsInProgress = tickets.Count(t => t.Status == Domain.DataTypes.Status.InProgress),
                    TicketsClosed = tickets.Count(t => t.Status == Domain.DataTypes.Status.Closed),
                    AverageTimeToClose = CalculateAverage(tickets.Where(t => t.Status == Domain.DataTypes.Status.Closed).ToList()).ToString(@"hh\:mm")
                };

                result.Add(reportData);
            }
            else
            {
                var operators = _maintenanceOperatorRepository.GetAll<MaintenanceOperator>();
                var ticketsAgrupedByOperator = tickets.GroupBy(t => t.IdOperatorAssigned);

                foreach (var ticketGroup in ticketsAgrupedByOperator.Where(ticket=>ticket.Key != null))
                {
                    var reportData = new TicketsByMaintenanceOperator
                    {
                        OperatorName = operators.FirstOrDefault(mOperator=>mOperator.Id == ticketGroup.Key)?.Name,
                        TicketsOpen = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.Open),
                        TicketsInProgress = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.InProgress),
                        TicketsClosed = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.Closed),
                        AverageTimeToClose = CalculateAverage(ticketGroup.Where(t => t.Status == Domain.DataTypes.Status.Closed).ToList()).ToString(@"hh\:mm")
                    };

                    result.Add(reportData);
                }

            }
            return result;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Error getting tickets by maintenance operator");
        }
    }

    public ICollection<TicketsByCategory> GetTicketsByCategory(string buildingName, string? categoryName = null)
    {
        try
        {
            Building building = _buildingRepository.GetByCondition(b => b.Name == buildingName);
            var tickets = building.Tickets;

            var result = new List<TicketsByCategory>();

            if (categoryName != null)
            {
                var ticketsOfCategoryName = tickets.Where(t => t.Category.Name == categoryName).ToList();
                var ticketsAgrupedByCategory = ticketsOfCategoryName.GroupBy(t => t.Category.Name);

                foreach (var ticketGroup in ticketsAgrupedByCategory)
                {
                    var reportData = new TicketsByCategory
                    {
                        CategoryName = ticketGroup.Key,
                        TicketsOpen = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.Open),
                        TicketsInProgress = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.InProgress),
                        TicketsClosed = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.Closed)
                    };

                    result.Add(reportData);
                }
            }
            else
            {
                var ticketsAgrupedByCategory = tickets.GroupBy(t => t.Category.Name);

                foreach (var ticketGroup in ticketsAgrupedByCategory)
                {
                    var reportData = new TicketsByCategory
                    {
                        CategoryName = ticketGroup.Key,
                        TicketsOpen = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.Open),
                        TicketsInProgress = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.InProgress),
                        TicketsClosed = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.Closed)
                    };

                    result.Add(reportData);
                }
            }
            return result;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Error getting tickets by category");
        }
    }

    private TimeSpan CalculateAverage(List<Ticket> tickets)
    {
        DateTime totalHours = new DateTime(0);
        int count = 0;
        foreach (var ticket in tickets)
        {
            totalHours += ticket.ClosingDate - ticket.AttentionDate;
            count++;
        }

        if (count == 0) return new TimeSpan(0);

        TimeSpan result = new TimeSpan(totalHours.Ticks / count);
        return result;
    }

    public ICollection<TicketByApartment> GetTicketsByApartment(string buildingName)
    {
        var currentUser = (Manager)_sessionService.GetCurrentUser();
        try
        {
            var building = currentUser.Buildings.Where(userBuilding=> userBuilding.Name == buildingName).FirstOrDefault();
            if(building == null)
            {
                throw new KeyNotFoundException("Building not found");
            }
            var result = new List<TicketByApartment>();

            var apartments = building.Apartments;
            foreach (var apartment in apartments)
            {
                var tickets = building.Tickets.Where(ticket=>ticket.Apartment == apartment).ToList();
                result.Add(new TicketByApartment
                {
                    ApartmentAndOwner = $"{apartment.DoorNumber} - {apartment.Owner.Name} {apartment.Owner.LastName}",
                    TicketsOpen = tickets.Where(ticket => ticket.Status == Domain.DataTypes.Status.Open).Count(),
                    TicketsInProgress = tickets.Where(ticket => ticket.Status == Domain.DataTypes.Status.InProgress).Count(),
                    TicketsClosed = tickets.Where(ticket => ticket.Status == Domain.DataTypes.Status.Closed).Count()
                });
            }
            return result;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Error getting tickets by apartment");
        }
    }

}

