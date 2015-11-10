using System.Reflection;
using Autofac;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy_Starter.Infrastructure;
using Serilog;
using Serilog.Events;

namespace Nancy_Starter
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            var loggingConfig = container.Resolve<LoggingConfig>();
            loggingConfig.Configure();
            Log.Write(LogEventLevel.Debug, "Application Starting");
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(Bootstrapper)));
            builder.Update(existingContainer.ComponentRegistry);
        }
    }
}