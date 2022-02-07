using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationWithCSharp.Base;

namespace TestAutomationWithCSharp.Page
{
    public class Cart:Hook
    {
        private IWebDriver webdriver;
        private CommonMethods commonMethods;
        public Cart(IWebDriver driver)
        {
            this.webdriver = driver;
            commonMethods = new CommonMethods(driver);
        }


        IWebElement TotalPrice => webdriver.FindElement(By.CssSelector(".total"));

        public class CartSummary
        {
            public string Quantity { get; set; }
            public string Item { get; set; }
            public string Price { get; set; }
            public string SubTotal { get; set; }
        }



        public IReadOnlyCollection<CartSummary> GetCartSummary()
        {
            var result = new List<CartSummary>();

            try
            {
                IReadOnlyCollection<IWebElement> Rows = commonMethods.FindElements(By.XPath("//tbody[1]/tr"));

                for (var i = 1; i <= Rows.Count; i++)
                {

                    var details = new CartSummary()
                    {
                        Quantity = webdriver.FindElement(By.XPath("//tbody[1]/tr[" + i + "]/td[1]/input[@name='quantity']")).GetAttribute("value"),
                        Item = webdriver.FindElement(By.XPath("//tbody[1]/tr[" + i + "]/td[2]")).Text,
                        Price = webdriver.FindElement(By.XPath("//tbody[1]/tr[" + i + "]/td[3]")).Text,
                        SubTotal = webdriver.FindElement(By.XPath("//tbody[1]/tr[" + i + "]/td[4]")).Text,

                    };
                    result.Add(details);
                }

            }
            catch (Exception)
            {
                return null;
            }

            return result.ToList().AsReadOnly();
        }

        public string GetTotalPrice()
        {
            try
            {
                return  TotalPrice.Text;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
