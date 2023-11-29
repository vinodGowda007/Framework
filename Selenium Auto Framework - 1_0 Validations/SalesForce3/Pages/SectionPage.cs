using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace SalesForce3.Pages;
public class SectionPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    public static string ScenarioCount, PolicyLimitId;
    public static string SectionFilepath = SubmissionPage.BaseURL + "Section/Section1_0.txt";

    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

    // Endorsement Webelements
    readonly By pageSection = By.XPath("//h2/span[contains(text(),'Section')]");
    readonly By TopPage = By.XPath("//h3[contains(text(),'Submission Detail')]");
    readonly By btnSave = By.XPath("//button[@name='cancel']/parent::div/button[1]");
    readonly By lblCreatedSection = By.XPath("//span[contains(text(),'Submission Name')]/following::div[1]/span/span");
    //readonly By lblErrorMessages = By.XPath("(//div[@role='alert'])[2]");

    readonly By lblErrorMessages = By.XPath("//div[@class='slds-notify__content']/h2/following::p[1]");
    readonly By lblErrorMessageInStageProgression = By.XPath("//div[@class='slds-notify__content']/h2");


    public SectionPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
    }

    public void SaveSection()
    {
        driver.MoveToTheElement(btnSave);
        Assert.IsTrue(driver.WaitAndClick(btnSave), "COULD NOT CLICK ON SAVE BUTTON");
        driver.CaptureScreen(_scenarioContext);
        driver.MoveToTheElement(TopPage);
    }
    public void SaveSectionRecord()
    {
        driver.MoveToTheElement(btnSave);
        Assert.IsTrue(driver.WaitAndClick(btnSave), "COULD NOT CLICK ON SAVE BUTTON");
        driver.CaptureScreen(_scenarioContext);
        driver.MoveToTheElement(pageSection);
        for (int waitIteration = 0; waitIteration < 3; waitIteration++)
        {
            if (driver.IsDisplayed(lblErrorMessageInStageProgression))
            {
                if (driver.IsDisplayed(lblErrorMessages))
                {
                    driver.ScrollToCenter(lblErrorMessages);
                    driver.CaptureScreen(_scenarioContext);
                    Console.WriteLine("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessages));
                    Log("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessages));
                    Assert.Fail("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessages));
                }
                else
                {
                    Console.WriteLine("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessageInStageProgression));
                    Log("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessageInStageProgression));
                    Assert.Fail("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessageInStageProgression));
                }
            }
            else
            {
                System.Threading.Thread.Sleep(2000);
            }
        }
        driver.WaitTillOverlayDisappears(btnSave);
        System.Threading.Thread.Sleep(1000);
        driver.Refresh();
        Assert.IsTrue(driver.WaitForElementToPresent(lblCreatedSection), "COULD NOT CREATE SECTION RECORD");
        Log(" SECTION RECORD IS CREATED SUCCESSFULLY");
        Console.WriteLine(" SECTION RECORD IS CREATED SUCCESSFULLY");















    }
}
