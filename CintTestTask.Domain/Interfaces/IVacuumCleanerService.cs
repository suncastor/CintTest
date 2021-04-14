namespace CintTestTask.Domain.Interfaces
{
    public interface IVacuumCleanerService
    {
        void SetInitialPosition(int x, int y);

        void Move(char direction, int tilesNumber);

        int GetClearedTilesNumber();
    }
}
