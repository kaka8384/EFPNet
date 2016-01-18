using System.Reflection;
using Autofac;
using EFPNet.IService;
using EFPNet.Domains.Repositories;

namespace EFPNet.Core
{
    public class ApplicationContainer
    {
        private static IContainer _builder;

        public static IContainer GetContainer()
        {
            if (_builder == null)
            {
                var builder = new ContainerBuilder();
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
