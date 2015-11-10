using Autofac;

namespace Nancy_Starter.Infrastructure
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoggingConfig>().AsSelf();
        }
    }
}