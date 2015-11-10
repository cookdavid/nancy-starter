using System.Reflection;
using Autofac;
using Nancy.Bootstrappers.Autofac;

namespace Nancy_Starter
{
    public class Boostrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(Boostrapper)));
            builder.Update(existingContainer.ComponentRegistry);
        }
    }
}