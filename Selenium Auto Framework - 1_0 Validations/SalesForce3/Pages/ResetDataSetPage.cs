using AventStack.ExtentReports.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using TechTalk.SpecFlow;
using static System.Collections.Specialized.BitVector32;

namespace SalesForce3.Pages;
public class ResetDataSetPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;

    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);   

    readonly FeatureContext _featureContext;

    public ResetDataSetPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
        _featureContext = featureContext;
    }

    public void DeleteAllFolderUnderDataSet()
    {
        String Assumed_Insurer = SubmissionPage.BaseURL + "Assumed_Insurer";
        String Clients1_0 = SubmissionPage.BaseURL + "Clients1_0";
        String Clone = SubmissionPage.BaseURL + "Clone";
        String Contacts = SubmissionPage.BaseURL + "Contacts";
        String Endorsement = SubmissionPage.BaseURL + "Endorsement";
        String Renewal1_0 = SubmissionPage.BaseURL + "Renewal1_0";
        String Section = SubmissionPage.BaseURL + "Section";
        String Sub1_0_Scenario = SubmissionPage.BaseURL + "Sub1_0_Validation";
        String Terrorism = SubmissionPage.BaseURL + "Terrorism";

        try
        {
            if (Directory.Exists(SubmissionPage.BaseURL))
            {
                //if (Directory.Exists(Assumed_Insurer))
                //{
                //    System.IO.Directory.Delete(Assumed_Insurer, true);
                //}
                if (Directory.Exists(Clients1_0))
                {
                    System.IO.Directory.Delete(Clients1_0, true);
                }
                if (Directory.Exists(Clone))
                {
                    System.IO.Directory.Delete(Clone, true);
                }
                if (Directory.Exists(Contacts))
                {
                    System.IO.Directory.Delete(Contacts, true);
                }
                if (Directory.Exists(Endorsement))
                {
                    System.IO.Directory.Delete(Endorsement, true);
                }
                if (Directory.Exists(Renewal1_0))
                {
                    System.IO.Directory.Delete(Renewal1_0, true);
                }
                
                if (Directory.Exists(Section))
                {
                    System.IO.Directory.Delete(Section, true);
                }
                if (Directory.Exists(Sub1_0_Scenario))
                {
                    System.IO.Directory.Delete(Sub1_0_Scenario, true);
                }

                if (Directory.Exists(Terrorism))
                {
                    System.IO.Directory.Delete(Terrorism, true);
                }
            }
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    public void CreateAllFolderUnderDataSet()
    {
        String DataSet = SubmissionPage.BaseURL;
        String Assumed_Insurer = SubmissionPage.BaseURL + "Assumed_Insurer";
        String Clients1_0 = SubmissionPage.BaseURL + "Clients1_0";
        String Clone = SubmissionPage.BaseURL + "Clone";
        String Contacts = SubmissionPage.BaseURL + "Contacts";
        String Endorsement = SubmissionPage.BaseURL + "Endorsement";
        String Renewal1_0 = SubmissionPage.BaseURL + "Renewal1_0";
        String Section = SubmissionPage.BaseURL + "Section";
        String Sub1_0_Scenario = SubmissionPage.BaseURL + "Sub1_0_Validation";
        String Terrorism = SubmissionPage.BaseURL + "Terrorism";

        System.Threading.Thread.Sleep(5000);
        if (!Directory.Exists(DataSet))
        {
            Directory.CreateDirectory(DataSet);
        }

        if (!Directory.Exists(Assumed_Insurer))
        {
            Directory.CreateDirectory(Assumed_Insurer);
        }

        if (!Directory.Exists(Clients1_0))
        {
            Directory.CreateDirectory(Clients1_0);
        }

        if (!Directory.Exists(Clone))
        {
            Directory.CreateDirectory(Clone);
        }
        if (!Directory.Exists(Contacts))
        {
            Directory.CreateDirectory(Contacts);
        }
        if (!Directory.Exists(Endorsement))
        {
            Directory.CreateDirectory(Endorsement);
        }
        if (!Directory.Exists(Renewal1_0))
        {
            Directory.CreateDirectory(Renewal1_0);
        }
        if (!Directory.Exists(Section))
        {
            Directory.CreateDirectory(Section);
        }
        if (!Directory.Exists(Sub1_0_Scenario))
        {
            Directory.CreateDirectory(Sub1_0_Scenario);
        }
        if (!Directory.Exists(Terrorism))
        {
            Directory.CreateDirectory(Terrorism);
        }


    }


}
