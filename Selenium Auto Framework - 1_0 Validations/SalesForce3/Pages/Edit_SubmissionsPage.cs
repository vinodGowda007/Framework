using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Config;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;

namespace SalesForce3.Pages;
public class Edit_SubmissionsPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    public static string RecordInfopath = "C:/SF_Automation/SeleniumAutoFramework/SalesForce3/RecordInfo/NameFile.txt";

    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

    readonly By txtPercentPolicyBooked = By.XPath("//input[@name='Amount']");
    readonly By txtProducerCommission = By.XPath("//input[@name='Producer_Commision_Percent__c']");
    readonly By txtProducerCommission1 = By.XPath("//input[@name='Producer_Commision_Percent__c']");
    readonly By btnEdit = By.XPath("//button[@name='Opportunity.Edit']");
    readonly By tabSubmission = By.XPath("//li/a[@id='SubmissionDetailsTab__item']");
    readonly By tabSubmission1 = By.XPath("//li/a[@id='SubmissionDetailsTab__item']");

    readonly By tabDetails = By.XPath("//ul/li/a[@id='detailTab__item']");
    readonly By tabPremiumLimit = By.XPath("//li/a[@id='PremiumLimitsTab__item']");
    readonly By tabPremiumLimit1 = By.XPath("//li/a[@id='PremiumLimitsTab__item']");
    readonly By btnSave = By.XPath("(//button[@name='save'])[1]");
    readonly By btnSave1 = By.XPath("(//button[@name='save'])[1]");
    readonly By lblErrorMessages = By.XPath("(//div[@role='alert'])[2]");
    readonly By lblErrorMessageActionsFails = By.XPath("//div[contains(text(),'You cannot move the stage to Working, Quoted or Bound if the Licensing Status is not \"Pass\" OR \"Not Required')]");

    readonly By btnCancel = By.XPath("(//button[@name='cancel'])[1]");

    readonly By lnkFirstSubmRecord = By.XPath("(//span/a[contains(text(),'TBD')])[1]");

    readonly By btnclearProjectName = By.XPath("((//label[contains(text(),'Project Name')]/following::div//input)[1]/following::div/button)[1]");
    readonly By ddProjectName = By.XPath("//label[contains(text(),'Project Name')]");
    readonly By slct2ndClientId = By.XPath("((//label[contains(text(),'Project Name')])/following::div//ul/li/lightning-base-combobox-item/span[2]/span/span)[3]");

    readonly By lblClientName = By.XPath("//button[@title='Edit Client Name']/parent::div//a");
    readonly By lblProjectNameUpdated = By.XPath("//button[@title='Edit Project Name']/parent::div//a");

    // Submission objects
    readonly By tabMoreDetails = By.XPath("//button[contains(text(),'More')]");
    readonly By tabActionValues = By.XPath("//div[@class='slds-dropdown slds-dropdown_right']//lightning-menu-item/a/span");

    readonly By lblProjectNameHeader = By.XPath("//th/div[contains(text(),'Project')]");
    readonly By lblProjectValue = By.XPath("(//tbody/tr/td[4])[1]");


    // Editing in View Edit mode
    readonly By editAssumedInsurerCarrierBranch = By.XPath("//span[@class='test-id__field-label'] [contains(text(),'Assumed Carrier Branch')]");
    //readonly By editAssumedInsurerCarrierBranch = By.XPath("//span[@class='test-id__field-label'] [contains(text(),'Assumed Insurer')]");
    readonly By editRCCCode = By.XPath("(//span[contains(text(),'Assumed Carrier Branch')]//following::button[1])[2]");

    // textarea
    //readonly By txtAreaAssumedInsurerCarrierBranch = By.XPath("(//label/span[contains(text(),'Assumed Carrier Branch')]//following::textarea)[1]");
    readonly By txtAreaAssumedInsurerCarrierBranch = By.XPath("((//span[text()='Assumed Carrier Branch'])[2]//following::textarea)[1]");

    readonly By txtAreaRCCCode = By.XPath("(//label/span[contains(text(),'Assumed Carrier Branch')]//following::textarea)[2]");

    readonly By btnSavebuttonInViewEditMode = By.XPath("//button/span[contains(text(),'Save')]");

    readonly By lblErrorMsg = By.XPath("//ul[@class='errorsList']/li");

    readonly FeatureContext _featureContext;

    public Edit_SubmissionsPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
        _featureContext = featureContext;
    }

    public void ClickOnEditButton()
    {
        driver.Refresh();
        driver.WaitForElementToPresent(btnEdit);
        Assert.IsTrue(driver.WaitForElementToPresent(btnEdit), " Edit button is not visible");
        Assert.IsTrue(driver.WaitAndClick(btnEdit), "Could not clicked on Edit button");
        Log("Clicked on Edit submission button");
        Assert.True(driver.WaitForElementToPresent(tabSubmission), "Could not navigate to Submission Edit Page");
    }

    public void ClickOnEditSubmissionButton()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(btnEdit), " Edit button is not visible");
        Assert.IsTrue(driver.WaitAndClick(btnEdit), "Could not clicked on Edit button");
        Log("Clicked on Edit submission button");
        //Assert.True(driver.WaitForElementToPresent(tabSubmission1), "Could not navigate to Submission Edit Page");
    }

    public void EditProjectField()
    {

        driver.ScrollToCenter(btnclearProjectName);
        driver.WaitAndClick(btnclearProjectName);
        driver.WaitAndClick(ddProjectName);
        Assert.IsTrue(driver.WaitForElementToPresent(slct2ndClientId), "CLIENT ID IS NOT VISIBLE");
        Assert.IsTrue(driver.WaitAndClick(slct2ndClientId), "COULD NOT CLICK ON THE CLIENT ID");
        Log("SUCCESSFULLY SELECTED OTHER PROJECT NAME");
    }

    public void FillValuesInSubmissionEditPagefor_Submission1_0()
    {
        driver.WaitAndClick(tabSubmission);
        driver.WaitAndClick(tabPremiumLimit);
        SendTextToField(txtPercentPolicyBooked, SubmissionPage.Generate2DigitRandomNumber(), "Percent Policy Booked ");
        SendTextToField(txtProducerCommission, SubmissionPage.Generate2DigitRandomNumber(), " Producer Commission ");
        Log("SUBMISSION EDIT FIELDS ARE UPDATED");
    }

    public void FillValuesInSubmissionEditPageforExistingRecord()
    {
        driver.WaitAndClick(tabSubmission1);
        driver.WaitAndClick(tabPremiumLimit1);
        SendTextToField(txtPercentPolicyBooked, SubmissionPage.Generate2DigitRandomNumber(), "100 % PERCENT POLICY BOOKED ");
        SendTextToField(txtProducerCommission1, SubmissionPage.Generate2DigitRandomNumber(), " PRODUCER COMMISSION");
        Log("SUBMISSION EDIT FIELDS ARE UPDATED");
    }

    public void SaveEditedRecord()
    {

        driver.ScrollToCenter(btnSave);
        Assert.IsTrue(driver.WaitAndClick(btnSave), "COULD NOT CLICK ON SAVE BUTTON");
        Log("CLICKED ON SAVE BUTTON");
        System.Threading.Thread.Sleep(3000);
        if (driver.IsDisplayed(lblErrorMessages))
        {
            Assert.IsFalse(driver.IsDisplayed(lblErrorMessages), "ERROR MESSAGE IS DISPLAYED");
        }
        driver.WaitTillOverlayDisappears(btnSave);
        driver.WaitForElementToPresent(tabDetails);
    }

    public void SaveEditedSubmissionRecord()
    {
        driver.ScrollToCenter(btnSave1);
        Assert.IsTrue(driver.WaitAndClick(btnSave1), "COULD NOT CLICK ON SAVE BUTTON");
        Log("CLICKED ON SAVE BUTTON");
        System.Threading.Thread.Sleep(3000);
        if (!driver.IsDisplayed(lblErrorMessages))
        {
            driver.WaitTillOverlayDisappears(btnSave1);
        }
    }

    public void ThenClickOnSaveButtonToVerifyTheErrorMessageIfIceAndLicenceCheckFails()
    {
        driver.ScrollToCenter(btnSave1);
        Assert.IsTrue(driver.WaitAndClick(btnSave1), "COULD NOT CLICK ON SAVE BUTTON");
        Log("CLICKED ON SAVE BUTTON");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblErrorMessageActionsFails));
        Assert.IsTrue(driver.IsDisplayed(lblErrorMessageActionsFails), "EXPECTED ERROR MESSAGE IS NOT DISPLAYED");
        Log("ICE CHECK OR LICENCE CHECK IS FAILED HENCE STOPPING THE EXECUTION");
    }

    public void CancelSubmissionCreateOrEdit()
    {
        driver.ScrollToCenter(btnCancel);
        Assert.IsTrue(driver.WaitAndClick(btnCancel), "COULD NOT CLICK ON CANCEL BUTTON");
        Log("CLICKED ON CANCEL BUTTON");
    }

    public void SaveEditedSubmissionRecordForStageProgressionBackTracking()
    {
        driver.ScrollToCenter(btnSave1);
        Assert.IsTrue(driver.WaitAndClick(btnSave1), "COULD NOT CLICK ON SAVE BUTTON");
        Log(" CLICKED ON SAVE BUTTON");
        //System.Threading.Thread.Sleep(3000);
        //if (driver.IsDisplayed(lblErrorMessages))
        //{
        //    Console.WriteLine("A ----->Yes it is displayed");
        //    Assert.IsFalse(driver.IsDisplayed(lblErrorMessages), "Error message is displayed");
        //}
        //driver.WaitTillOverlayDisappears(btnSave1);
        //driver.WaitForElementToPresent(tabDetails);
    }

    public void VerifySavedSubmissionRecord()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(btnEdit), " RECORD COULD NOT UPDATED SUCCESSFULLY");
        Log("RECORD UPDATED SUCCESSFULLY");
    }

    public void SendTextToField(By txtFieldWebElement, string txtFieldValue, string txtFieldName)
    {
        driver.WaitForElementToPresent(txtFieldWebElement);
        driver.ScrollToCenter(txtFieldWebElement);
        Assert.IsTrue(driver.ClearAndSend(txtFieldWebElement, txtFieldValue), txtFieldName + " FIELD WAS NOT INPUTED");
        Log(txtFieldName + " FIELD VALUE IS INPUTED");
    }

    public void FirstSubmissionRecord()
    {
        driver.WaitForElementToPresent(lnkFirstSubmRecord);
        Assert.IsTrue(driver.WaitAndClick(lnkFirstSubmRecord), "COULD NOT CLICKED ON SUBMISSION");
        Log("CLICKED ON SUBMISSION RECORD");
    }

    public void VerifyUpdatedProjectName()
    {
        string ClientName = driver.GetTextFromElement(lblClientName);
        string ProjectName = driver.GetTextFromElement(lblProjectNameUpdated);

        Assert.IsFalse(ClientName.Equals(ProjectName), "PROJECT NAME IS NOT UPDATED");
        Log("PROJECT NAME IS UPDATED SUCCESSFULLY");
    }

    public void UserStoreThenUserVerifyTheSnapshotInformation()
    {
        System.Threading.Thread.Sleep(3000);
        driver.SwitchToFrame(0);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblProjectValue));
        _featureContext["snapShotActualInfo_" + loggingStep.rowNo] = driver.GetTextFromElement(lblProjectValue);
        Console.WriteLine("ACTUAL PROJECT NAME  --> " + _featureContext["snapShotActualInfo_" + loggingStep.rowNo].ToString());

    }

    public void ThenUserVerifyTheSnapshotInformationAfterUpdate()
    {
        System.Threading.Thread.Sleep(4000);
        driver.SwitchToFrame(0);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblProjectValue));
        Console.WriteLine(driver.GetTextFromElement(lblProjectValue) + "===" + _featureContext["UpdatedProjectName" + loggingStep.rowNo].ToString());
        if (!driver.GetTextFromElement(lblProjectValue).Equals(_featureContext["UpdatedProjectName" + loggingStep.rowNo].ToString()))
        {
            Log("PROJECT NAME IS UDPATED IN THE SNAPSHOT INFORMATION TAB");
        }
        else
        {
            Console.WriteLine("PROJECT NAME IS NOT UPDATED INT HE SNAPSHOT INFORMATION");
            Log("PROJECT NAME IS NOT UPDATED INT HE SNAPSHOT INFORMATION");
            Assert.Fail("PROJECT NAME IS NOT UPDATED INT HE SNAPSHOT INFORMATION");
        }
    }

    public void ThenUserEditAssumedCarrierBranchField()
    {
        driver.WaitForElementToPresent(editAssumedInsurerCarrierBranch);
        driver.MoveToTheElementAndDoubleClick(editAssumedInsurerCarrierBranch);
        Log("CLICKED ON ASSUMED CARRIER BRANCH BUTTON");
    }

    public void ThenUserUpdatedFieldWithValue(string FieldName, string FieldValue)
    {
        if (FieldName.Equals("Assumed Carrier Branch"))
        {
            driver.ScrollToCenter(txtAreaAssumedInsurerCarrierBranch);
            driver.ScrollByPixel(80);
            driver.WaitAndClick(txtAreaAssumedInsurerCarrierBranch);
            Assert.IsTrue(driver.SendKeysOrText(txtAreaAssumedInsurerCarrierBranch, ";;;;;"), "COULD NOT INPUT THE VALUE IN ASSUMED CARRIER BRANCH FIELD");
            driver.ScrollToCenter(txtAreaAssumedInsurerCarrierBranch);
            driver.WaitAndClick(txtAreaRCCCode);
            driver.CaptureScreen(_scenarioContext);
            Log("SUCCESSFULLY INPUTTED THE VALUE IN ASSUMED CARRIER BRANCH FIELD");
        }
        if (FieldName.Equals("RCC Code"))
        {
            driver.ScrollToCenter(txtAreaRCCCode);
            driver.ScrollByPixel(80);
            driver.WaitAndClick(txtAreaRCCCode);
            driver.ClearAndSend(txtAreaRCCCode, "");
            Assert.IsTrue(driver.SendKeysOrText(txtAreaRCCCode, ""), "COULD NOT INPUT THE VALUE IN ASSUMED CARRIER BRANCH FIELD");
            driver.ScrollToCenter(txtAreaRCCCode);
            driver.WaitAndClick(txtAreaRCCCode);
             driver.CaptureScreen(_scenarioContext);
            Log("SUCCESSFULLY INPUTTED THE VALUE IN RCC CODE");
        }
    }

    public void ThenUserClickedOnSaveSubmissionInViewEditMode()
    {
        //driver.WaitTillElementIsClickable(btnSavebuttonInViewEditMode);
        driver.MoveToTheElementAndClick(btnSavebuttonInViewEditMode);
        System.Threading.Thread.Sleep(3000);
        Log("CLICKED ON SAVE BUTTON");
    }

    public void ThenUserVerifiedTheErrorMessageDisplayed(string ErrorMessage)
    {
        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblErrorMsg));
        System.Threading.Thread.Sleep(3000);
        string DisplayedErrorMessage = driver.GetTextFromElement(lblErrorMsg);
        Assert.AreEqual(ErrorMessage.Trim(), DisplayedErrorMessage.Trim(), "DISPLAYED ERROR MESSAGE ARE NOT EQUAL WITH EXPECTED ERROR MESSAGE - " + DisplayedErrorMessage);
        Console.WriteLine("DISPLAYED ERROR MESSAGE = " + DisplayedErrorMessage);
        Console.WriteLine("EXPECTED ERROR MESSAGE = " + ErrorMessage);
        Log("EXPECTED ERROR MESSAGE IS DISPLAYED - " + ErrorMessage);
        Log("DISPLAYED ERROR MESSAGE = " + DisplayedErrorMessage);
    }







}