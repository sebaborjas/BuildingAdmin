using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using Domain.DataTypes;
using Exceptions;
namespace Domain
{
  public class Invitation
  {
    private string _email;

    private DateTime _expirationDate;

    private InvitationStatus _status;

    public int Id { get {return 1;} set{} }

    public string Email { 
      get 
      {
        return _email;
      } 
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          throw new EmptyFieldException();
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

    public DateTime ExpirationDate { 
      get 
      {
        return _expirationDate;
      } 
      set 
      {
        if (value < DateTime.Now.Date)
        {
          throw new ArgumentOutOfRangeException();
        }
        _expirationDate = value;
      } 
    }

    public InvitationStatus Status
    {
      get
      {
        return _status;
      }
      set
      {
        _status = value;
      }
    }

    private bool IsValidFormat(string pattern, string value)
    {
      Regex regex = new(pattern, RegexOptions.IgnoreCase);

      return regex.IsMatch(value);
    }
  }
}