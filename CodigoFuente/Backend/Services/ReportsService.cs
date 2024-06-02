using IDataAccess;
using IServices;
using Domain;

namespace Services;

public class ReportsService : IReportServices
{
    private readonly IGenericRepository<Building> _buildingRepository;

    private readonly ISessionService _sessionService;

    public ReportsService(IGenericRepository<Building> buildingRepository, ISessionService sessionService)
    {
        _buildingRepository = buildingRepository;
        _sessionService = sessionService;
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
                tickets = tickets.Where(t => t.AssignedTo?.Name == operatorName).ToList();

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
                var ticketsAgrupedByOperator = tickets.GroupBy(t => t.AssignedTo?.Name);

                foreach (var ticketGroup in ticketsAgrupedByOperator)
                {
                    var reportData = new TicketsByMaintenanceOperator
                    {
                        OperatorName = ticketGroup.Key,
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
       return new List<TicketByApartment>();
    }

}

