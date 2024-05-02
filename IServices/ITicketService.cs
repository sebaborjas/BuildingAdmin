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

        List<Ticket> GetAssignedTickets();

        Ticket AssignTicket(int ticketId, int maintenanceOperatorId);

        Ticket StartTicket(int ticketId);

        Ticket CompleteTicket(int ticketId, float totalCost);
    }
}
