using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IReportServices
    {
        Dictionary<TKey, TValue> GetTicketsByBuilding<TKey, TValue>(int? id = null);

        Dictionary<TKey, TValue> GetTicketsByMaintenanceOperator<TKey, TValue>(int? id = null);
    }
}
