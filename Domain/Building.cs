using Exceptions;

namespace Domain;

public class Building
{
    private int id;

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
}
