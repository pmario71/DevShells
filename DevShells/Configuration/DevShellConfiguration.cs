namespace DevShells.Configuration
{
    public class DevShellConfiguration
    {
        /// <summary>
        /// Location where updater searches for update packages
        /// </summary>
        public string UpdateDropLocation { get; set; }

        public ShellConfiguration[] ShellConfigurations { get; set; }
    }
}