namespace Domain
{
  public class Administrator : User
  {
    private string _lastName;

    public override string LastName { 
      get => _lastName; 
      set {
          if (string.IsNullOrEmpty(value)) throw new ArgumentNullException();
          _lastName = value;
      }  
    }

    public ICollection<Invitation> invitations { get; set; }

    public void InviteManager(Invitation i) {}
  }
}