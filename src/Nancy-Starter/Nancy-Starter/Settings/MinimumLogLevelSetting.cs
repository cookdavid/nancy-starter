using ConfigInjector;
using Serilog.Events;

namespace Nancy_Starter.Settings
{
    public class MinimumLogLevelSetting : ConfigurationSetting<LogEventLevel>
    {
    }
}