using System;
using TechTalk.SpecFlow;
using Desktop_app.Driver;
using Desktop_app.Pages;
using System.Drawing;

namespace Desktop_app.Steps
{
    [Binding]
    public class WordSteps
    {

        public Drivers drivers;
        private PageManager PManager=> new(drivers);

        public WordSteps(Drivers driver)
        {
            drivers = driver;
        }


        [Given(@"I click on Blank Document from the Home menu")]
        public void GivenIClickOnBlankDocumentFromTheHomeMenu()
        {
            //Thread.Sleep(5000);
            PManager.Homepage.ClickNewDocument();
        }

        [Then(@"the default font is (.*)’")]
        public void ThenTheDefaultFontIsAptosBody(string font)
        {
            PManager.WordPage.FontButtonClick();
            font = "Aptos (Body)";
            Assert.That(font, Is.EqualTo(PManager.WordPage.AptosElement.Text));
        }

        [Given(@"I create a new document and paste in (.*) characters")]
        public void GivenICreateANewDocumentAndPasteInCharacters(int page0)
        {
            PManager.Homepage.ClickNewDocument();
            PManager.WordPage.TypeInDocument(page0);
        }
    }
}
