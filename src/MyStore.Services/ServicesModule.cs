using Autofac;

namespace MyStore.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ServicesModule).Assembly)
                .AsImplementedInterfaces();
        }
    }
}