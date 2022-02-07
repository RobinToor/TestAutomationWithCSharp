using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationWithCSharp.Base;

namespace TestAutomationWithCSharp.Config
{
    [JsonObject("testSettings")]
    public class TestSettings
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("aut")]
        public string AUT { get; set; }


        [JsonProperty("browser")]
        public BrowserType Browser { get; set; }

    }
}
