using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Exceptions;

namespace Domain
{
    public class Owner
    {
        private string _name;
        private string _lastName;
        private string _email;

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
            get { return _email; }
            set 
            {
                Regex regex = new(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", RegexOptions.IgnoreCase);

                
                if (value == String.Empty)
                {
                    throw new EmptyFieldException();
                } else if (!regex.IsMatch(value))
                {
                    throw new InvalidDataException();
                }
                _email = value; 
            }
        }
    }

    
}
