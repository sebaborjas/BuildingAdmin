﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IReportServices
    {
        ICollection<TicketByBuilding> GetTicketsByBuilding(int? id = null);

        ICollection<TicketsByMaintenanceOperator> GetTicketsByMaintenanceOperator(int? id = null);
        
        ICollection<TicketsByCategory> GetTicketsByCategory();
    }

    public struct TicketByBuilding
    {
        public string BuildingName { get; set; }
        public int TicketsOpen { get; set; }
        public int TicketsInProgress { get; set; }
        public int TicketsClosed { get; set; }
    }

    public struct TicketsByMaintenanceOperator
    {
        public string OperatorName { get; set; }
        public int TicketsOpen { get; set; }
        public int TicketsInProgress { get; set; }
        public int TicketsClosed { get; set; }
        public  float AverageTimeToClose { get; set; }
    }

    public struct TicketsByCategory
    {
    }
}
