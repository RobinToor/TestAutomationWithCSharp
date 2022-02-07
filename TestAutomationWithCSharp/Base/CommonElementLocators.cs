using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationWithCSharp.Base
{
    public class CommonElementLocators
    {
        public CommonElementLocators()
        {

        }

        public string Xpath_popUp = "//div[@class='modal-body']";
        public string Xpath_shopLink = "//a[.='Shop']";
        public string Xpath_cartLink = "//a[contains(@*,'cart')]";

    }
}
