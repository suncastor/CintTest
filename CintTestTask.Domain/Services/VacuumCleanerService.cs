using CintTestTask.Domain.Interfaces;
using CintTestTask.Domain.Models;
using System.Collections.Generic;

namespace Domain.Services
{
    public class VacuumCleanerService : IVacuumCleanerService
    {
        private const int _maxTileCoordinate = 100000;

        private TileCoordinates _cleanerCoordinates;

        private HashSet<TileCoordinates> _clearedTiles;

        public VacuumCleanerService()
        {
            _clearedTiles = new HashSet<TileCoordinates>();
            _cleanerCoordinates = new TileCoordinates();
        }

        public void SetInitialPosition(int x, int y)
        {

        }

        public void Move(char direction, int tilesNumber)
        {

        }

        public int GetClearedTilesNumber()
        {
            return _clearedTiles.Count;
        }
    }
}
