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
        Ticket ticket = new Ticket();
        ticket.Id = 1;
        Assert.AreEqual(1, ticket.Id);
    }

    [TestMethod]
    public void TestIdNegative()
    {
        Ticket ticket = new Ticket();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => ticket.Id = -1);
    }
}