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

        private CompanyAdministrator _user;
        private ConstructionCompany _company;

        [TestInitialize]
        public void SetUp()
        {
            _constructionCompanyRepositoryMock = new Mock<IGenericRepository<ConstructionCompany>>(MockBehavior.Strict);
            _sessionServiceMock = new Mock<ISessionService>(MockBehavior.Strict);

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
            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _constructionCompanyRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<ConstructionCompany, bool>> predicate, List<string> includes) => null);

            _constructionCompanyRepositoryMock.Setup(r => r.Insert(It.IsAny<ConstructionCompany>())).Verifiable();

            var constructionCompany = _service.CreateConstructionCompany("Test Construction Company");

            _constructionCompanyRepositoryMock.VerifyAll();
            Assert.AreEqual("Test Construction Company", constructionCompany.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestCreateConstructionCompanyWithNullUser()
        {
            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object);
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
            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object);
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
            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object);
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

            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object);
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
            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object);
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
            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object);
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
            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object);
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
            _service = new ConstructionCompanyService(_constructionCompanyRepositoryMock.Object, _sessionServiceMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _constructionCompanyRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<ConstructionCompany, bool>> predicate, List<string> includes) => new ConstructionCompany());

            _constructionCompanyRepositoryMock.Setup(r => r.Update(It.IsAny<ConstructionCompany>())).Verifiable();

            var constructionCompany = _service.ModifyConstructionCompany("");

            _constructionCompanyRepositoryMock.VerifyAll();
        }
    }
}
