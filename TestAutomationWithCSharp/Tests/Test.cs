using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationWithCSharp.Base;
using TestAutomationWithCSharp.Page;

namespace TestAutomationWithCSharp.Tests
{
   [TestFixture]
    public class Test:Hook
    {
        private Contact contact;
        private CommonMethods commonMethods;
        private CommonElementLocators CommonElementLocators;
        private Cart cart;
        public Test()
        {
            
        }



       private static IEnumerable<TestCaseData> TestCase1Data()
        {
            yield return new TestCaseData("John", "Smith", "test@example.com", "292992929", "This is a simple test");

        }
       //[TestCase("John","Smith","test@example.com",292992929,"This is a simple test")]
       [Test, TestCaseSource("TestCase1Data")]
        public void TestCase1(string forname,string surname, string email, string phoneNumber, string message )
        {

            contact = new Contact(driver);
            commonMethods = new CommonMethods(driver);

            commonMethods.FindElement("//a[.='Contact']").Click();

            Assert.NotNull(contact.IsContactPageVisible(), "Unable to validate Contact Page welcome message");
            //contact.PutValue("Smith");
            contact.SubmitForm();

            Assert.True(contact.IsElementPresent(RequiredFields.Email), "Email validation error not present");
            Assert.True(contact.IsElementPresent(RequiredFields.Name),  "Forename validation error not present");
            Assert.True(contact.IsElementPresent(RequiredFields.Message), "Message validation error not present");


            Assert.AreEqual("Forename is required", contact.GetValidationMessage(RequiredFields.Name), "Incorrect Forename req field validation message");
            Assert.AreEqual("Email is required", contact.GetValidationMessage(RequiredFields.Email), "Incorrect Email req field validation message");
            Assert.AreEqual("Message is required", contact.GetValidationMessage(RequiredFields.Message), "Incorrect Message req field validation message");

            contact.FillContactFormWithDetails(forname, "",email, "", message);

            Assert.True(!(contact.IsElementPresent(RequiredFields.Email)), "Email validation error present");
            Assert.True(!(contact.IsElementPresent(RequiredFields.Name)), "Forename validation error present");
            Assert.True(!(contact.IsElementPresent(RequiredFields.Message)), "Message validation error present");


        }


        private static IEnumerable<TestCaseData> TestCase2Data()
        {
            yield return new TestCaseData("John", "Smith", "test@example.com", "292992929", "This is a simple test");
            yield return new TestCaseData("John", "Smith", "test@example.com", "292992929", "This is a simple test");
            yield return new TestCaseData("John", "Smith", "test@example.com", "292992929", "This is a simple test");
            yield return new TestCaseData("John", "Smith", "test@example.com", "292992929", "This is a simple test");
            yield return new TestCaseData("John", "Smith", "test@example.com", "292992929", "This is a simple test");
        }
        //[TestCase("John","Smith","test@example.com",292992929,"This is a simple test")]
        [Test, TestCaseSource("TestCase2Data")]
        //[Repeat(5)]
        public void TestCase2(string forname, string surname, string email, string phoneNumber, string message)
        {

            contact = new Contact(driver);
            commonMethods = new CommonMethods(driver);
            CommonElementLocators = new CommonElementLocators();

            commonMethods.FindElement("//a[.='Contact']").Click();

            contact.FillContactFormWithDetails(forname, "", email, "", message);
            contact.SubmitForm();
            commonMethods.WaitUntilElementIsNotVisible(By.XPath(CommonElementLocators.Xpath_popUp));

            Assert
                .True(contact.IsElementPresent(RequiredFields.SuccessfulSubmission),
                "Message validation error not present");
           
            Assert
                .True((contact.GetValidationMessage(RequiredFields.SuccessfulSubmission))
                .Contains("we appreciate your feedback"),
                "Incorrect Forename req field validation message");
        }




