using Domain;
using Exceptions;

namespace TestDomain
{
  [TestClass]
  public class TestAdministrator
  {
    private Administrator _administrator;

    [TestInitialize]
    public void SetUp()
    {
     _administrator = new Administrator{Id = 1}; 
    }

    [TestMethod]
    public void TestGetId()
    {
      Assert.AreEqual(1, _administrator.Id);
    }
    
    [TestMethod]
    public void TestSetId()
    {
      _administrator.Id = 4;
      Assert.AreEqual(4, _administrator.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void EmptyNameException()
    {
      string name = "";
      _administrator.Name = name;
    }

    [TestMethod]
    public void CorrectName()
    {
      string name = "NewName";
      _administrator.Name = name;

      Assert.AreEqual("NewName", _administrator.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void EmptyLastNameException()
    {
      string lastName = "";
      _administrator.LastName = lastName;
    }

    [TestMethod]
    public void CorrectLastName()
    {
      string lastName = "NewLastName";
      _administrator.LastName = lastName;

      Assert.AreEqual("NewLastName", _administrator.LastName);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void EmptyEmailException()
    {
      string email = "";
      _administrator.Email = email;
    }

    [TestMethod]
    [ExpectedException(typeof(WrongEmailFormatException))]
    public void WrongEmailFormatException()
    {
      string email = "test.com";
      _administrator.Email = email;
    }

    [TestMethod]
    public void CorrectEmail()
    {
      string email = "prueba@test.com";
      _administrator.Email = email;

      Assert.AreEqual(email, _administrator.Email);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void EmptyPasswordException()
    {
      string password = "";
      _administrator.Password = password;
    }

    [TestMethod]
    [ExpectedException(typeof(PasswordNotFollowPolicy))]
    public void WrongPasswordLength()
    {
      string password = "123456";
      _administrator.Password = password;
    }

    [TestMethod]
    [ExpectedException(typeof(PasswordNotFollowPolicy))]
    public void PasswordWithoutSpecialCharacter()
    {
      string password = "Prueba123";
      _administrator.Password = password;
    }

    [TestMethod]
    [ExpectedException(typeof(PasswordNotFollowPolicy))]
    public void PasswordTooMuchLong()
    {
      string password = "Prueba.123456789";
      _administrator.Password = password;
    }

    [TestMethod]
    public void CorrectPassword()
    {
      string password = "Pru#eba.123$";
      _administrator.Password = password;

      Assert.AreEqual(password, _administrator.Password);
    }

    [TestMethod]
    public void TestInviteManager()
    {
      DateTime d = DateTime.Today.AddDays(15);
      string e = "test@invitation.ort";
      Invitation i = new Invitation{Id=1, Email = e, ExpirationDate = d};
      _administrator.InviteManager(i);

      Assert.AreEqual(i, _administrator.invitations.First());
    }
  }
}