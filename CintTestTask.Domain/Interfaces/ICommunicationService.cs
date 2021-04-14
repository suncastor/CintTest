namespace CintTestTask.Domain.Interfaces
{
    public interface ICommunicationService
    {
        void Write(string message);

        string Read();
    }
}
