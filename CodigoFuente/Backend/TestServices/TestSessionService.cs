using Domain;
using IDataAccess;
using IServices;
using Moq;
using Services;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServices
{
    [TestClass]
    public class TestSessionService
    {
        private ISessionService _sessionService;
        private Mock<ISessionRepository> _sessionRepository;
        private Mock<IGenericRepository<MaintenanceOperator>> _maintenanceOperatorRepository;
        private Mock<IGenericRepository<Administrator>> _adminRepository;
        private Mock<IGenericRepository<Manager>> _managerRepository;
        private Mock<IGenericRepository<CompanyAdministrator>> _companyAdministratorRepository;

        [TestInitialize]
        public void Setup()
        {
            _sessionRepository = new Mock<ISessionRepository>(MockBehavior.Strict);
            _maintenanceOperatorRepository = new Mock<IGenericRepository<MaintenanceOperator>>(MockBehavior.Strict);
            _adminRepository = new Mock<IGenericRepository<Administrator>>(MockBehavior.Strict);
            _managerRepository = new Mock<IGenericRepository<Manager>>(MockBehavior.Strict);
            _companyAdministratorRepository = new Mock<IGenericRepository<CompanyAdministrator>>(MockBehavior.Strict);

        }

        [TestMethod]
        public void TestGetCurrentUser()
        {

            var session = new Session()
            {
                Id = 1,
                User = new Administrator(),
            };
            _sessionRepository.Setup(r => r.GetByToken(It.IsAny<Guid>())).Returns(session);
            _sessionService = new SessionService(_sessionRepository.Object, _managerRepository.Object, _adminRepository.Object, _maintenanceOperatorRepository.Object, _companyAdministratorRepository.Object);

            var result = _sessionService.GetCurrentUser(session.Token);

            _sessionRepository.VerifyAll();
            Assert.AreEqual(session.User, result);
        }

        [TestMethod]
        public void TestGetNullCurrentUser()
        {
            Session session = null;
            _sessionRepository.Setup(r => r.GetByToken(It.IsAny<Guid>())).Returns(session);
            _sessionService = new SessionService(_sessionRepository.Object, _managerRepository.Object, _adminRepository.Object, _maintenanceOperatorRepository.Object, _companyAdministratorRepository.Object);

            var result = _sessionService.GetCurrentUser(Guid.NewGuid());

            _sessionRepository.VerifyAll();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestGetCurrentUserWithoutTokenAfterUseToken()
        {
            var session = new Session()
            {
                Id = 1,
                User = new Administrator(),
            };
            _sessionRepository.Setup(r => r.GetByToken(It.IsAny<Guid>())).Returns(session);
            _sessionService = new SessionService(_sessionRepository.Object, _managerRepository.Object, _adminRepository.Object, _maintenanceOperatorRepository.Object, _companyAdministratorRepository.Object);

            var resultUsingToken = _sessionService.GetCurrentUser(session.Token);
            var resultWithoutToken = _sessionService.GetCurrentUser();

            _sessionRepository.VerifyAll();
            Assert.AreEqual(session.User, resultWithoutToken);
        }

        [TestMethod]
        public void TestGetNullCurrentUserWithoutToken()
        {
            _sessionService = new SessionService(_sessionRepository.Object, _managerRepository.Object, _adminRepository.Object, _maintenanceOperatorRepository.Object, _companyAdministratorRepository.Object);

            var result = _sessionService.GetCurrentUser();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestLoginWithAdminCredentials()
        {
            var adminUser = new Administrator()
            {
                Id = 1,
                Email = "admin@correo.com",
                Name = "Administrador",
                LastName = "Administrator",
                Password = "Contra123.!"
            };
            _adminRepository.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns(adminUser);
            _sessionRepository.Setup(r => r.Insert(It.IsAny<Session>())).Verifiable();
            _sessionService = new SessionService(_sessionRepository.Object, _managerRepository.Object, _adminRepository.Object, _maintenanceOperatorRepository.Object, _companyAdministratorRepository.Object);

            var session = _sessionService.Login("admin@correo.com", "Contra123.!");

            _adminRepository.VerifyAll();
            _sessionRepository.VerifyAll();
            Assert.IsNotNull(session);
        }

        [TestMethod]
        public void TestLoginWithManagerCredentials()
        {
            var managerUser = new Manager()
            {
                Id = 1,
                Email = "manager@correo.com",
                Name = "Encargado",
                LastName = "Manager",
                Password = "Contra123.!",
                Buildings = new List<Building>()
            };
            Administrator adminUser = null;
            _adminRepository.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns(adminUser);
            _managerRepository.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns(managerUser);
            _sessionRepository.Setup(r => r.Insert(It.IsAny<Session>())).Verifiable();
            _sessionService = new SessionService(_sessionRepository.Object, _managerRepository.Object, _adminRepository.Object, _maintenanceOperatorRepository.Object, _companyAdministratorRepository.Object);

            var session = _sessionService.Login("manager@correo.com", "Contra123.!");

            _adminRepository.VerifyAll();
            _sessionRepository.VerifyAll();
            _managerRepository.VerifyAll();
            Assert.IsNotNull(session);
        }

        [TestMethod]
        public void TestLoginWithMaintenanceCredentials()
        {
            var maintenanceOperatorUser = new MaintenanceOperator()
            {
                Id = 1,
                Email = "maintenance@correo.com",
                Name = "Mantenimiento",
                LastName = "Maintenance",
                Password = "Contra123.!",
                Buildings = new List<Building>()
            };
            Administrator adminUser = null;
            Manager managerUser = null;
            _adminRepository.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns(adminUser);
            _managerRepository.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns(managerUser);
            _maintenanceOperatorRepository.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns(maintenanceOperatorUser);
            _sessionRepository.Setup(r => r.Insert(It.IsAny<Session>())).Verifiable();
            _sessionService = new SessionService(_sessionRepository.Object, _managerRepository.Object, _adminRepository.Object, _maintenanceOperatorRepository.Object, _companyAdministratorRepository.Object);

            var session = _sessionService.Login("maintenance@correo.com", "Contra123.!");

            _adminRepository.VerifyAll();
            _sessionRepository.VerifyAll();
            _managerRepository.VerifyAll();
            _maintenanceOperatorRepository.VerifyAll();
            Assert.IsNotNull(session);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void TestLoginWithWrongPassword()
        {
            var adminUser = new Administrator()
            {
                Id = 1,
                Email = "admin@correo.com",
                Name = "Administrador",
                LastName = "Administrator",
                Password = "Contra123.!"
            };
            _adminRepository.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns(adminUser);
            _sessionRepository.Setup(r => r.Insert(It.IsAny<Session>())).Verifiable();
            _sessionService = new SessionService(_sessionRepository.Object, _managerRepository.Object, _adminRepository.Object, _maintenanceOperatorRepository.Object, _companyAdministratorRepository.Object);

            var session = _sessionService.Login("admin@correo.com", "Mal123.!");

            _adminRepository.VerifyAll();
            _sessionRepository.VerifyAll();
            Assert.IsNull(session);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void TestLoginWithWrongEmail()
        {
            MaintenanceOperator maintenanceOperator = null;
            Administrator admin = null;
            Manager manager = null;
            CompanyAdministrator companyAdministrator = null;

            _adminRepository.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns(admin);
            _managerRepository.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns(manager);
            _maintenanceOperatorRepository.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns(maintenanceOperator);
            _companyAdministratorRepository.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<CompanyAdministrator, bool>>>(), It.IsAny<List<string>>())).Returns(companyAdministrator);
            _sessionService = new SessionService(_sessionRepository.Object, _managerRepository.Object, _adminRepository.Object, _maintenanceOperatorRepository.Object, _companyAdministratorRepository.Object);

            var session = _sessionService.Login("adminMal@correo.com", "Pass123.!");
        }
    }
}
