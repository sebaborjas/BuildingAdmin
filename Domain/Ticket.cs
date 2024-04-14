using Exceptions;
namespace Domain;

public class Ticket
{
    private int _id;
    private string _description;

    public int Id
    {
        get
        {
            return _id;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                _id = value;
            }
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            const int minLength = 10;

            if (string.IsNullOrWhiteSpace(value) || value.Trim().Length < minLength)
            {
                throw new ArgumentNullException();
            }
            else if (value.Length < minLength)
            {
                throw new ArgumentOutOfRangeException($"La descripción debe tener al menos {minLength} caracteres");
            }
            _description = value;
        }
    }

}
