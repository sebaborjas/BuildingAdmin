using Domain;
using IDataAccess;
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
        private IGenericRepository<Building> _buildingRepository;
        private ISessionService _sessionService;

        public TicketService(IGenericRepository<Ticket> repository, ISessionService sessionService, IGenericRepository<Category> categoryRepository, IGenericRepository<MaintenanceOperator> maintenanceRepository, IGenericRepository<Building> buildingRepository)
        {
            _ticketRepository = repository;
            _sessionService = sessionService;
            _categoryRepository = categoryRepository;
            _maintenanceRepository = maintenanceRepository;
            _buildingRepository = buildingRepository;
        }

        public Ticket CreateTicket(Ticket ticket)
        {
            try
            {
                if (!IsValidCreateTicket(ticket))
                {
                    throw new InvalidDataException("Invalid ticket data");
                }
                var categoryId = ticket.Category.Id;
                var apartmentId = ticket.Apartment.Id;
                var currentUser = _sessionService.GetCurrentUser();

                var newTicketCategory = _categoryRepository.Get(categoryId);
                if (newTicketCategory == null)
                {
                    throw new InvalidDataException("Invalid category id");
                };

                var apartmentBuilding = ((Manager)currentUser).Buildings.Where(building => building.Apartments.Any(apartment => apartment.Id == apartmentId)).FirstOrDefault();
                if (apartmentBuilding == null)
                {
                    throw new InvalidDataException("Invalid apartment id");
                }

                var newTicketApartment = apartmentBuilding.Apartments.Find(apartment => apartment.Id == apartmentId);

                ticket.CreatedBy = currentUser as Manager;
                ticket.Apartment = newTicketApartment;
                ticket.Category = newTicketCategory;

                _ticketRepository.Insert(ticket);

                apartmentBuilding.Tickets.Add(ticket);
                _buildingRepository.Update(apartmentBuilding);

                return ticket;
            }
            catch (Exception e)
            {
                throw new Exception("Error creating ticket", e);
            }
        }

        public Ticket AssignTicket(int ticketId, int maintenanceOperatorId)
        {
            try
            {
                Manager currentUser = (Manager)_sessionService.GetCurrentUser();
                var ticket = _ticketRepository.Get(ticketId);

                var ticketBuilding = currentUser.Buildings.Find(building => building.Apartments.Contains(ticket.Apartment));
                if (ticket == null || ticketBuilding == null || ticket.Status != Domain.DataTypes.Status.Open)
                {
                    throw new InvalidDataException("Invalid ticket data");
                }

                var maintenance = _maintenanceRepository.Get(maintenanceOperatorId);
                if (maintenance == null || !maintenance.Building.Equals(ticketBuilding))
                {
                    throw new InvalidDataException("Invalid maintenance operator id");
                }

                ticket.AssignedTo = maintenance;
                _ticketRepository.Update(ticket);
                return ticket;
            }
            catch (Exception e)
            {
                throw new Exception("Error assigning ticket", e);
            }
        }

        public Ticket CompleteTicket(int ticketId, float totalCost)
        {
            MaintenanceOperator currentUser = (MaintenanceOperator)_sessionService.GetCurrentUser();
            var ticket = _ticketRepository.Get(ticketId);
            if (ticket == null || !currentUser.Equals(ticket.AssignedTo) || ticket.Status != Domain.DataTypes.Status.InProgress)
            {
                return null;
            }
            ticket.CloseTicket(totalCost);
            _ticketRepository.Update(ticket);
            return ticket;
        }

        public List<Ticket> GetTickets(string category = null)
        {
            var allTickets = _ticketRepository.GetAll<Ticket>();
            Manager currentUser = (Manager)_sessionService.GetCurrentUser();
            var resultTickets = allTickets.Where(ticket => currentUser.Buildings.Any(building => building.Apartments.Contains(ticket.Apartment)));
            if (category != null)
            {
                resultTickets = resultTickets.Where(ticket => ticket.Category.Name.Equals(category));
            }
            else
            {
            }
            return resultTickets.ToList();
        }

        public List<Ticket> GetAssignedTickets()
        {
            MaintenanceOperator currentUser = (MaintenanceOperator)_sessionService.GetCurrentUser();
            var allTickets = _ticketRepository.GetAll<Ticket>();
            return allTickets.Where(ticket => currentUser.Equals(ticket.AssignedTo)).ToList();
        }

        public Ticket StartTicket(int id)
        {
            MaintenanceOperator currentUser = (MaintenanceOperator)_sessionService.GetCurrentUser();
            var ticket = _ticketRepository.Get(id);
            if (ticket == null || !currentUser.Equals(ticket.AssignedTo) || ticket.Status != Domain.DataTypes.Status.Open)
            {
                return null;
            }
            ticket.AttendTicket();
            _ticketRepository.Update(ticket);
            return ticket;
        }

        private bool IsValidCreateTicket(Ticket createTicket)
        {
            return createTicket != null && createTicket.Apartment != null && createTicket.Category != null;
        }
    }
}
