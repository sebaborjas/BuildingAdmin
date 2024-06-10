using System.Text.RegularExpressions;
namespace Domain;

public class ConstructionCompany
{
    private string _name;
    public int Id { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException();
            else if (!Regex.IsMatch(value, @"^[a-zA-Z\s]*$")) throw new InvalidDataException("El nombre debe contener solo letras y espacios.");
            else if (value.Length > 100) throw new ArgumentOutOfRangeException("El nombre no puede contener mas de 100 caracteres");

            _name = value;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is ConstructionCompany constructionCompany)
        {
            return constructionCompany.Id == Id;
        }
        return false;
    }
}
