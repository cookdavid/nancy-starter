using System.Reflection;
using Nancy_Starter.Settings;
using Serilog;
using SerilogWeb.Classic.Enrichers;

namespace Nancy_Starter.Infrastructure
{
    public class LoggingConfig
    {
        private readonly SeqServerUriSetting _seqServerUri;
        private readonly ApplicationNameSetting _applicationName;
        private readonly EnvironmentNameSetting _environmentName;
        private readonly MinimumLogLevelSetting _minimumLogLevel;
        private readonly Assembly _thisAssembly;

        public LoggingConfig(SeqServerUriSetting seqServerUri, ApplicationNameSetting applicationName, EnvironmentNameSetting environmentName, MinimumLogLevelSetting minimumLogLevel)
        {
            _seqServerUri = seqServerUri;
            _applicationName = applicationName;
            _environmentName = environmentName;
            _minimumLogLevel = minimumLogLevel;
            _thisAssembly = typeof (LoggingConfig).Assembly;
        }

        public void Configure()
        {
            var logConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is(_minimumLogLevel)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.With<HttpRequestIdEnricher>()
                .Enrich.With<UserNameEnricher>()
                .Enrich.WithProperty("ApplicationName", _applicationName)
                .Enrich.WithProperty("ApplicationVersion", _thisAssembly.GetName().Version)
                .Enrich.WithProperty("EnvironmentName", _environmentName.Value)
                .Enrich.With<HttpRequestNumberEnricher>()
                .Enrich.With<HttpRequestRawUrlEnricher>()
                .Enrich.With<HttpRequestTraceIdEnricher>()
                .Enrich.With<HttpRequestTypeEnricher>()
                .Enrich.With<HttpRequestUserAgentEnricher>()
                .WriteTo.Seq(_seqServerUri);
            Log.Logger = logConfiguration.CreateLogger();
        }
    }
}