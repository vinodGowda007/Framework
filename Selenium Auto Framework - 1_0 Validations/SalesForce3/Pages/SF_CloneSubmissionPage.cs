using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace SalesForce3.Pages;
public class SF_CloneSubmissionPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;

    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

    //SF HOMEPAGE
    readonly By searchBox = By.XPath("//button[@type='button' and @aria-label='Search']");
    readonly By popUpedSearchBox = By.XPath("//input[@type='search' and @placeholder='Search...']");
    //SEARCH PAGE
    readonly By submissionName = By.XPath("//th//a[@data-refid='recordId' and @href]");
    //SUBMISSION PAGE
    readonly By buttonGroup = By.XPath("//div[contains(@class,'windowViewMode-normal')]//ul[@class='slds-button-group-list']");
    readonly By btnIceCheck = By.XPath("//*[@title='ICE Check']");
    readonly By btnLicenseCheck = By.XPath("//*[@title='License Check']/parent::li");
    readonly By btnMoreOptionDownArrow = By.XPath("//*[.='Show more actions']");
    readonly By btnCloneSubmission = By.XPath("//*[@title='Clone Submission']");
    readonly By btnRegularClone = By.XPath("//button[.='Regular Clone']");
    readonly By btnDismiss = By.XPath("(//button[@name='dismiss' and .='Dismiss'])[last()]");
    readonly By btnRefresh = By.XPath("(//button[@name='refresh' and .='Refresh'])[last()]");
    readonly By btnclose = By.XPath("//button[.='Close']");
    //SUBMISSION DETAILS
    readonly By btnSummaryTab = By.XPath("//a[.='SUMMARY']");
    readonly By btnEditname = By.XPath("(//button[@title='Edit Insured Name'])[last()]");
    readonly By txtInsured_Name = By.XPath("//*[.='Insured Name']/following-sibling::input[@type='text']");
    readonly By txtInsured_Street = By.XPath("//*[.='Insured Street']/following-sibling::input[@type='text']");
    readonly By txtInsured_City = By.XPath("//*[.='Insured Street']/following-sibling::input[@type='text']");
    readonly By txtInsured_State = By.XPath("//*[.='Insured State / Province']/following-sibling::input[@type='text']");
    readonly By txtInsured_Zip = By.XPath("//*[.='Insured Zip/Postal Code']/following-sibling::input[@type='text']");
    readonly By txtInsured_Country = By.XPath("//*[.='Insured Country']/following-sibling::input[@type='text']");
    readonly By txtPolicy_EffectiveDate = By.XPath("//*[.='Policy Effective Date']/following::div[2]/input");
    readonly By txtPolicy_ExpirationDate = By.XPath("//*[.='Policy Expiration Date']/following-sibling::div/input");
    readonly By txtPolicy_NumberCurrent = By.XPath("//span[.='Policy Number - Current']/parent::label/following-sibling::input");
    readonly By stage = By.XPath("//span/span[.='Stage']/following::a");
    readonly By DdWorking = By.XPath("//li/a[.='Working']");
    readonly By btnSave = By.XPath("(//button/span)[last()]");
    readonly By btnCoverageTab = By.XPath("//li[@title='Coverages']");
    readonly By btnAddCoverages = By.XPath("//input[@value='Add Coverage(s)']");
    readonly By coverages = By.XPath("//table[ contains(@id,'submissionCoveragePage') and @class='list']//td/span");
    readonly By cbCoverage = By.XPath("//table[ contains(@id,'submissionCoveragePage') and @class='list']//td/span//ancestor::tr/td/input");
    readonly By frameCoverage = By.XPath("//iframe");
    readonly By SubmissionNumber = By.XPath("(//p[@title='Starr Submission Number']/following-sibling::p/slot/lightning-formatted-text)[last()]");

    public SF_CloneSubmissionPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
    }

    //SENDING SUBMISSION NUMBER IN SEARCH BOX IN SF HOMEPAGE
    public void EnterSubmissionNumberInSearchBox(string submissionNumber)
    {
        driver.ElementDisplayed(searchBox);
        driver.WaitAndClick(searchBox);
        driver.ElementDisplayed(popUpedSearchBox);
        driver.ClearAndSend(popUpedSearchBox, submissionNumber + Keys.Enter);
    }

    //CLICK ON SUBMISISON NAME IN SEARCH RESULTS
    public void ClickOnSubmissionNameOnSearchedResult()
    {
        driver.WaitAndClick(submissionName);
        driver.WaitForNextPage();
    }

    //CLONE THE SUBMISSION
    public void CloneTheSubmission()
    {
        if (!driver.ElementDisplayed(btnCloneSubmission))
            driver.WaitAndClick(btnMoreOptionDownArrow);
        driver.MoveToTheElementAndClick(driver.FindElement(buttonGroup).FindElement(btnCloneSubmission));
        Assert.IsTrue(driver.WaitAndClick(btnRegularClone), "Regular Clone Was NOT Clicked");
        driver.WaitForResponse();
        Thread.Sleep(5000);
        driver.Refresh();
        driver.WaitForNextPage();
        driver.WaitForNextPage();
    }

    //PERFORM ICE CHECK
    public void ICE_CheckForClonedSubmission()
    {
        Assert.IsTrue(driver.WaitAndClick(driver.FindElement(buttonGroup).FindElement(btnIceCheck)), "ICE Check Button Was NOT Clicked");
        driver.MoveToTheElementAndClick(btnDismiss);//will dismiss the first popup
        //WAITING FOR REFRESH BUTTON TO APPEAR, AFTER THAT CLICKING THE DISMISS BUTTON
        DefaultWait<IWebDriver> wait = new(driver);
        wait.Timeout = TimeSpan.FromSeconds(30);
        wait.PollingInterval = TimeSpan.FromMilliseconds(100);
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
        wait.Until(x => x.FindElement(btnRefresh).Displayed == true);

        driver.WaitAndClick(btnDismiss);//NEED TO CLICK TILL DISAPPEAR
        Console.WriteLine();
    }

    //PERFORM LICENSE CHECK
    public void LicenseCheckForClonedSubmission()
    {
        driver.WaitTillElementIsClickable(buttonGroup);
        Assert.IsTrue(driver.WaitAndClick(driver.FindElement(buttonGroup).FindElement(btnLicenseCheck)), "License Check Button Was NOT Clicked");
        Assert.IsTrue(driver.WaitAndClick(btnclose), "CloseButton In Licence Check Pop-Up Was Not Clicked");
    }

    //ENTER SUBMISSION DETAILS
    public void FillDetailsInClonedSubmission(string[] data)
    {
        string name = data[0];
        string street = data[1];
        string city = data[2];
        string state = data[3];
        string zip = data[4];
        string country = data[5];
        string effDate = data[6];
        string expDate = data[7];
        driver.WaitForNextPage();
        driver.WaitForNextPage();
        driver.WaitTillElementIsClickable(btnEditname);
        driver.MoveToTheElementAndClick(btnEditname);
        if (name != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtInsured_Name, name), "Insure Name Was NOT Inputed");
        driver.MoveToTheElementAndClick(stage);
        driver.MoveToTheElementAndClick(DdWorking);
        if (street != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtInsured_Street, street), "Insured Street Was NOT Inputed");
        if (city != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtInsured_City, city), "Insured City Was NOT Inputed");
        if (state != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtInsured_State, state), "Insured StateWas NOT Inputed");
        if (zip != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtInsured_Zip, zip), "Insured ZipWas NOT Inputed");
        if (country != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtInsured_Country, country), "Insured CountryWas NOT Inputed");
        driver.ClearAndSend(txtPolicy_NumberCurrent, "");
        if (effDate != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtPolicy_EffectiveDate, effDate), "Effective Date Was NOT Inputed");
        else
            Assert.IsTrue(driver.ClearAndSend(txtPolicy_EffectiveDate, DateTime.Now.ToString("d")), "Effective Date Was NOT Inputed");
        if (expDate != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtPolicy_ExpirationDate, expDate), "Expiration Date Was NOT Inputed");
        else
            Assert.IsTrue(driver.ClearAndSend(txtPolicy_ExpirationDate, DateTime.Now.AddYears(1).ToString("d")), "Expiration Date Was NOT Inputed");
    }

    //CLICK SAVE BUTTON
    public void ClickSaveButon()
    {
        if (!driver.IsDisplayed(btnSave))
        {
            driver.ScrollToCenter(btnSummaryTab);
            driver.MoveToTheElementAndClick(btnSummaryTab);
        }
        //driver.ScrollDown(BtnSummaryTab);
        // driver.WaitForResponse();
        driver.MoveToTheElementAndClick(btnSave);

        driver.WaitTillOverlayDisappears(By.XPath("//*[@class='invisibleClass']"));

    }

    //string submission_Number;
    //PRINT SUBMISSION NUMBER
    public void PrintSubmissionNumber()
    {
        _scenarioContext.Add("SubmissionNumber", driver.GetTextFromElement(SubmissionNumber));
        Console.WriteLine(_scenarioContext["SubmissionNumber"].ToString());
    }

    //SELECT COVERAGES
    public void SelectCoverages(string[] coverages)
    {
        driver.MoveToTheElementAndClick(btnCoverageTab);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        driver.SwitchToFrame(driver.FindElement(frameCoverage));
        driver.ScrollToCenter(btnAddCoverages);
        driver.MoveToTheElementAndClick(btnAddCoverages);
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(15));
        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(this.coverages));
        var CoverageList = driver.ListOfElements(this.coverages);
        for (int i = 0; i < coverages.Length; i++)
        {
            for (int j = 0; j < CoverageList.Count; j++)
            {
                Console.WriteLine(CoverageList[j].Text);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center' , inline: 'nearest'});", (driver.FindElements(cbCoverage)[j]));
                if (CoverageList[j].Text.ToLower() == coverages[i].Trim().ToLower())
                {
                    //coverage.FindElement(CbCoverage).Click();
                    driver.MoveToTheElementAndClick(driver.FindElements(cbCoverage)[j]);
                    //driver.FindElements(CbCoverage)[j].Click();
                }
            }
        }
    }

}
