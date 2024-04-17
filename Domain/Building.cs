using Exceptions;

namespace Domain;

public class Building
{
    private int _id;
    private string _name;

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
            }else if (!IsNameValid(value))
            {
                throw new InvalidDataException("El nombre solo puede contener letras y espacios");
            }
            _name = value;
        }
    }

    public float expenses
    {
        get { return 0; }
        set { }
    }

    private bool IsNameValid(string name)
    {
        return name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
    }

}
