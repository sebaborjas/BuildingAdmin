namespace Domain;

using System.Text.RegularExpressions;
using Exceptions;

public abstract class User
{
  private int _id;
  private string _name;
  private string _lastName;
  private string _email;
  private string _password;
  
  public int Id { 
    get => _id;
  }
  public string Name { 
    get => _name;
    set {
      if (string.IsNullOrEmpty(value))
      {
          throw new ArgumentNullException();
      }
      _name = value;
    } 
  }

  public virtual string LastName { 

    get => _lastName;
    set => _lastName = "";
  }

  public string Email { 
    get => _email; 
    set {
      if (string.IsNullOrEmpty(value))
      {
          throw new ArgumentNullException();
      }

      string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

      bool correctEmail = IsValidFormat(pattern, value);

      if (!correctEmail)
      {
          throw new WrongEmailFormatException();
      }

      _email = value;
    }
  }

  public string Password { 
    get => _password;  
    set {

      if (string.IsNullOrEmpty(value))
      {
          throw new ArgumentNullException();
      }

      //Password must have at least one uppercase letter, one special character and be between 6 and 15 characters
      string pattern = @"^(?=.*[A-Z])(?=.*[\W_]).{6,15}$"; 

      bool passwordCorrect = IsValidFormat(pattern, value);


      if (!passwordCorrect)
      {
          throw new PasswordNotFollowPolicy();
      }

      _password = value;
    } 
  }

  private bool IsValidFormat(string pattern, string value)
  {
    Regex regex = new(pattern, RegexOptions.IgnoreCase);

    return regex.IsMatch(value);
  }
}
