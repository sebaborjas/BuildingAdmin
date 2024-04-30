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
        private IGenericRepository<Category> _categoryRepository;
        private ISessionService _sessionService;

        public TicketService(IGenericRepository<Ticket> repository, ISessionService sessionService, IGenericRepository<Category> categoryRepository)
        {
            _ticketRepository = repository;
            _sessionService = sessionService;
            _categoryRepository = categoryRepository;
        }

        public Ticket CreateTicket(Ticket ticket)
        {
            var categoryId = ticket.Category.Id;
            var apartmentId = ticket.Apartment.Id;
            var currentUser = _sessionService.GetCurrentUser();
            
            var newTicketCategory = _categoryRepository.Get(categoryId);
            if(newTicketCategory == null )
            {
                throw new InvalidDataException("Invalid category id");
            };
            
            var apartmentBuilding = ((Manager)currentUser).Buildings.Where(building=>building.Apartments.Any(apartment => apartment.Id == apartmentId)).FirstOrDefault();
            if(apartmentBuilding == null)
            {
                throw new InvalidDataException("Invalid apartment id");
            }
            var newTicketApartment = apartmentBuilding.Apartments.Find(apartment => apartment.Id == apartmentId);
            
            ticket.CreatedBy = currentUser;
            ticket.Apartment = newTicketApartment;
            ticket.Category = newTicketCategory;

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
