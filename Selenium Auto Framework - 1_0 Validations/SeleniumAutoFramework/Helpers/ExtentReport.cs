using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using SeleniumAutoFramework.Base;
using System.IO;
using System.Threading;
using TechTalk.SpecFlow;

namespace SeleniumAutoFramework.Helpers;

public sealed class ExtentReport
{
    private ExtentReport() { }
    private static ExtentReports extent;
    private static readonly string failedTestPath = Directory.GetCurrentDirectory().Split("bin")[0] + @"FailedTest\FailedTestCases.txt";
    /// <summary>
    /// Create an object for writing log to ExtentReport
    /// </summary>
    public static void InitReports()
    {
        if (extent == null)
        {
            ExtentHtmlReporter htmlReporter = new(TestInitializeHook.dir);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReporter.Config.DocumentTitle = "Automation Report";
            htmlReporter.Config.ReportName = "Automation Report";
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
    }
    /// <summary>
    /// Attach logs and screenshot to the ExtentReport
    /// </summary>
    public static void FlushReports()
    {
        if (extent != null)
        {
            extent.Flush();
        }
    }

    //TO CREATE AND USE SCENARIO NODE
    private static readonly ThreadLocal<ExtentTest> scenarioNode = new();
    public static ExtentTest Test() => scenarioNode.Value;
    /// <summary>
    /// Create feature and scenario node in extent report for current test execution.
    /// </summary>
    public static void SetTest(string featureName, string scenarioName) => scenarioNode.Value = extent.CreateTest<Feature>(featureName).CreateNode<Scenario>(scenarioName);

    //TO CREATE AND USE STEP NODE
    private static readonly ThreadLocal<ExtentTest> stepNode = new();
    /// <summary>
    /// Upload execute step result and screenshot to step node in extent report
    /// </summary>
    public static ExtentTest StepNode() => stepNode.Value;

    ///  /// <summary>
    /// Create a  step node inside a scenario node in extent report
    /// </summary>
    public static void SetStepNode(ScenarioContext scenarioContext) => stepNode.Value = ExtentReport.Test().CreateNode(new GherkinKeyword(scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString()), scenarioContext.StepContext.StepInfo.Text);

    /// <summary>
    /// Logs the final result of test execution to logfile and to write failed tests in FailedTestCase.txt
    /// </summary>
    public static void Output(ScenarioContext _scenarioContext)
    {
        if (_scenarioContext.ScenarioExecutionStatus.ToString() == "UndefinedStep")
        {
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "TEST RESULT : " + "[FAILED] Unimplemented Steps");
            ExtentReport.Test().Log(Status.Skip);
        }
        else if (_scenarioContext.TestError != null)
        {
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "TEST RESULT : [FAILED] " + _scenarioContext.TestError.Message);
        }
        else if (_scenarioContext.TestError == null)
        {
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "TEST RESULT : [PASSED]");
        }
    }

}


