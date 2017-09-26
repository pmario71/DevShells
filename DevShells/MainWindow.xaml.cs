﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly ShellConfiguration[] myShellConfigurations;

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
    }
}
