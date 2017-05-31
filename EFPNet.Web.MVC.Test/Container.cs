using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EFPNet.Domain.Data;
using EFPNet.Infrastructure.Data;

namespace EFPNet.Web.MVC.Test
{
    public class Container
    {
        private static IContainer _builder;

        public static IContainer GetContainer()
        {
            if (_builder == null)
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<EFUnitOfWorkContext>().As<IUnitOfWork>().InstancePerLifetimeScope();
                builder.RegisterType<EfpDbContext>().Named<DbContext>("EF").InstancePerLifetimeScope();
                var uassembly = Assembly.Load("EFPNet.Repositories");
                builder.RegisterAssemblyTypes(uassembly).Where(a => a.Name.EndsWith("Repository")).AsImplementedInterfaces();
                _builder = builder.Build();
            }
            return _builder;

        }
    }
}
