using CintTestTask.Domain.Models;

namespace CintTestTask.Domain.Interfaces
{
    public interface ICommunicationService
    {
        void Write(string message);

        int ReadNumberOfCommands();

        TileCoordinates ReadInitialPosition();

        Command ReadCommand();
    }
}
