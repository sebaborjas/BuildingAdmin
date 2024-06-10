using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.DataTypes;

namespace DTO.Out
{
    public class TicketOutput
    {
        public TicketOutput(Ticket ticket)
        {
            Id = ticket.Id;
            Description = ticket.Description;
            CreationDate = ticket.CreationDate;
            Apartment = new GetTicketApartmentOutput(ticket.Apartment);
            TotalCost = ticket.TotalCost;
            CreatedBy = new GetTicketCreatedByUserOutput(ticket.CreatedBy);
            Category = new GetCategoryOutput(ticket.Category);
            Status = ticket.Status;
            AttentionDate = ticket.AttentionDate;
            ClosingDate = ticket.ClosingDate;
            IdOperatorAssignedTo = ticket.IdOperatorAssigned;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public GetTicketApartmentOutput Apartment { get; set; }
        public float TotalCost { get; set; }
        public GetTicketCreatedByUserOutput CreatedBy { get; set; }
        public GetCategoryOutput Category { get; set; }
        public Status Status { get; set; }
        public DateTime AttentionDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public int? IdOperatorAssignedTo { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            TicketOutput ticketModel = (TicketOutput)obj;
            var output = (TicketOutput)obj;
            return output != null &&
                    Id == output.Id &&
                    Description == output.Description &&
                    CreationDate.Equals(output.CreationDate) &&
                    Apartment.Equals(output.Apartment) &&
                    TotalCost == output.TotalCost &&
                    CreatedBy.Equals(output.CreatedBy) &&
                    Category.Equals(output.Category) &&
                    Status == output.Status &&
                    AttentionDate == output.AttentionDate &&
                    ClosingDate == output.ClosingDate &&
                    IdOperatorAssignedTo == output.IdOperatorAssignedTo;
        }
    }

    public class GetTicketApartmentOutput
    {
        public int Id { get; set; }
        public int DoorNumber { get; set; }

        public GetTicketApartmentOutput(Apartment apartment)
        {
            Id = apartment.Id;
            DoorNumber = apartment.DoorNumber;
        }

        public override bool Equals(object obj)
        {
            if(obj is GetTicketApartmentOutput)
            {
                var objToCompare = (GetTicketApartmentOutput)obj;
                return objToCompare.Id == Id && objToCompare.DoorNumber == DoorNumber;
            }
            return false;
        }
    }

    public class GetTicketCreatedByUserOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GetTicketCreatedByUserOutput(Manager user)
        {
            Id = user.Id;
            Name = user.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is GetTicketCreatedByUserOutput)
            {
                var objToCompare = (GetTicketCreatedByUserOutput)obj;
                return objToCompare.Id == Id && objToCompare.Name == Name;
            }
            return false;
        }
    }
}
