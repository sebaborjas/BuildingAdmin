using IDataAccess;
using Services;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Linq.Expressions;
using IServices;

namespace TestServices
{
    [TestClass]
    public class TestReportService
    {

        ReportsService _reportService;

        Mock<ISessionService> _sessionServiceMock;
        Mock<IGenericRepository<Building>> _buildingRepositoryMock;

        [TestInitialize]
        public void SetUp()
        {
            _buildingRepositoryMock = new Mock<IGenericRepository<Building>>(MockBehavior.Strict);
            _sessionServiceMock = new Mock<ISessionService>(MockBehavior.Strict);
        }
        
        [TestMethod]
        public void TestGetTicketsByBuilding()
        {
            Building firstBuilding = new Building
            {
                Id = 1,
                Name = "Building Uno",
                Tickets = new List<Ticket>
                {
                    new Ticket { Status = Domain.DataTypes.Status.Open },
                    new Ticket { Status = Domain.DataTypes.Status.InProgress },
                    new Ticket { Status = Domain.DataTypes.Status.InProgress }
                }
            };

            Building secondBuilding = new Building
            {
                Id = 2,
                Name = "Building Dos",
                Tickets = new List<Ticket>
                {
                    new Ticket { Status = Domain.DataTypes.Status.Closed },
                    new Ticket { Status = Domain.DataTypes.Status.InProgress },
                    new Ticket { Status = Domain.DataTypes.Status.Closed }
                }
            };

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>()))
                .Returns(new Manager
                {
                    Buildings = new List<Building> { firstBuilding, secondBuilding }
                });

            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            var ticketsByBuilding = _reportService.GetTicketsByBuilding();

            _buildingRepositoryMock.VerifyAll();

            var expectedResults = new[]
            {
                new { BuildingName = "Building Uno", TicketsOpen = 1, TicketsInProgress = 2, TicketsClosed = 0 },
                new { BuildingName = "Building Dos", TicketsOpen = 0, TicketsInProgress = 1, TicketsClosed = 2 }
            };

