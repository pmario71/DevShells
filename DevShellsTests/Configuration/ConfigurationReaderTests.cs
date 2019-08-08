using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevShells.Configuration;
using NUnit.Framework;


namespace DevShellsTests
{
    [TestFixture]
    public class ConfigurationReaderTests
    {

        [Test]
        public void Throws_if_config_shall_be_modified_after_first_access()
        {
            var jsonConfigurationFile = ConfigurationReader.JsonConfigurationFile;
            Assert.Throws<InvalidOperationException>(() =>
            {
                ConfigurationReader.JsonConfigurationFile = "";
            });
        }

        [Test]
        public void ConfigFile_resolved_correctly()
        {
            var filepath = ConfigurationReader.JsonConfigurationFile;

            var directoryName = Path.GetDirectoryName(filepath);

            Console.WriteLine(directoryName);
            Assert.IsTrue(Directory.Exists(directoryName));
        }

        [Test, Explicit]
        public void Location_for_storing_application_data()
        {
            Console.WriteLine(ConfigurationReader.JsonConfigurationFile);
        }
    }
}
