namespace Domain;

public abstract class User
{
  private int _id; 
  
  public int Id { 
    get { 
      return _id;
        } 
  }
  public string Name { get; set; }

  private string _lastName;

  public virtual string LastName { 

    get {
      return _lastName;
    }

    set {
      _lastName = "";
    }
  }

  public string Email { get; set; }

  public string Password { get; set; }
}
