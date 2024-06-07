using Moq;
using System;
using System.Linq.Expressions;
using IDataAccess;
using Services;
using Domain;
using IServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestServices
{
    [TestClass]
    public class TestConstructionCompanyService
    {
        private ConstructionCompanyService _service;

        private Mock<ISessionService> _sessionServiceMock;
        private Mock<IGenericRepository<ConstructionCompany>> _constructionCompanyRepositoryMock;
        private Mock<IGenericRepository<CompanyAdministrator>> _companyAdministratorRepositoryMock;

        private CompanyAdministrator _user;
        private ConstructionCompany _company;

        [TestInitialize]
        public void SetUp()
        {
            _constructionCompanyRepositoryMock = new Mock<IGenericRepository<ConstructionCompany>>(MockBehavior.Strict);
            _sessionServiceMock = new Mock<ISessionService>(MockBehavior.Strict);
            _companyAdministratorRepositoryMock = new Mock<IGenericRepository<CompanyAdministrator>>(MockBehavior.Strict);
            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object, _companyAdministratorRepositoryMock.Object);

            _user = new CompanyAdministrator()
            {
                Email = "companyAdmin@correo.com",
                Id = 1,
                Name = "Test",
                LastName = "Example",
                Password = "Test.1234!"
            };
        }

        [TestMethod]
        public void TestCreateConstructionCompany()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _constructionCompanyRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<ConstructionCompany, bool>> predicate, List<string> includes) => null);

            _constructionCompanyRepositoryMock.Setup(r => r.Insert(It.IsAny<ConstructionCompany>())).Verifiable();
            _companyAdministratorRepositoryMock.Setup(r => r.Update(It.IsAny<CompanyAdministrator>())).Verifiable();

            var constructionCompany = _service.CreateConstructionCompany("Test Construction Company");

            _constructionCompanyRepositoryMock.VerifyAll();
            _companyAdministratorRepositoryMock.VerifyAll();
            Assert.AreEqual("Test Construction Company", constructionCompany.Name);
            Assert.AreEqual(_user.ConstructionCompany, constructionCompany);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestCreateConstructionCompanyWithNullUser()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns((User)null);

            _constructionCompanyRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<ConstructionCompany, bool>> predicate, List<string> includes) => null);

            _constructionCompanyRepositoryMock.Setup(r => r.Insert(It.IsAny<ConstructionCompany>())).Verifiable();

            var constructionCompany = _service.CreateConstructionCompany("Test Construction Company");

            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateConstructionCompanyWithUserWithConstructionCompany()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _constructionCompanyRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<ConstructionCompany, bool>> predicate, List<string> includes) => new ConstructionCompany());

            _constructionCompanyRepositoryMock.Setup(r => r.Insert(It.IsAny<ConstructionCompany>())).Verifiable();

            var constructionCompany = _service.CreateConstructionCompany("Test Construction Company");

            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateConstructionCompanyWithNullName()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _constructionCompanyRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<ConstructionCompany, bool>> predicate, List<string> includes) => null);

            _constructionCompanyRepositoryMock.Setup(r => r.Insert(It.IsAny<ConstructionCompany>())).Verifiable();

            var constructionCompany = _service.CreateConstructionCompany(null);

            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TestModifyConstructionCompany()
        {

            var currentUser = new CompanyAdministrator()
            {
                Email = "companyAdmin@correo.com",
                Id = 1,
                Name = "Test",
                LastName = "Example",
                Password = "Test.1234!",
                ConstructionCompany = new ConstructionCompany()
                {
                    Id = 1,
                    Name = "Test Construction Company"
                }
            };

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);

            _constructionCompanyRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<ConstructionCompany, bool>> predicate, List<string> includes) => null);

            _constructionCompanyRepositoryMock.Setup(r => r.Update(It.IsAny<ConstructionCompany>())).Verifiable();

            var constructionCompany = _service.ModifyConstructionCompany("Construction Company");
            _constructionCompanyRepositoryMock.VerifyAll();
            Assert.AreEqual("Construction Company", constructionCompany.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestModifyConstructionCompanyWithNullUser()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns((User)null);

            _constructionCompanyRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<ConstructionCompany, bool>> predicate, List<string> includes) => new ConstructionCompany());

            _constructionCompanyRepositoryMock.Setup(r => r.Update(It.IsAny<ConstructionCompany>())).Verifiable();

            var constructionCompany = _service.ModifyConstructionCompany("Test Construction Company");

            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestModifyConstructionCompanyWithUserWithoutConstructionCompany()
        {
             _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _constructionCompanyRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<ConstructionCompany, bool>> predicate, List<string> includes) => null);

            _constructionCompanyRepositoryMock.Setup(r => r.Update(It.IsAny<ConstructionCompany>())).Verifiable();

            var constructionCompany = _service.ModifyConstructionCompany("Test Construction Company");

            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestModifyConstructionCompanyWithNullName()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _constructionCompanyRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<ConstructionCompany, bool>> predicate, List<string> includes) => new ConstructionCompany());

            _constructionCompanyRepositoryMock.Setup(r => r.Update(It.IsAny<ConstructionCompany>())).Verifiable();

            var constructionCompany = _service.ModifyConstructionCompany(null);

            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestModifyConstructionCompanyWithEmptyName()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _constructionCompanyRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<ConstructionCompany, bool>> predicate, List<string> includes) => new ConstructionCompany());

            _constructionCompanyRepositoryMock.Setup(r => r.Update(It.IsAny<ConstructionCompany>())).Verifiable();

            var constructionCompany = _service.ModifyConstructionCompany("");

            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TestGetUserCompany()
        {
            var company = new ConstructionCompany()
            {
                Id = 1,
                Name = "Company"
            };
            _user.ConstructionCompany = company;
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object);

            var result = _service.GetUserCompany();
            
            _sessionServiceMock.VerifyAll();
            Assert.AreEqual(result, company);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestGetNonExistentUserCompany()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object);

            var result = _service.GetUserCompany();
        }
    }
}
