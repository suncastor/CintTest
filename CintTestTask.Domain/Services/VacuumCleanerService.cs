using CintTestTask.Domain.Constants;
using CintTestTask.Domain.Interfaces;
using CintTestTask.Domain.Models;
using System;
using System.Collections.Generic;

namespace CintTestTask.Domain.Services
{
    public class VacuumCleanerService : IVacuumCleanerService
    {
        private Dictionary<char, Direction> _directions;

        private TileCoordinates _cleanerCoordinates;

        private HashSet<TileCoordinates> _clearedTiles;

        public VacuumCleanerService()
        {
            _clearedTiles = new HashSet<TileCoordinates>();
            _cleanerCoordinates = new TileCoordinates();
            _directions = new Dictionary<char, Direction>
            {
                { 'E', new Direction { Increment = 1, IsHorizontal = true } },
                { 'W', new Direction { Increment = -1, IsHorizontal = true } },
                { 'N', new Direction { Increment = 1, IsHorizontal = false } },
                { 'S', new Direction { Increment = -1, IsHorizontal = false } },
            };
        }

        public void SetInitialPosition(TileCoordinates initialCoordinates)
        {
            _cleanerCoordinates = initialCoordinates;
            _clearedTiles.Add(_cleanerCoordinates); // Assuming initial tile is cleared anyway
        }

        public void Move(Command command)
        {
            var direction = _directions[command.Direction];
            var currentCoordinate = direction.IsHorizontal
                ? _cleanerCoordinates.X
                : _cleanerCoordinates.Y;
            var lastStepCoordinate = currentCoordinate + command.TilesNumber * direction.Increment;
            lastStepCoordinate = Math.Min(VacuumCleanerConstants.MaxTileCoordinate, lastStepCoordinate);
            lastStepCoordinate = Math.Max(-VacuumCleanerConstants.MaxTileCoordinate, lastStepCoordinate);
            var stepsNumber = Math.Abs(currentCoordinate - lastStepCoordinate);

            for (var i = 0; i < stepsNumber; i++)
            {
                var newCoordinates = new TileCoordinates
                {
                    X = _cleanerCoordinates.X,
                    Y = _cleanerCoordinates.Y,
                };

                currentCoordinate += direction.Increment;
                if (direction.IsHorizontal)
                {
                    newCoordinates.X = currentCoordinate;
                }
                else
                {
                    newCoordinates.Y = currentCoordinate;
                }

                _cleanerCoordinates = newCoordinates;
                if (!_clearedTiles.Contains(_cleanerCoordinates))
                {
                    _clearedTiles.Add(_cleanerCoordinates);
                }
            }
        }

        public int GetClearedTilesNumber()
        {
            return _clearedTiles.Count;
        }

        private class Direction
        {
            public int Increment { get; set; }

            public bool IsHorizontal { get; set; }
        }
    }
}
