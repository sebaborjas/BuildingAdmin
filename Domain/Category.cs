using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Exceptions;

namespace Domain
{
    public class Category
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new EmptyFieldException();
                }
                else if (!IsCategoryValid(value))
                {
                    throw new InvalidDataException("Solo se permiten letras y espacios en blanco");
                }
                _name = value;
            }
        }

        private bool IsCategoryValid(string name)
        {
            string pattern = @"^[a-zA-Z\s]*$";
            return Regex.IsMatch(name, pattern);

        }

    }

}