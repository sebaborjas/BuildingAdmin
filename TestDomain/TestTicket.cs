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
    public void TestIdNegative()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => ticket.Id = -1);
    }

    [TestMethod]
    public void TestDescription()
    {
        ticket.Description = "Descripción válida con más de 10 caracteres.";
        Assert.AreEqual("Descripción válida con más de 10 caracteres.", ticket.Description);
    }

    [TestMethod]
    public void TestDescriptionShort()
    {
        Assert.ThrowsException<ArgumentNullException>(() => ticket.Description = "Corta");
    }

    [TestMethod]
    public void TestDescriptionNull()
    {
        Assert.ThrowsException<ArgumentNullException>(() => ticket.Description = null);
    }

    [TestMethod]
    public void TestDescriptionEmpty()
    {
        Assert.ThrowsException<ArgumentNullException>(() => ticket.Description = "");
    }

    [TestMethod]
    public void TestDescriptionWhiteSpace()
    {
        Assert.ThrowsException<ArgumentNullException>(() => ticket.Description = "             ");
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
}