using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationWithCSharp.Base;

namespace TestAutomationWithCSharp.Page
{
    public enum Toys
    {
        Handmade_Doll,
        Stuffed_Frog,
        Fluffy_Bunny,
        Funny_Cow
    }
    public class Shop:Hook
    {
        private IWebDriver webDriver;
        public Shop(IWebDriver driver)
        {
            this.webDriver = driver;
        }


        /// <summary>
        /// Returns Toy's price
        /// </summary>
        /// <param name="toyname"></param>
        /// <returns></returns>
        public string GetToyPrice(string toyname)
        {
            try
            {
                return webDriver.FindElement(By.XPath("//h4[.='"+toyname+"']/following-sibling::p[2]/span")).Text;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public int BuyToy(string toyname)
        {
            try
            {
                webDriver.FindElement(By.XPath("//h4[.='" + toyname + "']/following-sibling::p[2]/a")).Click();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
