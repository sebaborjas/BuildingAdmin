using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain
{
    public class Category
    {
        private string _name;
        private int _id;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException();

                string pattern = @"^[a-zA-Z\s]*$";

                bool isValid = IsValidFormat(pattern, value);
                bool isLengthValid = (value.Length >= 3 && value.Length <= 20);

                if (!isValid || !isLengthValid) throw new InvalidDataException("Solo se permiten letras y espacios, mínimo 3 y máximo 20 caracteres.");

                _name = value;
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                    throw new InvalidDataException("El id debe ser mayor a 0");

                _id = value;
            }
        }

        private bool IsValidFormat(string pattern, string value)
        {
            Regex regex = new(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(value);
        }

    }

}