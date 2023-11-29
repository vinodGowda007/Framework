using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using SeleniumAutoFramework.Config;
using SeleniumAutoFramework.Helpers;
using System;
using System.IO;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumAutoFramework.Base
{
    public class TestInitializeHook
    {
        public readonly BrowserType _browser;
        public readonly ParallelConfig _parallelConfig;
        readonly LoggingStep _loggingStep1;
        public static ExtentReports extent;
        public ExtentTest test;
        //FOR FOLDER CREATION TO STORE EXTENT REPORT
        static string date = DateTime.Now.ToString("dd-MM-yyyy");
        static string time = DateTime.Now.ToString("hh_mm_ss tt");
        public static string dir = Directory.GetCurrentDirectory().Split("bin")[0] + @"Reports\" + DateTime.Now.ToString("dd-MM-yyyy") + @"\" + DateTime.Now.ToString("hh_mm_ss tt") + @"\";

        //FOR AZURE ARTIFACT LOCATION
        //public static string dir = AppDomain.CurrentDomain.BaseDirectory+@"\"+@"Reports\" + date + @"\" + time + @"\";

        //TO INITIALIZE HOOKS
        public TestInitializeHook(ParallelConfig parallelConfig, LoggingStep loggingStep)
        {
            ConfigReader.SetFrameWorkSettings();
            _browser = Settings.Config_BrowserType;
            _parallelConfig = parallelConfig;
            _loggingStep1 = loggingStep;
        }


        //TO INITIALIZE BROWSER SETTINGS
        public void InitializeSettings(PdfPreview pdfPreview)
        {
            OpenBrowser(_browser, pdfPreview);
            LogHelper.LogFile(_loggingStep1.FeatureFileName, "Initialized Framework");
        }

        //TO LAUNCH BROWSER BASED ON SELECTION MADE IN GLOBAL CONFIG FILE
        public void OpenBrowser(BrowserType browserType, PdfPreview pdfPreview)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:        //CHROME BROWSER
                    ChromeOptions option = new ChromeOptions();
                    option.AddArgument("start-maximized");
                    option.AddArgument("--disable-gpu");
                    option.AddUserProfilePreference("download.default_directory", dir);
                    option.AddUserProfilePreference("download.prompt_for_download", false);
                    option.AddUserProfilePreference("download.directory_upgrade", true);
                    if (pdfPreview == PdfPreview.True)
                    {
                        option.AddUserProfilePreference("plugins.plugins_disabled", "Chrome PDF Viewer");
                        option.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
                    }
                    //option.AddArgument("--headless");
                    //option.AddArgument("window-size=1366,667");
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    _parallelConfig.Driver = new ChromeDriver(option);
                    _parallelConfig.Driver.Manage().Cookies.DeleteAllCookies();
                    break;
                case BrowserType.Edge:    //EDGE BROWSER
                    EdgeOptions opt = new();
                    opt.AddArgument("--incognito");
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    _parallelConfig.Driver = new EdgeDriver(opt);
                    _parallelConfig.Driver.Manage().Window.Maximize();
                    _parallelConfig.Driver.Manage().Cookies.DeleteAllCookies();
                    break;
                default:
                    ChromeOptions optiond = new ChromeOptions();
                    optiond.AddArgument("start-maximized");
                    optiond.AddArgument("--disable-gpu");
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    _parallelConfig.Driver = new ChromeDriver(optiond);
                    break;
            }
        }

        //TO INITIALIZE EXTENT REPORTS FOR GENERATING HTML REPORTS.
        public static void TestInitialize()
        {
            //INITAILIZE EXTENT REPORT BEFORE TEST STARTS
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(dir);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            //Attach report to reporter
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

        }
    }
    public enum PdfPreview
    {
        True,
        False
    }

    public enum BrowserType
    {
        Chrome,
        internetExplorer,
        Firefox,
        Edge
    }
}
