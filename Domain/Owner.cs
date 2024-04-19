using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions;

namespace Domain
{
    public class Owner
    {
        private string _name;
        private string _lastName;

        public string Name
        {
            get { return _name; }
            
            set 
            {
                if (value == String.Empty)
                {
                    throw new EmptyFieldException();
                }
                _name = value; 
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set {
                if (value == String.Empty)
                {
                    throw new EmptyFieldException();
                }
                _lastName = value; }
        }

        public string Email
        {
            get { return ""; }
            set { }
        }
    }

    
}
