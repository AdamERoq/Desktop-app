using Desktop_app.Driver;
using System;
using TechTalk.SpecFlow;
using Desktop_app.Pages;
using NUnit.Framework;

namespace Desktop_app.Steps
{
    [Binding]
    public class FileSteps
    {
        public Drivers drivers;
        private PageManager PManager => new(drivers);
        public string fileName;
        public string pdfName;
        string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"); 

        public FileSteps(Drivers driver)
        {
            drivers = driver;
        }

        [When(@"I save the file onto the desktop with a random name")]
        public void WhenISaveTheFileOntoTheDesktopWithARandomName()
        {
            fileName = PManager.FilePage.SaveRandomWord();
        }

        [Then(@"the document will be created and saved in the desktop directory")]
        public void ThenTheDocumentWillBeCreatedAndSavedInTheDesktopDirectory()
        {
            string filePath = Path.Combine(downloadsPath, $"{fileName}.docx");
            //Assert.That(System.IO.File.Exists($@"C:\Users\{Environment.UserName}\Downloads\{fileName}.docx"));
            Assert.That(System.IO.File.Exists(filePath));
        }

        [When(@"I save document as a pdf document to the desktop")]
        public void WhenISaveDocumentAsAPdfDocumentToTheDesktop()
        {
            pdfName = PManager.FilePage.SavePDF();
        }

        [Then(@"a PDF file will be created in the desktop directory")]
        public void ThenAPDFFileWillBeCreatedInTheDesktopDirectory()
        {
            string filePath = Path.Combine(downloadsPath, $"{pdfName}.pdf");
            //SpinWait.SpinUntil(() => File.Exists(filePath), TimeSpan.FromSeconds(30));
            //Assert.That(File.Exists(filePath), Is.True, $"PDF '{filePath}' was not found.");
            //Assert.That(System.IO.File.Exists($@"C:\Users\{Environment.UserName}\Downloads\{pdfName}.pdf"));
            Assert.That(System.IO.File.Exists(filePath));
        }

    }
}