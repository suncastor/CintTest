using CintTestTask.Domain.Interfaces;
using System;

namespace CintTestTask.Domain.Services
{
    public class ConsoleCommunicationService : ICommunicationService
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
