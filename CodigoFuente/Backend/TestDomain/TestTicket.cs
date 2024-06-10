using Domain;

namespace TestDomain;

[TestClass]
public class TestTicket
{
    Ticket ticket;

    [TestInitialize]
    public void Setup()
    {
        ticket = new Ticket();
    }

    [TestMethod]
    public void TestId()
    {
        ticket.Id = 1;
        Assert.AreEqual(1, ticket.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestIdNegative()
    {
        ticket.Id = -1;
    }

    [TestMethod]
    public void TestDescription()
    {
        ticket.Description = "Descripcion valida con mas de 10 caracteres.";
        Assert.AreEqual("Descripcion valida con mas de 10 caracteres.", ticket.Description);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestDescriptionShort()
    {
        ticket.Description = "Corta";
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDescriptionNull()
    {
        ticket.Description = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDescriptionEmpty()
    {
        ticket.Description = "";
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDescriptionWhiteSpace()
    {
        ticket.Description = "             ";
    }

    [TestMethod]
    public void TestCreationDate()
    {
        Assert.AreEqual(DateTime.Today.Date, ticket.CreationDate);
    }

    [TestMethod]
    public void TestApartment()
    {
        Apartment apartment = new Apartment();
        ticket.Apartment = apartment;
        Assert.AreEqual(apartment, ticket.Apartment);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestApartmentNull()
    {
        ticket.Apartment = null;
    }

    [TestMethod]
    public void TestCategory()
    {
        Category category = new Category();
        ticket.Category = category;
        Assert.AreEqual(category, ticket.Category);
    }

    [TestMethod]
    public void TestStatus()
    {
        Assert.AreEqual(Domain.DataTypes.Status.Open, ticket.Status);
    }

    [TestMethod]
    public void TestAssignedTo()
    {
        ticket.AssignedTo = new MaintenanceOperator();
        Assert.IsNotNull(ticket.AssignedTo);
    }

    [TestMethod]
    public void TestCreatedBy()
    {
        ticket.CreatedBy = new Manager();
        Assert.IsNotNull(ticket.CreatedBy);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestNullCreatedBy()
    {
        ticket.CreatedBy = null;
    }

    [TestMethod]
    public void TestAttendTicket()
    {
        ticket.AttendTicket();

        Ticket expectedTicket = new Ticket();
        expectedTicket = ticket;

        Assert.AreEqual(expectedTicket, ticket);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestAttendTicketTwice()
    {
        ticket.AttendTicket();
        ticket.AttendTicket();
    }

    [TestMethod]
    public void TestCloseTicket()
    {
        ticket.AttendTicket();
        float totalCost = 100.0f;
        ticket.CloseTicket(totalCost);

        Ticket expectedTicket = new Ticket();
        expectedTicket = ticket;

        Assert.AreEqual(expectedTicket, ticket);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestCloseTicketWithoutAttending()
    {
        float totalCost = 100.0f;
        ticket.CloseTicket(totalCost);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestCloseTicketTwice()
    {
        ticket.AttendTicket();
        float totalCost = 100.0f;
        ticket.CloseTicket(totalCost);
        ticket.CloseTicket(totalCost);
    }

    [TestMethod]
    public void TestTotalCost()
    {
        ticket.AttendTicket();
        float totalCost = 100.0f;
        ticket.CloseTicket(totalCost);
        Assert.AreEqual(totalCost, ticket.TotalCost);
    }

    [TestMethod]
    public void TestAttentionDate()
    {
        ticket.AttendTicket();
        Assert.IsNotNull(ticket.AttentionDate);
    }

    [TestMethod]
    public void TestClosingDate()
    {
        ticket.AttendTicket();
        float totalCost = 100.0f;
        ticket.CloseTicket(totalCost);
        Assert.IsNotNull(ticket.ClosingDate);
    }

    [TestMethod]
    public void TestNotEquals()
    {
        Ticket ticket1 = new Ticket() { Id = 1 };
        Ticket ticket2 = new Ticket() { Id = 2 };

        Assert.AreNotEqual(ticket1, ticket2);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCloseTicketWithInvalidCost()
    {
        ticket.AttendTicket();
        float totalCost = -1;
        ticket.CloseTicket(totalCost);
    }
}
