using Domain;
using Domain.DataTypes;
using Exceptions;
namespace TestDomain;

[TestClass]
public class TestMaintenanceOperator
{
    MaintenanceOperator _operator;
    ICollection<Ticket> _tickets;

    Ticket _ticket;

    [TestInitialize]
    public void TestInitialize()
    {
        _operator = new MaintenanceOperator { Name = "John", LastName = "Doe", Email = "test@test.com", Password = "Prueba.123" };
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void EmptyNameException()
    {
        string name = "";
        _operator.Name = name;
    }

    [TestMethod]
    public void CorrectName()
    {
        string name = "NewName";
        _operator.Name = name;

        Assert.AreEqual("NewName", _operator.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void EmptyLastNameException()
    {
        string lastName = "";
        _operator.LastName = lastName;
    }

    [TestMethod]
    public void CorrectLastName()
    {
        string lastName = "NewLastName";
        _operator.LastName = lastName;

        Assert.AreEqual("NewLastName", _operator.LastName);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void EmptyEmailException()
    {
        string email = "";
        _operator.Email = email;
    }

    [TestMethod]
    [ExpectedException(typeof(WrongEmailFormatException))]
    public void WrongEmailFormatException()
    {
        string email = "test.com";
        _operator.Email = email;
    }

    [TestMethod]
    public void CorrectEmail()
    {
        string email = "prueba@test.com";
        _operator.Email = email;

        Assert.AreEqual(email, _operator.Email);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void EmptyPasswordException()
    {
        string password = "";
        _operator.Password = password;
    }

    [TestMethod]
    [ExpectedException(typeof(PasswordNotFollowPolicy))]
    public void WrongPasswordLength()
    {
        string password = "123456";
        _operator.Password = password;
    }

    [TestMethod]
    [ExpectedException(typeof(PasswordNotFollowPolicy))]
    public void PasswordWithoutSpecialCharacter()
    {
        string password = "Prueba123";
        _operator.Password = password;
    }

    [TestMethod]
    [ExpectedException(typeof(PasswordNotFollowPolicy))]
    public void PasswordTooMuchLong()
    {
        string password = "Prueba.123456789";
        _operator.Password = password;
    }

    [TestMethod]
    public void CorrectPassword()
    {
        string password = "Pru#eba.123$";
        _operator.Password = password;

        Assert.AreEqual(password, _operator.Password);
    }

}