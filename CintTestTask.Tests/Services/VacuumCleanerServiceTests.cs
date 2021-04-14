using Domain.Services;
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
            var x = _randomGenerator.Next();
            var y = _randomGenerator.Next();

            Assert.DoesNotThrow(() => vacuumCleanerService.SetInitialPosition(x, y));
        }

        [Test]
        public void MoveShouldNotThrowException()
        {
            var vacuumCleanerService = new VacuumCleanerService();
            var directionIndex = _randomGenerator.Next(0, 3);
            var direction = new char[] { 'E', 'W', 'S', 'N' }[directionIndex];
            var tilesNumber = _randomGenerator.Next();

            Assert.DoesNotThrow(() => vacuumCleanerService.Move(direction, tilesNumber));
        }

        [TestCase('E')]
        [TestCase('W')]
        [TestCase('S')]
        [TestCase('N')]
        public void MoveToDirectionShouldIncreaseNumberOfClearedTiles(char direction)
        {
            var vacuumCleanerService = new VacuumCleanerService();
            var tilesNumber = _randomGenerator.Next(-100000, 100000);
            var expectedNumber = Math.Abs(tilesNumber) + 1;

            vacuumCleanerService.SetInitialPosition(0, 0);
            vacuumCleanerService.Move(direction, tilesNumber);
            var actualNumber = vacuumCleanerService.GetClearedTilesNumber();

            Assert.Equals(expectedNumber, actualNumber);
        }

        [Test]
        public void EachMoveCallInARowShouldIncreaseNumberOfClearedTiles()
        {
            var vacuumCleanerService = new VacuumCleanerService();
            var directionsToMove = new char[] { 'E', 'S' };
            var stepsToMove = directionsToMove.Select(x => _randomGenerator.Next(-50000, 50000)).ToArray();
            var expectedNumber = stepsToMove.Sum() + 1;

            vacuumCleanerService.SetInitialPosition(0, 0);
            for(var i = 0; i < directionsToMove.Length; i++)
            {
                vacuumCleanerService.Move(directionsToMove[i], stepsToMove[i]);
            }

            var actualNumber = vacuumCleanerService.GetClearedTilesNumber();
            
            Assert.Equals(expectedNumber, actualNumber);
        }

        [Test]
        public void MoveDoesNotIncreaseNumberOfClearedTilesIfTheyAreAlreadyCleared()
        {
            var vacuumCleanerService = new VacuumCleanerService();
            var directionsToMove = new char[] { 'E', 'W' };
            var stepsToMove = directionsToMove.Select(x => 100).ToArray();
            var expectedNumber = stepsToMove[0] + 1;

            vacuumCleanerService.SetInitialPosition(0, 0);
            for(var i = 0; i < directionsToMove.Length; i++)
            {
                vacuumCleanerService.Move(directionsToMove[i], stepsToMove[i]);
            }

            var actualNumber = vacuumCleanerService.GetClearedTilesNumber();
            
            Assert.Equals(expectedNumber, actualNumber);
        }
    }
}
