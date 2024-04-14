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

}