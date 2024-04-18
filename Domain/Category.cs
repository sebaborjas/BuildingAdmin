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
        private int _id;

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new EmptyFieldException();
                }
                else if (!IsCategoryValid(value) || !IsCategoryLengthValid(value))
                {
                    throw new InvalidDataException("Solo se permiten letras y espacios, mínimo 3 y máximo 20 caracteres.");
                }
                _name = value;
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= 0)
                {
                    throw new InvalidDataException("El id debe ser mayor a 0");
                }
                _id = value;
            }
        }

        private bool IsCategoryValid(string name)
        {
            string pattern = @"^[a-zA-Z\s]*$";
            return Regex.IsMatch(name, pattern);
        }

        private bool IsCategoryLengthValid(string name)
        {
            return name.Length >= 3 && name.Length <= 20;
        }

    }

}