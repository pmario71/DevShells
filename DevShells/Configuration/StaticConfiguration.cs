using System;
using System.IO;

namespace DevShells.Configuration
{
    class StaticConfiguration
    {
        public static string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DevShells");
    }
}