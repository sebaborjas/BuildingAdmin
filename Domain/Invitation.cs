using Exceptions;
namespace Domain
{
  public class Invitation
  {
    public string Email { 
      get 
      {
        return "hola@hola.com";
      } 
      set
      {
        if (value == "")
        {
          throw new EmptyFieldException();
        }
        if (!value.Contains("@"))
        {
          throw new WrongEmailFormatException();
        }
        if (!value.Contains("."))
        {
          throw new WrongEmailFormatException();
        }
      } 
    }
  }
}