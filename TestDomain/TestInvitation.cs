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

    [TestMethod]
    [ExpectedException(typeof(WrongEmailFormatException))]
    public void TestEmailWithoutDot()
    {
      string email = "hola@hola";
      _invitation.Email = email;
    }

    [TestMethod]
    [ExpectedException(typeof(WrongEmailFormatException))]
    public void TestEmailWrongFormat()
    {
      string email = "hola@.com";
      _invitation.Email = email;
    }

    [TestMethod]
    public void TestGetExpirationDate()
    {
      //new date time now
      DateTime d = DateTime.Now.Date;
      _invitation.D = d;
      Assert.AreEqual(d, _invitation.D);
    }
  }
  
}