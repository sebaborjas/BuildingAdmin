using Domain;
using IServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using DTO.In;
using DTO.Out;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi
{
    [TestClass]
    public class TestLoginController
    {

        Mock<ISessionService> _sessionService;

        [TestMethod]
        public void TestSuccessfulLogin()
        {
            Session session = new Session()
            {
                Id = 1,
                User = new Administrator()
            };
            _sessionService = new Mock<ISessionService>(MockBehavior.Strict);
            _sessionService.Setup(r => r.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(session);
            var loginController = new LoginController(_sessionService.Object);

            var loginInput = new LoginInput()
            {
                Email = "correo@correo.com",
                Password = "contraseña123!"
            };

            var result = loginController.Login(loginInput);
            var okResult = result as OkObjectResult;
            var succesfulLoginResponse = okResult.Value as SuccessfulLoginOutput;

            _sessionService.VerifyAll();
            Assert.AreEqual(succesfulLoginResponse.Token, session.Token);

        }

        [TestMethod]
        public void TestFailedLogin()
        {
            _sessionService = new Mock<ISessionService>(MockBehavior.Strict);
            _sessionService.Setup(r => r.Login(It.IsAny<string>(), It.IsAny<string>())).Throws(new InvalidDataException());
            var loginController = new LoginController(_sessionService.Object);

            var loginInput = new LoginInput()
            {
                Email = "correoMal@correo.com",
                Password = "contraseña123!"
            };
            var result = loginController.Login(loginInput);

            _sessionService.VerifyAll();
            Assert.IsTrue(result.GetType().Equals(typeof(UnauthorizedObjectResult)));
        }

    }
}
