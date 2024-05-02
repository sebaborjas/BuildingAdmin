﻿using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using IServices;
using Moq;
using DTO.In;
using DTO.Out;

namespace TestWebApi
{
    [TestClass]
    public class TestInvitationController
    {

        private Mock<IInvitationService> _invitationServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _invitationServiceMock = new Mock<IInvitationService>(MockBehavior.Strict);
        }

        [TestMethod]
        public void TestCreateInvitation()
        {
            Invitation invitationCreated = new Invitation()
            {
                Id = 11,
                Email = "correoInvitado@correo.com",
                Status = Domain.DataTypes.InvitationStatus.Pending,
                ExpirationDate = DateTime.Now.AddDays(3)
            };
            _invitationServiceMock.Setup(r => r.CreateInvitation(It.IsAny<Invitation>())).Returns(invitationCreated);
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new CreateInvitationInput()
            {
                Email = "correoInvitado@correo.com",
                Name = "Nombre Invitado",
                ExpirationDate = DateTime.Now.AddDays(3)
            };

            var result = controller.CreateInvitation(input);
            var okResult = result as OkObjectResult;
            var mewInvitationResponse = okResult.Value as CreateInvitationOutput;


            _invitationServiceMock.VerifyAll();
            Assert.AreEqual(11, mewInvitationResponse.InvitationId);
        }

        [TestMethod]
        public void TestCreateInvitationWithoutBody()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            CreateInvitationInput input = null;

