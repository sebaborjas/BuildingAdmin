using Domain;
using Domain.Exceptions;

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
    [ExpectedException(typeof(ArgumentNullException))]
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
    [ExpectedException(typeof(ArgumentNullException))]
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
    [ExpectedException(typeof(ArgumentNullException))]
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
    [ExpectedException(typeof(ArgumentNullException))]
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

    [TestMethod]
    public void SetBuildings()
    {
        List<Building> buildings = new List<Building>(){
            new Building()
            {
                Id = 1,
                Address = "Calle, 1234, esquina",
                Apartments = new List<Apartment>(),
                ConstructionCompany = new ConstructionCompany(),
                Expenses = 1200,
                Location = "1234,1234",
                Name = "Edificio uno",
                Tickets= new List<Ticket>()
            },
            new Building()
            {
                Id = 12,
                Address = "Calle, 4567, esquina",
                Apartments = new List<Apartment>(),
                ConstructionCompany = new ConstructionCompany(),
                Expenses = 2100,
                Location = "4321,4321",
                Name = "Edificio dos",
                Tickets= new List<Ticket>()
            }
        };
        _operator.Buildings = buildings;

        CollectionAssert.AreEqual(buildings, _operator.Buildings);
    }

}