using System;
using System.IO;

using Newtonsoft.Json;


namespace DevShells.Configuration
{
    internal class ConfigurationReader
    {
        public static readonly string JsonConfigurationFile = $"{Path.GetDirectoryName(typeof(ConfigurationReader).Assembly.Location)}\\DevShellsConfig.json";
   
        public static ShellConfiguration[] ReadConfiguration()
        {
            if (!File.Exists(JsonConfigurationFile))
            {
                var shellConfigurations = new[]
                {
                    new ShellConfiguration { Name = "ExtAppHosting", Path = "c:\\TFS\\ExtAppHosting\\bin\\x64\\Debug"},
                    new ShellConfiguration { Name = "via", Path = "c:\\TFS\\via\\Deploy\\bin\\x64\\Debug"},
                    new ShellConfiguration { Name = "VS2017_WF", Path = "D:\\TFS\\vs2017\\WF\\bin\\x64\\Debug"},
                };

                Save(shellConfigurations);

                return shellConfigurations;
            }
            return JsonConvert.DeserializeObject<ShellConfiguration[]>(File.ReadAllText(JsonConfigurationFile));
        }

        private static void Save(ShellConfiguration[] shellConfigurations)
        {
            var serializeObject = JsonConvert.SerializeObject(shellConfigurations);
            File.WriteAllText(JsonConfigurationFile, serializeObject);
        }
    }
}