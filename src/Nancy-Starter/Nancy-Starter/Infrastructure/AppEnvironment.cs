using System;
using System.Reflection;
using ConfigInjector.QuickAndDirty;
using Nancy_Starter.Settings;

namespace Nancy_Starter.Infrastructure
{
    public static class AppEnvironment
    {
        private static bool _isInitialized;
        private static bool _isSimulating;

        private static string _machineName;
        private static EnvironmentType? _environmentType;
        private static EnvironmentName? _environmentName;

        private static string _simulatedMachineName;
        private static EnvironmentType? _simulatedEnvironmentType;
        private static EnvironmentName? _simulatedEnvironmentName;

        private static ApplicationNameSetting _applicationName;

        private static Assembly _hostApplicationAssembly;

        internal static Assembly HostApplicationAssembly
        {
            get
            {
                return _hostApplicationAssembly;
            }
            set
            {
                if (_hostApplicationAssembly != null) throw new Exception("This should only be set once.");
                _hostApplicationAssembly = value;
            }
        }

        public static string MachineName
        {
            get
            {
                if (_isInitialized == false) Initialize();
                return _isSimulating ? _simulatedMachineName : _machineName;
            }
        }

        public static string ApplicationName
        {
            get
            {
                if (_isInitialized == false) Initialize();
                return _applicationName;
            }
        }

        public static EnvironmentType EnvironmentType
        {
            get
            {
                if (_isInitialized == false) Initialize();
                return _isSimulating ? _simulatedEnvironmentType.Value : _environmentType.Value;
            }
        }

        public static EnvironmentName EnvironmentName
        {
            get
            {
                if (_isInitialized == false) Initialize();
                return _isSimulating ? _simulatedEnvironmentName.Value : _environmentName.Value;
            }
        }

        public static string VersionNumber => HostApplicationAssembly.GetName().Version.ToString();

        public static bool IsLocal()
        {
            return EnvironmentType == EnvironmentType.Local;
        }

        public static bool IsProduction()
        {
            return EnvironmentType == EnvironmentType.Production;
        }

        public static bool IsTest()
        {
            return EnvironmentType == EnvironmentType.Test;
        }

        public static void Simulate(string machineName, EnvironmentType? type, EnvironmentName? name)
        {
            _simulatedMachineName = machineName;
            _simulatedEnvironmentType = type;
            _simulatedEnvironmentName = name;
            _isSimulating = true;
        }

        private static void Initialize()
        {
            // Note: Skip initialization if we are simulating
            if (_isSimulating) return;

            _machineName = Environment.MachineName;

            _environmentType = DefaultSettingsReader.Get<EnvironmentTypeSetting>();

            _environmentName = DefaultSettingsReader.Get<EnvironmentNameSetting>();

            _applicationName = DefaultSettingsReader.Get<ApplicationNameSetting>();

            _isInitialized = true;
        }

        public static void Reset()
        {
            _isInitialized = false;
            _isSimulating = false;
            _machineName = null;
            _simulatedMachineName = null;
            _environmentType = null;
            _simulatedEnvironmentType = null;
            _environmentName = null;
            _simulatedEnvironmentName = null;
        }
    }
}
