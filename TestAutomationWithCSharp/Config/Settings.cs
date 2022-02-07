using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationWithCSharp.Base;

namespace TestAutomationWithCSharp.Config
{
    public class Settings
    {
        public static string name { get; set; }

        public static string aut { get; set; }

        public static  BrowserType browserType { get; set; }
    }
}
