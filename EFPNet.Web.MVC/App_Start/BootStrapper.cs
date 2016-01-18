using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace EFPNet.Web.MVC.App_Start
{
    public class BootStrapper
    {
        public static void Initialise()
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(assembly);
            var uassembly = Assembly.Load("EFPNet.Repositories");
            builder.RegisterAssemblyTypes(uassembly).Where(a => a.Name.EndsWith("Repository")).AsImplementedInterfaces();
            var sassembly = Assembly.Load("EFPNet.Service");
            builder.RegisterAssemblyTypes(sassembly).Where(a => a.Name.EndsWith("Service")).AsImplementedInterfaces();
            //builder.RegisterControllers(sassembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}