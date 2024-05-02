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
        private InvitationService _service;

        [TestInitialize]
        public void setUp()
        {
            _invitationRepositoryMock = new Mock<IGenericRepository<Invitation>>(MockBehavior.Strict);
            _adminRepositoryMock = new Mock<IGenericRepository<Administrator>>(MockBehavior.Strict);
            _managerRepositoryMock = new Mock<IGenericRepository<Manager>>(MockBehavior.Strict);

        }

        [TestMethod]
        public void TestCreateInvitation()
        {
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

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
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

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
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

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
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int invitationId) => new Invitation());

            _invitationRepositoryMock.Setup(r => r.Delete(It.IsAny<Invitation>())).Verifiable();

            _service.DeleteInvitation(1);

            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteInvitationAccepted()
        {
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int invitationId) => new Invitation { Status = Domain.DataTypes.InvitationStatus.Accepted });

            _service.DeleteInvitation(1);

            _invitationRepositoryMock.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestModifyInvitation()
        {
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int invitationId) => new Invitation());

            _invitationRepositoryMock.Setup(r => r.Update(It.IsAny<Invitation>())).Verifiable();

            _service.ModifyInvitation(1, DateTime.Now.AddDays(5));

            _invitationRepositoryMock.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestModifyInvitationAccepted()
        {
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int invitationId) => new Invitation { Status = Domain.DataTypes.InvitationStatus.Accepted });

            _service.ModifyInvitation(1, DateTime.Now.AddDays(5));

            _invitationRepositoryMock.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestModifyInvitationNotExpired()
        {
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int invitationId) => new Invitation { ExpirationDate = DateTime.Now.AddDays(5), Status = Domain.DataTypes.InvitationStatus.Pending });

            _service.ModifyInvitation(1, DateTime.Now.AddDays(8));

            _invitationRepositoryMock.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        }


        [TestMethod]
        public void TestAcceptInvitation()
        {
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => new Invitation());

            _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<Manager, bool>> predicate, List<string> includes) => new Manager());

            _invitationRepositoryMock.Setup(r => r.Update(It.IsAny<Invitation>()));

            _managerRepositoryMock.Setup(r => r.Insert(It.IsAny<Manager>())).Verifiable();

            var invitation = new Invitation
            {
                Email = "mail@test.com",
                Name = "Test",
                ExpirationDate = DateTime.Now.AddDays(3),
                Status = InvitationStatus.Pending
            };
            var manager = _service.AcceptInvitation(invitation, "contraseña123!");

            Assert.IsNotNull(manager);

            _invitationRepositoryMock.Verify(r => r.Update(It.IsAny<Invitation>()), Times.Once);
            _managerRepositoryMock.Verify(r => r.Insert(It.IsAny<Manager>()), Times.Once);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAcceptInvitationDoesNotExist()
        {
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

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
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

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
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => new Invitation());

            _invitationRepositoryMock.Setup(r => r.Delete(It.IsAny<Invitation>())).Verifiable();

            _service.RejectInvitation("mail@test.com");
            _invitationRepositoryMock.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRejectInvitationDoesNotExist()
        {
            _service = new InvitationService(_invitationRepositoryMock.Object, _adminRepositoryMock.Object, _managerRepositoryMock.Object);

            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => null);

            _service.RejectInvitation("mail@test.com");
            _invitationRepositoryMock.VerifyAll();

        }

    }
}
