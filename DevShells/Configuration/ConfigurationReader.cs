using System;
using System.IO;

using Newtonsoft.Json;


namespace DevShells.Configuration
{
    internal class ConfigurationReader
    {
        private static string _jsonConfigurationFile = Path.Combine(  StaticConfiguration.BaseFolder, "DevShellsConfig.json");

        private static bool _initialized;

        public static string JsonConfigurationFile
        {
            get
            {
                if (!_initialized)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_jsonConfigurationFile));
                    _initialized = true;
                }

                return _jsonConfigurationFile;
            }
            internal set
            {
                if (_initialized)
                {
                    throw new InvalidOperationException("Manipulating config location is only possible before first consumer has accessed the location!");
                }
                _jsonConfigurationFile = value;
            }
        }

        public static DevShellConfiguration ReadConfiguration()
        {
            if (!File.Exists(JsonConfigurationFile))
            {
                var shellConfigurations = new[]
                {
                    new ShellConfiguration { Name = "ExtAppHosting", Path = "c:\\TFS\\ExtAppHosting\\bin\\x64\\Debug"},
                    new ShellConfiguration { Name = "via", Path = "c:\\TFS\\via\\Deploy\\bin\\x64\\Debug"},
                    new ShellConfiguration { Name = "VS2017_WF", Path = "D:\\TFS\\vs2017\\WF\\bin\\x64\\Debug"},
                };
                var config = new DevShellConfiguration()
                {
                    ShellConfigurations = shellConfigurations
                };

                Save(config);

                return config;
            }
            return JsonConvert.DeserializeObject<DevShellConfiguration>(File.ReadAllText(JsonConfigurationFile));
        }

        private static void Save(object shellConfigurations)
        {
            var serializeObject = JsonConvert.SerializeObject(shellConfigurations);
            File.WriteAllText(JsonConfigurationFile, serializeObject);
        }
    }
}