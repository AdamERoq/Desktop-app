using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop_app.Utils.Selenium;

namespace Desktop_app.Driver
{
    public class Drivers
    {
        public WindowsDriver<WindowsElement> drivers;
        

        public void Setup()
        {
            AppiumOptions options = new();
            options.AddAdditionalCapability("app", @"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE");
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "10");
            drivers = new WindowsDriver<WindowsElement>(new Uri(Settings.DriverUrl), options);
        }

        public static void CreateWInAppDriver()
        {
            Process process = new();
            process.StartInfo.FileName = "C:\\Users\\adam.eckley\\IdeaProjects\\Adam.Eckley-Kittens-Assessment\\Drivers\\Windows Application Driver\\WinAppDriver.exe";
            process.Start();
        }

        public static void KillWinAppDriver()
        {
            Process.GetProcessesByName("WinAppDriver").ToList().ForEach(i => i.Kill());
        }

        public void QuitSession()
        {
            drivers.Quit();
            Process.GetProcessesByName("WINWORD").ToList().ForEach(i => i.Kill());
            Process.GetProcessesByName("Acrobat").ToList().ForEach(i => i.Kill());
            
            
        }
    }
}
