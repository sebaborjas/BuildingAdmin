using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DTO.Out
{
    public class TicketOutput
    {
        public TicketOutput(Ticket ticket)
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
            TicketOutput ticketModel = (TicketOutput)obj;
            return Id == ticketModel.Id;
        }
    }
}
