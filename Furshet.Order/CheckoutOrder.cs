using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Furshet.Order
{
    class CheckoutOrder
    {
        public int typeOfAddress { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string numberOfHouse { get; set; }
        public int numberOfOffice { get; set; }
        public int numberOfPorch { get; set; }
        public int numberOfFlat { get; set; }
        public string commentToAddress { get; set; }

        public CheckoutOrder()
        { }
        public CheckoutOrder(int typeOfAddress, string city, string street, string numberOfHouse, int numberOfOffice,
            int numberOfPorch, int numberOfFlat, string commentToAddress)
        {
            this.typeOfAddress = typeOfAddress;
            this.city = city;
            this.street = street;
            this.numberOfHouse = numberOfHouse;
            this.numberOfOffice = numberOfOffice;
            this.numberOfPorch = numberOfPorch;
            this.numberOfFlat = numberOfFlat;
            this.commentToAddress = commentToAddress;
        }

        public void PlaceForDelivery(IWebDriver driverForBrowser)
        {
            if (driverForBrowser.FindElement(By.XPath("//*[@id='form-order-process']/div[1]/div")).Displayed)
            {
                var select = driverForBrowser.FindElement(By.XPath("//*[@id='addr']/select"));
                var selector = new SelectElement(select);
                System.Collections.Generic.IList<OpenQA.Selenium.IWebElement> options = selector.Options;
                selector.SelectByValue(Convert.ToString(typeOfAddress));
                driverForBrowser.FindElement(By.Id("fix_address_city")).Clear();
                driverForBrowser.FindElement(By.Id("fix_address_city")).SendKeys(city);
                driverForBrowser.FindElement(By.XPath("//input[@name='address_street']")).SendKeys(street);
                driverForBrowser.FindElement(By.XPath("//input[@name='address_building']")).SendKeys(numberOfHouse);
                driverForBrowser.FindElement(By.XPath("//input[@name='address_room']")).SendKeys(numberOfOffice.ToString());
                driverForBrowser.FindElement(By.XPath("//input[@name='address_entrance']")).SendKeys(numberOfPorch.ToString());
                driverForBrowser.FindElement(By.XPath("//input[@name='address_floor']")).SendKeys(numberOfFlat.ToString());
                driverForBrowser.FindElement(By.XPath("//input[@name='address_instructions']")).SendKeys(commentToAddress);
            }
        }
    }
}
