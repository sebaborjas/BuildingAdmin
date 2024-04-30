using Domain;
using IDataAcess;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TicketService : ITicketService
    {
        private IGenericRepository<Ticket> _ticketRepository;
        private ISessionService _sessionService;

        public TicketService(IGenericRepository<Ticket> repository, ISessionService sessionService)
        {
            _ticketRepository = repository;
            _sessionService = sessionService;
        }

        public Ticket CreateTicket(Ticket ticket)
        {
            var currentUser = _sessionService.GetCurrentUser();
            ticket.CreatedBy = currentUser;
            _ticketRepository.Insert(ticket);
            return ticket;
        }

        public Ticket AssignTicket(int id, int maintenanceOperatorId)
        {
            throw new NotImplementedException();
        }

        public Ticket CompleteTicket(int id, float totalCost)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> GetTickets(string category = null)
        {
            throw new NotImplementedException();
        }

        public Ticket StartTicket(int id)
        {
            throw new NotImplementedException();
        }
    }
}
