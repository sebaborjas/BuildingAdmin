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
    public void TestSetAttentionDate_ValidDate()
    {
        DateTime validDate = DateTime.Today.AddDays(1).Date;
        ticket.SetAttentionDate(validDate);
        Assert.AreEqual(validDate, ticket.AttentionDate);
    }

    [TestMethod]
    public void TestSetAttentionDate_InvalidDate_BeforeCreation()
    {
        DateTime invalidDate = DateTime.Today.AddDays(-1).Date;
        Assert.ThrowsException<InvalidOperationException>(() => ticket.SetAttentionDate(invalidDate));
    }

    [TestMethod]
    public void TestSetAttentionDate_InvalidDate_AlreadySet()
    {
        DateTime validDate = DateTime.Today.AddDays(1).Date;
        ticket.SetAttentionDate(validDate);
        DateTime invalidDate = DateTime.Today.AddDays(2).Date;
        Assert.ThrowsException<InvalidOperationException>(() => ticket.SetAttentionDate(invalidDate));
    }

    [TestMethod]
    public void TestSetClosingDate_ValidDate()
    {
        DateTime validDate = DateTime.Today.AddDays(1).Date;
        ticket.SetAttentionDate(DateTime.Today.Date);
        ticket.SetClosingDate(validDate);
        Assert.AreEqual(validDate, ticket.ClosingDate);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestSetClosingDate_InvalidDate_BeforeAttention()
    {
        DateTime invalidDateClosing = DateTime.Today.AddDays(-1).Date;
        DateTime validDateAttention = DateTime.Today.Date;
        ticket.SetAttentionDate(validDateAttention);
        ticket.SetClosingDate(invalidDateClosing);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestSetClosingDate_InvalidDate_AlreadySet()
    {
        DateTime validDate = DateTime.Today.AddDays(1).Date; 
        ticket.SetAttentionDate(DateTime.Today.Date); 
        ticket.SetClosingDate(validDate); 
        DateTime invalidDate = DateTime.Today.AddDays(2).Date; 
        ticket.SetClosingDate(invalidDate);
    }

}