using Desktop_app.Driver;
using Desktop_app.Pages; 
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_app.Pages
{
    public class Base
    {
        protected Drivers driver;

        protected Base(Drivers driver)
        {
            this.driver = driver;

        }


    }

}
