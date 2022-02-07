using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace TestAutomationWithCSharp.Base
{
    public class CommonMethods:Hook
    {
        private IWebDriver webDriver;
        public CommonMethods(IWebDriver driver)
        {
            this.webDriver = driver;
        }

        public IWebElement FindElement( string path, int waitTime = GlobalVariables.iDriverLongwait)
        {
            try
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(waitTime));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(path)));
                IWebElement item = webDriver.FindElement(By.XPath(path));
                return item;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Method to wait and find all elements
        /// </summary>
        /// <param name="by"></param>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        public IReadOnlyCollection<IWebElement>FindElements(By by, int waitTime = GlobalVariables.iDriverLongwait)
        {
            try
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(waitTime));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
                var item = webDriver.FindElements(by);
                return item;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Method to wait until a particular element is not visible on the page
        /// </summary>
        /// <param name="by"></param>
        /// <param name="waitTime"></param>
        public void WaitUntilElementIsNotVisible(By by, int waitTime = GlobalVariables.iDriverLongwait)
        {
            try
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(waitTime));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
            }
            catch (WebDriverTimeoutException)
            {

            }
        }

        /// <summary>
        /// Wait for page to load
        /// </summary>
        public void WaitForPageLoad()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(GlobalVariables.iDriverShortWait));
            IJavaScriptExecutor jsScript = webDriver as IJavaScriptExecutor;
            wait.Until((d) => (bool)jsScript.ExecuteScript("return jQuery.active == 0"));
        }

        /// <summary>
        /// To check if element is present on the page
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public bool IsElementPresent(By by)
        {
            try
            {
                webDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string ReturnText(By by)
        {
            try
            {
                return webDriver.FindElement(by).Text;             
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// splits a simple string i.e. "{item1;item2...}" and returns a list ["item1", "item2"...]
        /// </summary>
        /// <param name="rawlist"></param>
        /// <returns></returns>
        public List<string> SplitSimpleList(string rawlist)
        {
            try
            {
                return rawlist.Replace("{", "").Replace("}", "").Split(";").ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert string to decimal price without currency Symbol
        /// </summary>
        /// <param name="strPrice"></param>
        /// <returns></returns>
        public decimal CovertStringToDecimalPrice(string strPrice)
        {
            try
            {
                return Decimal.Parse((strPrice), NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
            }
            catch(Exception)
            {
                return 0;
            }
        }


        public string DecimalToString(decimal dec)
        {
            string strdec = dec.ToString(CultureInfo.InvariantCulture);
            var l = strdec.Contains(".") ? strdec.TrimEnd('0').TrimEnd('.') : strdec;
            return l;
        }
    }
}
