using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SalesForce3.Pages;
public class IceCheckPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    public static string ClientName;


    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

    // Selecting REcord Type
    readonly By btnIceCheck = By.XPath("//button[@name='Account.ICE_Check']");
    readonly By labelIceCheckComplete = By.XPath("(//div/div/h2[starts-with(@id,'title')])[1]");
    readonly By btnIceCheckRefresh1_0 = By.XPath("//div[@class='DESKTOP uiContainerManager']/div[1]//button[@name='refresh']");
    readonly By btnIceCheckRefresh2_0 = By.XPath("//div[@class='DESKTOP uiContainerManager']/div[2]//button[@name='refresh']");

    readonly By btnEdit = By.XPath("//button[@name='Edit']");
    readonly By btnSave = By.XPath("//button[@name='SaveEdit']");
    //readonly By IceCheckStatusValue = By.XPath("((//span[contains(text(),'ICE Status')])[1]/following::div//span/span)[1]");
    readonly By IceCheckStatusValue = By.XPath("(//span[contains(text(),'ICE Status')])[1]/following::div[1]/span/span");
    readonly By btnAppLauncher = By.XPath("//div[@class='slds-r5']");
    readonly By txtSearchAppandItem = By.XPath("//input[contains(@placeholder,'Search apps and items')]");
    readonly By appStarrLightning = By.XPath("//a[@data-label='Starr Underwriting Lightning']");
    readonly By ddValue = By.XPath("//lightning-base-combobox-item/span/span");
    readonly By ddIceStatus = By.XPath("//label[contains(text(),'ICE Status')]");
    readonly By btnDismiss = By.XPath("//button[@name='dismiss']");

    public IceCheckPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
    }

    public static int GenerateRandomNumbers()
    {
        Random random = new Random();
        return random.Next();
    }

    public void ClickOnIceCheckButton()
    {
        try
        {
            driver.WaitForElementToPresent(btnIceCheck);
            Assert.IsTrue(driver.WaitAndClick(btnIceCheck), "Could not click on Ice check button");
            if (_scenarioContext["CurrentSubmissionVersion"].ToString().Equals("Submission1_0"))
            {
                driver.WaitForElementToPresent(btnIceCheckRefresh1_0);
            }
            else if (_scenarioContext["CurrentSubmissionVersion"].ToString().Equals("Submission1_0"))
            {
                driver.WaitForElementToPresent(btnIceCheckRefresh2_0);
            }
        }
        catch (Exception e)
        {
            driver.WaitForElementToPresent(btnIceCheck);
            driver.WaitAndClick(btnIceCheck);
            if (_scenarioContext["CurrentSubmissionVersion"].ToString().Equals("Submission1_0"))
            {
                driver.WaitForElementToPresent(btnIceCheckRefresh1_0);
            }
            else if (_scenarioContext["CurrentSubmissionVersion"].ToString().Equals("Submission2_0"))
            {
                driver.WaitForElementToPresent(btnIceCheckRefresh2_0);
            }
        }

        Log("ICE CHECK BUTTON IS CLICKED");
    }

    public void VerifyIceCheckStautusCompletion()
    {
        driver.WaitForElement(labelIceCheckComplete);
        Assert.IsTrue(driver.IsDisplayed(labelIceCheckComplete), "Ice Check complete popup is not displayed");
        Log("Ice Check popup is displayed");
    }


    public void UserClickedOnRefreshButton()
    {
        if (_scenarioContext["CurrentSubmissionVersion"].ToString().Equals("Submission1_0"))
        {
            driver.WaitForElementToPresent(btnIceCheckRefresh1_0);
            driver.WaitAndClick(btnIceCheckRefresh1_0);
            driver.WaitTillOverlayDisappears(btnIceCheckRefresh1_0);
        }
        if (_scenarioContext["CurrentSubmissionVersion"].ToString().Equals("Submission2_0"))
        {
            driver.WaitForElementToPresent(btnIceCheckRefresh2_0);
            driver.WaitAndClick(btnIceCheckRefresh2_0);
            driver.WaitTillOverlayDisappears(btnIceCheckRefresh2_0);
        }
        Log("CLICKED ON REFRESH BUTTON");
    }


    public void VerifyIceStatus()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(IceCheckStatusValue));
            driver.ScrollToCenter(IceCheckStatusValue);
            Assert.IsTrue(driver.GetTextFromElement(IceCheckStatusValue).Equals("Pass"), "ICE CHECK STATUS IS NOT UPDATED");
            Log("ICE CHECK STATUS IS " + driver.GetTextFromElement(IceCheckStatusValue).ToString() + "IN CLIENT PAGE");
        }
        catch(Exception e)
        {
            Log("COULT NOT FIND THE ICE CHECK STATUS FIELD");
            Assert.Fail("COULT NOT FIND THE ICE CHECK STATUS FIELD");
        }
    }

    public void ThenUserVerifyIceCheckStatusIsBeforeICECHECKAction()
    {
        driver.WaitForElementToPresent(IceCheckStatusValue);
        driver.ScrollToCenter(IceCheckStatusValue);
        Assert.IsTrue(driver.GetTextFromElement(IceCheckStatusValue).Equals("Unchecked"), "ICE CHECK STATUS IS NOT UPDATED");
        Log("ICE CHECK STATUS IS " + driver.GetTextFromElement(IceCheckStatusValue).ToString() + "IN CLIENT PAGE");
    }


    public void NavigateToStarrUnderwritingLightningApp()
    {
        driver.WaitForNextPage();
        driver.WaitForElementToPresent(btnAppLauncher);
        driver.WaitAndClick(btnAppLauncher);
        driver.WaitAndClick(txtSearchAppandItem);
        driver.ClearAndSend(txtSearchAppandItem, "Starr underwriting lightning");
        driver.JSClick(appStarrLightning);
        driver.WaitForNextPage();
    }

    public void ClickedOnIceStatusdropdown()
    {
        driver.WaitForNextPage();
        driver.WaitAndClick(ddIceStatus);
        driver.WaitForResponse();
        Log("Clicked on Ice Status Dropdown");
    }

    public void VerifyIceStatusDropdownValue()
    {
        string[] ExpectedProjectTypeValues = { "--None--", "Unchecked", "Pass", "Frozen", "Review Pending" };

        IList<IWebElement> ActualProjectTypeValues = driver.ListOfElements(ddValue);
        foreach (IWebElement ProjectTypeValue in ActualProjectTypeValues)
        {
            Boolean Match = false;
            for (int i = 0; i < ExpectedProjectTypeValues.Length; i++)
            {
                if (ProjectTypeValue.GetElementText().Equals(ExpectedProjectTypeValues[i]))
                {
                    Match = true;
                    Console.WriteLine("Ice status values " + ExpectedProjectTypeValues[i]);
                }
            }
            Assert.IsTrue(Match, "Ice Status Dropdown values are matching");
        }
    }

    public void SelectValueFromIceCheckDropdown()
    {
        SelectValueFromDropdown(ddValue, "Frozen");
        Log("Selected Frozen from the dropdown");
    }

    public void SelectValueFromDropdown(By Elements, String Value)
    {
        IList<IWebElement> elements = driver.ListOfElements(Elements);
        foreach (IWebElement element in elements)
        {
            if (element.GetElementText() == Value.ToString())
            {
                driver.WaitAndClick(element);
                break;
            }
        }
    }


}