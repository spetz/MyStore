using System.Threading.Tasks;
using Autofac;

namespace MyStore.Services
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _componentContext;

        public CommandDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        
        public async Task SendAsync<T>(T command) where T : ICommand
        {
            await _componentContext.Resolve<ICommandHandler<T>>().HandleAsync(command);
        }
    }
}