            Assert.IsTrue(ticketsByBuilding.Select((t, index) => new
            {
                t.BuildingName,
                t.TicketsOpen,
                t.TicketsInProgress,
                t.TicketsClosed
            }).SequenceEqual(expectedResults),
            "Unexpected results for tickets by building");
        }

        [TestMethod]
        public void TestGetTicketsByOneBuilding()
        {
            Building firstBuilding = new Building
            {
                Id = 1,
                Name = "Building Uno",
                Tickets = new List<Ticket>
                {
                    new Ticket { Status = Domain.DataTypes.Status.Open },
                    new Ticket { Status = Domain.DataTypes.Status.InProgress },
                    new Ticket { Status = Domain.DataTypes.Status.InProgress }
                }
            };

            Building secondBuilding = new Building
            {
                Id = 2,
                Name = "Building Dos",
                Tickets = new List<Ticket>
                {
                    new Ticket { Status = Domain.DataTypes.Status.Closed },
                    new Ticket { Status = Domain.DataTypes.Status.InProgress },
                    new Ticket { Status = Domain.DataTypes.Status.Closed }
                }
            };

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>()))
                .Returns(new Manager
                {
                    Buildings = new List<Building> { firstBuilding, secondBuilding }
                });

            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            var expected = new
            {
                BuildingName = "Building Uno",
                TicketsOpen = 1,
                TicketsInProgress = 2,
                TicketsClosed = 0
            };

            var ticketsByBuilding = _reportService.GetTicketsByBuilding("Building Uno");

            _buildingRepositoryMock.VerifyAll();

            var actual = new
            {
                BuildingName = ticketsByBuilding.ElementAt(0).BuildingName,
                TicketsOpen = ticketsByBuilding.ElementAt(0).TicketsOpen,
                TicketsInProgress = ticketsByBuilding.ElementAt(0).TicketsInProgress,
                TicketsClosed = ticketsByBuilding.ElementAt(0).TicketsClosed
            };

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TestGetTicketsByMaintenanceOperator()
        {
            MaintenanceOperator operatorUno = new MaintenanceOperator { Name = "Operator Uno" };
            MaintenanceOperator operatorDos = new MaintenanceOperator { Name = "Operator Dos" };

            Ticket ticketUno = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorUno };
            ticketUno.AttendTicket();

            Ticket ticketDos = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorUno };
            ticketDos.AttendTicket();
            ticketDos.CloseTicket(100);

            Ticket ticketTres = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorUno };
            Ticket ticketCuatro = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorDos };
            Ticket ticketCinco = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorDos };
            ticketCinco.AttendTicket();
            Ticket ticketSeis = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorDos };

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>()))
                .Returns(new Manager
                {
                    Buildings = new List<Building>
                    {
                        new Building
                        {
                            Id = 1,
                            Name = "Building Uno",
                            Tickets = new List<Ticket>
                            {
                                ticketUno,
                                ticketDos,
                                ticketTres,
                                ticketCuatro,
                                ticketCinco,
                                ticketSeis
                            }
                        }
                    }
                });

            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            var ticketsByOperator = _reportService.GetTicketsByMaintenanceOperator("Building Uno");

            _buildingRepositoryMock.VerifyAll();
            var expected = new[]
            {
                new
                {
                    OperatorName = "Operator Uno",
                    TicketsOpen = 1,
                    TicketsInProgress = 1,
                    TicketsClosed = 1,
                    AverageTimeToClose = "00:00"
                },
                new
                {
                    OperatorName = "Operator Dos",
                    TicketsOpen = 2,
                    TicketsInProgress = 1,
                    TicketsClosed = 0,
                    AverageTimeToClose = "00:00"
                }
            };

            var actual = ticketsByOperator.Select(t => new
            {
                t.OperatorName,
                t.TicketsOpen,
                t.TicketsInProgress,
                t.TicketsClosed,
                t.AverageTimeToClose
            }).ToArray();

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void TestGetTicketsBySpecificMaintenanceOperator()
        {
            MaintenanceOperator operatorUno = new MaintenanceOperator { Name = "Operator Uno" };
            MaintenanceOperator operatorDos = new MaintenanceOperator { Name = "Operator Dos" };

            Ticket ticketUno = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorUno };
            ticketUno.AttendTicket();

            Ticket ticketDos = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorUno };
            ticketDos.AttendTicket();
            ticketDos.CloseTicket(100);

            Ticket ticketTres = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorUno };
            Ticket ticketCuatro = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorDos };
            Ticket ticketCinco = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorDos };
            ticketCinco.AttendTicket();
            Ticket ticketSeis = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorDos };

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>()))
                .Returns(new Manager
                {
                    Buildings = new List<Building>
                    {
                        new Building
                        {
                            Id = 1,
                            Name = "Building Uno",
                            Tickets = new List<Ticket>
                            {
                                ticketUno,
                                ticketDos,
                                ticketTres,
                                ticketCuatro,
                                ticketCinco,
                                ticketSeis
                            }
                        }
                    }
                });

            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            var ticketsByOperator = _reportService.GetTicketsByMaintenanceOperator("Building Uno", "Operator Uno");

            _buildingRepositoryMock.VerifyAll();

            var expected = new
            {
                OperatorName = "Operator Uno",
                TicketsOpen = 1,
                TicketsInProgress = 1,
                TicketsClosed = 1,
                AverageTimeToClose = "00:00"
            };

            var actual = ticketsByOperator.Select(t => new
            {
                t.OperatorName,
                t.TicketsOpen,
                t.TicketsInProgress,
                t.TicketsClosed,
                t.AverageTimeToClose
            }).First();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetTicketsByCategory()
        {
            Category categoryUno = new Category { Name = "Category Uno" };
            Category categoryDos = new Category { Name = "Category Dos" };

            Ticket ticketUno = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryUno };
            ticketUno.AttendTicket();

            Ticket ticketDos = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryUno };
            ticketDos.AttendTicket();
            ticketDos.CloseTicket(100);

            Ticket ticketTres = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryDos };

            Ticket ticketCuatro = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryDos };

            Ticket ticketCinco = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryDos };
            ticketCinco.AttendTicket();

            Ticket ticketSeis = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryUno };

            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<Building, bool>> predicate, List<string> includes) => new Building
                {
                    Id = 1,
                    Name = "Building Uno",
                    Tickets = new List<Ticket>
                    {
                        ticketUno,
                        ticketDos,
                        ticketTres,
                        ticketCuatro,
                        ticketCinco,
                        ticketSeis
                    }
                });

            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            var ticketsByOperator = _reportService.GetTicketsByCategory("Building Uno");

            _buildingRepositoryMock.VerifyAll();

            Assert.IsTrue(ticketsByOperator.All(t =>
                (t.CategoryName == "Category Uno" && t.TicketsOpen == 1 && t.TicketsInProgress == 1 && t.TicketsClosed == 1) ||
                (t.CategoryName == "Category Dos" && t.TicketsOpen == 2 && t.TicketsInProgress == 1 && t.TicketsClosed == 0)));
        }

        [TestMethod]
        public void TestGetTicketsBySpecificCategory()
        {
            Category categoryUno = new Category { Name = "Category Uno" };
            Category categoryDos = new Category { Name = "Category Dos" };

            Ticket ticketUno = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryUno };
            ticketUno.AttendTicket();

            Ticket ticketDos = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryUno };
            ticketDos.AttendTicket();
            ticketDos.CloseTicket(100);

            Ticket ticketTres = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryDos };

            Ticket ticketCuatro = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryDos };

            Ticket ticketCinco = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryDos };
            ticketCinco.AttendTicket();

            Ticket ticketSeis = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryUno };

            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<Building, bool>> predicate, List<string> includes) => new Building
                {
                    Id = 1,
                    Name = "Building Uno",
                    Tickets = new List<Ticket>
                    {
                        ticketUno,
                        ticketDos,
                        ticketTres,
                        ticketCuatro,
                        ticketCinco,
                        ticketSeis
                    }
                });

            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            var ticketsByOperator = _reportService.GetTicketsByCategory("Building Uno", "Category Dos");

            _buildingRepositoryMock.VerifyAll();

            Assert.IsTrue(ticketsByOperator.All(t =>
                t.CategoryName != "Category Dos" ||
                (t.TicketsOpen == 2 && t.TicketsInProgress == 1 && t.TicketsClosed == 0)),
                "Unexpected result for category 'Category Dos'");
        }

        
        [TestMethod]
        public void TestGetTicketsByApartment()
        {

            var apartment1 = new Apartment
            {
                Id = 1,
                Owner = new Owner { Name = "Jose", LastName = "Rodriguez" },
                DoorNumber = 101
            };

            var apartment2 = new Apartment
            {
                Id = 2,
                Owner = new Owner { Name = "Miguel", LastName = "Gonzalez" },
                DoorNumber = 102
            };

            Building firstBuilding = new Building
            {
                Id = 1,
                Name = "BuildingUno",
                Apartments = [apartment1, apartment2],
                Tickets = new List<Ticket>
                {
                    new Ticket 
                    { 
                        Status = Domain.DataTypes.Status.Open,
                        Apartment = apartment1
                    },
                    new Ticket
                    {
                        Status = Domain.DataTypes.Status.InProgress,
                        Apartment = apartment1
                    },
                    new Ticket
                    {
                        Status = Domain.DataTypes.Status.InProgress,
                        Apartment = apartment1
                    },
                    new Ticket
                    {
                        Status = Domain.DataTypes.Status.Open,
                        Apartment = apartment2
                    },
                    new Ticket
                    {
                        Status = Domain.DataTypes.Status.InProgress,
                        Apartment = apartment2
                    },
                    new Ticket
                    {
                        Status = Domain.DataTypes.Status.Closed,
                        Apartment = apartment2
                    },
                    new Ticket
                    {
                        Status = Domain.DataTypes.Status.Closed,
                        Apartment = apartment2
                    },
                }
            };

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>()))
                .Returns(new Manager
                {
                    Buildings = new List<Building> { firstBuilding }
                });

            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            var expected = new List<TicketByApartment>
            { 
                new TicketByApartment
                {
                    ApartmentAndOwner = "101 - Jose Rodriguez",
                    TicketsOpen = 1,
                    TicketsClosed = 0,
                    TicketsInProgress = 2,
                },
                new TicketByApartment
                {
                    ApartmentAndOwner = "102 - Miguel Gonzalez",
                    TicketsOpen = 1,
                    TicketsClosed = 2,
                    TicketsInProgress = 1,
                }
            };

            var ticketsByApartment = _reportService.GetTicketsByApartment("BuildingUno");

            _buildingRepositoryMock.VerifyAll();

            Assert.AreEqual<TicketByApartment>(expected.ElementAt(0), ticketsByApartment.ElementAt(0));
            Assert.AreEqual<TicketByApartment>(expected.ElementAt(1), ticketsByApartment.ElementAt(1));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetTicketsByBuildingError()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns((Manager)null);
            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);
            _reportService.GetTicketsByBuilding();
        }
        

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetTicketsByMaintenanceOperatorError()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns((User)null);
            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);
            _reportService.GetTicketsByMaintenanceOperator("Edificio");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetTicketsByCategoryError()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns((User)null);
            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);
            _reportService.GetTicketsByCategory("Edificio");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetTicketsByApartmentError()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns((User)null);
            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);
            _reportService.GetTicketsByApartment("Edificio");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetTicketsByApartmentWithInvalidBuilding()
        {
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(new Manager() { Buildings = [new Building { Name = "Edificio"}]});
            _reportService = new ReportsService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);
            _reportService.GetTicketsByApartment("Edificio mal");
        }



    }
}