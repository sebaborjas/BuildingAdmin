using Domain;
using Domain.DataTypes;
using Domain.Exceptions;

namespace TestDomain
{
    [TestClass]
    public class TestInvitation
    {
        private Invitation _invitation;

        [TestInitialize]
        public void SetUp()
        {
            _invitation = new Invitation
            {
                Id = 1,
                Email = "test@test.com",
                Name = "Test",
                ExpirationDate = DateTime.Now.Date.AddDays(14),
                Status = InvitationStatus.Pending
            };
        }

        [TestMethod]
        public void TestGetId()
        {
            int id = 1;
            Assert.AreEqual(id, _invitation.Id);
        }

        [TestMethod]
        public void TestSetId()
        {
            int id = 3;
            _invitation.Id = id;
            Assert.AreEqual(id, _invitation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSetNegativeId()
        {
            int id = -1;
            _invitation.Id = id;
        }

        [TestMethod]
        public void TestGetEmail()
        {
            string email = "test@test.com";
            Assert.AreEqual(email, _invitation.Email);
        }

        [TestMethod]
        public void TestSetEmail()
        {
            string email = "hola@hola.com";
            _invitation.Email = email;
            Assert.AreEqual(email, _invitation.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetEmptyEmailException()
        {
            string email = "";
            _invitation.Email = email;
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEmailFormatException))]
        public void TestEmailWithoutAt()
        {
            string email = "hola";
            _invitation.Email = email;
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEmailFormatException))]
        public void TestEmailWithoutDot()
        {
            string email = "hola@hola";
            _invitation.Email = email;
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEmailFormatException))]
        public void TestEmailWrongFormat()
        {
            string email = "hola@.com";
            _invitation.Email = email;
        }

        [TestMethod]
        public void TestGetExpirationDate()
        {
            DateTime date = DateTime.Now.Date.AddDays(14);
            Assert.AreEqual(date, _invitation.ExpirationDate);
        }

        [TestMethod]
        public void TestSetExpirationDate()
        {
            DateTime date = DateTime.Now.Date.AddDays(14);
            _invitation.ExpirationDate = date;
            Assert.AreEqual(date, _invitation.ExpirationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSetPastExpirationDate()
        {
            DateTime date = DateTime.Now.Date.AddDays(-2);
            _invitation.ExpirationDate = date;
        }

        [TestMethod]
        public void TestGetStatus()
        {
            InvitationStatus status = InvitationStatus.Pending;
            Assert.AreEqual(status, _invitation.Status);
        }

        [TestMethod]
        public void TestSetStatus()
        {
            InvitationStatus status = InvitationStatus.Accepted;
            _invitation.Status = status;
            Assert.AreEqual(status, _invitation.Status);

        }

        [TestMethod]
        public void TestGetName()
        {
            string name = "Test";
            Assert.AreEqual(name, _invitation.Name);
        }

    }

}