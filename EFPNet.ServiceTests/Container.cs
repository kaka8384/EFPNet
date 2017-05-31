using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EFPNet.Domain.Data;
using EFPNet.Infrastructure.Data;

namespace EFPNet.ServiceTests
{
    public class Container
    {
        private static IContainer _builder;

        public static IContainer GetContainer()
        {
            if (_builder == null)
            {
                var builder = new ContainerBuilder();
                //builder.RegisterType<EFUnitOfWorkContext>().As<IUnitOfWork>().InstancePerLifetimeScope();
                //builder.RegisterType<EfpDbContext>().Named<DbContext>("EF").InstancePerLifetimeScope();
                //builder.RegisterType<EfpCachingDbContext>().Named<DbContext>("EFCaching").InstancePerLifetimeScope();
                var uassembly = Assembly.Load("EFPNet.Repositories");
                builder.RegisterAssemblyTypes(uassembly).Where(a => a.Name.EndsWith("Repository")).AsImplementedInterfaces();
                var sassembly = Assembly.Load("EFPNet.Service");
                builder.RegisterAssemblyTypes(sassembly).Where(a => a.Name.EndsWith("Service")).AsImplementedInterfaces();
                _builder = builder.Build();
            }
            return _builder;

        }
    }
}
