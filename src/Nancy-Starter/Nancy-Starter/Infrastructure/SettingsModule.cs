using Autofac;
using ConfigInjector.Configuration;

namespace Nancy_Starter.Infrastructure
{
    public class SettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigurationConfigurator.RegisterConfigurationSettings()
                .FromAssemblies(ThisAssembly)
                .RegisterWithContainer(
                    setting => builder.RegisterInstance(setting).AsSelf().AsImplementedInterfaces().SingleInstance())
                .DoYourThing();
        }
    }
}