using Domain;
using Domain.DataTypes;
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
      DateTime date = DateTime.Now.Date;
      _invitation.ExpirationDate = date;
      Assert.AreEqual(date, _invitation.ExpirationDate);
    }

    [TestMethod]
    public void TestSetExpirationDate()
    {
      DateTime date = DateTime.Now.Date.AddDays(14);
      _invitation.ExpirationDate = date;
      Assert.AreEqual(date, _invitation.ExpirationDate);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestSetPastExpirationDate()
    {
      DateTime date = DateTime.Now.Date.AddDays(-2);
      _invitation.ExpirationDate = date;
    }

    [TestMethod]
    public void TestGetStatus()
    {
      InvitationStatus status = InvitationStatus.Rejected;
      _invitation.Status = status;
      Assert.AreEqual(status, _invitation.Status);
    }
    
    [TestMethod]
    public void TestSetStatus()
    {
      InvitationStatus s = InvitationStatus.Accepted;
      _invitation.Status = s;
      Assert.AreEqual(s, _invitation.Status);

    }
  }
  
}