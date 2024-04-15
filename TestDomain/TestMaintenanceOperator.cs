using Domain;
using Exceptions;
namespace TestDomain;

[TestClass]
public class TestMaintenanceOperator
{
  MaintenanceOperator _operator;

  [TestInitialize]
  public void TestInitialize()
  {
    // _operator = new MaintenanceOperator {Name = "John", Email = "test@test.com", Password ="Prueba.123"};
  }

  [TestMethod]
  [ExpectedException(typeof(EmptyFieldException))]
  public void EmptyNameException()
  {
    _operator = new MaintenanceOperator {Name = "", Email = "test@test.com", Password ="Prueba.123"};
  }

  [TestMethod]
  public void CorrectName()
  {
    _operator = new MaintenanceOperator {Name = "John", Email = "test@test.com", Password ="Prueba.123"};

    Assert.AreEqual("John", _operator.Name);
  }
  
  [TestMethod]
  [ExpectedException(typeof(EmptyFieldException))]
  public void EmptyEmailException()
  {
    _operator = new MaintenanceOperator {Name = "John", Email = "", Password ="Prueba.123"};
  }

  [TestMethod]
  [ExpectedException(typeof(WrongEmailFormatException))]
  public void WrongEmailFormatException()
  {
    string email = "test.com";
    _operator = new MaintenanceOperator {Name = "John", Email = email, Password ="Prueba.123"};
  }

  [TestMethod]
  public void CorrectEmail()
  {
    string email = "prueba@test.com";
    _operator = new MaintenanceOperator {Name = "John", Email = email, Password ="Prueba.123"};

    Assert.AreEqual(email, _operator.Email);
  }
  
  [TestMethod]
  [ExpectedException(typeof(EmptyFieldException))]
  public void EmptyPasswordException()
  {
     _operator = new MaintenanceOperator {Name = "John", Email = "prueba@test.com", Password =""};
  }
  
  [TestMethod]
  [ExpectedException(typeof(PasswordNotFollowPolicy))]
  public void WrongPasswordLength()
  {
  }
  
  [TestMethod]
  [ExpectedException(typeof(PasswordNotFollowPolicy))]
  public void PasswordWithoutSpecialCharacter()
  {
  }

  [TestMethod]
  [ExpectedException(typeof(PasswordNotFollowPolicy))]
  public void PasswordTooMuchLong()
  {
  }
  
  [TestMethod]
  public void CorrectPassword()
  {
  }
  
  [TestMethod]
  public void GetEmptyListOfTickets()
  {
  }
  
  [TestMethod]
  public void GetTicketsCorrect()
  {
  }
  
  [TestMethod]
  public void CloseTicket()
  {
  }
  
  [TestMethod]
  public void TakeTicket()
  {
  }
}