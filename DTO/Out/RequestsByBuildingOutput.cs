using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Out
{
    public class RequestsByBuildingOutput
    {
        public string BuildingName {  get; set; }
        public short OpenTickets { get; set; }
        public short CloseTickets { get; set; }
        public short WorkingTickets { get; set; }
    }
}
