using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IReportServices
    {
        Dictionary<TKey, TValue> GetRequestsByBuilding<TKey, TValue>(int? id = null);
    }
}
