using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using TestAutomationWithCSharp.Config;

namespace TestAutomationWithCSharp.Base
{
    public enum BrowserType
    {
        Chrome,
        FireFox
    }

    [TestFixture]

    public class Hook
    {
        //Setup Webdriver
        private BrowserType _browserType;
        public IWebDriver driver;

        public Hook()
        {
            
        }


        
        [SetUp]
        public void Setup()
        {
            //Set driver and open browser
            InitializeTest();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(GlobalVariables.iDriverShortWait);
            driver.Navigate().GoToUrl(Settings.aut);

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }


        public void InitializeTest()
        {
            //Read and Set Testing configurations from Json file
            ConfigReader.SetTestingConfiguration();

            // Open Browser
            OpenBrowser(GetDriverOptions(Settings.browserType));
        }


        public void SelectDriverInstance(BrowserType browserType)
        {
            if (browserType == BrowserType.Chrome)
            {
                driver = new ChromeDriver();
            }
            else if (browserType == BrowserType.FireFox)
            {
                driver = new FirefoxDriver();
            }
        }


        public DriverOptions GetDriverOptions(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return new ChromeOptions();
                case BrowserType.FireFox:
                    return new FirefoxOptions();
                default:
                    return new ChromeOptions();
            }
        }

        public void OpenBrowser(DriverOptions driverOptions)
        {
            switch (driverOptions)
            {
                case ChromeOptions chromeOptions:
                    chromeOptions.AddArguments("--start-maximized");
                    //chromeOptions.AddArguments("--headless");

                    driver = new ChromeDriver(chromeOptions);
                    break;
                case FirefoxOptions firefoxOptions:
                    firefoxOptions.AddArguments("--width=1920");
                    firefoxOptions.AddArguments("--height=1080");
                    firefoxOptions.AddArguments("--headless");
                    driver = new FirefoxDriver(firefoxOptions);
                    break;

            }

            //driver.Manage().Window.Maximize();
        }



    }
}
