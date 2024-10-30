using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop_app.Driver;


namespace Desktop_app.Pages
{
    public class WordPage : Base
    {
        public WordPage(Drivers driver) : base(driver)
        {

        }

        public WindowsElement AptosElement => driver.drivers.FindElement(By.Name("Aptos (Body)"));

        public WindowsElement EditDocument => driver.drivers.FindElement(By.Name("Page 1 content"));

        public WindowsElement FontButtonElement => driver.drivers.FindElement(By.Name("Open"));

        public WindowsElement NewDocument => driver.drivers.FindElement(By.Name("Page 1"));

        public void TypeInDocument(int page0)
        {
            string textToPaste = new string('a', page0);
            EditDocument.Click();
            IWebElement activeElement = driver.drivers.SwitchTo().ActiveElement();
            activeElement.SendKeys(textToPaste);
        }
        public void FontButtonClick()
        {
            FontButtonElement.Click();
        }
        public void NewDocumentClick()
        {
            NewDocument.Click();
        }
    }
}
