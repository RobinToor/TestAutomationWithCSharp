using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationWithCSharp.Config
{
    public class ConfigReader
    {
        public static void SetTestingConfiguration()
        {
            var sectionname = "testSettings";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testingSettings.json");
            IConfigurationRoot configurationRoot = builder.Build();

            Settings.name = configurationRoot.GetSection(sectionname).Get<TestSettings>().Name;
            Settings.browserType = configurationRoot.GetSection(sectionname).Get<TestSettings>().Browser;
            Settings.aut = configurationRoot.GetSection(sectionname).Get<TestSettings>().AUT;

        }
    }
}
