using IDataAcess;
using IServices;
using Domain;

namespace Services;

public class ReportsService : IReportServices
{
    private readonly IGenericRepository<Ticket> _ticketRepository;
    private readonly IGenericRepository<Building> _buildingRepository;
    private readonly IGenericRepository<MaintenanceOperator> _maintenanceOperatorRepository;

    public ReportsService(IGenericRepository<Ticket> ticketRepository, IGenericRepository<Building> buildingRepository, IGenericRepository<MaintenanceOperator> maintenanceOperatorRepository)
    {
        _ticketRepository = ticketRepository;
        _buildingRepository = buildingRepository;
        _maintenanceOperatorRepository = maintenanceOperatorRepository;
    }

    public ICollection<TicketByBuilding> GetTicketsByBuilding(int? id = null)
    {
        var buildings = _buildingRepository.GetAll<Building>();
        var ticketDataList = new List<TicketByBuilding>();

        if (id != null)
        {
            buildings = buildings.Where(b => b.Id == id);
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

    public ICollection<TicketsByMaintenanceOperator> GetTicketsByMaintenanceOperator(int? id = null)
    {
        throw new NotImplementedException();
    }

    public ICollection<TicketsByCategory> GetTicketsByCategory()
    {
        throw new NotImplementedException();
    }


}

