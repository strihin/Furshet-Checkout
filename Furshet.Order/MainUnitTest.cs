using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Furshet.Order
{
    class MainUnitTest
    {
        public IWebDriver driverForBrowser { get; set; }
        public string numberOfPhone { get; set; }
        public string password { get; set; }
        public string[] dataID { get; set; }
        public CheckoutOrder order { get; set; }
        public bool[] valueOfModule { get; set; }

       public List<string> productsList { get; set; }
        /*
         * 1 - authorization
         * 2 - addtobusket
         * 3 - successfull order
         */

        public MainUnitTest(string numberOfPhone, string password, string[] dataID, IWebDriver driverForBrowser)
        {
            this.driverForBrowser = driverForBrowser;
            this.numberOfPhone = numberOfPhone;
            this.password = password;
            this.dataID = dataID;
            this.order = new CheckoutOrder();
            this.valueOfModule = new bool[3];
            this.productsList  = new List<string>();
        }

        public void Navigate()
        {
            driverForBrowser.Manage().Window.Maximize();
            driverForBrowser.Navigate().GoToUrl("http://furshet.altsolution.ua/");
            InputData();
            Thread.Sleep(2000);
            //add products to basket
            AddToBusket(dataID);
            Thread.Sleep(2000);
            driverForBrowser.Navigate().GoToUrl("http://furshet.altsolution.ua/order/process");
            SetPassingOfModule(2, "//*[@id='content']/main/div/div[1]");
        }
        public void InputData()
        {
            //exception for inputing login
            if (driverForBrowser.FindElement(By.XPath("//*[@id='header']/div/div/div[4]/a[1]")).Text == "ВЫХОД")
                return;

            driverForBrowser.FindElement(By.ClassName("link-sign")).Click();
            Thread.Sleep(2000);
            var phoneNumber = driverForBrowser.FindElement(By.Id("phone1"));
            phoneNumber.Clear();
            phoneNumber.SendKeys(numberOfPhone);
            Thread.Sleep(2000);
            var clientPassword = driverForBrowser.FindElement(By.Id("client_password"));

            if (clientPassword.Displayed)
                clientPassword.SendKeys(password);

            SetPassingOfModule(0, "//*[@id='header']/div/div/div[4]/a[1]");
            driverForBrowser.FindElement(By.Id("login_btn")).Click();

            Thread.Sleep(2000);
            DataInputxception();
        }
        public void CheckoutOrder(CheckoutOrder order)
        {
            order.PlaceForDelivery(driverForBrowser);
            TypeOfPayment();
            driverForBrowser.Navigate().GoToUrl("http://furshet.altsolution.ua/auth/signout");
        }

        public void AddToBusket(string[] datasID)
        {
            foreach (var dID in datasID)
            {
                IWebElement query = driverForBrowser.FindElement(By.XPath("//a[@data-id='" + dID + "']"));
                
                IWebElement parent = query.FindElement(By.XPath(".."));
                productsList.Add(parent.Text.Split('\r')[0]);

                query.Click();
                Thread.Sleep(1000);
            }
            /////Error
            SetPassingOfModule(1,"//*[@id='content']/aside/div[1]/div[2]/div/article[1]");
        }

        public void DataInputxception()
        {
            if (driverForBrowser.FindElement(By.XPath("//*[@id='alert-box-error']")).Displayed)
            {
                Console.WriteLine("ERROR: " + driverForBrowser.FindElement(By.Id("alert-box-error")).Text);
            }
        }

        public void TypeOfPayment()
        {
            Thread.Sleep(2000);
            //2
            driverForBrowser.FindElement(By.XPath("//*[@id='form-order-process']/div[2]/p[2]/span")).Click();
            //3
            driverForBrowser.FindElement(By.XPath("//*[@id='form-order-process']/p[2]/span")).Click();
            //4
            driverForBrowser.FindElement(By.XPath("//*[@id='form-order-process']/div[3]/p[3]/span")).Click();
            //5
            driverForBrowser.FindElement(By.XPath("//*[@id='form-order-process']/div[5]/div[1]/textarea")).Click();
            //check checkout
            driverForBrowser.FindElement(By.XPath("//*[@id='form-order-process']/button")).Click();

            #region jsTrouble
            /*agree
           JavascriptExecutor js = (JavascriptExecutor)driver;
              driverForBrowser.FindElement(By.Id("check-agreement")).


                   WebDriver driver; // Assigned elsewhere
                   JavascriptExecutor js = (JavascriptExecutor) driver;
                   js.executeScript("document.getElementById('//id of element').setAttribute('attr', '10')");
               */
            #endregion
        }

        public static IWebElement SetAttribute(IWebElement element, string name, string value)
        {
            var driver = ((IWrapsDriver) element).WrappedDriver;
            var jsExecutor = (IJavaScriptExecutor) driver;
            jsExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", element, name, value);

            return element;
        }

        public void SetPassingOfModule(int indexOfModule,string path)
        {
            if (driverForBrowser.FindElement(By.XPath(path)).Displayed)
            {
                valueOfModule[indexOfModule] = true;
            }
            else
            {
                valueOfModule[indexOfModule] = false;
            }
        }
    }
}