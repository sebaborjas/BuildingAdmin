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

  [TestMethod]
  public void TestUpdateInvitation()
  {
    List<Invitation> list = Data();
    LoadConext(list);

    Invitation invitation = list[2];

    string nuevoEmail = "nuevo@email.com";

    invitation.Email = nuevoEmail;

    _repository.Update(invitation);

    _context.SaveChanges();

    Assert.AreEqual(nuevoEmail, _context.Invitations.Find(3).Email);
  }

  [TestMethod]
  public void TestGetInvitation()
  {
    List<Invitation> list = Data();
    LoadConext(list);

    Invitation invitation = list[0];

    var admin = _repository.Get(1);

    Assert.AreEqual(invitation, admin);
  }

  [TestMethod]
  public void TestGetAllInvitations()
  {
    List<Invitation> list = Data();
    LoadConext(list);

    var invitations = _repository.GetAll<Invitation>().ToList();

    CollectionAssert.AreEqual(list, invitations);
  }

  [TestMethod]
  public void TestGetAllNoList()
  {
    var invitations = _repository.GetAll<Invitation>().ToList();

    int count = invitations.Count;

    Assert.AreEqual(0, count);
  }

  [TestMethod]
  public void TestDeleteInvitation()
  {
    List<Invitation> list = Data();
    LoadConext(list);

    Invitation invitation = list[1];

    _repository.Delete(invitation);

    _context.SaveChanges();

    Assert.IsNull(_context.Invitations.Find(2));
  }

  [TestCleanup]
  public void CleanUp()
  {
    _context.Database.EnsureDeleted();
    _context.Dispose();
    _connection.Close();
  }

  private List<Invitation> Data()
  {
    List<Invitation> list = new()
    {
      new Invitation
      {
        Id = 1,
        Email = "manager1@email.com",
        ExpirationDate = DateTime.Now.AddDays(15),
        Status = InvitationStatus.Pending
      },
      new Invitation
      {
        Id = 2,
        Email = "manager2@email.com",
        ExpirationDate = DateTime.Now.AddDays(15),
        Status = InvitationStatus.Pending
      },
      new Invitation
      {
        Id = 3,
        Email = "manager3@email.com",
        ExpirationDate = DateTime.Now.AddDays(15),
        Status = InvitationStatus.Pending
      }
    };
    return list;
  }

  private void LoadConext(List<Invitation> list)
  {
    _context.Invitations.AddRange(list);
    _context.SaveChanges();
  }
}