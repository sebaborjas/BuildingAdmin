using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain
{
    public class Owner
    {
        private const string EMAIL_PATTERN = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

        private int _id;
        private string _name;
        private string _lastName;
        private string _email;

        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _id = value;
            }
        }

        public string Name
        {
            get => _name;

            set
            {
                if (value == String.Empty)
                {
                    throw new ArgumentNullException();
                }
                _name = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == String.Empty)
                {
                    throw new ArgumentNullException();
                }
                _lastName = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (value == String.Empty)
                {
                    throw new ArgumentNullException();
                }
                else if (!IsValidEmail(value))
                {
                    throw new InvalidDataException();
                }
                _email = value;
            }
        }

        public bool IsValidEmail(string email)
        {
            Regex regex = new(EMAIL_PATTERN, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

    }


}
