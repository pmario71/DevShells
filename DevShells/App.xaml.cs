using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            //if (!((WindowsPrincipal) Thread.CurrentPrincipal).IsInRole(WindowsBuiltInRole.Administrator))
            //{
                
            //}
            base.OnStartup(e);
        }
    }
}
