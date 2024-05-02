using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Session
    {

        private User _user;
        public int Id { get; set; }
        public Guid Token { get; set; }
        public User User 
        { 
            get => _user;
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException();
                }
                _user = value;
            } 
        }

        public Session()
        {
            Token = Guid.NewGuid();
        }
    }
}
