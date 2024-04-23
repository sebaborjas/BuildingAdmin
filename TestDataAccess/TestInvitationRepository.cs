using Domain;
using DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.DataTypes;

namespace TestDataAccess;

[TestClass]
public class TestInvitationRepository
{
  private BuildingAdminContext _context;

  private SqliteConnection _connection;

  private InvitationRepository _repository;

  [TestInitialize]
  public void SetUp()
  {
    _connection = new SqliteConnection("DataSource=:memory:");
    _connection.Open();

    var options = new DbContextOptionsBuilder<BuildingAdminContext>()
      .UseSqlite(_connection)
      .Options;

    _context = new BuildingAdminContext(options);
    _context.Database.EnsureCreated();

    _repository = new InvitationRepository(_context);
  }

  [TestMethod]
  public void TestAddInvitation()
  {
    Invitation invitation = new Invitation
    {
      Id = 1,
      Email = "test@test.com",
      ExpirationDate = DateTime.Now.AddDays(15),
      Status = InvitationStatus.Pending
    };

    _repository.Insert(invitation);

    Assert.AreEqual(invitation, _context.Invitations.Find(1));
  }
}