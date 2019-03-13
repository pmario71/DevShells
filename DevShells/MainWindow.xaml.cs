using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DevShells.Configuration;
using DevShells.MVVM;
using DevShells.Utils;


namespace DevShells
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RelayCommand<string> myCreateShellCommand;
        private ShellConfiguration[] myShellConfigurations;
        private RelayCommand<string> myOpenConfigCommand;
        private RelayCommand<string> myReloadConfigCommand;

        public MainWindow()
        {
            InitializeComponent();

            myShellConfigurations = ConfigurationReader.ReadConfiguration();
            this.DataContext = myShellConfigurations;
        }

        public ICommand CreateShellCommand
        {
            get {
                myCreateShellCommand = myCreateShellCommand ?? new RelayCommand<string>(OnCreateShell);
                return myCreateShellCommand;
            }
        }

        private void OnCreateShell(string configurationName)
        {
            var cfg = myShellConfigurations.Single(c => c.Name == configurationName);

            string filename= "C:\\Windows\\System32\\cmd.exe";

            //var batch = BatchFileCreator.Create(cfg);
            //string args= $"/k \"{batch}\"";
            string args = $"/k \"devshell\"";

            var processStartInfo = new ProcessStartInfo(filename, args);
            processStartInfo.WorkingDirectory = cfg.Path;

            Process.Start(processStartInfo);
        }

        public ICommand OpenConfigCommand
        {
            get
            {
                myOpenConfigCommand = myOpenConfigCommand ?? new RelayCommand<string>(OnOpenConfig);
                return myOpenConfigCommand;
            }
        }

        private void OnOpenConfig(string obj)
        {
            string vsCode = DetectVSCodeLocation();

            var jsonConfigurationFile = ConfigurationReader.JsonConfigurationFile;

            if (vsCode == null)
            {
                Clipboard.SetText(jsonConfigurationFile);
                MessageBox.Show("Path to VS Code not found! Path to config file copied to Clipboard.", "Attention");
                return;
            }

            Process.Start(vsCode, jsonConfigurationFile);
        }

        private static string DetectVSCodeLocation()
        {
            string[] paths = new[]
            {
                @"C:\Users\plenma66\AppData\Local\Programs\Microsoft VS Code\Code.exe",
                @"C:\Program Files\Microsoft VS Code\Code.exe"
            };

            foreach (var path in paths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }
            
            return null;
        }

        public ICommand ReloadConfigCommand
        {
            get
            {
                myReloadConfigCommand = myReloadConfigCommand ?? new RelayCommand<string>(OnReloadConfig);
                return myReloadConfigCommand;
            }
        }

        private void OnReloadConfig(string obj)
        {
            myShellConfigurations = ConfigurationReader.ReadConfiguration();
            this.DataContext = myShellConfigurations;
        }
    }
}
