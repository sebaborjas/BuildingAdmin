using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDataAccess;
using Services;
using Domain;
using Moq;
using IServices;
using System.Linq.Expressions;
using Domain.DataTypes;

namespace TestServices
{
    [TestClass]
    public class TestInvitationService
    {
        private Mock<IGenericRepository<Invitation>> _invitationRepositoryMock;
        private Mock<IGenericRepository<Administrator>> _adminRepositoryMock;
        private Mock<IGenericRepository<Manager>> _managerRepositoryMock;
        private Mock<IGenericRepository<CompanyAdministrator>> _companyAdministratorRepositoryMock;
        private Mock<ISessionService> _sessionServiceMock;
        private InvitationService _service;

        [TestInitialize]
        public void Setup()
        {
            _invitationRepositoryMock = new Mock<IGenericRepository<Invitation>>(MockBehavior.Strict);
            _adminRepositoryMock = new Mock<IGenericRepository<Administrator>>(MockBehavior.Strict);
            _managerRepositoryMock = new Mock<IGenericRepository<Manager>>(MockBehavior.Strict);
            _companyAdministratorRepositoryMock = new Mock<IGenericRepository<CompanyAdministrator>>(MockBehavior.Strict);
            _sessionServiceMock = new Mock<ISessionService>(MockBehavior.Strict);
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object, _companyAdministratorRepositoryMock.Object, _sessionServiceMock.Object);
        }
        
