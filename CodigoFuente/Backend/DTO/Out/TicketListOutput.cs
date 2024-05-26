using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Out
{
    public class TicketListOutput
    {
        public List<TicketOutput> Tickets { get; set; }
        public TicketListOutput() { }

        public TicketListOutput(List<TicketOutput> tickets)
        {
            Tickets = tickets;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            TicketListOutput ticketListModel = (TicketListOutput)obj;
            return Tickets == ticketListModel.Tickets;
        }

    }
}
