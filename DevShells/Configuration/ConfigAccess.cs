using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevShells.Configuration
{
    class ConfigAccess : IConfigAccess
    {
        private DevShellConfiguration _configuration;

        public ConfigAccess()
        {
            
        }

        public DevShellConfiguration Get()
        {
            if (_configuration == null)
                _configuration = ConfigurationReader.ReadConfiguration();

            return _configuration;
        }
    }

    public interface IConfigAccess
    {
        DevShellConfiguration Get();
    }
}
