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

            var result = loginController.Login("correo@correo.com", "contraseña123!");
            var okResult = result as OkObjectResult;
            var succesfulLoginResponse = okResult.Value as SuccessfulLoginOutput;

            _sessionService.VerifyAll();
            Assert.AreEqual(succesfulLoginResponse.Token, session.Token);

        }

    }
}
