using OpenQA.Selenium;
using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Config;
using SeleniumAutoFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps
{
    [Binding]
    public class LoginPageSteps : BaseStep
    {
        private readonly LoggingStep _loggingStep;
        private readonly IWebDriver Driver;

        //DEPENDENCY INJECTION OR CONTEXT INJECTION
        private readonly ExcelHelper excelHelper;
        static DataTableCollection dataCollection;
        private static List<DataCollection> _dtColprpg = new();

        //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
        public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColprpg, _loggingStep.rowNo);


        //CREATING OBJECT REFERENCE FOR PAGE CLASS
        private readonly LoginPage LP;

        //DEPENDENCY INJECTION OR CONTEXT INJECTION 
        public LoginPageSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
        {
            _loggingStep = loggingStep;
            Driver = parallelConfig.Driver;
            LP = new(parallelConfig, loggingStep, scenairoContext);
            // Added By vinod
            excelHelper = loggingStep.excelHelper;
            dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);
            _dtColprpg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Profile", dataCollection);
        }

        //THIS METHOD IS USED TO SIMPLIFY THE LOG WRITING
        public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

        


        // Login with multiple user
        [When(@"UW enters the login credentials in single sign in to test (.*) using ""([^""]*)""")]
        public void WhenUWEntersTheLoginCredentialsInSingleSignInToTestScenarioUsing(string p0, string p1)
        {
            _loggingStep.rowNo = p0;
            //LP.CheckIfLoginPageIsDisplayed();
            string userName = Settings.Config_AUT_UserName;
            string password = Settings.Config_AUT_Password;
            //LP.LoginToStarrInsurance(userName, password);
            LP.EnterUserNameAndPassword(userName, password);
            try
            {
                LP.ChangeUserProfile(ExcelValue(p1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        [When(@"User Enters Admin credentials Scenario(.*)")]
        public void WhenUserEntersAdminCredentialsScenario(string p0)
        {
            _loggingStep.rowNo = p0;
            LP.CheckIfLoginPageIsDisplayed();
            string userName = Settings.Config_AUT_UserName;
            string password = Settings.Config_AUT_Password;
            LP.LoginToStarrInsurance(userName, password);
            //LP.EnterUserNameAndPassword(userName, password);
            //LP.ClickLogin();
        }

        [When(@"User logged in with ""([^""]*)""")]
        public void WhenUserLoggedInWith(string p0)
        {
            LP.ChangeUserProfile(ExcelValue(p0));
        }


        [When(@"User enters the login credentials in single sign in to test Scenario(.*)")]
        public void WhenUserEntersTheLoginCredentialsInSingleSignInToTestScenario(string p0)
        {
            _loggingStep.rowNo = p0;
            LP.CheckIfLoginPageIsDisplayed();
            string userName = Settings.Config_AUT_UserName;
            string password = Settings.Config_AUT_Password;
            LP.LoginToStarrInsurance(userName, password);
            //LP.EnterUserNameAndPassword(userName, password);
            //LP.ClickLogin();
        }


        //NAVIGATING THE APPLICATION
        [Given(@"UW Navigate To Salesforce Application")]
        public void UWNavigateToSalesforceApplication()
        {
            Log("Testcase steps Execution Started in Login Steps");
            Driver.Url = Settings.Config_AUT;
            //Driver.Navigate().GoToUrl(Settings.Config_AUT);
            Log("Browser has been opened");
        }


        [Then(@"User Navigate to the record based on the Stage")]
        public void ThenUserNavigateToTheRecordBasedOnTheStage()
        {
            if (SubmissionPage.flagCancelled == true)
            {
                Driver.Url = Settings.Config_AUT;
                LP.ChangeUserProfile(ExcelValue("Under Writer"));
            }
        }

        //ENCRYPT LOGIN PASSWORD
        [Given(@"Encrypt The Given String ""(.*)""")]
        public void GivenEncryptTheGivenString(string p0)
        {
            LP.EncryptString(p0);
        }

        [Then(@"User enter the ""([^""]*)"" Username")]
        public void ThenUserEnterTheUsername(string p0)
        {
            LP.ChangeUserProfile(ExcelValue(p0));
        }
    }
}
