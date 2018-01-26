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
        public void ConfigFile_resolved_correctly()
        {
            var filepath = ConfigurationReader.JsonConfigurationFile;

            var directoryName = Path.GetDirectoryName(filepath);

            Console.WriteLine(directoryName);
            Assert.IsTrue(Directory.Exists(directoryName));
        }

    }
}
