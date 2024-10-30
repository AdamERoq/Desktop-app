using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop_app.Driver;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;



namespace Desktop_app.Pages
{
    public class FilePage : Base 
    {
        public PageManager manager;
        public FilePage(Drivers driver) : base(driver)
        {

        }
        public string savedFileName;

        public WindowsElement BrowseButtonElement => driver.drivers.FindElement(By.Name("Browse"));

        public WindowsElement DownloadsButtonElement => driver.drivers.FindElement(By.Name("Downloads (pinned)"));

        public WindowsElement FileNameBoxElement => driver.drivers.FindElement(By.Name("Enter file name here"));

        public WindowsElement FileTabElement => driver.drivers.FindElement(By.Name("File Tab"));

        public WindowsElement SaveAsTypeElement => driver.drivers.FindElement(By.Name("Save as type"));
        public WindowsElement SaveTypePdfElement => driver.drivers.FindElement(By.Name("PDF (*.pdf)"));
        public WindowsElement SaveButtonElement => driver.drivers.FindElement(By.Name("Save"));

        public WindowsElement SaveAsButtonElement => driver.drivers.FindElement(By.Name("Save As"));

        private WebDriverWait Wait => new WebDriverWait(driver.drivers, TimeSpan.FromSeconds(10));

        public void BrowseButtonClick()
        {
            Wait.Until(driver => BrowseButtonElement.Displayed);
            BrowseButtonElement.Click();
            Thread.Sleep(5000);
        }
        public void ClickFileTab()
        {
            FileTabElement.Click();
        }

        public void DownloadsButtonClick()
        {

            Wait.Until(driver => DownloadsButtonElement.Displayed);
            DownloadsButtonElement.Click();
            Thread.Sleep(5000);
        }

        public void FileNameBoxWrite(string fileName)
        {
            Wait.Until(driver => FileNameBoxElement.Displayed);
            FileNameBoxElement.Clear();
            FileNameBoxElement.SendKeys(fileName);
            Thread.Sleep(5000);

        }
        public void SaveButtonClick()
        {

            Wait.Until(driver => SaveButtonElement.Displayed);
            SaveButtonElement.Click();
            Thread.Sleep(5000);
        }
        public void SaveAsButtonClick()
        {
            Wait.Until(driver => SaveAsButtonElement.Displayed);
            SaveAsButtonElement.Click();
            Thread.Sleep(5000);
        }

        public void SaveAsTypeClick()
        {
            Wait.Until(driver => SaveAsTypeElement.Displayed);
            SaveAsTypeElement.Click();
            Thread.Sleep(5000);
        }

        public void SaveTypePdfClick()
        {
            Wait.Until(driver => SaveTypePdfElement.Displayed);
            SaveTypePdfElement.Click();
            Thread.Sleep(5000);
        }

        public string SaveRandomWord()
        {
            //Guid changes the random generated name to a string
            savedFileName = Guid.NewGuid().ToString();

            ClickFileTab();

            SaveAsButtonClick();

            FileNameBoxWrite(savedFileName);

            BrowseButtonClick();

            DownloadsButtonClick();

            SaveButtonClick();


            Console.WriteLine($"Saved file name: {savedFileName}");
            Console.WriteLine($"Expected file path: {Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", savedFileName)}");
            return savedFileName;

        }

        public string SavePDF()
        {
            //Guid changes the random generated name to a string
            savedFileName = Guid.NewGuid().ToString();

            ClickFileTab();

            SaveAsButtonClick();

            FileNameBoxWrite(savedFileName);

            SaveAsTypeClick();

            SaveTypePdfClick();

            BrowseButtonClick();

            DownloadsButtonClick();

            SaveButtonClick();

            Console.WriteLine($"Saved file name: {savedFileName}");
            Console.WriteLine($"Expected file path: {Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", savedFileName)}");
            return savedFileName;

        }

        public string ExtractTextFromPDF(string pdfFileName)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", $"{pdfFileName}.pdf");

            StringBuilder stringBuilder = new();

            using (PdfReader reader = new(filePath))
            {
                using (PdfDocument pdfDocument = new(reader))
                {
                    PdfPage pdfPage = pdfDocument.GetPage(1);
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string text = PdfTextExtractor.GetTextFromPage(pdfPage, strategy);
                    stringBuilder.Append(text);
                }
            }

            string extractedText = stringBuilder.ToString().Trim().Replace("\n", "");
            return extractedText;
        }

    }
}
