using Exceptions;
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
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyFieldException();
            }
            else if (!Regex.IsMatch(value, @"^[a-zA-Z\s]*$"))
            {
                throw new InvalidDataException("El nombre debe contener solo letras y espacios.");
            }
            else
            {
                _name = value;
            }
        }
    }


}
