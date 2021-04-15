using CintTestTask.Domain.Services;
using System;

namespace CintTestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var vacuumCleanerService = new VacuumCleanerService();
            var consoleCommunicationService = new ConsoleCommunicationService();
            var commandsService = new CommandsService(vacuumCleanerService, consoleCommunicationService);
            commandsService.ProcessCleaning();
            Console.ReadKey();
        }
    }
}
