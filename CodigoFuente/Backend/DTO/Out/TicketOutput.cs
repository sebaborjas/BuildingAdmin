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
            Apartment = ticket.Apartment;
            TotalCost = ticket.TotalCost;
            CreatedBy = ticket.CreatedBy;
            Category = ticket.Category;
            Status = ticket.Status;
            AttentionDate = ticket.AttentionDate;
            ClosingDate = ticket.ClosingDate;
            AssignedTo = ticket.AssignedTo;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public Apartment Apartment { get; set; }
        public float TotalCost { get; set; }
        public Manager CreatedBy { get; set; }
        public Category Category { get; set; }
        public Status Status { get; set; }
        public DateTime AttentionDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public MaintenanceOperator AssignedTo { get; set; }

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
