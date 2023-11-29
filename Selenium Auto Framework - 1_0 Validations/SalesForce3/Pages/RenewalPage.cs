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
public class RenewalPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    public static string ScenarioCount, RenewedSubmissionNumber;
    public static string Clone1_0Filepath = SubmissionPage.BaseURL + "Clone/Clone1_0.txt";
    public static string Clone2_0Filepath = SubmissionPage.BaseURL + "Clone/Clone2_0.txt";


    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

    // Renewal Webelements
    readonly By btnMoreActions = By.XPath("(//ul[@class='slds-button-group-list']/li//button)[8]");
    readonly By btnActionNames = By.XPath("//div[@class='slds-dropdown slds-dropdown_right']//runtime_platform_actions-action-renderer//a/span");

    readonly By lblRenewalPage = By.XPath("//h2/span[contains(text(),'Renewal')]");
    readonly By btnSaveRenewal = By.XPath("//button[contains(text(),'Save')]");
    //readonly By lblErrorMessages = By.XPath("(//div[@role='alert'])[2]");
    readonly By lblErrorMessages = By.XPath("//span[@class='toastMessage forceActionsText'] | //div[@class='slds-notify__content']/h2/following::p[1]");
    readonly By lblCreatedRenewal = By.XPath("(//div/h1//lightning-formatted-text)[2]");
    readonly By lblErrorDialogMessage = By.XPath("//div[@role='alertdialog']//span[@class='toastMessage forceActionsText']");

    readonly By btnViewSubmissionList = By.XPath("//ul/li//lightning-button/button");

    // Submission objects
    readonly By tabMoreDetails = By.XPath("//button[contains(text(),'More')]");
    readonly By tabActionValues = By.XPath("//div[@class='slds-dropdown slds-dropdown_right']//lightning-menu-item/a/span");
    readonly By linkRelatedSubNumber = By.XPath("//div/span[contains(text(),'Related Submission Number')]/following::div/span/a");

    // Read value from field during Detail view
    readonly By lblDetailsTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[1]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblDetailsFieldValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[1]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[1]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");

    // Child records
    readonly By linkAllChildRecords = By.XPath("//a/span[@title='Endorsements/Sections/Terrorism']");
    readonly By tableAllChildRecordList = By.XPath("//tbody/tr");
    readonly By lblChildEndorsement = By.XPath("//tbody/tr[1]/td[2]/span/span");
    readonly By lblChildSection = By.XPath("//tbody/tr[2]/td[2]/span/span");
    readonly By lblChildTerrorism = By.XPath("//tbody/tr[3]/td[2]/span/span");

    // clone
    readonly By btnSave = By.XPath("//button[@name='cancel']/parent::div/button[1]");
    readonly By lblCloneSubmission = By.XPath("//h2/span[contains(text(),'Clone Submission')]");
    readonly By lblCreatedClone = By.XPath("(//div/h1//lightning-formatted-text)[2]");
    readonly By btnRegularClone = By.XPath("//button[contains(text(),'Regular Clone')]");

    // Renewal
    readonly By lblSubmissionName = By.XPath("//h1/slot/lightning-formatted-text");
    readonly By lblRelatedSubmissionNumber = By.XPath("//span[contains(text(),'Related Submission Number')]");
    readonly By lblStarrSubmissionName = By.XPath("//div/h1/slot/lightning-formatted-text");

    readonly FeatureContext _featureContext;
    public RenewalPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
        _featureContext = featureContext;
    }


    // Backup for Already renewed error message
    public void VerifyAlreadyRenewedSubmissionCreatedErrorMessage()
    {
        driver.WaitForElementToPresent(lblErrorDialogMessage);
        if (driver.IsDisplayed(lblErrorDialogMessage))
        {
            Assert.IsTrue(driver.IsDisplayed(lblErrorDialogMessage), "RENEWAL IS ALREADY CREATED FOR THIS SUBMISSION ERROR MESSAGE IS NOT DISPLAYED");
            Log("RENEWAL IS ALREADY CREATED FOR THIS SUBMISSION ERROR MESSAGE IS DISPLAYED");
        }
    }

    public void ThenUserVerifiedParentInformationInCreatedRenewalRecord()
    {
        driver.WaitForElementToPresent(tabMoreDetails);
        driver.WaitAndClick(tabMoreDetails);
        Assert.IsTrue(driver.SelectFromList(tabActionValues, "Sys Info"), "COULD NOT CLICK ON Sys Info TAB");
        driver.ScrollToCenter(linkRelatedSubNumber);
        Assert.IsTrue(driver.WaitForElementToPresent(linkRelatedSubNumber), "Related Submission Number is not present");
        driver.CaptureScreen(_scenarioContext);
        Log("CLICKED ON Sys Info TAB");
    }

    public void ThenUserVerifiedTheCreatedChildRecordsInTheRenewalRecord()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(linkAllChildRecords));
        driver.CaptureScreen(_scenarioContext);
        driver.WaitAndClick(linkAllChildRecords);
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(tableAllChildRecordList));

        int ChildCount = driver.ListOfElements(tableAllChildRecordList).Count;
        driver.CaptureScreen(_scenarioContext);
        if(ChildCount == 3)
        {
            Assert.IsTrue(driver.GetTextFromElement(lblChildEndorsement).Equals("Endorsement"), "ENDORSEMENT RECORD IS NOT CREATED UNDER RENEWAL RECORD");
            Assert.IsTrue(driver.GetTextFromElement(lblChildSection).Equals("Section"), "SECTION RECORD IS NOT CREATED UNDER RENEWAL RECORD");
            Assert.IsTrue(driver.GetTextFromElement(lblChildTerrorism).Equals("Terrorism"), "TERRORISM RECORD IS NOT CREATED UNDER RENEWAL RECORD");
        }
        else
        {
            Assert.Fail("CREATED CHILD RECORDS ARE NOT DISPLAYED");
        }
        Log("ENDORSEMENT / SECTION / TERRORISM RECORDS DISPLAYED IN RENEWAL SUBMISSION RECORD");
    }

    public void SaveRenewalRecord()
    {
        string RecordInfo;

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        if (_featureContext["App_Version" + loggingStep.rowNo].ToString().Equals("1"))
        {
            driver.ScrollToBottom();
            driver.ScrollToCenter(btnSaveRenewal);
            Assert.IsTrue(driver.WaitAndClick(btnSaveRenewal), "COULD NOT CLICK ON SAVE BUTTON");
            driver.WaitForNextPage();
            driver.ScrollToCenter(lblRenewalPage);
            if (driver.IsDisplayed(lblErrorMessages))
            {
                driver.ScrollToCenter(lblErrorMessages);
                Assert.IsFalse(driver.IsDisplayed(lblErrorMessages), "ERROR MESSAGE IS DISPLAYED");
            }
            driver.WaitTillOverlayDisappears(btnSaveRenewal);
            Log("CLICKED ON SAVE BUTTON TO CREATE RENEWAL SUBMISSION");
            driver.WaitForNextPage();
            driver.Refresh();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblDetailsTabFieldsName));
            _featureContext["CreatedSubmissionName_" + loggingStep.rowNo] = GetValueUsingFieldName("Submission Name", lblDetailsTabFieldsName, lblDetailsFieldValue);
            _featureContext["AssignmentType_" + loggingStep.rowNo] = GetValueUsingFieldName("Policy Number - Assignment Type", lblDetailsTabFieldsName, lblDetailsFieldValue);
            driver.CaptureScreen(_scenarioContext);
            RecordInfo = _featureContext["RecordType_" + loggingStep.rowNo].ToString() + " # " + driver.Url + " = " + _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString();
            if (_featureContext["TestingType_" + loggingStep.rowNo].ToString().Trim().Equals("Validation"))
            {
                File.WriteAllText(SubmissionPage.Renewal1_0Filsepath + loggingStep.rowNo + ".txt", RecordInfo);
            }
            else
            {
                File.WriteAllText(SubmissionPage.Renewal1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt", RecordInfo);
            }
        }
    }


    public void SaveRecordToVerifyErrorMessageForChildRecords()
    {
        driver.ScrollToBottom();
        driver.ScrollToCenter(btnSaveRenewal);
        Assert.IsTrue(driver.WaitAndClick(btnSaveRenewal), "COULD NOT CLICK ON SAVE BUTTON");
        driver.WaitForNextPage();
        driver.ScrollToCenter(lblRenewalPage);
        Log("CLICKED ON SAVE BUTTON TO CREATE RENEWAL SUBMISSION");
    }




    public void ThenUserVerifiedRenewalSubmissionCreation()
    {
        string RecordInfo = "";
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));

        if (_featureContext["App_Version" + loggingStep.rowNo].ToString().Equals("1"))
        {
            System.Threading.Thread.Sleep(3000);
            driver.Refresh();
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblDetailsTabFieldsName));
            _featureContext["CreatedRenewalSubmissionName_" + loggingStep.rowNo] = GetValueUsingFieldName("Submission Name", lblDetailsTabFieldsName, lblDetailsFieldValue);
            _featureContext["AssignmentType_" + loggingStep.rowNo] = GetValueUsingFieldName("Policy Number - Assignment Type", lblDetailsTabFieldsName, lblDetailsFieldValue);
            driver.CaptureScreen(_scenarioContext);
            RecordInfo = _featureContext["RecordType_" + loggingStep.rowNo].ToString() + " # " + driver.Url + " = " + _featureContext["CreatedRenewalSubmissionName_" + loggingStep.rowNo].ToString();
            File.WriteAllText(SubmissionPage.Renewal1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt", RecordInfo);
        }
        Log("SUCCESSFULLY CREATED THE RENEWAL SUBMISSION " + _featureContext["CreatedRenewalSubmissionName_" + loggingStep.rowNo].ToString());
        Console.WriteLine("SUCCESSFULLY CREATED THE RENEWAL SUBMISSION AND SUBMISSSION NAME = " + _featureContext["CreatedRenewalSubmissionName_" + loggingStep.rowNo].ToString());

        _featureContext["RenewalURL_" + loggingStep.rowNo] =  driver.Url;
        _featureContext["RenewalSubmissionName_" + loggingStep.rowNo] = _featureContext["CreatedRenewalSubmissionName_" + loggingStep.rowNo].ToString();
    }


    public string GetValueUsingFieldName(string FieldName, By ListFieldName, By ListFields)
    {

        IList<IWebElement> FieldNamesList = driver.ListOfElements(ListFieldName);
        IList<IWebElement> FieldsList = driver.ListOfElements(ListFields);

        for (int FieldCount = 0; FieldCount < FieldNamesList.Count - 1; FieldCount++)
        {
            string ActualFieldname = FieldNamesList[FieldCount].GetElementText();
            Console.WriteLine(ActualFieldname + " == " + FieldName);
            if (ActualFieldname.Trim().Equals(FieldName.Trim()))
            {
                driver.MoveToTheElement(FieldsList[FieldCount]);
                string FieldValue = FieldsList[FieldCount].GetElementText();
                return FieldValue;
            }
        }
        Console.WriteLine("COULD NOT FIND THE FIELD " + FieldName);
        Log("COULD NOT FIND THE FIELD " + FieldName);
        return null;
    }

    public void ThenUserClickedOnSaveButtonInCloneCreationPage(string SubmissionVersion)
    {
        driver.ScrollToBottom();
        driver.ScrollToCenter(btnSave);
        Assert.IsTrue(driver.WaitAndClick(btnSave), "COULD NOT CLICK ON SAVE BUTTON");
        driver.WaitForNextPage();
        driver.ScrollToCenter(lblCloneSubmission);
        if (driver.IsDisplayed(lblErrorMessages))
        {
            driver.ScrollToCenter(lblErrorMessages);
            Assert.IsFalse(driver.IsDisplayed(lblErrorMessages), "ERROR MESSAGE IS DISPLAYED");
        }
        driver.WaitTillOverlayDisappears(btnSave);

        Log(" CLONE RECORD IS CREATED SUCCESSFULLY");
    }


    public void ThenUserVerifiedCreatedCloneRecord()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblCreatedClone));
        Assert.IsTrue(driver.IsDisplayed(lblCreatedClone),"CLONE RECORD IS NOT CREATED /  DISPLAYED ");
        Log("CLONE RECORD IS CREATED SUCCESSFULLY");
    }

    public void ThenUserVerifyRenewedSubmissionCreation()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(lblSubmissionName),"RENEWAL RECORD IS NOT CREATED");
        RenewedSubmissionNumber = driver.GetTextFromElement(lblSubmissionName);
        Log("NEW RENEWAL SUBMISSION IS CREATED SUCCESSFULLY");
    }

    public void ThenUserVerifyCloneSubmissionIsCreatedSuccessfully()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(lblSubmissionName), "CLONE SUBMISSION RECORD IS NOT CREATED");
        RenewedSubmissionNumber = driver.GetTextFromElement(lblSubmissionName);
        Log("CLONE SUBMISSION RECORD IS CREATED");
    }

    public void WhenUserClickedOnRegularCloneButton()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(btnRegularClone), "REGULAR CLONE BUTTON IS NOT DISPLAYED");
        Assert.IsTrue(driver.WaitAndClick(btnRegularClone),"COULD NOT CLICKED ON REGULAR CLONE BUTTON");
        Log("CLICKED ON REGULAR CLONE BUTTON");
    }


    public void ThenUserVerifyParentSubmisisonNumberUnderRenewedSubmission()
    {
        driver.WaitForElementToPresent(lblRelatedSubmissionNumber);
        string ParentSubmissionName = GetValueUsingFieldName("Related Submission Number", lblDetailsTabFieldsName, lblDetailsFieldValue);
        Assert.AreEqual(ParentSubmissionName, _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString(), "Parent submission is not same as mentioned in the Renewed submission record");
        Log("PARENT SUBMISSION IS SAME AS IN RENEWED SUBMISSION");
    }

    public void ThenUserVerifyRenewedSubmissionNumberUnderParentSubmission()
    {
        driver.WaitForElementToPresent(lblRelatedSubmissionNumber);
        string RenewedSubmissionNumberInParent = GetValueUsingFieldName("Related Submission Number", lblDetailsTabFieldsName, lblDetailsFieldValue);
        Assert.AreEqual(RenewedSubmissionNumberInParent, RenewedSubmissionNumber, "Parent submission is not same as mentioned in the Renewed submission record");
        Log("PARENT SUBMISSION IS SAME AS IN RENEWED SUBMISSION");
    }





}
