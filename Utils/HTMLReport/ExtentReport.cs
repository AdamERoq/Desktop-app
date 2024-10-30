using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Desktop_app.Driver;

namespace Desktop_app.Utils.HTMLReport
{
    public class ExtentReport
    {
        public static ExtentReports extentReports;
        public static ExtentTest feature;
        public static ExtentTest scenario;
        public static string reportFilePath = "";

        public static void ExtentReportInit()
        {
            // creates a folder for the HTML report to sit in
            string projectName = "Desktop_app";
            string reportFolder = "TestResults";
            string projectPath = Directory.GetCurrentDirectory();
            string reportPath = Path.Combine(projectPath, projectName, reportFolder);
            Directory.CreateDirectory(reportPath);

            //creates a file name with date and time stamp.
            string reportTimeStamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
            string fileName = "_Extent_Report.html";
            string AUT = "Desktop";
            string reportName = $"{reportTimeStamp}_{AUT}_{fileName}";

            //creates full report path with name
            reportFilePath = Path.Combine(reportPath, reportName);
            ExtentSparkReporter htmlReporter = new ExtentSparkReporter(reportFilePath);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReporter);
        }

        public static void ExtentReportTearDown()
        {
            extentReports.Flush();
        }

        public static string addScreenshot(Drivers driver, ScenarioContext scenarioContext)
        {
            //Navigate to Extent reports folder
            string currentDirectory = Directory.GetCurrentDirectory();
            string reportFolder = "Desktop_app/TestResults";
            string reportPath = Path.Combine(currentDirectory, reportFolder);
            Directory.CreateDirectory(reportPath);

            //Navigate to screenshot folder
            string screenshotFolder = "Test_Failed_Screenshots";
            string ScreenshotsFolderLoc = Path.Combine(reportPath, screenshotFolder);
            Directory.CreateDirectory(ScreenshotsFolderLoc);

            //Creates a screenshot file name with date and time stamps
            string screenshotTimeStamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
            string screenshotFileName = "Screenshots.png";
            string AUT = "Desktop";
            string screenshotName = $"{screenshotTimeStamp}_{AUT}_{screenshotFileName}";

            //Takes screenshot
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
            Screenshot screenshot = takesScreenshot.GetScreenshot();

            //Save screenshots to folder
            string screenshotLoc = Path.Combine(ScreenshotsFolderLoc, screenshotName);
            screenshot.SaveAsFile(screenshotLoc);

            //Gereate path relative to reports folder
            string screenshotPath = Path.Combine("Test_Failed_Screenshots", screenshotName);

            return screenshotPath;
        }
    }
}