        [TestMethod]
        public void TestCreateInvitation()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => null);
            _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => null);

            _invitationRepositoryMock.Setup(r => r.Insert(It.IsAny<Invitation>())).Verifiable();

            var invitation = new Invitation
            {
                Email = "test@tes.com",
                ExpirationDate = DateTime.Now.AddDays(3),
                Name = "Test",
            };

            var invitationModel = _service.CreateInvitation(invitation);

            _invitationRepositoryMock.VerifyAll();
            Assert.AreEqual(invitation, invitationModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateInvitationAlreadyExist()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => new Invitation()); ;
            _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => null);

            var existingInvitation = new Invitation
            {
                Email = "mail@test.com",
                ExpirationDate = DateTime.Now.AddDays(3),
                Name = "Test",
            };

            _service.CreateInvitation(existingInvitation);

            _invitationRepositoryMock.Verify(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateInvitationUserAlreadyExist()
        {
            var existingUser = new Administrator
            {
                Email = "user@test.com",
                Name = "Test",
                Password = "1234.Pass!"
            };

            _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => existingUser);

            var invitation = new Invitation
            {
                Email = "user@test.com",
                ExpirationDate = DateTime.Now.AddDays(3),
                Name = "Test"
            };

            _service.CreateInvitation(invitation);
            _invitationRepositoryMock.Verify(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>()), Times.Once);

            _adminRepositoryMock.Verify(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>()), Times.Once);

            _invitationRepositoryMock.Verify(r => r.Insert(It.IsAny<Invitation>()), Times.Once);

        }

        [TestMethod]
        public void TestDeleteInvitation()
        {
            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int invitationId) => new Invitation());

            _invitationRepositoryMock.Setup(r => r.Delete(It.IsAny<Invitation>())).Verifiable();

            _service.DeleteInvitation(1);

            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDeleteInvitationAccepted()
        {
            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int invitationId) => new Invitation { Status = Domain.DataTypes.InvitationStatus.Accepted });

            _service.DeleteInvitation(1);

            _invitationRepositoryMock.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestModifyInvitation()
        {
            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int invitationId) => new Invitation());

            _invitationRepositoryMock.Setup(r => r.Update(It.IsAny<Invitation>())).Verifiable();

            _service.ModifyInvitation(1, DateTime.Now.AddDays(5));

            _invitationRepositoryMock.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestModifyInvitationAccepted()
        {
            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int invitationId) => new Invitation { Status = Domain.DataTypes.InvitationStatus.Accepted });

            _service.ModifyInvitation(1, DateTime.Now.AddDays(5));

            _invitationRepositoryMock.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestModifyInvitationNotExpired()
        {
            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int invitationId) => new Invitation { ExpirationDate = DateTime.Now.AddDays(5), Status = Domain.DataTypes.InvitationStatus.Pending });

            _service.ModifyInvitation(1, DateTime.Now.AddDays(8));

            _invitationRepositoryMock.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        }


        [TestMethod]
        public void TestAcceptInvitationForManager()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => new Invitation() { Name = "Test", Email = "mail@test.com", ExpirationDate = DateTime.Now.AddDays(4), Id = 1, Status = InvitationStatus.Pending, Role = InvitationRoles.Manager });

            _managerRepositoryMock.Setup(r => r.Insert(It.IsAny<Manager>())).Verifiable();
            _invitationRepositoryMock.Setup(r => r.Delete(It.IsAny<Invitation>())).Verifiable();

            var invitation = new Invitation
            {
                Email = "mail@test.com",
            };
            var manager = _service.AcceptInvitation(invitation, "contraseña123!");

            Assert.IsNotNull(manager);

            _invitationRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAcceptInvitationDoesNotExist()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => null);

            var invitation = new Invitation
            {
                Email = "mail@test.com",
                Name = "Test",
                ExpirationDate = DateTime.Now.AddDays(3)
            };

            _service.AcceptInvitation(invitation, "contraseña123!");
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAcceptInvitationAlreadyAccepted()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => new Invitation { Status = Domain.DataTypes.InvitationStatus.Accepted });

            var invitation = new Invitation
            {
                Email = "mail@test.com",
                Name = "Test",
                ExpirationDate = DateTime.Now.AddDays(3)
            };

            _service.AcceptInvitation(invitation, "contraseña123!");
            _invitationRepositoryMock.VerifyAll();
        }


        [TestMethod]
        public void TestRejectInvitation()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => new Invitation());

            _invitationRepositoryMock.Setup(r => r.Delete(It.IsAny<Invitation>())).Verifiable();

            _service.RejectInvitation("mail@test.com");
            _invitationRepositoryMock.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRejectInvitationDoesNotExist()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => null);

            _service.RejectInvitation("mail@test.com");
            _invitationRepositoryMock.VerifyAll();

        }

        [TestMethod]
        public void TestAcceptInvitationForConstructionCompanyAdministrator()
        {
            var invitation = new Invitation
            {
                Id = 1,
                Name = "Test",
                Email = "mail@test.com",
                ExpirationDate = DateTime.Now.AddDays(4),
                Status = InvitationStatus.Pending,
                Role = InvitationRoles.ConstructionCompanyAdministrator
            };
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>()))
                .Returns(invitation);

            _companyAdministratorRepositoryMock.Setup(r => r.Insert(It.IsAny<CompanyAdministrator>())).Verifiable();
            _invitationRepositoryMock.Setup(r => r.Delete(It.IsAny<Invitation>())).Verifiable();

            var invitationToAccept = new Invitation()
            {
                Email = invitation.Email
            };
            var companyAdministrator = _service.AcceptInvitation(invitationToAccept, "contraseña123!");

            Assert.IsNotNull(companyAdministrator);

            _invitationRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetInvalidInvitation()
        {

            var currentUser = new Administrator
            {
                Email = "user@test.com",
                Name = "Test",
                Password = "1234.Pass!"
            };
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), null)).Returns((Invitation)null);

            _service.GetInvitation(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetInvitationWithoutUser()
        {

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns((User)null);
            _service.GetInvitation(1);
        }

        [TestMethod]
        public void TestGetInvitation()
        {
            Invitation invitation = new Invitation();

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(new Administrator());
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns(invitation);
            var result = _service.GetInvitation(1);

            _sessionServiceMock.VerifyAll();
            _invitationRepositoryMock.VerifyAll();
            Assert.AreEqual(invitation, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetAllInvitationWithoutUser()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns((User)null);

            _service.GetAllInvitations();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetAllInvitationNull()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(new Administrator());
            _invitationRepositoryMock.Setup(r => r.GetAll<Invitation>()).Returns((List<Invitation>)null);

            _service.GetAllInvitations();
        }

        [TestMethod]
        public void TestGetAllInvitation()
        {
            List<Invitation> invitations = [new Invitation(), new Invitation()];
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(new Administrator());
            _invitationRepositoryMock.Setup(r => r.GetAll<Invitation>()).Returns(invitations);

            var result = _service.GetAllInvitations();

            _sessionServiceMock.VerifyAll();
            _invitationRepositoryMock.VerifyAll();
            CollectionAssert.AreEqual(result, invitations);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TesModifyInvalidInvitation()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(new Administrator());
            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((Invitation)null);

            _service.ModifyInvitation(1, new DateTime().AddDays(5));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestModifyInvitationError()
        {
            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int invitationId) => new Invitation());

            _invitationRepositoryMock.Setup(r => r.Update(It.IsAny<Invitation>())).Throws(new Exception());

            _service.ModifyInvitation(1, DateTime.Now.AddDays(5));

            _invitationRepositoryMock.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAcceptExpiredInvitation()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => new Invitation() { Name = "Test", Email = "mail@test.com", ExpirationDate = DateTime.Now, Id = 1, Status = InvitationStatus.Pending, Role = InvitationRoles.Manager });

            var invitation = new Invitation
            {
                Email = "mail@test.com",
            };
            var manager = _service.AcceptInvitation(invitation, "contraseña123!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAcceptInvitationError()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => new Invitation() { Name = "Test", Email = "mail@test.com", ExpirationDate = DateTime.Now.AddDays(4), Id = 1, Status = InvitationStatus.Pending, Role = InvitationRoles.Manager });

            _managerRepositoryMock.Setup(r => r.Insert(It.IsAny<Manager>())).Verifiable();
            _invitationRepositoryMock.Setup(r => r.Delete(It.IsAny<Invitation>())).Throws(new Exception());

            var invitation = new Invitation
            {
                Email = "mail@test.com",
            };
            var manager = _service.AcceptInvitation(invitation, "contraseña123!");

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestCreateInvitationError()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => null);
            _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => null);

            _invitationRepositoryMock.Setup(r => r.Insert(It.IsAny<Invitation>())).Throws(new Exception());

            var invitation = new Invitation
            {
                Email = "test@tes.com",
                ExpirationDate = DateTime.Now.AddDays(3),
                Name = "Test",
            };

            var invitationModel = _service.CreateInvitation(invitation);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateInvalidInvitation()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => null);
            _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => null);

            var invitation = new Invitation
            {
                Email = "test@tes.com",
                ExpirationDate = DateTime.Now.AddDays(3),
            };

            var invitationModel = _service.CreateInvitation(invitation);

        }
    }
}
