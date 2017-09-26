using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevShells.Configuration;


namespace DevShells.Utils
{
    class BatchFileCreator
    {

        public static string Create(ShellConfiguration config)
        {
            string path = $"{Path.GetTempPath()}{config.Name}.cmd";

            if (!File.Exists(path))
            {
                var sb = new StringBuilder();
                sb.AppendLine("devshell");
                sb.AppendLine($"title {config.Name}");

                File.WriteAllText(path, sb.ToString());
            }

            return path;
        }

    }
}
