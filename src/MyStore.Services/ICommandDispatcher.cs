using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}