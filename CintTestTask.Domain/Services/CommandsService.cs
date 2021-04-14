using CintTestTask.Domain.Interfaces;

namespace CintTestTask.Domain.Services
{
    public class CommandsService
    {
        private int _commandsNumber;

        private IVacuumCleanerService _vacuumCleanerService;

        private ICommunicationService _communicationService;

        public CommandsService(IVacuumCleanerService vacuumCleanerService, ICommunicationService communicationService)
        {
            _vacuumCleanerService = vacuumCleanerService;
            _communicationService = communicationService;
        }

        public void ProcessCleaning()
        {
            var clearedTilesNumber = 0;
            try
            {
                InitVacuumCleaner();
                ProcessCleaningCommands();
                clearedTilesNumber = _vacuumCleanerService.GetClearedTilesNumber();
            }
            finally
            {
                _communicationService.Write($"=> Cleaned: {clearedTilesNumber}");
            }
        }

        private void InitVacuumCleaner()
        {
            _commandsNumber = _communicationService.ReadNumberOfCommands();
            var initialPosition = _communicationService.ReadInitialPosition();
            _vacuumCleanerService.SetInitialPosition(initialPosition);
        }

        private void ProcessCleaningCommands()
        {
            for(var i = 0; i < _commandsNumber; i++)
            {
                var command = _communicationService.ReadCommand();
                _vacuumCleanerService.Move(command);
            }
        }
    }
}
