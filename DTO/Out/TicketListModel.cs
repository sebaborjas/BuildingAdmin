using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Out
{
    public class TicketListModel
    {
        public List<TicketModel> Tickets { get; set; }
        public TicketListModel() { }

        public TicketListModel(List<TicketModel> tickets)
        {
            Tickets = tickets;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            TicketListModel ticketListModel = (TicketListModel)obj;
            return Tickets == ticketListModel.Tickets;
        }

    }
}
