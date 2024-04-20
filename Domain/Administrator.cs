namespace Domain
{
  public class Administrator : User
  {
    private int _id;

    public int Id
    {
      get => _id;
      set 
      { 
        _id = value; 
      }
    }
  }
}