using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace IServices
{
    public interface ITicketService
    {
        Ticket CreateTicket(Ticket ticket);

        List<Ticket> GetTickets(string category = null);

        Ticket AssignTicket(int id, MaintenanceOperator maintenanceOperator);



    }
}
