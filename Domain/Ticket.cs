using Exceptions;
namespace Domain;

public class Ticket
{
    private int _id;

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

}
