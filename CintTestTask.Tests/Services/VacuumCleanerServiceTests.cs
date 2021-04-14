using CintTestTask.Domain.Models;
using CintTestTask.Domain.Services;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests.Services
{
    [TestFixture]
    public class VacuumCleanerServiceTests
    {
        private Random _randomGenerator;

        [SetUp]
        public void SetUp()
        {
            _randomGenerator = new Random();
        }

        [Test]
        public void SetInitialPositionShouldNotThrowException()
        {
            var vacuumCleanerService = new VacuumCleanerService();
            var initialPosition = new TileCoordinates
            {
                X = _randomGenerator.Next(),
                Y = _randomGenerator.Next(),
            };

            Assert.DoesNotThrow(() => vacuumCleanerService.SetInitialPosition(initialPosition));
        }

        [Test]
        public void MoveShouldNotThrowException()
        {
            var vacuumCleanerService = new VacuumCleanerService();
            var directionIndex = _randomGenerator.Next(0, 3);
            var command = new Command
            {
                Direction = new char[] { 'E', 'W', 'S', 'N' }[directionIndex],
                TilesNumber = _randomGenerator.Next(),
            };

            Assert.DoesNotThrow(() => vacuumCleanerService.Move(command));
        }

        [TestCase('E')]
        [TestCase('W')]
        [TestCase('S')]
        [TestCase('N')]
        public void MoveToDirectionShouldIncreaseNumberOfClearedTiles(char direction)
        {
            var vacuumCleanerService = GetInitedService();
            var command = new Command
            {
                Direction = direction,
                TilesNumber = _randomGenerator.Next(0, 100000),
            };

            var expectedNumber = Math.Abs(command.TilesNumber) + 1;
            
            vacuumCleanerService.Move(command);
            var actualNumber = vacuumCleanerService.GetClearedTilesNumber();
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [Test]
        public void EachMoveCallInARowShouldIncreaseNumberOfClearedTiles()
        {
            var vacuumCleanerService = GetInitedService();
            var directionsToMove = new char[] { 'E', 'S' };
            var commands = directionsToMove.Select(x => new Command
            {
                Direction = x,
                TilesNumber = _randomGenerator.Next(0, 50000),
            }).ToArray();

            var expectedNumber = commands.Sum(x => x.TilesNumber) + 1;
            foreach(var command in commands)
            {
                vacuumCleanerService.Move(command);
            }

            var actualNumber = vacuumCleanerService.GetClearedTilesNumber();
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [Test]
        public void MoveDoesNotIncreaseNumberOfClearedTilesIfTheyAreAlreadyCleared()
        {
            var vacuumCleanerService = GetInitedService();
            var tilesToMoveInOneDirection = 100;
            var directionsToMove = new char[] { 'E', 'W' };
            var commands = directionsToMove.Select(x => new Command
            {
                Direction = x,
                TilesNumber = tilesToMoveInOneDirection,
            }).ToArray();

            var expectedNumber = tilesToMoveInOneDirection + 1;
            foreach (var command in commands)
            {
                vacuumCleanerService.Move(command);
            }

            var actualNumber = vacuumCleanerService.GetClearedTilesNumber();
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        private VacuumCleanerService GetInitedService()
        {
            var vacuumCleanerService = new VacuumCleanerService();
            var initialPosition = new TileCoordinates
            {
                X = 0,
                Y = 0,
            };

            vacuumCleanerService.SetInitialPosition(initialPosition);
            return vacuumCleanerService;
        }
    }
}
