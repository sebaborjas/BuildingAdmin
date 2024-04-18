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

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]	
    public void SetEmptyEmailException()
    {
      string email = "";
      Invitation invitation = new Invitation();
      invitation.Email = email;
    }

    [TestMethod]
    [ExpectedException(typeof(WrongEmailFormatException))]
    public void TestWrongEmailFormat()
    {
      string e = "hola";
      Invitation i = new Invitation();
      i.Email = e;
    }
  }
  
}