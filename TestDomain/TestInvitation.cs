using Domain;
using Exceptions;

namespace TestDomain
{
  [TestClass]
  public class TestInvitation
  {
    private Invitation _invitation;

    [TestInitialize]
    public void SetUp()
    {
      _invitation = new Invitation();
    }
    [TestMethod]
    public void TestSetEmail()
    {
      string email = "hola@hola.com";
      _invitation.Email = email;
      Assert.AreEqual(email, _invitation.Email);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]	
    public void SetEmptyEmailException()
    {
      string email = "";
      _invitation.Email = email;
    }

    [TestMethod]
    [ExpectedException(typeof(WrongEmailFormatException))]
    public void TestEmailWithoutAt()
    {
      string email = "hola";
      _invitation.Email = email;
    }
  }
  
}