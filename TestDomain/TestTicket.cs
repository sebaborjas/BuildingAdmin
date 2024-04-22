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
    [ExpectedException(typeof(ArgumentNullException))]
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
    public void TestTotalCost()
    {
        ticket.TotalCost = 100;
        Assert.AreEqual(100, ticket.TotalCost);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestTotalCostNegative()
    {
        ticket.TotalCost = -1;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestTotalCostZero()
    {
        ticket.TotalCost = 0;
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
    public void TestCreatedByNull()
    {
        ticket.CreatedBy = null;
    }

    [TestMethod]
    public void TestSetAttention_ValidDateTime()
    {
        DateTime validDateTime = DateTime.Today.AddDays(1).AddHours(10).AddMinutes(30);
        ticket.ProcessTicket(Domain.DataTypes.Status.InProgress, validDateTime);
        Assert.AreEqual(validDateTime, ticket.AttentionDate);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestSetAttention_NullDateTime()
    {
        ticket.ProcessTicket(Domain.DataTypes.Status.InProgress, null);
    }

    [TestMethod]
    public void TestSetClosing_ValidDateTime()
    {
        DateTime validDateTime = DateTime.Today.AddDays(1).AddHours(15).AddMinutes(45);
        ticket.ProcessTicket(Domain.DataTypes.Status.InProgress, DateTime.Today);
        ticket.ProcessTicket(Domain.DataTypes.Status.Closed, validDateTime);
        Assert.AreEqual(validDateTime, ticket.ClosingDate);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestSetClosing_NullDateTime()
    {
        ticket.ProcessTicket(Domain.DataTypes.Status.InProgress, DateTime.Today);
        ticket.ProcessTicket(Domain.DataTypes.Status.Closed, null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestSetClosing_InvalidDateTime_BeforeAttention()
    {
        ticket.ProcessTicket(Domain.DataTypes.Status.InProgress, DateTime.Today);
        ticket.ProcessTicket(Domain.DataTypes.Status.Closed, DateTime.Today.AddDays(-1));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestSetClosing_InvalidDateTime_EarlierThanAttentionTime()
    {
        ticket.ProcessTicket(Domain.DataTypes.Status.InProgress, DateTime.Today);
        ticket.ProcessTicket(Domain.DataTypes.Status.Closed, DateTime.Today.AddHours(-5));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestSetClosing_InvalidDateTime_AlreadySet()
    {
        ticket.ProcessTicket(Domain.DataTypes.Status.InProgress, DateTime.Today);
        ticket.ProcessTicket(Domain.DataTypes.Status.Closed, DateTime.Today.AddDays(1));
        ticket.ProcessTicket(Domain.DataTypes.Status.Closed, DateTime.Today.AddDays(2));
    }
}
