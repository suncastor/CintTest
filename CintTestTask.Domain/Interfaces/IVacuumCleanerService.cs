using CintTestTask.Domain.Models;

namespace CintTestTask.Domain.Interfaces
{
    public interface IVacuumCleanerService
    {
        void SetInitialPosition(TileCoordinates coordinates);

        void Move(Command command);

        int GetClearedTilesNumber();
    }
}
