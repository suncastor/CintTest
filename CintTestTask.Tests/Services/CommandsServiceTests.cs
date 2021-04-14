using CintTestTask.Domain.Interfaces;
using CintTestTask.Domain.Models;
using CintTestTask.Domain.Services;
using Moq;
using NUnit.Framework;
using System;

namespace CintTestTask.Tests.Services
{
    [TestFixture]
    public class CommandsServiceTests
    {
        private Random _randomGenerator;

        private CommandsService _commandsService;

        private Mock<IVacuumCleanerService> _vacuumCleanerServiceMock;

        private Mock<ICommunicationService> _communicationServiceMock;

        [SetUp]
        public void SetUp()
        {
            _randomGenerator = new Random();
            _vacuumCleanerServiceMock = new Mock<IVacuumCleanerService>();
            _communicationServiceMock = new Mock<ICommunicationService>();
            _commandsService = new CommandsService(_vacuumCleanerServiceMock.Object, _communicationServiceMock.Object);
        }

        [Test]
        public void ProcessCleaningShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => _commandsService.ProcessCleaning());
            _communicationServiceMock.Verify(x => x.Write(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ProcessCleaningShouldReadInitialInformationOnce()
        {
            _commandsService.ProcessCleaning();
            _communicationServiceMock.Verify(x => x.ReadNumberOfCommands(), Times.Once);
            _communicationServiceMock.Verify(x => x.ReadInitialCoordinates(), Times.Once);
            _communicationServiceMock.Verify(x => x.Write(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ProcessCleaningShouldPassInitialCoordinates()
        {
            var initialCoordinates = new TileCoordinates
            {
                X = _randomGenerator.Next(-100, 100),
                Y = _randomGenerator.Next(-100, 100),
            };
            _communicationServiceMock.Setup(x => x.ReadInitialCoordinates()).Returns(initialCoordinates);

            _commandsService.ProcessCleaning();
            _vacuumCleanerServiceMock.Verify(x => x.SetInitialPosition(initialCoordinates), Times.Once);
        }

        [Test]
        public void ProcessCleaningShouldProcessSetNumberOfCommands()
        {
            var commandsNumber = _randomGenerator.Next(1, 20);
            var command = new Command
            {
                Direction = 'W',
                TilesNumber = _randomGenerator.Next(0, 100),
            };
            _communicationServiceMock.Setup(x => x.ReadNumberOfCommands()).Returns(commandsNumber);
            _communicationServiceMock.Setup(x => x.ReadCommand()).Returns(command);

            _commandsService.ProcessCleaning();
            _communicationServiceMock.Verify(x => x.ReadCommand(), Times.Exactly(commandsNumber));
            _vacuumCleanerServiceMock.Verify(x => x.Move(command), Times.Exactly(commandsNumber));
        }

        [Test]
        public void ProcessCleaningShouldPrintResult()
        {
            var commandsNumber = _randomGenerator.Next(1, 20);
            var clearedTilesNumber = _randomGenerator.Next(1, 100);
            _communicationServiceMock.Setup(x => x.ReadNumberOfCommands()).Returns(commandsNumber);
            _vacuumCleanerServiceMock.Setup(x => x.GetClearedTilesNumber()).Returns(clearedTilesNumber);

            _commandsService.ProcessCleaning();
            _communicationServiceMock.Verify(x => x.Write(It.Is<string>(y => y.Contains($"clearedTilesNumber"))), Times.Once);
        }
    }
}
