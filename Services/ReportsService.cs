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
        throw new NotImplementedException();
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

