using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using BoDi;
using Desktop_app.Driver;
using Desktop_app.Utils.HTMLReport;
using Desktop_app.Utils.Selenium;
using Desktop_app.Pages;
using TechTalk.SpecFlow;

namespace Desktop_app.Utils.Hooks
{
    [Binding]
    public class Hooks
    {
        public IObjectContainer _objectContainer { get; set; }
        private readonly ScenarioContext _scenarioContext;
        


        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext) 
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("Running before test run...");
            ExtentReport.ExtentReportInit();
            Drivers.CreateWInAppDriver();

            /*Console.WriteLine("Running before test run...");
            ExtentReportInit();*/
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Running after test run...");
            Drivers.KillWinAppDriver();
            ExtentReport.ExtentReportTearDown();

            /*Console.WriteLine("Running after test run...");
            ExtentReportTearDown();
            drivers.QuitSession();*/
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Console.WriteLine("Running before feature...");
            ExtentReport.feature = ExtentReport.extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Console.WriteLine("Running after feature...");
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Drivers drivers = new();
            drivers.Setup();
            _objectContainer.RegisterInstanceAs(drivers);


           //Drivers.InitChrome();
            ExtentReport.scenario = ExtentReport.feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }
        /*
                [BeforeScenario]
                [Scope(Tag = "Firefox")]
                internal static void StartFireFox(ScenarioContext scenarioContext)
                {
                    Driver.InitFirefox();
                    scenario = feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
                }*/

        [AfterScenario]
        public void AfterScenario(Drivers drivers)
        {
            drivers.QuitSession();

            _objectContainer.Dispose();
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext, Drivers drivers)
        {
            Console.WriteLine("Running after step....");
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;

            //When scenario passed
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    ExtentReport.scenario.CreateNode<Given>(stepName);
                }
                else if (stepType == "When")
                {
                    ExtentReport.scenario.CreateNode<When>(stepName);
                }
                else if (stepType == "Then")
                {
                    ExtentReport.scenario.CreateNode<Then>(stepName);
                }
                else if (stepType == "And")
                {
                    ExtentReport.scenario.CreateNode<And>(stepName);
                }
            }

            //When scenario fails
            if (scenarioContext.TestError != null)
            {
                var screenshotPath = ExtentReport.addScreenshot(drivers, scenarioContext);
                var mediaEntity = MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build();

                if (stepType == "Given")
                {
                    ExtentReport.scenario.CreateNode<Given>(stepName).Fail(_scenarioContext.TestError.Message, mediaEntity);
                }
                else if (stepType == "When")
                {
                    ExtentReport.scenario.CreateNode<When>(stepName).Fail(_scenarioContext.TestError.Message, mediaEntity);
                }
                else if (stepType == "Then")
                {
                    ExtentReport.scenario.CreateNode<Then>(stepName).Fail(_scenarioContext.TestError.Message, mediaEntity);
                }
                else if (stepType == "And")
                {
                    ExtentReport.scenario.CreateNode<And>(stepName).Fail(_scenarioContext.TestError.Message, mediaEntity);
                }



                /*if (stepType == "Given")
                {


                    if (selectedRunEnv == "Local")
                    {
                        scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError,
                                               MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(Driver.LocalDriver, scenarioContext)).Build());
                    }
                    else if (selectedRunEnv == "Grid")
                    {
                        scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError,
                                              MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(Driver.LocalDriver, scenarioContext)).Build());
                    }
                }
                else if (stepType == "When")
                {

                    if (selectedRunEnv == "Local")
                    {
                        scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError,
                                               MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(Driver.LocalDriver, scenarioContext)).Build());
                    }
                    else if (selectedRunEnv == "Grid")
                    {
                        scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError,
                                              MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(Driver.LocalDriver, scenarioContext)).Build());
                    }
                }
                else if (stepType == "Then")
                {
                    if (selectedRunEnv == "Local")
                    {
                        scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError,
                                               MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(Driver.LocalDriver, scenarioContext)).Build());
                    }
                    else if (selectedRunEnv == "Grid")
                    {
                        scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError,
                                              MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(Driver.LocalDriver, scenarioContext)).Build());
                    }

                }
                else if (stepType == "And")
                {
                    if (selectedRunEnv == "Local")
                    {
                        scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError,
                                               MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(Driver.LocalDriver, scenarioContext)).Build());
                    }
                    else if (selectedRunEnv == "Grid")
                    {
                        scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError,
                                              MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(Driver.LocalDriver, scenarioContext)).Build());
                    }*/

            }
        }

    }
}