        private static IEnumerable<TestCaseData> TestCase3Data()
        {
            yield return new TestCaseData("{Funny Cow;Fluffy Bunny}", "{2;1}");
        }
        [Test,TestCaseSource("TestCase3Data")]
        public void TestCase3(string toyNames, string orderQuantities)
        {

            commonMethods = new CommonMethods(driver);

            //Create a list of Toys for purchase
            List<string> toynameList = commonMethods.SplitSimpleList(toyNames);
            //Create a list of quantity per Toy Type for purchase  
            List<int> orderQuantityList = commonMethods.SplitSimpleList(orderQuantities).ConvertAll(int.Parse);
            
            
            CommonElementLocators = new CommonElementLocators();
            Shop shop = new Shop(driver);

            //Navigate to Shop page
            commonMethods.FindElement(CommonElementLocators.Xpath_shopLink).Click();
            commonMethods.WaitForPageLoad();


            // Buy/Add Toys to cart
            for (int i=0; i<toynameList.Count; i++)
            {
                
                for (int j=0 ; j<orderQuantityList[i]; j++)
                {
                    shop.BuyToy(toynameList[i]);
                }

            }

            cart = new Cart(driver);

            //Navigate to Cart Page
            commonMethods.FindElement(CommonElementLocators.Xpath_cartLink).Click();
            commonMethods.WaitForPageLoad();

            var result = cart.GetCartSummary();
                     
            //Validate Toy name and its quantity in Cart
            for (int i =0; i< toynameList.Count; i++)
            {
                var filterItem = 
                    result.Where
                    (w => w.Item == toynameList[i] && w.Quantity == Convert.ToString(orderQuantityList[i]))
                    .First();

                Assert.NotNull(filterItem, "Validation error: Expected record with Item name: " + toynameList[i] + " and Quantity: " + orderQuantityList[i] + " not found/.");
            }
            
        }

        private static IEnumerable<TestCaseData> TestCase4Data()
        {
            yield return new TestCaseData("{Stuffed Frog;Fluffy Bunny;Valentine Bear}", "{2;5;3}");
        }
        [Test, TestCaseSource("TestCase4Data")]
        public void TestCase4(string toyNames, string orderQuantities)
        {

            commonMethods = new CommonMethods(driver);

            //Create a list of different Toys in cart
            List<string> toynameList = commonMethods.SplitSimpleList(toyNames);
            
            //Create a list of quantity per Toy Type  in cart 
            List<int> orderQuantityList = commonMethods.SplitSimpleList(orderQuantities).ConvertAll(int.Parse);

            // Creata a list of Price per toy type
            List<string> PriceList = new List<string>();

            //Create a list of subtotal per toy type  
            List<decimal> SubTotalList = new List<decimal>();


            CommonElementLocators = new CommonElementLocators();
            Shop shop = new Shop(driver);

            //Navigate to Shop page
            commonMethods.FindElement(CommonElementLocators.Xpath_shopLink).Click();

            //WaitforPageLoad
            commonMethods.WaitForPageLoad();

            
            // Buy/Add Toys to cart
            for (int i = 0; i < toynameList.Count; i++)
            {

                for (int j = 0; j < orderQuantityList[i]; j++)
                {
                    shop.BuyToy(toynameList[i]);
                }

                PriceList.Add(shop.GetToyPrice(toynameList[i]));
            }

            cart = new Cart(driver);

            //Navigate to Cart Page
            commonMethods.FindElement(CommonElementLocators.Xpath_cartLink).Click();
            commonMethods.WaitForPageLoad();


            var result = cart.GetCartSummary();

            //Validate the Price for each item
            for (int i = 0; i < toynameList.Count; i++)
            {
                var filterItem =
                    result.Where
                    (w => w.Item == toynameList[i] && w.Price == Convert.ToString(PriceList[i])).First();
                    

                Assert.NotNull
                    (
                    filterItem, 
                    "Validation error: Expected price: " + PriceList[i] + " not matching with Actual price."
                    );
            }


            //Validate subtotal for each item
            for (int i = 0; i < toynameList.Count; i++)
            {

                var price = commonMethods.CovertStringToDecimalPrice(PriceList[i]);
                SubTotalList.Add((orderQuantityList[i]) * price);

                var subTotal = String.Format("{0:C}", SubTotalList[i]);
                
                var filterItem =
                    result.Where
                    (w => w.Item == toynameList[i] && w.SubTotal == subTotal)
                    .First();

                Assert.NotNull(filterItem, "Validation error: Expected subtotal: " + SubTotalList[i] + "not matching with Actual subtotal");
            }


            var TotalPrice =  commonMethods.DecimalToString(SubTotalList.Sum());

            Assert.True
                (
                cart.GetTotalPrice().Contains((TotalPrice)),
                "Validation Error: Expected Total - " + TotalPrice + " not matching with Actual Total - " + cart.GetTotalPrice() + "."
                );
        }
    }
}
