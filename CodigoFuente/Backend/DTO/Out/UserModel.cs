using Domain;

namespace DTO.Out
{
  public class UserModel
  {
    public UserModel(User user)
    {
      Name = user.Name;
      Email = user.Email;
      Role = user.GetType().Name;
    }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }
  }
}