using Domain;

namespace TestDomain
{
  [TestClass]
  public class TestAdministrator
  {
    private Administrator _administrator;

    [TestInitialize]
    public void SetUp()
    {
     _administrator = new Administrator{Id = 1}; 
    }

    [TestMethod]
    public void TestGetId()
    {
      Assert.AreEqual(1, _administrator.Id);
    }
    
    [TestMethod]
    public void TestSetId()
    {
      _administrator.Id = 4;
      Assert.AreEqual(4, _administrator.Id);
    }
  }
}