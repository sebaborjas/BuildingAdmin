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
    public List<Ticket> Tickets { get; set; } = new List<Ticket>();

    public int Id
    {
        get => _id;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException();

            _id = value;
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException();

            string pattern = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\d]+$";
            bool isValid = IsValidFormat(pattern, value);

            if (!isValid) throw new InvalidDataException("Solo se permiten letras y números");

            _name = value;
        }
    }

    public float Expenses
    {
        get => _expenses;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException();

            _expenses = value;
        }
    }

    public ConstructionCompany ConstructionCompany
    {
        get => _constructionCompany;
        set
        {
            if (value == null) throw new ArgumentNullException();

            _constructionCompany = value;
        }
    }

    public string Location
    {
        get => _location;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException();

            string pattern = @"^\s*-?\d+(?:\.\d+)?\s*,\s*-?\d+(?:\.\d+)?\s*$";

            bool isValid = IsValidFormat(pattern, value);
            if (!isValid) throw new InvalidDataException("Formato esperado: 'longitud, latitud'");

            _location = value;
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException();

            string patternCalle = @"[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\d\.]+";
            string patternNumero = @"\d{1,4}";
            string patternEsquina = @"[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\d\.]+";

            string pattern = $"^{patternCalle}, {patternNumero}, {patternEsquina}$";   
            bool isValid = IsValidFormat(pattern, value);

            if (!isValid) throw new InvalidDataException("Formato esperado: calle principal, número de puerta, esquina");

            _address = value;
        }
    }

    private bool IsValidFormat(string pattern, string value)
    {
        Regex regex = new(pattern, RegexOptions.IgnoreCase);

        return regex.IsMatch(value);
    }

    public override bool Equals(object obj)
    {
        if (obj is Building building)
        {
            return building.Id == Id;
        }
        return false;
    }
}
