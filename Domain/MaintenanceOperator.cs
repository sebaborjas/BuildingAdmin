using Domain.DataTypes;

namespace Domain
{
    public class MaintenanceOperator : User
    {
        public ICollection<Ticket> Tickets { get; set; }
        
        public void TakeTicket(Ticket ticket){
            ticket.Status = Status.InProgress;
            ticket.AssignedTo = this;
            Tickets.Add(ticket);
        }

        public void CloseTicket(Ticket ticket){
            ticket.Status = Status.Closed;
        }

        public Ticket GetTicket(int id){
        return Tickets.FirstOrDefault(t => t.Id == id);
        }
    }
}