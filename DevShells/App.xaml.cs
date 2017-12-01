using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;


namespace DevShells
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        const string eventSource = "DevShells";

        protected override void OnStartup(StartupEventArgs e)
        {
            Dispatcher.UnhandledException += (sender, args) =>
            {
                var exString = args.Exception.ToString();
                
                if (!EventLog.SourceExists(eventSource))
                {
                    EventLog.CreateEventSource(eventSource, "Application");
                }
                EventLog.WriteEntry(eventSource, exString, EventLogEntryType.Error);

                MessageBox.Show($"Full error is logged to EventLog!\r\n{exString}", "Unexpected Error");
                args.Handled = true;
            };

            base.OnStartup(e);
        }
    }
}
