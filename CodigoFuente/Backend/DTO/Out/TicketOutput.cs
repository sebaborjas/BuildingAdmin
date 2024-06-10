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
            Apartment = new GetApartmentOutput(ticket.Apartment);
            TotalCost = ticket.TotalCost;
            CreatedBy = new GetManagerOutput(ticket.CreatedBy);
            Category = new GetCategoryOutput(ticket.Category);
            Status = ticket.Status;
            AttentionDate = ticket.AttentionDate;
            ClosingDate = ticket.ClosingDate;
            if(ticket.AssignedTo != null)
            {
                AssignedTo = new MaintenanceOperatorOutput(ticket.AssignedTo);
            }
            else
            {
                AssignedTo = null;
            }
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public GetApartmentOutput Apartment { get; set; }
        public float TotalCost { get; set; }
        public GetManagerOutput CreatedBy { get; set; }
        public GetCategoryOutput Category { get; set; }
        public Status Status { get; set; }
        public DateTime AttentionDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public MaintenanceOperatorOutput AssignedTo { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            TicketOutput ticketModel = (TicketOutput)obj;
            return obj is TicketOutput output &&
                    Id == output.Id &&
                    Description == output.Description &&
                    CreationDate == output.CreationDate &&
                    Apartment == output.Apartment &&
                    TotalCost == output.TotalCost &&
                    CreatedBy == output.CreatedBy &&
                    Category == output.Category &&
                    Status == output.Status &&
                    AttentionDate == output.AttentionDate &&
                    ClosingDate == output.ClosingDate &&
                    AssignedTo == output.AssignedTo;
        }
    }
}
