using IDataAccess;
using IServices;

namespace Services;

public class ReportsService : IReportServices
{
    public Dictionary<TKey, TValue> GetTicketsByBuilding<TKey, TValue>(int? id = null)
    {
        throw new NotImplementedException();
    }

    public Dictionary<TKey, TValue> GetTicketsByMaintenanceOperator<TKey, TValue>(int? id = null)
    {
        throw new NotImplementedException();
    }
}