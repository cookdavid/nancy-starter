using System.Reflection;
using Autofac;
using Nancy.Bootstrappers.Autofac;

namespace Nancy_Starter
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(Bootstrapper)));
            builder.Update(existingContainer.ComponentRegistry);
        }
    }
}