using AngleSharp.Dom;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Config;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace SalesForce3.Pages;
public class AssumedInsurerPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    private string InsurerName;
    private string ScenarioCount;
    public static string AssumedInsurerPropertyFilePath = SubmissionPage.BaseURL + "Assumed_Insurer/AssumedInsurer.txt";


    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);


    //SF HOMEPAGE
    readonly By lblActualAppName = By.XPath("//div[@class='slds-context-bar__primary navLeft']/div/span/span");
    readonly By btnAppLauncher = By.XPath("//div[@class='slds-r5']");
    readonly By txtSearchAppandItem = By.XPath("//input[contains(@placeholder,'Search apps and items')]");
    readonly By optionAssumedInsurance = By.XPath("(//lightning-formatted-rich-text/span/p/b)[1]");
    readonly By btnNewProject = By.XPath("//a/div[@title='New']");
    readonly By txtSearchSetup = By.XPath("//input[@placeholder='Search Setup']");
    readonly By opnSelectRole = By.XPath("(//li[@class='slds-listbox__item']/div/span/span)[1]");
    readonly By btnMoveToChosen = By.XPath("(//button[@title='Move selection to Chosen'])[1]");
    readonly By opnChoosenRole = By.XPath("(//ul[starts-with(@id,'selected-list')])[1]");
    readonly By errorMsgBox = By.XPath("//div/h2[contains(@title,'We hit a snag')]");
    readonly By linkErrorMsg = By.XPath("//div[@class='pageLevelErrors']/ul/li | (//div[@class='panel-content scrollable']//ul/li/a)[1]");
    readonly By btnCloseErrorDialog = By.XPath("//button[starts-with(@title,'Close error dialog')]");
    readonly By txtInsurerName = By.XPath("//input[@name='Name']");
    readonly By cbActive = By.XPath("(//input[@name='Active__c'])[1]");
    readonly By lblInsurerName = By.XPath("//div/h1/slot/lightning-formatted-text");
    readonly By btnSave = By.XPath("//button[@name='SaveEdit']");
    readonly By btnNew = By.XPath("//a/div[@title='New']");
    readonly By opnSelectRegion = By.XPath("//div[contains(text(),'Applicable Regions')]/following::div[1]//ul/li//span/span");
    readonly By btnMoveRegionToChoosen = By.XPath("(//button[@title='Move selection to Chosen'])[2]");
    readonly By opnChoosenRegion = By.XPath("(//ul[starts-with(@id,'selected-list')])[2]");
    readonly By opnSelectCarrierBranch = By.XPath("(//div/div[contains(text(),'Applicable Regions')]/following::div/div/div[3]//ul/li)[6]");
    readonly By btnMoveCarrierToChoosen = By.XPath("(//button[@title='Move selection to Chosen'])[3]");
    readonly By opnChoosenCarrier = By.XPath("(//ul[starts-with(@id,'selected-list')])[3]");
    //readonly By btnSaveAssumedInsurer = By.XPath("(//input[@value='Save'])[1]");
    readonly By btnSaveAssumedInsurer = By.XPath("//lightning-button/button[contains(text(),'Save')]");


    //readonly By lblAssumedInsurerCourrier = By.XPath("//div[@class='Popuppage']//label");
    readonly By lblAssumedInsurerCourrier = By.XPath("//select[@class='slds-select']");
    readonly By slctValueAssumedRegion = By.XPath("//select[@class='slds-select']");

    readonly By saveAssumedInsurerCourrier = By.XPath("(//button[contains(text(),'Save')])[2]");

    readonly By lblAssumedInsurerPresent = By.XPath("//span[text()='Assumed Insurer']/following::div[1]/span/span");
    readonly By btnEditAssumedInsurer = By.XPath("//button[@title='Edit Assumed Insurer']");
    readonly By txtRCCCode = By.XPath("//input[@name='RCC_Code__c']");
    readonly By txtEffectiveDate = By.XPath("//input[@name='Effective_Date__c']");

    // Logout
    readonly By imgProfile = By.XPath("//button//div/span[@class='uiImage']");
    readonly By lnkLogoutFromProfile = By.XPath("//a[contains(text(),'Log Out')]");
    readonly By lnkLogOut = By.XPath("//div/a[contains(text(),'Log out as')]");
    readonly By txtUserName = By.XPath("//input[@name='username']");

    // Submission View objects Tabs
    readonly By tabListAllTabs = By.XPath("//ul[@class='slds-tabs_default__nav']/li[@class='slds-tabs_default__item']/a");
    readonly By tabMoreDetails = By.XPath("(//button[contains(text(),'More')])[1]");


    readonly By tabMoreDetails2_0 = By.XPath("((//lightning-tabset[@class='flexipage-tabset'])[1]/div//ul/li)[6]");
    readonly By tabListAllTabsIn2_0InViewMode = By.XPath("(//lightning-tabset[@class='flexipage-tabset'])[1]/div//ul/li");

    readonly By tabActionValues = By.XPath("//div[@class='slds-dropdown slds-dropdown_right']//lightning-menu-item/a/span | //slot/lightning-menu-item/a/span");
    readonly By lnkClickHere = By.XPath("//span[contains(text(),'Choose Assumed Insurer')]/following::div[2]//a");
    readonly By pageAssumedInsurers = By.XPath("//tr/th/div[contains(text(),'Assumed Insurer')]");
    //readonly By cbAssumedInsurersValue = By.XPath("//tbody[@id='j_id0:pageForm:pageblock:pb1:tb']/tr/td[4]/input");
    readonly By cbAssumedInsurersValue = By.XPath("//lightning-card//tbody/tr/th[4]/div//span/input");
    //readonly By lblAssumedInsurersValue = By.XPath("//tbody[@id='j_id0:pageForm:pageblock:pb1:tb']/tr/td[1]/label"); // Need to remove once assumed insurer lwc component works
    readonly By lblAssumedInsurersValue = By.XPath("//lightning-card//tbody/tr/th[1]/div");
    readonly By frameAssumedInsurer = By.XPath("//iframe[@title='accessibility title']");
    //readonly By TabCreateSubmissionlist = By.XPath("//ul[@class='slds-tabs_scoped__nav']/li/a | //ul[@class='slds-tabs_default__nav']/li/a");


    readonly FeatureContext _featureContext;

    public AssumedInsurerPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
        _featureContext = featureContext;
    }

    public static string GenerateRandomNumbers()
    {
        Random random = new Random();
        return random.Next(9999).ToString();
    }
    public void NavigateToAssumedInsurer(string AppName)
    {
        //driver.WaitForElementToPresent(txtSearchSetup);
        System.Threading.Thread.Sleep(3000);
        //driver.WaitTillElementIsClickable(btnAppLauncher);
        driver.WaitAndClick(btnAppLauncher);
        driver.WaitForElementToPresent(txtSearchAppandItem);
        driver.ClearAndSend(txtSearchAppandItem, AppName);
        driver.WaitTillElementIsClickable(optionAssumedInsurance);
        driver.JSClick(optionAssumedInsurance);
        System.Threading.Thread.Sleep(2000);
        Log("NAVIGATED TO " + AppName + " OBJECT");
    }

    public void VerifyErrorMessages(string errorMessages)
    {
        driver.WaitForElement(errorMsgBox);
        Assert.IsTrue(driver.WaitForElementToPresent(errorMsgBox), "Error message box is not displayed");
        Log("Error message box is displayed");
        System.Threading.Thread.Sleep(1000);
        Assert.AreEqual(driver.GetTextFromElement(linkErrorMsg).ToString(), errorMessages.ToString(), errorMessages + " Error message is not displayed");
        driver.WaitAndClick(btnCloseErrorDialog);
        Log(errorMessages + "  Error message is displayed");
    }

    public void LogoutFromUser()
    {
        System.Threading.Thread.Sleep(2000);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        if(driver.IsDisplayed(lnkLogOut))
        {
            Assert.IsTrue(driver.WaitAndClick(lnkLogOut), "COULD NOT CLICK ON THE LOGOUT BUTTON");
            Log("CLICKED ON LOGOUT BUTTON");
            wait.Until(ExpectedConditions.ElementToBeClickable(btnAppLauncher));
            driver.WaitTillElementIsClickable(btnAppLauncher);
        }
        
    }

    public void LogoutFromApplication()
    {
        driver.WaitForElementToPresent(imgProfile);
        driver.WaitAndClick(imgProfile);
        driver.WaitAndClick(lnkLogoutFromProfile);
        driver.WaitForElementToPresent(txtUserName);
        Log("Successfully logged out from the applicaiton");
    }

    public void InputInsurerName(string Name)
    {
        Assert.IsTrue(driver.ClearAndSend(txtInsurerName, Name), "Insurer Name Was NOT Inputed");
        Log("Insurer Name was inputed");
    }

    public void FillValuesInAssumedInsurerPage(string[] data)
    {
        ScenarioCount = data[0];
        InsurerName = data[1] + GenerateRandomNumbers();

        InputInsurerName(InsurerName);
        SelectActiveCheckbox();
        driver.ClearAndSend(txtEffectiveDate, "1/1/1990");
        SelectRegion();
        driver.SendKeysOrText(txtRCCCode, GenerateRandomNumbers());
        SelectCarrier();
        Log("Details are added in Assumed Insurer creation Page");
    }
    public void SelectActiveCheckbox()
    {
        driver.ScrollToCenter(cbActive);
        Assert.IsTrue(driver.CheckTheBox(cbActive), "Could not select the Checkbox");
        Log("Active Checkbox is checked");
    }

    public void SelectRoles()
    {
        driver.ScrollToCenter(opnSelectRole);
        driver.WaitAndClick(opnSelectRole);
        driver.WaitAndClick(btnMoveToChosen);
        Assert.IsTrue(driver.IsDisplayed(opnChoosenRole), " COULD NOT SELECT THE ROLES  ");
        Log("ROLES ARE SELECTED");
    }

    public void SelectRegion()
    {
        driver.ScrollToCenter(opnSelectRegion);

        foreach (IWebElement child in driver.ListOfElements(opnSelectRegion))
        {
            child.Click();
            driver.WaitAndClick(btnMoveRegionToChoosen);
        }
        driver.WaitAndClick(btnMoveRegionToChoosen);
        Assert.IsTrue(driver.IsDisplayed(opnChoosenRegion), " COULD NOT SELECT THE REGION ");
        Log("REGION IS SELECTED");
    }
    public void SelectCarrier()
    {
        driver.ScrollToCenter(opnSelectCarrierBranch);
        driver.WaitAndClick(opnSelectCarrierBranch);
        driver.WaitAndClick(btnMoveCarrierToChoosen);
        Assert.IsTrue(driver.IsDisplayed(opnChoosenCarrier), " COUL DNOT SELECT THE CARRIERS  ");
        Log("CARRIERS ARE SELECTED");
    }

    public void ClickOnSaveButton()
    {
        driver.WaitAndClick(btnSave);
        Log("CLICKED ON SAVE BUTTON");
    }
    public void VerifyCreatedInsurerRecord()
    {
        //driver.Refresh();
        driver.WaitForElementToPresent(lblInsurerName);
        Assert.AreEqual(driver.GetTextFromElement(lblInsurerName).ToString(), InsurerName.ToString(), "Could not create the Assumed Insurer record");
        Log("Assumed Insurer record is created successfully");
        File.WriteAllText(AssumedInsurerPropertyFilePath, "Assumed_Insurer =" + InsurerName);
    }

    public void ClickOnNewButton()
    {
        driver.WaitAndClick(btnNew);
        driver.WaitForNextPage();
        Assert.IsTrue(driver.WaitForElementToPresent(txtInsurerName), "New Insurer Creation page is not displayed");
        Log("Insurer CREATION PAGE IS DISPLAYED");
    }

    public void NavigateToPolicyInfoTabInSubmission()
    {
        driver.WaitForElementToPresent(tabMoreDetails);
        driver.ScrollToCenter(tabMoreDetails);
        driver.WaitAndClick(tabMoreDetails);
        Assert.IsTrue(driver.SelectFromList(tabActionValues, "Policy Info"), "Could not click on Policy Info tab");
        Assert.IsTrue(driver.WaitForElementToPresent(lnkClickHere), "Could not navigate to Policy Info tab");
        Log("Clicked on Policy Info tab");
    }

    public void ThenUserNavigatedToTab(string Tabname)
    {
        System.Threading.Thread.Sleep(4000);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(tabListAllTabs));
        try
        {
            if (driver.SelectFromList(tabListAllTabs, Tabname) == false)
            {
                if (driver.IsDisplayed(tabMoreDetails))
                {
                    //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(tabMoreDetails));
                    driver.ScrollToCenter(tabMoreDetails);
                    driver.JSClick(tabMoreDetails);
                }
                //if (version.Equals("2"))
                else if (driver.IsDisplayed(tabMoreDetails2_0))
                {
                    //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(tabMoreDetails2_0));
                    driver.ScrollToCenter(tabMoreDetails2_0);
                    driver.JSClick(tabMoreDetails2_0);
                }
                if (driver.SelectFromList(tabActionValues, Tabname) == false)
                {
                    Console.WriteLine(Tabname + " IS NOT AVAILABLE IN THE LIST");
                    Log(Tabname + " IS NOT AVAILABLE IN THE LIST");
                    Assert.Fail(Tabname + " IS NOT AVAILABLE IN THE LIST");
                }
                else
                {
                    driver.WaitForNextPage();
                    Log("CLICKED ON THE TAB " + Tabname);
                }
            }
            else
            {
                driver.WaitForNextPage();
                Log("CLICKED ON THE TAB " + Tabname);
            }
        }
        catch (Exception e)
        {
            Log("COULD NOT CLICK ON THE TAB " + Tabname);
            Console.WriteLine("COULD NOT CLICK ON THE TAB " + Tabname);
        }


    }

    public void NavigateToAddAssumedInsurerPage()
    {
        if (!_featureContext["RecordType_" + loggingStep.rowNo].ToString().Contains("Starr Casualty International London"))
        {
            System.Threading.Thread.Sleep(2000);
            driver.ScrollToCenter(lnkClickHere);
            driver.MoveToTheElementAndClick(lnkClickHere);
            Log("Clicked on Click Here link");
            System.Threading.Thread.Sleep(3000);
            //try
            //{
            //    driver.SwitchToFrame(0);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("COULD NOT FIND THE FRAME TO ADD ASSUMED INSURER");
            //    Log("COULD NOT FIND THE FRAME TO ADD ASSUMED INSURER");
            //    Assert.Fail("COULD NOT FIND THE FRAME TO ADD ASSUMED INSURER");
            //}
            Assert.IsTrue(driver.WaitForElementToPresent(pageAssumedInsurers), "Could not navigate to add assumed Insurer page");
            Log("Navigate to Add Assumed Insurer page");
            _featureContext["Assumed_Require" + loggingStep.rowNo] = "Yes";
        }
        else
        {
            Console.WriteLine("ASSUMED INSURER IS NOT REQUIRED FOR THIS RECORD TYPE " + _featureContext["RecordType_" + loggingStep.rowNo].ToString());
            Log("ASSUMED INSURER IS NOT REQUIRED FOR THIS RECORD TYPE " + _featureContext["RecordType_" + loggingStep.rowNo].ToString());
            _featureContext["Assumed_Require" + loggingStep.rowNo] = "No";
        }
    }

    public void SelectAssumedInsurers()
    {
        bool flag = false;
        IList<IWebElement> ListOfAssumedInsurer = driver.ListOfElements(lblAssumedInsurersValue);
        IList<IWebElement> ListOfCBAssumedInsurer = driver.ListOfElements(cbAssumedInsurersValue);
        InsurerName = SubmissionPage.ReadDataFromFile(AssumedInsurerPropertyFilePath);

        if (driver.IsDisplayed(lblAssumedInsurersValue))
        {
            for (int i = 0; i < ListOfAssumedInsurer.Count; i++)
            {
                if (ListOfAssumedInsurer[i].GetElementText().ToString() == InsurerName.ToString())
                {
                    driver.MoveToTheElement(ListOfAssumedInsurer[i]);
                    if (ListOfCBAssumedInsurer[i].Selected == false)
                    {
                        try
                        {
                            driver.MoveToTheElement(ListOfCBAssumedInsurer[i]);
                            driver.JSClick(ListOfCBAssumedInsurer[i]);
                            System.Threading.Thread.Sleep(2000);

                            if (ListOfCBAssumedInsurer[i].Selected == true)
                            {
                                Log("ASSUMED INSURER IS SELECTED");
                                driver.CaptureScreen(_scenarioContext);
                                flag = true;
                            }
                            else
                            {
                                Log("COULD NOT SELECTED ASSUMED INSURER ");
                                flag = false;
                            }
                            break;
                        }
                        catch (Exception e)
                        {
                            Log("COULD  NOT FIND THE ASSUMED INSURER IN THE LIST");
                            Assert.Fail("COULD  NOT FIND THE ASSUMED INSURER IN THE LIST");
                        }
                    }
                    else if (ListOfCBAssumedInsurer[i].Selected == true)
                    {
                        Log("ASSUMED INSURER IS ALREADY SELECTED IN THE LIST");
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
        }
        else
        {
            Assert.Fail("ASSUMED INSURERS ARE NOT DISPLAYED");
        }

        if (flag == false)
        {
            Log("EXPECTED ASSUMED INSURERS ARE NOT LISTED / DISPLAYED IN THE SCREEN");
            driver.CaptureScreen(_scenarioContext);
            Assert.Fail("EXPECTED ASSUMED INSURERS ARE NOT LISTED / DISPLAYED IN THE SCREEN");
        }
    }



    public void SaveAssuredInsurer()
    {
        driver.MoveToTheElement(btnSaveAssumedInsurer);
        Assert.IsTrue(driver.WaitAndClick(btnSaveAssumedInsurer), "Could not click on Save button");
        Log("Clicked on Save button in Add Assumed Insurer page");
    }

    //public void LogoutFromApplication()
    //{
    //    driver.WaitAndClick(btnLogoutImage);
    //    driver.WaitAndClick(lnkLogOut);
    //    Assert.IsTrue(driver.WaitForElementToPresent(txtUserName),"Could not Logged Out from the application");
    //    Log("Logged out from the Application");
    //    System.Threading.Thread.Sleep(2000);
    //}

    public void PageRefresh()
    {
        System.Threading.Thread.Sleep(2000);
        driver.Refresh();
        System.Threading.Thread.Sleep(3000);
        Log("Refresh the page");
    }

    public void AddCourrier()
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(slctValueAssumedRegion));
            driver.SelectValueFromDropDownListUsingIndex(slctValueAssumedRegion, 1);
            driver.CaptureScreen(_scenarioContext);
            System.Threading.Thread.Sleep(2000);
            driver.WaitAndClick(saveAssumedInsurerCourrier);
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(btnSaveAssumedInsurer));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(tabMoreDetails));
            System.Threading.Thread.Sleep(3000);
            Log("Added Assumed Insurer");
        }
        catch (Exception e)
        {
            Log("COULD NOT FIND THE ASSUMED INSURER COURRIER DROPDOWN");
            Assert.Fail("COULD NOT FIND THE ASSUMED INSURER COURRIER DROPDOWN");
        }
    }

    public void VerifyAddedAssumedInsurer()
    {
        System.Threading.Thread.Sleep(2000);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblAssumedInsurerPresent));
            driver.ScrollByPixel(120);
            //driver.WaitForElementToPresent(lblAssumedInsurerPresent);
            Assert.AreEqual(driver.GetTextFromElement(lblAssumedInsurerPresent).ToString(), InsurerName.ToString(), "Assumed insurer are not added");
            driver.CaptureScreen(_scenarioContext);
            Log("Assumed Insurer Added Successfully");
        }
        catch (Exception e)
        {
            Log("COULD NOT FIND THE ASSUMED INSURER FROM SUBMISSION");
            Assert.Fail("COULD NOT FIND THE ASSUMED INSURER FROM SUBMISSION");
            driver.CaptureScreen(_scenarioContext);
        }
    }
}
