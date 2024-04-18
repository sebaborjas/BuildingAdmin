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

        public Building building { get; set; }
        
        public void TakeTicket(Ticket ticket){
            //TODO
        }

        public void CloseTicket(Ticket ticket){
            ticket.Status = Status.Closed;
        }

        public Ticket GetTicket(int id){
            //TODO
            return new Ticket();
        }
    }
}