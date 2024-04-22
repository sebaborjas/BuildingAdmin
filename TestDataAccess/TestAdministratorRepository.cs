using DataAccess;

namespace TestDataAccess
{
    [TestClass]
    public class TestAdministratorRepository
    {
        private BuildingAdminContext _context;

        private SqlLiteConnection _connection;

        private AdministratorRepository _repository;

        [TestInitialize]
        public void SetUp()
        {
            _connection = new SqlLiteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<BuildingAdminContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new BuildingAdminContext(options);

            _repository = new AdministratorRepository(_context);
        }

        [TestMethod]
        public void ListAllAdministrators()
        {
            
        }
    }
}