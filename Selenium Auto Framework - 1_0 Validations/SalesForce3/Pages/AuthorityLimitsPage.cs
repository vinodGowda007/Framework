using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace SalesForce3.Pages;
public class AuthorityLimitsPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    public static string ScenarioCount, PolicyLimitId, UpdateSubmissionCurrency;
    public static decimal StarLimitUSDValue;
    public static string AuthorityLimitFilepath = SubmissionPage.BaseURL + "Authority_Limit/";

    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);


    //SF HOMEPAGE
    readonly By lblActualAppName = By.XPath("//div[@class='slds-context-bar__primary navLeft']/div/span/span");
    readonly By btnAppLauncher = By.XPath("//div[@class='slds-r5']");
    readonly By txtSearchAppandItem = By.XPath("//input[contains(@placeholder,'Search apps and items')]");
    readonly By optionAssumedInsurance = By.XPath("(//lightning-formatted-rich-text/span/p/b)[1]");

    // Logout
    readonly By imgProfile = By.XPath("//button//div/span[@class='uiImage']");
    readonly By lnkLogoutFromProfile = By.XPath("//a[contains(text(),'Log Out')]");
    readonly By lnkLogOut = By.XPath("//div/a[contains(text(),'Log out as')]");
    readonly By txtUserName = By.XPath("//input[@name='username']");
    readonly By txtPolicyLimitID = By.XPath("//input[@name='Name']");

    readonly By ddLimitType = By.XPath("//label[contains(text(),'Limit Type')]");
    readonly By ddLimitTypeValue = By.XPath("(//label[contains(text(),'Limit Type')]/following::div)[1]//lightning-base-combobox-item/span[2]/span");

    readonly By txtAuthorityLimitAmount = By.XPath("//input[@name='Authority_Limit_Amount__c']");

    readonly By ddAssignedUnderwriter = By.XPath("(//label[contains(text(),'Assigned Underwriter')]/following::div//input)[1]");
    readonly By ddAssignedUnderwriterValue = By.XPath("(//label[contains(text(),'Assigned Underwriter')]/following::div//ul/li//span[2]/span/lightning-base-combobox-formatted-text)[1]");
    readonly By slctAssignedUnderwriter = By.XPath("(//label[contains(text(),'Assigned Underwriter')]/following::div//ul/li//lightning-base-combobox-formatted-text)[1]");

    readonly By txtPremiumLimit = By.XPath("//input[@name='Premium_Limit__c']");

    readonly By ddApprover = By.XPath("(//label[contains(text(),'Approver')]/following::div//input)[1]");
    readonly By slctApproverValue = By.XPath("(//label[contains(text(),'Approver')]/following::div//ul/li//lightning-base-combobox-formatted-text)[1]");
    readonly By ddApproverValue = By.XPath("(//label[contains(text(),'Approver')]/following::div//ul/li//span[2]/span/lightning-base-combobox-formatted-text)[1]");

    readonly By AMEAuthorityLimit = By.XPath("//input[@name='AME_Authority_Limit__c']");
    readonly By txtProductProfitCenter = By.XPath("name='Product_Profit_Center__c'");

    readonly By ddBusinessUnit = By.XPath("(//label[contains(text(),'Business Unit')]/following::div//button)[1]");
    readonly By ddBusinessUnitValue = By.XPath("(//label[contains(text(),'Business Unit')]/following::div)[1]//lightning-base-combobox-item/span[2]/span");

    readonly By ddProfitCenter = By.XPath("((//label[contains(text(),'Profit Center')])[2]/following::div//button)[1]");
    readonly By ddProfitCenterValue = By.XPath("((//label[contains(text(),'Profit Center')])[2]/following::div)[1]//lightning-base-combobox-item/span[2]/span");

    readonly By txtAccidentalDeathAuthorityLimit = By.XPath("//input[@name='Accidental_Death_Authority_Limit__c']");

    readonly By ddCurrency = By.XPath("(//label[contains(text(),'Currency')]/following::div//button)[1]");
    readonly By ddCurrencyValue = By.XPath("(//label[contains(text(),'Currency')]/following::div)[1]//lightning-base-combobox-item/span[2]/span");

    readonly By ddDivision = By.XPath("(//label[contains(text(),'Division')]/following::div//button)[1]");
    readonly By ddDivisionValue = By.XPath("(//label[contains(text(),'Division')]/following::div)[1]//lightning-base-combobox-item/span[2]/span");

    readonly By btnNewAuthorityLimit = By.XPath("//li/a[@title='New']");
    readonly By btnSave = By.XPath("//button[@name='SaveEdit']");
    readonly By lblCreatedAuthorityLimit = By.XPath("//img[@title='Authority Limit']/following::div/h1//slot/lightning-formatted-text");

    readonly By btnEditSubmission = By.XPath("//button[@name='Opportunity.Edit']");
    readonly By btnEditAuthorityLimit = By.XPath("//button[@name='Edit']");
    readonly By tabSubmission1 = By.XPath("//li/a[@id='SubmissionDetailsTab__item']");
    readonly By ddSubmissionCurrency = By.XPath("//button[@name='CurrencyIsoCode']");
    readonly By ddSubmissionCurrencyCurrentValue = By.XPath("//button[@name='CurrencyIsoCode']/span");
    readonly By ddSubmissionCurrencyValue = By.XPath("//button[@name='CurrencyIsoCode']/following::div[2]//span[2]/span");

    readonly By tabCurrency = By.XPath("//a[contains(text(),'Currency')]");
    readonly By lblStarLimitUSDValue = By.XPath("(//span[contains(text(),\"Starr Limit USD (Lloyd's)\")]/following::div)[1]/span/span");
    readonly By btnSaveAuthorityLimit = By.XPath("//button[@name='SaveEdit']");

    readonly By btnDeleteAuthorityLimit = By.XPath("//button[@name='Delete']");
    readonly By btnConfirmDeleteAuthrotiLimit = By.XPath("//span[contains(text(),'Delete')]");

    public AuthorityLimitsPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
    }

    public static string GenerateRandomNumbers()
    {
        Random random = new Random();
        return random.Next(999999).ToString();
    }

    public void NewAuthorityLimitCreationPage()
    {
        driver.WaitForElementToPresent(btnNewAuthorityLimit);
        driver.WaitAndClick(btnNewAuthorityLimit);
        Assert.IsTrue(driver.WaitForElementToPresent(txtPolicyLimitID), "COULD NOT NAVIGATE TO AUTHORITY LIMITE CREATION PAGE");
        Log("NAVIGATE TO AUTHORITY LIMIT CREATION PAGE");
    }

    public void SaveAuthorityLimiteRecord()
    {
        driver.WaitAndClick(btnSave);
        Log("CLICKED ON SAVE BUTTON");
    }

    //public void FillValuesInAuthorityLimit(string[] Data)
    //{
    //    PolicyLimitId = Data[0] + GenerateRandomNumbers();
    //    string LimitType = Data[1];
    //    string AuthorityLimitAmount = Data[2];
    //    ScenarioCount = Data[3];
    //    //string AssignedUnderwriter = Data[3];
    //    //string Approver = Data[4];

    //    driver.ClearAndSend(txtPolicyLimitID, PolicyLimitId);
    //    SendDropdownValue(ddLimitType, ddLimitTypeValue, LimitType, " Limit Type ");
    //    SelectLookUpValue(ddAssignedUnderwriter, SubmissionPage.AssignedUnderwriter, slctAssignedUnderwriter, "Assigned Underwriter");
    //    System.Threading.Thread.Sleep(2000);
    //    SelectLookUpValue(ddApprover, SubmissionPage.AssignedUnderwriter, slctApproverValue, "Approver");
    //    SendTextToField(txtAuthorityLimitAmount, AuthorityLimitAmount, "Authority Limit Amount");
    //    SendDropdownValue(ddCurrency, ddCurrencyValue, SubmissionPage.SubmissionCurrency, "Currency");
    //    SendDropdownValue(ddDivision, ddDivisionValue, SubmissionPage.RecordType, "Division");

    //}
    public void SendDropdownValue(By ddFieldWebElement, By Listelement, string inputValue, string FieldName)
    {
        driver.ScrollToCenter(ddFieldWebElement);
        driver.WaitAndClick(ddFieldWebElement);
        Assert.IsTrue(driver.SelectFromList(Listelement, inputValue), FieldName + " FIELD WAS NOT INPUTED");
        Log(FieldName + " FIELD VALUE IS INPUTED");
    }

    public void SendTextToField(By txtFieldWebElement, string txtFieldValue, string txtFieldName)
    {
        driver.ScrollToCenter(txtFieldWebElement);
        Assert.IsTrue(driver.ClearAndSend(txtFieldWebElement, txtFieldValue), txtFieldName + " FIELD WAS NOT INPUTED");
        Log(txtFieldName + " FIELD VALUE IS INPUTED");
    }

    public void SelectLookUpValue(By dropdownField, string inputValue, By dropdownFieldValue, string FieldName)
    {
        driver.ScrollToCenter(dropdownField);
        driver.WaitAndClick(dropdownField);
        driver.ClearAndSend(dropdownField, inputValue);
        driver.WaitForElementToPresent(dropdownFieldValue);
        Assert.IsTrue(driver.WaitAndClick(dropdownFieldValue), FieldName + "  WAS NOT INPUTED");
        Log(FieldName + "FIELD VALUE IS INPUTED");
    }

    public void VerifyCreatedAuthorityLimitRecord()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(lblCreatedAuthorityLimit), "AUTHORITY LIMIT RECORD IS NOT CREATED");
        Assert.IsTrue(driver.GetTextFromElement(lblCreatedAuthorityLimit).Equals(PolicyLimitId), "COULD NOT SAVE THE RECORD");
        string AuthorityLimitURL = "Authority Limit = " + driver.Url;
        File.WriteAllText(AuthorityLimitFilepath + ScenarioCount + ".txt", AuthorityLimitURL);
        Log("SUCCESSFULLY CREATED AUTHORITY LIMIT RECORD");
    }

    public void ClickOnEditSubmissionButton()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(btnEditSubmission), " Edit button is not visible");
        Assert.IsTrue(driver.WaitAndClick(btnEditSubmission), "Could not clicked on Edit button");
        Log("Clicked on Edit submission button");
        Assert.True(driver.WaitForElementToPresent(tabSubmission1), "Could not navigate to Submission Edit Page");
    }

    public void updateSubmissionForAuthorityLimitCheck(string[] AuthorityData)
    {
        UpdateSubmissionCurrency = AuthorityData[0];
        SendDropdownValue(ddSubmissionCurrency, ddSubmissionCurrencyValue, UpdateSubmissionCurrency, " Submission Currency ");
    }

    public void VerifyStarLimitUSDLoydsvalue()
    {
        driver.WaitForElementToPresent(tabCurrency);
        driver.ScrollToCenter(tabCurrency);
        driver.WaitAndClick(tabCurrency);
        driver.WaitForElementToPresent(lblStarLimitUSDValue);
        driver.ScrollToCenter(lblStarLimitUSDValue);
        StarLimitUSDValue = decimal.Parse(driver.GetTextFromElement(lblStarLimitUSDValue));
        Log("Star Limit USD (Loyd's) value is --> " + StarLimitUSDValue);
    }

    public void SaveAuthorityLimit()
    {
        driver.ScrollToCenter(btnSaveAuthorityLimit);
        driver.WaitAndClick(btnSaveAuthorityLimit);
        driver.WaitTillOverlayDisappears(btnSaveAuthorityLimit);
        Log("Authority Limit updated successfully");
    }


    public void UpdateAuthorityLimit()
    {
        decimal value = StarLimitUSDValue - (StarLimitUSDValue / 2);
        Console.WriteLine("Star Limit USD value after calculating before inputting -----------------> " + value);
        driver.WaitForElementToPresent(btnEditAuthorityLimit);
        driver.WaitAndClick(btnEditAuthorityLimit);
        driver.WaitForElementToPresent(txtPolicyLimitID);

        SendTextToField(txtAuthorityLimitAmount, value.ToString(), "Authority Limit Amount");
        SendDropdownValue(ddCurrency, ddCurrencyValue, UpdateSubmissionCurrency, "Currency");
        Log("Updated Authority Check fields");
    }

    public void NavigateToAuthorityLimitRecord(string ScenarioCount)
    {
        string AuthorityLimitUrl = SubmissionPage.ReadDataFromFile(AuthorityLimitFilepath + ScenarioCount + ".txt");
        driver.Navigate().GoToUrl(AuthorityLimitUrl);
        driver.WaitForNextPage();
    }

    public void DeleteAuthorityLimitRecord()
    {
        driver.WaitForElementToPresent(btnDeleteAuthorityLimit);
        driver.WaitAndClick(btnDeleteAuthorityLimit);
        driver.WaitTillElementIsClickable(btnConfirmDeleteAuthrotiLimit);
        driver.WaitAndClick(btnConfirmDeleteAuthrotiLimit);
        Assert.IsTrue(driver.WaitForElementToPresent(btnNewAuthorityLimit), "Could not delete the authority limit record");
        Log("Authority Limit is deleted successfully");
    }
}
