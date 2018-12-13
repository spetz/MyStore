using Autofac;
using MyStore.Core.Repositories;
using MyStore.Infrastructure.Cache.Repositories;
using MyStore.Infrastructure.EF.Repositories;

namespace MyStore.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(InfrastructureModule).Assembly)
                .AsImplementedInterfaces();
//            builder.RegisterType<InMemoryProductRepository>()
//                .As<IProductRepository>()
//                .SingleInstance();
            builder.RegisterType<EfProductRepository>().As<IProductRepository>();
        }
    }
}