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
        private IGenericRepository<MaintenanceOperator> _maintenanceRepository;
        private ISessionService _sessionService;

        public TicketService(IGenericRepository<Ticket> repository, ISessionService sessionService, IGenericRepository<Category> categoryRepository, IGenericRepository<MaintenanceOperator> maintenanceRepository)
        {
            _ticketRepository = repository;
            _sessionService = sessionService;
            _categoryRepository = categoryRepository;
            _maintenanceRepository = maintenanceRepository;
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

        public Ticket AssignTicket(int ticketId, int maintenanceOperatorId)
        {
            Manager currentUser = (Manager)_sessionService.GetCurrentUser();
            var ticket = _ticketRepository.Get(ticketId);
            if(ticket == null || ticket.Status != Domain.DataTypes.Status.Open)
            {
                return null;
            }

            var ticketBuilding = currentUser.Buildings.Find(building => building.Apartments.Contains(ticket.Apartment));
            if (ticketBuilding == null)
            {
                return null;
            }

            var maintenance = _maintenanceRepository.Get(maintenanceOperatorId);
            if(maintenance == null || !maintenance.Building.Equals(ticketBuilding))
            {
                return null;
            }

            ticket.AssignedTo = maintenance;
            _ticketRepository.Update(ticket);
            return ticket;
        }

        public Ticket CompleteTicket(int ticketId, float totalCost)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> GetTickets(string category = null)
        {
            throw new NotImplementedException();
        }

        public Ticket StartTicket(int id)
        {
            MaintenanceOperator currentUser = (MaintenanceOperator)_sessionService.GetCurrentUser();
            var ticket = _ticketRepository.Get(id);
            if(ticket == null || !currentUser.Equals(ticket.AssignedTo) || ticket.Status != Domain.DataTypes.Status.Open)
            {
                return null;
            }
            ticket.AttendTicket();
            _ticketRepository.Update(ticket);
            return ticket;
        }
    }
}
