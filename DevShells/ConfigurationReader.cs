namespace DevShells
{
    internal class ConfigurationReader
    {
        public static ShellConfiguration[] ReadConfiguration()
        {
            return new[]
            {
                new ShellConfiguration { Name = "ExtAppHosting", Path = "c:\\TFS\\ExtAppHosting\\bin\\x64\\Debug"},
                new ShellConfiguration { Name = "via", Path = "c:\\TFS\\via\\Deploy\\bin\\x64\\Debug"},
            };
        }
    }
}