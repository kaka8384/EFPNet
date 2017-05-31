using System.Data.Entity;
using Autofac;
using EFPNet.Infrastructure.Data;

namespace EFPNet.Domain.Data
{
    public class DomainContainer
    {
        private static IContainer _builder;

        public static IContainer GetContainer()
        {
            if (_builder == null)
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<EFUnitOfWorkContext>().As<IUnitOfWork>().InstancePerLifetimeScope();
                builder.RegisterType<EfpDbContext>().As<IDbContext>().InstancePerLifetimeScope();
                builder.RegisterType<EfpDbContext>().Named<DbContext>("EF").InstancePerLifetimeScope();
                _builder = builder.Build();
            }
            return _builder;

        }
    }
}
