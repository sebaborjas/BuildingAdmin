using Domain;
using Microsoft.AspNetCore.Mvc;
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
            var invitationInput = new CreateInvitationInput()
            {
                Email = "correoInvitado@correo.com",
                Name = "Nombre Invitado",
                ExpirationDate = DateTime.Now.AddDays(3)
            };

            var result = controller.CreateInvitation(invitationInput);
            var okResult = result as OkObjectResult;
            var mewInvitationResponse = okResult.Value as CreateInvitationOutput;


            _invitationServiceMock.VerifyAll();
            Assert.AreEqual(11, mewInvitationResponse.InvitationId);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestCreateInvitationWithoutBody()
        {
            _invitationServiceMock.Setup(r => r.CreateInvitation(It.IsAny<Invitation>())).Throws(new NullReferenceException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            CreateInvitationInput invitationInput = null;

            var result = controller.CreateInvitation(invitationInput);

            var okResult = result as OkObjectResult;
            var mewInvitationResponse = okResult.Value as CreateInvitationOutput;
            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateInvitationWithoutEmail()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new CreateInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
                Name = "Nombre Invitado"
            };

            var result = controller.CreateInvitation(invitationInput);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateInvitationWithEmptyEmail()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new CreateInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
                Email = "",
                Name = "Nombre Invitado"
            };

            var result = controller.CreateInvitation(invitationInput);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateInvitationWithoutName()
        {
            _invitationServiceMock.Setup(r => r.CreateInvitation(It.IsAny<Invitation>())).Throws(new ArgumentNullException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new CreateInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
                Email = "correoInvitado@correo.com",
            };

            var result = controller.CreateInvitation(invitationInput);

            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateInvitationWithEmptyName()
        {
            _invitationServiceMock.Setup(r => r.CreateInvitation(It.IsAny<Invitation>())).Throws(new ArgumentNullException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);

            var invitationInput = new CreateInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
                Email = "correoInvitado@correo.com",
                Name = ""
            };

            var result = controller.CreateInvitation(invitationInput);

            var okResult = result as OkObjectResult;
            var mewInvitationResponse = okResult.Value as CreateInvitationOutput;

            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateInvitationWithNoExpirationDate()
        {
            _invitationServiceMock.Setup(r => r.CreateInvitation(It.IsAny<Invitation>())).Throws(new ArgumentOutOfRangeException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new CreateInvitationInput()
            {
                Email = "test@mail.com",
                Name = "Nombre "
            };
            var result = controller.CreateInvitation(invitationInput);

            _invitationServiceMock.VerifyAll();
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
        [ExpectedException(typeof(Exception))]
        public void TestDeleteInvalidInvitation()
        {
            _invitationServiceMock.Setup(r => r.DeleteInvitation(It.IsAny<int>())).Throws(new Exception());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);

            var result = controller.DeleteInvitation(11);

            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        public void TestModifyInvitation()
        {
            _invitationServiceMock.Setup(r => r.ModifyInvitation(It.IsAny<int>(), It.IsAny<DateTime>()));
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new ModifyInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
            };

            var result = controller.ModifyInvitation(1, invitationInput);

            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestModifyInvitationWithoutBody()
        {
            _invitationServiceMock.Setup(r => r.ModifyInvitation(It.IsAny<int>(), It.IsAny<DateTime>())).Throws(new NullReferenceException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            ModifyInvitationInput invitationInput = null;

            var result = controller.ModifyInvitation(1, invitationInput);
            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestModifyInvitationWithGoneExpirationDate()
        {
            _invitationServiceMock.Setup(r => r.ModifyInvitation(It.IsAny<int>(), It.IsAny<DateTime>())).Throws(new ArgumentException("Invitation can not be modified"));
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new ModifyInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(-1),
            };

            var result = controller.ModifyInvitation(1, invitationInput);

            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestModifyInvalidInvitation()
        {
            _invitationServiceMock.Setup(r => r.ModifyInvitation(It.IsAny<int>(), It.IsAny<DateTime>())).Throws(new ArgumentNullException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new ModifyInvitationInput()
            {
                ExpirationDate = DateTime.Now.AddDays(3),
            };

            var result = controller.ModifyInvitation(1, invitationInput);

            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        public void TestAcceptInvitation()
        {
            var createdManager = new Manager() { Id = 10 };
            _invitationServiceMock.Setup(Setup => Setup.AcceptInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Returns(createdManager);
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new AcceptInvitationInput()
            {
                Email = "newManager@correo.com",
                Password = "Contrasena1234*!"
            };

            var result = controller.AcceptInvitation(invitationInput);
            var okResult = result as OkObjectResult;
            var acceptInvitationResponse = okResult.Value as AcceptInvitationOutput;


            _invitationServiceMock.VerifyAll();
            Assert.AreEqual(10, acceptInvitationResponse.ManagerId);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAcceptInvitationWithoutBody()
        {
            _invitationServiceMock.Setup(r => r.AcceptInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Throws(new NullReferenceException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);

            AcceptInvitationInput invitationInput = null;

            var result = controller.AcceptInvitation(invitationInput);

            _invitationServiceMock.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAcceptInvitationWithoutEmail()
        {
            _invitationServiceMock.Setup(r => r.AcceptInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Throws(new ArgumentNullException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            AcceptInvitationInput input = new AcceptInvitationInput()
            {
                Password = "Contrasena1234*!"
            };

            var result = controller.AcceptInvitation(input);
            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAcceptInvitationWithEmptyEmail()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            AcceptInvitationInput input = new AcceptInvitationInput()
            {
                Email = "",
                Password = "Contrasena1234*!"
            }; ;

            var result = controller.AcceptInvitation(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAcceptInvitationWithoutPassword()
        {
            _invitationServiceMock.Setup(r => r.AcceptInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Throws(new ArgumentNullException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            AcceptInvitationInput input = new AcceptInvitationInput()
            {
                Email = "newManager@correo.com",
            }; ;

            var result = controller.AcceptInvitation(input);

            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAcceptInvitationWithEmptyPassword()
        {
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);

            AcceptInvitationInput input = new AcceptInvitationInput()
            {
                Password = "",
                Email = "newManager@correo.com",
            };

            _invitationServiceMock.Setup(r => r.AcceptInvitation(It.IsAny<Invitation>(), It.Is<string>(p => string.IsNullOrEmpty(p))))
                                  .Throws(new ArgumentException("Password cannot be empty"));

            var result = controller.AcceptInvitation(input);

            _invitationServiceMock.VerifyAll();
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAcceptInvalidInvitation()
        {
            _invitationServiceMock.Setup(r => r.AcceptInvitation(It.IsAny<Invitation>(), It.IsAny<string>())).Throws(new Exception());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new AcceptInvitationInput()
            {
                Email = "correoSinInvitacion@correo.com",
                Password = "Contrasena1234*!"
            };

            var result = controller.AcceptInvitation(invitationInput);

            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        public void TestRejectInvitation()
        {
            _invitationServiceMock.Setup(r => r.RejectInvitation(It.IsAny<string>())).Verifiable();
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new RejectInvitationInput()
            {
                Email = "correoParaRechazar@correo.com",
            };

            var result = controller.RejectInvitation(invitationInput);

            _invitationServiceMock.VerifyAll();
            Assert.IsTrue(result.GetType().Equals(typeof(OkResult)));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestRejectInvitationWithoutBody()
        {
            _invitationServiceMock.Setup(r => r.RejectInvitation(It.IsAny<string>())).Throws(new ArgumentNullException());

            InvitationController controller = new InvitationController(_invitationServiceMock.Object);

            RejectInvitationInput input = null;

            var result = controller.RejectInvitation(input);

            _invitationServiceMock.VerifyAll();
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRejectInvitationWithoutEmail()
        {
            _invitationServiceMock.Setup(r => r.RejectInvitation(It.IsAny<string>())).Throws(new ArgumentNullException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            RejectInvitationInput input = new RejectInvitationInput()
            {
                Email = null,
            };

            var result = controller.RejectInvitation(input);
            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRejectInvitationWithEmptyEmail()
        {
            _invitationServiceMock.Setup(r => r.RejectInvitation(It.IsAny<string>())).Throws(new ArgumentNullException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new RejectInvitationInput()
            {
                Email = "",
            };

            var result = controller.RejectInvitation(invitationInput);

            _invitationServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRejectInvalidInvitation()
        {
            _invitationServiceMock.Setup(r => r.RejectInvitation(It.IsAny<string>())).Throws(new ArgumentNullException());
            InvitationController controller = new InvitationController(_invitationServiceMock.Object);
            var invitationInput = new RejectInvitationInput()
            {
                Email = "correoParaRechazar@correo.com"
            };

            var result = controller.RejectInvitation(invitationInput);
            _invitationServiceMock.VerifyAll();
        }
    }
}
