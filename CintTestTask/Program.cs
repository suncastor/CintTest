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
            var commandsSermice = new CommandsService(vacuumCleanerService, consoleCommunicationService);
            commandsSermice.ProcessCleaning();
            Console.ReadKey();
        }
    }
}
