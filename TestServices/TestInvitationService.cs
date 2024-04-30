using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDataAcess;
using Services;
using Domain;
using Moq;
using IServices;
using System.Linq.Expressions;

namespace TestServices
{
    [TestClass]
    public class TestInvitationService
    {
        private Mock<IGenericRepository<Invitation>> _invitationRepositoryMock;
        private InvitationService _service;

        [TestInitialize]
        public void setUp()
        {
            _invitationRepositoryMock = new Mock<IGenericRepository<Invitation>>(MockBehavior.Strict);
        }

        [TestMethod]
        public void TestCreateInvitation()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => null);

            _invitationRepositoryMock.Setup(r => r.Insert(It.IsAny<Invitation>())).Verifiable();

            _service = new InvitationService(_invitationRepositoryMock.Object);

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
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => new Invitation());

            _service = new InvitationService(_invitationRepositoryMock.Object);

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
        public void TestDeleteInvitation()
        {
            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int id) => new Invitation());

            _invitationRepositoryMock.Setup(r => r.Delete(It.IsAny<Invitation>())).Verifiable();

            _service = new InvitationService(_invitationRepositoryMock.Object);

            _service.DeleteInvitation(1);

            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TestModifyInvitation()
        {
            _invitationRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((int id) => new Invitation());

            _invitationRepositoryMock.Setup(r => r.Update(It.IsAny<Invitation>())).Verifiable();

            _service = new InvitationService(_invitationRepositoryMock.Object);

            _service.ModifyInvitation(1, DateTime.Now.AddDays(5));

            _invitationRepositoryMock.VerifyAll();

        }

        [TestMethod]
        public void TestAcceptInvitation()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => new Invitation());

            _invitationRepositoryMock.Setup(r => r.Update(It.IsAny<Invitation>())).Verifiable();

            _service = new InvitationService(_invitationRepositoryMock.Object);

            var invitation = new Invitation
            {
                Email = "mail@test.com",
                Name = "Test",
                ExpirationDate = DateTime.Now.AddDays(3)
            };

            var manager = _service.AcceptInvitation(invitation);

            _invitationRepositoryMock.VerifyAll();

            Assert.IsNotNull(manager);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAcceptInvitationDoesNotExist()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => null);

            _service = new InvitationService(_invitationRepositoryMock.Object);

            var invitation = new Invitation
            {
                Email = "mail@test.com",
                Name = "Test",
                ExpirationDate = DateTime.Now.AddDays(3)
            };

            _service.AcceptInvitation(invitation);
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TestRejectInvitation()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => new Invitation());

            _invitationRepositoryMock.Setup(r => r.Delete(It.IsAny<Invitation>())).Verifiable();

            _service = new InvitationService(_invitationRepositoryMock.Object);

            _service.RejectInvitation("mail@test.com");
            _invitationRepositoryMock.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRejectInvitationDoesNotExist()
        {
            _invitationRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<List<string>>())).Returns((Expression<Func<Invitation, bool>> predicate, List<string> includes) => null);

            _service = new InvitationService(_invitationRepositoryMock.Object);

            _service.RejectInvitation("mail@test.com");
            _invitationRepositoryMock.VerifyAll();

        }

    }
}
