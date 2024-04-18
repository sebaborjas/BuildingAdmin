using Domain;
using Exceptions;

namespace TestDomain
{
  [TestClass]
  public class TestInvitation
  {
    [TestMethod]
    public void TestSetEmail()
    {
      string email = "hola@hola.com";
      Invitation invitation = new Invitation();
      invitation.Email = email;
      Assert.AreEqual(email, invitation.Email);
    }
  }
  
}