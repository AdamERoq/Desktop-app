using Desktop_app.Pages;
using System;
using TechTalk.SpecFlow;
using Desktop_app.Driver;

namespace Desktop_app.Steps
{
    [Binding]
    public class PDFSteps
    {
        public Drivers drivers;
        private PageManager PManager=> new(drivers);
        public string pdfName;
        private string expectedText = new string('a', 100);
        public PDFSteps(Drivers driver)
        {
            drivers = driver;
        }
        [Then(@"the PDF file will contain the correct text")]
        public void ThenThePDFFileWillContainTheCorrectText()
        {
            string PDFText = PManager.FilePage.ExtractTextFromPDF(pdfName);
            PDFText = PDFText.Trim().Replace("\n", "");
            Assert.That(PDFText, Is.EqualTo(expectedText), $"Expected PDF Text: {expectedText}, Actual Text: {PDFText}");
        }
    }
}