            var result = controller.CreateInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestObjectResult)));
        }

        [TestMethod]
        public void TestCreateInvitationWithoutEmail()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new CreateInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
                Name = "Nombre Invitado"
            };

            var result = controller.CreateInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestObjectResult)));
        }

        [TestMethod]
        public void TestCreateInvitationWithEmptyEmail()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new CreateInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
                Email = "",
                Name = "Nombre Invitado"
            };

            var result = controller.CreateInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestObjectResult)));
        }

        [TestMethod]
        public void TestCreateInvitationWithoutName()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new CreateInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
                Email = "correoInvitado@correo.com",
            };

            var result = controller.CreateInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestObjectResult)));
        }

        [TestMethod]
        public void TestCreateInvitationWithEmptyName()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new CreateInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
                Email = "correoInvitado@correo.com",
                Name = ""
            };

            var result = controller.CreateInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestObjectResult)));
        }

        [TestMethod]
        public void TestCreateInvitationWithGoneExpirationDate()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new CreateInvitationInput()
            {
                Email = "correoInvitado@correo.com",
                Name = "Nombre invitado",
                ExpirationDate = DateTime.Now.AddDays(-1),
            };

            var result = controller.CreateInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestObjectResult)));
        }

        [TestMethod]
        public void TestDeleteInvitation()
        {
            _invitationServiceMock.Setup(r => r.DeleteInvitation(It.IsAny<int>()));
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);

            var result = controller.DeleteInvitation(11);

            _invitationServiceMock.VerifyAll();
            Assert.IsTrue(result.GetType().Equals(typeof(OkResult)));
        }

        [TestMethod]
        public void TestDeleteInvalidInvitation()
        {
            _invitationServiceMock.Setup(r => r.DeleteInvitation(It.IsAny<int>())).Throws(new Exception());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);

            var result = controller.DeleteInvitation(11);

            _invitationServiceMock.VerifyAll();
            Assert.IsTrue(result.GetType().Equals(typeof(NotFoundResult)));
        }

        [TestMethod]
        public void TestModifyInvitation()
        {
            _invitationServiceMock.Setup(r => r.ModifyInvitation(It.IsAny<int>(), It.IsAny<DateTime>()));
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new ModifyInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
            };

            var result = controller.ModifyInvitation(1, input);

            _invitationServiceMock.VerifyAll();
            Assert.IsTrue(result.GetType().Equals(typeof(OkResult)));
        }

        [TestMethod]
        public void TestModifyInvitationWithoutBody()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            ModifyInvitationInput input = null;

            var result = controller.ModifyInvitation(1, input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestModifyInvitationWithGoneExpirationDate()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new ModifyInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(-1),
            };

            var result = controller.ModifyInvitation(1, input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestModifyInvalidInvitation()
        {
            _invitationServiceMock.Setup(r => r.ModifyInvitation(It.IsAny<int>(), It.IsAny<DateTime>())).Throws(new Exception());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new ModifyInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
            };

            var result = controller.ModifyInvitation(123, input);

            _invitationServiceMock.VerifyAll();
            Assert.IsTrue(result.GetType().Equals(typeof(NotFoundObjectResult)));
        }

        [TestMethod]
        public void TestAcceptInvitation()
        {
            var createdManager = new Manager() { Id = 10 };
            _invitationServiceMock.Setup(Setup => Setup.AcceptInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Returns(createdManager);
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new AcceptInvitationInput()
            {
                Email = "newManager@correo.com",
                Password = "Contrasena1234*!"
            };

            var result = controller.AcceptInvitation(input);
            var okResult = result as OkObjectResult;
            var acceptInvitationResponse = okResult.Value as AcceptInvitationOutput;


            _invitationServiceMock.VerifyAll();
            Assert.AreEqual(10, acceptInvitationResponse.ManagerId);
        }

        [TestMethod]
        public void TestAcceptInvitationWithoutBody()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            AcceptInvitationInput input = null;

            var result = controller.AcceptInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestAcceptInvitationWithoutEmail()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            AcceptInvitationInput input = new AcceptInvitationInput()
            {
                Password = "Contrasena1234*!"
            }; ;

            var result = controller.AcceptInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestAcceptInvitationWithEmptyEmail()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            AcceptInvitationInput input = new AcceptInvitationInput()
            {
                Email = "",
                Password = "Contrasena1234*!"
            }; ;

            var result = controller.AcceptInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestAcceptInvitationWithoutPassword()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            AcceptInvitationInput input = new AcceptInvitationInput()
            {
                Email = "newManager@correo.com",
            }; ;

            var result = controller.AcceptInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestAcceptInvitationWithEmptyPassword()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            AcceptInvitationInput input = new AcceptInvitationInput()
            {
                Password = "",
                Email = "newManager@correo.com",
            }; ;

            var result = controller.AcceptInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestAcceptInvalidInvitation()
        {
            _invitationServiceMock.Setup(r => r.AcceptInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Throws(new Exception());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new AcceptInvitationInput()
            {
                Email = "correoSinInvitacion@correo.com",
                Password = "Contrasena1234*!"
            };

            var result = controller.AcceptInvitation(input);

            _invitationServiceMock.VerifyAll();
            Assert.IsTrue(result.GetType().Equals(typeof(NotFoundObjectResult)));
        }

        [TestMethod]
        public void TestRejectInvitation()
        {
            _invitationServiceMock.Setup(r => r.RejectInvitation(It.IsAny<string>())).Verifiable();
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new RejectInvitationInput()
            {
                Email = "correoParaRechazar@correo.com",
            };

            var result = controller.RejectInvitation(input);

            _invitationServiceMock.VerifyAll();
            Assert.IsTrue(result.GetType().Equals(typeof(OkResult)));
        }

        [TestMethod]
        public void TestRejectInvitationWithoutBody()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            RejectInvitationInput input = null;

            var result = controller.RejectInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestRejectInvitationWithoutEmail()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            RejectInvitationInput input = new RejectInvitationInput();

            var result = controller.RejectInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestRejectInvitationWithEmptyEmail()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            RejectInvitationInput input = new RejectInvitationInput()
            {
                Email = ""
            };

            var result = controller.RejectInvitation(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestRejectInvalidInvitation()
        {
            _invitationServiceMock.Setup(r => r.RejectInvitation(It.IsAny<string>())).Throws(new Exception());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var input = new RejectInvitationInput()
            {
                Email = "correoParaRechazar@correo.com",
            };

            var result = controller.RejectInvitation(input);

            _invitationServiceMock.VerifyAll();
            Assert.IsTrue(result.GetType().Equals(typeof(NotFoundObjectResult)));
        }
    }
}