using System.Text.RegularExpressions;
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
        if(Regex.IsMatch(value, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$") == false)
        {
          throw new WrongEmailFormatException();
        }
      } 
    }
  }
}