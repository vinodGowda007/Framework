using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Config;
using SeleniumAutoFramework.Helpers;
using System;
using TechTalk.SpecFlow;

//UNCOMMENT THE BELOW LINES TO RUN THE TESTS IN PARALLEL
//[assembly: Parallelizable(ParallelScope.Fixtures)]
//[assembly: LevelOfParallelism(2)]             //NUMBER OF TESTS TO RUN IN PARALLEL SHOULD MENTIONED INSIDE

namespace Salesforce3.Hook;

[Binding]
public class Hook : TestInitializeHook
{
    private readonly new ParallelConfig _parallelConfig;
    private readonly FeatureContext _featurecontext;
    private readonly ScenarioContext _scenarioContext;
    private readonly LoggingStep loggingStep;
    public Hook(ParallelConfig parallelConfig, LoggingStep loggingStep, FeatureContext featureContext, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
    {
        _parallelConfig = parallelConfig;
        _featurecontext = featureContext;
        _scenarioContext = scenarioContext;
        this.loggingStep = loggingStep;
    }

    [BeforeStep]        // create stepNode in report for each step execution
    public void BeforeEachStep()
    {
        var stepNode = ((ExtentTest)_scenarioContext["ScenarioReport"]).CreateNode(new GherkinKeyword(_scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString()), _scenarioContext.StepContext.StepInfo.Text);
        _scenarioContext.Add("StepNode", stepNode);     // creating setpnode in scenario context key before each step execution
    }

    [AfterStep]         // saving the stepNode in report with status and screenshot

    public void AfterEachStep()
    {
        var screenshot = _parallelConfig.CaptureScreenshotAndReturnModel(_scenarioContext.ScenarioInfo.Title.Trim());
        if (screenshot != null)
        {
            switch (_scenarioContext.TestError)
            {
                case null:
                    ((ExtentTest)_scenarioContext["StepNode"]).Pass("", screenshot);
                    break;
                default:
                    ((ExtentTest)_scenarioContext["StepNode"]).Fail(_scenarioContext.TestError.Message, screenshot);
                    _parallelConfig.Driver.Quit();
                    break;
            }
        }
        else
            ((ExtentTest)_scenarioContext["ScenarioReport"]).Fail("");
        _scenarioContext.Remove("StepNode");        // removing setpnode from scenario context key after each step execution
    }

    [BeforeTestRun]
    public static void BeforeTestRunMethod()
    {
        ConfigReader.SetFrameWorkSettings();
    }
    [BeforeFeature]
    public static void BeforeFeature(FeatureContext fc)
    {
        //--------------Setting htmlreport---------------------------------//
        string indexpath = dir + fc.FeatureInfo.Title + @"\";       //index path will be ../date/time/featureName/index.html
        ExtentHtmlReporter htmlReporter = new(indexpath);
        htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
        htmlReporter.Config.DocumentTitle = fc.FeatureInfo.Title;
        htmlReporter.Config.ReportName = "Automation Report";
        //--------------Setting extentreport---------------------------------//
        ExtentReports extentReport = new();
        extentReport.AttachReporter(htmlReporter);
        fc.Add("ExReport", extentReport);               // create extent report
        ExtentTest featureReport = extentReport.CreateTest<Feature>(fc.FeatureInfo.Title);
        fc.Add("FeatureReport", featureReport);     // create feature node in extent report
    }
    [BeforeScenario]
    public void BeforeScenario()
    {
        //--------------Setting feature name---------------------------------//
        loggingStep.FeatureFileName = _featurecontext.FeatureInfo.Title;
        LoggingStep.SetFeatureFileName(_featurecontext.FeatureInfo.Title);
        LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "EXECUTION STARTED");
        LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "TEST NAME [" + LoggingStep.GetFeatureFileName() + "]");

        //--------------Setting scenario node for report---------------------------------//         
        var scenarioReport = ((ExtentTest)_featurecontext["FeatureReport"])
            .CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        _scenarioContext.Add("ScenarioReport", scenarioReport);
        LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "SCENARIO NAME [" + _scenarioContext.ScenarioInfo.Title + "]");

        //--------------Setting driver---------------------------------//
        InitializeSettings(PdfPreview.False);       //TO INITIALIZE BROWSER

        //--------------Setting excel file reader---------------------------------//
        loggingStep.excelHelper = new ExcelHelper();
        loggingStep.fileName = Environment.CurrentDirectory.ToString() + "\\" + _featurecontext.FeatureInfo.FolderPath.Replace("Features", "Data") + "\\" + loggingStep.FeatureFileName + ".xlsx";
        LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"EXCEL FILE LOCATION [{loggingStep.fileName}]");
    }
    [AfterScenario]
    public void AfterScenario()
    {
        LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "BROWSER exiting....");
        _scenarioContext.Remove("ScenarioReport");//removing key from scenario context after scenario execution
        _parallelConfig.Driver.Quit();
        LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "BROWSER EXITED");
        LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "\n\n*******************************************\n");
    }
    [AfterFeature]
    public static void AfterFeature(FeatureContext fc)
    {
        ((ExtentReports)fc["ExReport"]).Flush();         //flushing extent report for each feature
        fc.Remove("ExReport");                                 //removing key from feature context after feature execution
        fc.Remove("FeatureReport");                         //removing key from feature context after feature execution
    }
}



