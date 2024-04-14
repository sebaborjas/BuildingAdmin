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
        ticket.Description = "Descripción válida con más de 10 caracteres.";
        Assert.AreEqual("Descripción válida con más de 10 caracteres.", ticket.Description);
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
    public void TestSetAttention_ValidDateTime()
    {
        DateTime validDateTime = DateTime.Today.AddDays(1).AddHours(10).AddMinutes(30); 
        ticket.SetAttention(validDateTime);
        Assert.AreEqual(validDateTime.Date, ticket.AttentionDate);
        Assert.AreEqual(validDateTime.TimeOfDay, ticket.AttentionTime);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestSetAttention_InvalidDateTime_BeforeCreation()
    {
        DateTime invalidDateTime = DateTime.Today.AddDays(-1).AddHours(10).AddMinutes(30);
        ticket.SetAttention(invalidDateTime);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestSetAttention_InvalidDateTime_AlreadySet()
    {
        DateTime validDateTime = DateTime.Today.AddDays(1).AddHours(10).AddMinutes(30);
        ticket.SetAttention(validDateTime);

        DateTime invalidDateTime = DateTime.Today.AddDays(1).AddHours(12); 
        ticket.SetAttention(invalidDateTime);
    }

    [TestMethod]
    public void TestSetClosing_ValidDateTime()
    {
        DateTime validDateTime = DateTime.Today.AddDays(1).AddHours(15).AddMinutes(45);
        ticket.SetAttention(DateTime.Today);
        ticket.SetClosing(validDateTime);
        Assert.AreEqual(validDateTime.Date, ticket.ClosingDate);
        Assert.AreEqual(validDateTime.TimeOfDay, ticket.ClosingTime);
    }

    [TestMethod]
    public void TestSetClosing_ValidDateTime_SameDateLaterTime()
    {
        DateTime validDateTime = DateTime.Today.AddMinutes(45);
        ticket.SetAttention(DateTime.Today);
        ticket.SetClosing(validDateTime);
        Assert.AreEqual(validDateTime.Date, ticket.ClosingDate);
        Assert.AreEqual(validDateTime.TimeOfDay, ticket.ClosingTime);
    }


    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestSetClosing_InvalidDateTime_BeforeAttention()
    {
        DateTime invalidDateTime = DateTime.Today.AddDays(-1);
        ticket.SetAttention(DateTime.Today);
        ticket.SetClosing(invalidDateTime);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestSetClosing_InvalidDateTime_SameDateEarlierTime()
    {
        DateTime validDateTime = DateTime.Today.AddDays(1).AddHours(15).AddMinutes(45); 
        ticket.SetAttention(DateTime.Today);
        ticket.SetClosing(validDateTime);

        DateTime invalidDateTime = DateTime.Today.AddDays(1).AddHours(15).AddMinutes(30); 
        ticket.SetClosing(invalidDateTime);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestSetClosing_InvalidDateTime_AlreadySet()
    {
        DateTime validDateTime = DateTime.Today.AddDays(1).AddHours(15).AddMinutes(45); 
        ticket.SetAttention(DateTime.Today);
        ticket.SetClosing(validDateTime);

        DateTime invalidDateTime = DateTime.Today.AddDays(1).AddHours(15); 
        ticket.SetClosing(invalidDateTime);
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
        ticket.Category = Domain.DataTypes.Category.Electrician;
        Assert.AreEqual(Domain.DataTypes.Category.Electrician, ticket.Category);
    }


    [TestMethod]
    public void TestStatus()
    {
        Assert.AreEqual(Domain.DataTypes.Status.Open, ticket.Status);
    }

    [TestMethod]
    public void TestChangeStatus_InProgress()
    {
        ticket.SetAttention(DateTime.Today);
        ticket.ChangeStatus(Domain.DataTypes.Status.InProgress);
        Assert.AreEqual(Domain.DataTypes.Status.InProgress, ticket.Status);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestChangeStatus_InProgress_NoAttentionDate()
    {
        ticket.ChangeStatus(Domain.DataTypes.Status.InProgress);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestChangeStatus_Open()
    {
        ticket.ChangeStatus(Domain.DataTypes.Status.Open);
    }

    [TestMethod]
    public void TestChangeStatus_Closed()
    {
        ticket.SetAttention(DateTime.Today);
        ticket.SetClosing(DateTime.Today.AddDays(1));
        ticket.ChangeStatus(Domain.DataTypes.Status.Closed);
        Assert.AreEqual(Domain.DataTypes.Status.Closed, ticket.Status);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestChangeStatus_Closed_NoClosingDate()
    {
        ticket.SetAttention(DateTime.Today);
        ticket.ChangeStatus(Domain.DataTypes.Status.Closed);
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
        ticket.AssignedTo = new User();
        Assert.IsNotNull(ticket.AssignedTo);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAssignedToNull()
    {
        ticket.AssignedTo = null;
    }

    [TestMethod]
    public void TestCreatedBy()
    {
        ticket.CreatedBy = new User();
        Assert.IsNotNull(ticket.CreatedBy);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreatedByNull()
    {
        ticket.CreatedBy = null;
    }
}