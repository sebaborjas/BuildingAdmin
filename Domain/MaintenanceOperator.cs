using Domain.DataTypes;
using Exceptions;

namespace Domain
{
    public class MaintenanceOperator : User
    {
        private string _lastName;

        public override string LastName { 
            get {
                return _lastName;
            } 
            set {
                if (string.IsNullOrEmpty(value)) throw new EmptyFieldException();
                _lastName = value;
            } 
        }

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