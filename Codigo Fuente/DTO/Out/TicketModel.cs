using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DTO.Out
{
    public class TicketModel
    {
        public TicketModel(Ticket ticket)
        {
            Id = ticket.Id;
        }

        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            TicketModel ticketModel = (TicketModel)obj;
            return Id == ticketModel.Id;
        }
    }
}
