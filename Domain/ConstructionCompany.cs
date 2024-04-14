using Exceptions;
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
            if (value == "")
            {
                throw new EmptyFieldException();
            }
            else
            {
                _name = value;
            }
        }
    }


}
