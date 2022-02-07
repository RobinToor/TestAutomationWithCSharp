using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationWithCSharp.Base;

namespace TestAutomationWithCSharp.Page
{
    public enum RequiredFields
    {
        Name,
        Email,
        Message,
        SuccessfulSubmission
    }

    public class Contact:Hook
    {
        private IWebDriver webDriver;
        private CommonMethods commonMethod;
        public Contact(IWebDriver driver)
        {
            this.webDriver = driver;
            commonMethod = new CommonMethods(driver);

        }

        IWebElement Forname => webDriver.FindElement(By.Id("forename"));
        IWebElement Surname => webDriver.FindElement(By.Id("surname"));
        IWebElement Email => webDriver.FindElement(By.Id("email"));
        IWebElement Telephone => webDriver.FindElement(By.Id("telephone"));
        IWebElement Message => webDriver.FindElement(By.Id("message"));
        IWebElement BtnSubmit => webDriver.FindElement(By.Id("contact-submit-btn"));
        IWebElement BtnBack => webDriver.FindElement(By.CssSelector("[ng-click='goBack()']"));

        string elementId_ForenameValidationMsge = "forename-err";
        string elementId_EmailValidationMsge = "email-err";
        string elementId_MessageValidationMsge = "message-err";
        string elementXpath_SuccsfulSubmissionMsge = "//div[@class='alert alert-success']";

        string welcomeMessage = "//strong[.='We welcome your feedback']";


        public void FillContactFormWithDetails(string foreName, string surname, string email, string phoneNumber, string message)
        {
            Forname.SendKeys(foreName);
            Surname.SendKeys(surname);
            Email.SendKeys(email);
            Telephone.SendKeys(phoneNumber);
            Message.SendKeys(message);
            //BtnSubmit.Click();

        }

        public void SubmitForm()
        {
            BtnSubmit.Click();
        }

        /// <summary>
        /// Return text for the required element
        /// </summary>
        /// <param name="requiredFields"></param>
        /// <returns></returns>
        public string GetValidationMessage(RequiredFields requiredFields)
        {
            switch (requiredFields)
            {
                case RequiredFields.Email:
                    return commonMethod.ReturnText(By.Id(elementId_EmailValidationMsge));
                case RequiredFields.Name:
                    return commonMethod.ReturnText(By.Id(elementId_ForenameValidationMsge));
                case RequiredFields.Message:
                    return commonMethod.ReturnText(By.Id(elementId_MessageValidationMsge));
                case RequiredFields.SuccessfulSubmission:
                    return commonMethod.ReturnText(By.XPath(elementXpath_SuccsfulSubmissionMsge));
                default:
                    return null;
            }

        }

        /// <summary>
        /// Check if element is present and return bool value
        /// </summary>
        /// <param name="requiredFields"></param>
        /// <returns></returns>
        public bool IsElementPresent(RequiredFields requiredFields)
        {
            switch (requiredFields)
            {
                case RequiredFields.Email:
                    return commonMethod.IsElementPresent(By.Id(elementId_EmailValidationMsge));
                case RequiredFields.Name:
                    return commonMethod.IsElementPresent(By.Id(elementId_ForenameValidationMsge));
                case RequiredFields.Message:
                    return commonMethod.IsElementPresent(By.Id(elementId_MessageValidationMsge));
                case RequiredFields.SuccessfulSubmission:
                    return commonMethod.IsElementPresent(By.XPath(elementXpath_SuccsfulSubmissionMsge));
                default:
                    return false;
            }
        }

        public IWebElement IsContactPageVisible()
        {
            return commonMethod.FindElement(welcomeMessage);
        }
    }
}
