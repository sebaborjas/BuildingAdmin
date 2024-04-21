using Exceptions;
using System.Text.RegularExpressions;

namespace Domain;

public class Building
{
    private int _id;
    private string _name;
    private float _expenses;
    private string _location;
    private string _address;
    private ConstructionCompany _constructionCompany;
    public List<Apartment> Apartments { get; set; } = new List<Apartment>();
    public List<Ticket> Tickets {
        get { return null; }
        set { }
    }

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
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyFieldException();
            }

            bool isValid = value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
            if (!isValid)
            {
                throw new InvalidDataException("El nombre solo puede contener letras y espacios");
            }
            _name = value;
        }
    }

    public float Expenses
    {
        get => _expenses;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _expenses = value;
        }
    }

    public ConstructionCompany ConstructionCompany
    {
        get => _constructionCompany;
        set
        {
            if (value == null)
            {
                throw new EmptyFieldException();
            }
            _constructionCompany = value;
        }
    }

    public string Location
    {
        get => _location;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyFieldException();
            }

            string pattern = @"^\s*-?\d+(?:\.\d+)?\s*,\s*-?\d+(?:\.\d+)?\s*$";

            bool isValid = IsValidFormat(pattern, value);
            if (!isValid)
            {
                throw new InvalidDataException("Formato esperado: 'longitud, latitud'");
            }
            _location = value;
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyFieldException();
            }

            string pattern = @"^[a-zA-Z\s]+,\s\d{1,4},\s[a-zA-Z\s]+$";

            bool isValid = IsValidFormat(pattern, value);
            if (!isValid)
            {
                throw new InvalidDataException("Formato esperado: calle principal, número de puerta, esquina");
            }
            _address = value;
        }
    }

    private bool IsValidFormat(string pattern, string value)
    {
        Regex regex = new(pattern, RegexOptions.IgnoreCase);

        return regex.IsMatch(value);
    }
}
