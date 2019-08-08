using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using DevShells.Configuration;
using DevShells.Services;
using Serilog;
using SimpleInjector;


namespace DevShells
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Container _container;

        readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        const string eventSource = "DevShells";

        public App()
        {
            _container = new Container();
            SetupLogging(_container);

            _container.RegisterSingleton<IConfigAccess, ConfigAccess>();
            _container.RegisterSingleton<INotifications, NotificationsSvc>();
            _container.RegisterSingleton<MainWindow>();
            _container.Verify();

            var wnd = _container.GetInstance<MainWindow>();
            this.MainWindow = wnd;

            wnd.Show();
        }



        private void OnSessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
            var updater = _container.GetInstance<Updater>();
            if (updater.IsUpdatePending)
            {
                MessageBox.Show("Pending update has been downloaded and will be applied on next start.", "Update pending ..");
            }

            _tokenSource.Cancel();
            _container.Dispose();
        }

        private void SetupLogging(Container container)
        {
            //string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "Snippet\\Snippet.log";
            //StaticConfigration.LogFile = logFilePath;
            var log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(Path.Combine(StaticConfiguration.BaseFolder,"DevShell.log"), rollOnFileSizeLimit:true)
                .CreateLogger();
            _container.Register<ILogger>(() => log, Lifestyle.Singleton);
        }
    }
}
