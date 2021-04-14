using CintTestTask.Domain.Interfaces;
using CintTestTask.Domain.Models;
using System;

namespace CintTestTask.Domain.Services
{
    public class ConsoleCommunicationService : ICommunicationService
    {
        public Command ReadCommand()
        {
            var commandString = Console.ReadLine();
            var command = commandString.Split(' ');
            return new Command
            {
                Direction = command[0][0],
                TilesNumber = int.Parse(command[1]),
            };
        }

        public TileCoordinates ReadInitialCoordinates()
        {
            var coordinatesString = Console.ReadLine();
            var coordinates = coordinatesString.Split(' ');
            return new TileCoordinates
            {
                X = int.Parse(coordinates[0]),
                Y = int.Parse(coordinates[1]),
            };
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public int ReadNumberOfCommands()
        {
            return int.Parse(Console.ReadLine());
        }
    }
}
