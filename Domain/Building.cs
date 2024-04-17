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
            else if (!IsNameValid(value))
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
            else if (!IsLocationValid(value))
            {
                throw new InvalidDataException("Formato esperado: 'longitud, latitud'");
            }
            _location = value;
        }
    }

    public string Address
    {
        get
        {
            return _address;

        }
        set
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyFieldException();
            }
            else if (!IsAddressValid(value))
                {
                throw new InvalidDataException("Formato esperado: calle principal, número de puerta, esquina");
            }
            else
                {
                    _address = value;
                }
            
        }
    }

    private bool IsNameValid(string name)
    {
        return name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
    }

    private bool IsLocationValid(string location)
    {
        string pattern = @"^\s*-?\d+(?:\.\d+)?\s*,\s*-?\d+(?:\.\d+)?\s*$";
        return Regex.IsMatch(location, pattern);
    }

    private bool IsAddressValid(string address)
    {
        string pattern = @"^[a-zA-Z\s]+,\s\d{1,4},\s[a-zA-Z\s]+$";
        return Regex.IsMatch(address, pattern);
    }

}
