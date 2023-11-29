using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SalesForce3.Pages;
public class ReservationCheckPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    private string ClientName;

    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

    // ReserVation Check Objects
    readonly By lnkExpClientName = By.XPath("(//div/records-hoverable-link/div/a//span)[1]");
    readonly By lblActClientName = By.XPath("(//div/p/div/div/lightning-formatted-text)[3]");

    readonly By btnAllActionsList = By.XPath("(//div[@class='slds-dropdown slds-dropdown_right'])[3]//runtime_platform_actions-action-renderer//a/span");
    readonly By btnShowMoreActions = By.XPath("(//li/lightning-button-menu/button)[1]");
    readonly By btnReservationCheckClients = By.XPath("//lightning-button/button[contains(text(),'Reservation Check')]");
    readonly By lblClientNameClientObj = By.XPath("//div/lightning-formatted-text");


    public ReservationCheckPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
    }

    public void ClickOnReservationCheck(string ObjName)
    {
        Boolean flag = false;

        if (ObjName.ToString() == "Submission")
        {
            ClientName = driver.GetTextFromElement(lnkExpClientName).ToString();
            driver.ScrollToCenter(btnShowMoreActions);
            driver.WaitAndClick(btnShowMoreActions);
            flag = SelectValueFromDropdown(btnAllActionsList, "Reservation Check");
        }
        if (ObjName.ToString() == "Clients")
        {
            ClientName = driver.GetTextFromElement(lblClientNameClientObj).ToString();
            driver.WaitForElementToPresent(btnReservationCheckClients);
            driver.WaitAndClick(btnReservationCheckClients);
            flag = true;
        }

        Assert.IsTrue(flag, "Could not click on Reservation Check Button");
        Log("Clicked on Reservation Check button");
        System.Threading.Thread.Sleep(2000);
    }

    //Method to Select value from the dropdown
    public Boolean SelectValueFromDropdown(By Elements, String Value)
    {
        IList<IWebElement> elements = driver.ListOfElements(Elements);
        foreach (IWebElement element in elements)
        {
            if (element.GetElementText() == Value.ToString())
            {
                driver.WaitAndClick(element);
                return true;
            }
        }
        return false;
    }


    public void NavigatedToReservationCheckPage()
    {
        Assert.AreEqual(2, driver.WindowHandles.Count, "Reservation Check page is not displayed");
        SwitchToWindow();
        Assert.IsTrue(driver.WaitForTitleContains("Aura"), "Could not navigate to Reservation Check Window");
        Log("Switch to Reservation Check Page");
    }

    // Verify Reservation Check
    public void VerifyReservationCheckPage()
    {
        Console.WriteLine("Exp :" + ClientName);
        Console.WriteLine("Act :" + driver.GetTextFromElement(lblActClientName));
        Assert.AreEqual(ClientName.ToString(), driver.GetTextFromElement(lblActClientName).ToString(), "Reservation Check page is not verified");
        Log("Reservation Page contents are verified");
    }

    public void SwitchToWindow()
    {
        string originalWindow = driver.CurrentWindowHandle;
        foreach (string window in driver.WindowHandles)
        {
            if (originalWindow != window)
            {
                driver.SwitchTo().Window(window);
                Log("Switch to another window");
                break;
            }
        }
    }
}