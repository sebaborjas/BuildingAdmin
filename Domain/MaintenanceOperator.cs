using Domain.DataTypes;

namespace Domain
{
    public class MaintenanceOperator : User
    {
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        
        public void TakeTicket(Ticket ticket){
            ticket.Status = Status.InProgress;
            ticket.AssignedTo = this;
            Tickets.Add(ticket);
        }

        public void CloseTicket(Ticket ticket){
            ticket.Status = Status.Closed;
        }

        public Ticket GetTicket(int id){
            var ticket = Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null) throw new ArgumentNullException("No se enontro el ticket");
            return ticket;
        }
    }
}