using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}