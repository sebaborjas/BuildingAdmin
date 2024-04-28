using Domain;
using IDataAccess;
using IDataAcess;
using IServices;
using Moq;
using Services;
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

        [TestMethod]
        public void TestGetCurrentUser() {

            var session = new Session()
            {
                Id = 1,
                User = new Administrator(),
            };

            _sessionRepository = new Mock<ISessionRepository>(MockBehavior.Strict);
            _sessionRepository.Setup(r=>r.GetByToken(It.IsAny<Guid>())).Returns(session);
            _sessionService = new SessionService(_sessionRepository.Object);

            var result = _sessionService.GetCurrentUser(session.Token);

            Assert.AreEqual(session.User, result);
        }

        [TestMethod]
        public void TestGetNullCurrentUser()
        {

            Session session = null;

            _sessionRepository = new Mock<ISessionRepository>(MockBehavior.Strict);
            _sessionRepository.Setup(r => r.GetByToken(It.IsAny<Guid>())).Returns(session);
            _sessionService = new SessionService(_sessionRepository.Object);

            var result = _sessionService.GetCurrentUser(Guid.NewGuid());

            Assert.IsNull(result);
        }
    }
}
