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
      Invitation i = new Invitation();
      i.Email = email;
      Assert.AreEqual(email, i.Email);
    }
  }
  
}