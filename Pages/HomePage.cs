using Desktop_app.Driver;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium;


namespace Desktop_app.Pages
{
    public class HomePage : Base
    {
        public HomePage(Drivers driver) : base(driver)
        {
        }

        public WindowsElement BlankDocument => driver.drivers.FindElement(By.Name("Blank document"));
       // public WindowsElement BlankDocument => driver.drivers.FindElementByName("Blank document");


        public void ClickNewDocument()
        {
            BlankDocument.Click();
        }
    }
    
}
