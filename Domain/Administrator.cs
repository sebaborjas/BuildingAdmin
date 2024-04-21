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

    public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public void InviteManager(Invitation i) {
      Invitations.Add(i);
    }
  }
}