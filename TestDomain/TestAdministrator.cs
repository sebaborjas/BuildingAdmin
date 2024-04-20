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
     _administrator = new Administrator(); 
    }
    
    [TestMethod]
    public void TestId()
    {
      _administrator.Id = 1;
      Assert.AreEqual(1, _administrator.Id);
    }
  }
}