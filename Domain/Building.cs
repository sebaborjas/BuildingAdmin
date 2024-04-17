using Exceptions;

namespace Domain;

public class Building
{
    private int id;
    private string name;

    public int Id
    {
        get => id;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            id = value;
        }
    }

   public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException();
            }
            name = value;
        }
    }   

}
