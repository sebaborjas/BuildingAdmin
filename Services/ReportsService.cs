using IDataAccess;
using IServices;

namespace Services;

public class ReportsService : IReportServices
{
    public Dictionary<TKey, TValue> GetRequestsByBuilding<TKey, TValue>(int? id = null)
    {
        throw new NotImplementedException();
    }
}