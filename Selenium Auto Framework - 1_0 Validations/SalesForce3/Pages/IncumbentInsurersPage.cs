using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using System.IO;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SalesForce3.Pages;
public class IncumbentInsurersPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    private string Insurer;
    public static string CarrierName;
    private string InsurerName;


    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);


    //SF HOMEPAGE

    readonly By btnAppLauncher = By.XPath("//div[@class='slds-r5']");
    readonly By txtSearchAppandItem = By.XPath("//input[contains(@placeholder,'Search apps and items')]");
    readonly By opnIncumbentInsurers = By.XPath("(//lightning-formatted-rich-text/span/p/b)[1]");
    readonly By txtSearchSetup = By.XPath("//div[@class='rightScroll']");
    readonly By errorMsgBox = By.XPath("//div/h2[contains(@title,'We hit a snag')]");
    readonly By linkErrorMsg = By.XPath("//div[@class=\"pageLevelErrors\"]/ul/li | (//div[@class='panel-content scrollable']//ul/li/a)[1]");
    readonly By btnCloseErrorDialog = By.XPath("//button[starts-with(@title,'Close error dialog')]");
    readonly By txtInsurer = By.XPath("//input[@name='Insurer_if_Other__c']");
    readonly By cbShareUnknown = By.XPath("//input[@name='Share_Unknown__c']");
    readonly By lblInsurerName = By.XPath("//div/h1/slot/lightning-formatted-text");
    readonly By btnSave = By.XPath("//button[@name='SaveEdit'] | //button[@name='Save'] ");
    readonly By btnSaveCarrier = By.XPath("//button[@title='Save']");
    readonly By btnNew = By.XPath("//a/div[@title='New']");
    //readonly By optnSubmissionValue = By.XPath("(//ul/li/lightning-base-combobox-item/span[2]/span[1]/span)[1]");
    readonly By optnSubmissionValue = By.XPath("(//ul/li/lightning-base-combobox-item/span[2]/span/lightning-base-combobox-formatted-text)[2]");

    readonly By txtSubmission = By.XPath("//input[@placeholder='Search Submissions...']");
    readonly By lblInsurerIfOtherValue = By.XPath("(//div/span/slot/lightning-formatted-text)[1]");
    readonly By txtInsurerShare = By.XPath("//input[@name='Incumbent_Insurer_Share__c']");
    readonly By txtInsurerName = By.XPath("(//div/lightning-base-combobox//input)[1]");
    readonly By lblNewCarrier = By.XPath("//span[@title='New Carrier']");
    readonly By txtCarrierName = By.XPath("(//div[@class='slds-form-element__control']/div//input)[3]");
    readonly By txtParentCarrier = By.XPath("(//div[@class='slds-form-element__control']/div//input)[5]");
    readonly By cbActive = By.XPath("(//div[@class='slds-form-element__control']/div//input)[4]");
    readonly By lblInsurerNameValue = By.XPath("(//div/records-hoverable-link//slot)[2]/span");
    readonly By lblInsurerNameValueUpdated = By.XPath("(//input[starts-with(@aria-controls,'dropdown-element')])[1]");
    readonly By btnEdit = By.XPath("//button[@name='Edit']");
    readonly By lnkSubmissionName = By.XPath("((//span[contains(text(),'Submission')])[3]/following::div)[1]//a//span");
    readonly By tabSubmissionDetail = By.XPath("(//li/a[@id='detailTab__item'])[2]");
    readonly By tabPolicy2_0 = By.XPath("//a[@id='customTab3__item']");
    readonly By lblIncumbentCucumberInSubmission = By.XPath("(//span[contains(text(),'Incumbent Insurers')])[4]/following::div[2]//span[@class='uiOutputTextArea']");
    readonly By txtRecordId = By.XPath("//input[@name='Name']");
    readonly By lblClientErrorMsg = By.XPath("//div[@class='slds-form-element__help slds-m-left_none']");
    readonly By txtInsuerIfAny = By.XPath("//input[@name='Insurer_if_Other__c']");

    readonly FeatureContext _featureContext;

    public IncumbentInsurersPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
        _featureContext = featureContext;
    }

    public static int GenerateRandomNumbers()
    {
        Random random = new Random();
        return random.Next();
    }
    public void NavigateToIncumbentInsurersPage()
    {
        driver.WaitForElementToPresent(txtSearchSetup);
        driver.WaitTillElementIsClickable(btnAppLauncher);
        driver.WaitAndClick(btnAppLauncher);
        driver.WaitForElementToPresent(txtSearchAppandItem);
        driver.ClearAndSend(txtSearchAppandItem, "Incumbent Insurers");
        driver.WaitTillElementIsClickable(opnIncumbentInsurers);
        driver.JSClick(opnIncumbentInsurers);
        Assert.IsTrue(driver.WaitForElementToPresent(btnNew), "Couldn't navigated to Incumbent Insurers Object");
        Log("Navigated to Incumbent Insurers Object");
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

    public void InputInsurerIfAny()
    {
        string data = "Insurer_";
        Insurer = data + GenerateRandomNumbers();

        if (Insurer != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtInsurer, Insurer), "Insurer(if any) field Was NOT Inputed");
        Log("Insurer(if any) field was inputted");
    }

    public void SelectShareUnknownCheckbox()
    {
        driver.ScrollToCenter(cbShareUnknown);
        driver.CheckTheBox(cbShareUnknown);
        Log("Share Unknown checkbox is checked");
    }


    public void ClickOnSaveButton()
    {
        driver.WaitAndClick(btnSave);
        Log("CLICKED ON SAVE BUTTON");
    }
    public void VerifyCreatedInsurerRecord()
    {
        Assert.AreEqual(driver.GetTextFromElement(lblInsurerName).ToString(), Insurer.ToString(), "COULD NOT CREATE THE ASSUMED INSURER RECORD");
        Log("ASSUMED INSURER RECORD IS CREATED SUCCESSFULLY");
    }

    public void ClickOnNewButton()
    {
        driver.WaitAndClick(btnNew);
        Assert.IsTrue(driver.WaitForElementToPresent(txtInsurer), "NEW INCUMBENT INSURER CREATION PAGE IS NOT DISPLAYED");
        driver.WaitForNextPage();
        Log("INCUMBENT INSURER CREATION PAGE IS DISPLAYED");
    }

    public static string ReadDataFromFile(string Filepath)
    {
        string[] LInes = File.ReadAllLines(Filepath);
        string[] value;
        string returnValue;
        foreach (string str in LInes)
        {
            value = str.Split("=");
            Console.WriteLine("VALUE IS = " + value);
            Console.WriteLine("VALUE INDEX COUNT  = " + value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                Console.WriteLine("OUT PUT OF  " + i + " = ", value[i]);
            }
            returnValue = value[1];
            return returnValue;
        }
        Console.WriteLine("COULD NOT FETCH VALUE FROM THE FILE");
        return null;
    }

    public void InputValueInSubmission(string SubmissionType)
    {
        System.Threading.Thread.Sleep(2000);
        if (SubmissionType.Trim().ToLower().Equals("new") && _featureContext["App_Version" + loggingStep.rowNo].ToString().Equals("1"))
        {
            if(_featureContext["TestingType_" + loggingStep.rowNo].ToString().ToLower().Equals("functional"))
            {
                _featureContext["SubmissionName_" + loggingStep.rowNo] = ReadDataFromFile(SubmissionPage.Submission1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt");
            }
            if (_featureContext["TestingType_" + loggingStep.rowNo].ToString().ToLower().Equals("validation"))
            {
                _featureContext["SubmissionName_" + loggingStep.rowNo] = ReadDataFromFile(SubmissionPage.Submission1_0ValidationRecords + loggingStep.rowNo +"Prospect"+ ".txt");
            }
            Console.WriteLine("SUBMISSION NAME " + _featureContext["SubmissionName_" + loggingStep.rowNo].ToString());
            Log("ADDING SUBMISSION NAME " + _featureContext["SubmissionName_" + loggingStep.rowNo].ToString() + "IN THE SUBMISSION FIELD");
            driver.ClearAndSend(txtSubmission, _featureContext["SubmissionName_" + loggingStep.rowNo].ToString());
        }
        if (SubmissionType.Trim().ToLower().Equals("renewal") && _featureContext["App_Version" + loggingStep.rowNo].ToString().Equals("1"))
        {
            _featureContext["RenewalSubmissionName_" + loggingStep.rowNo] = ReadDataFromFile(SubmissionPage.Renewal1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt");
            Console.WriteLine("RENEWAL SUBMISSION NAME " + _featureContext["RenewalSubmissionName_" + loggingStep.rowNo].ToString());
            Log("RENEWAL SUBMISSION NAME " + _featureContext["RenewalSubmissionName_" + loggingStep.rowNo].ToString());
            driver.ClearAndSend(txtSubmission, _featureContext["RenewalSubmissionName_" + loggingStep.rowNo].ToString());
        }
        System.Threading.Thread.Sleep(3000);
        Assert.IsTrue(driver.WaitForElementToPresent(optnSubmissionValue), "COULD NOT FIND THE VALUE IN THE FIELD");
        driver.JSClick(optnSubmissionValue);
        Log("SUBMISSION NAME IS INPUTED IN THE FILE");
    }

    public void InputValueInSubmissions(string SubmissionType, string Stage)
    {
        System.Threading.Thread.Sleep(2000);
        if (SubmissionType.Trim().ToLower().Equals("new") && _featureContext["App_Version" + loggingStep.rowNo].ToString().Equals("1"))
        {
            if (_featureContext["TestingType_" + loggingStep.rowNo].ToString().ToLower().Equals("functional"))
            {
                _featureContext["SubmissionName_" + loggingStep.rowNo] = ReadDataFromFile(SubmissionPage.Submission1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt");
            }
            if (_featureContext["TestingType_" + loggingStep.rowNo].ToString().ToLower().Equals("validation"))
            {
                _featureContext["SubmissionName_" + loggingStep.rowNo] = ReadDataFromFile(SubmissionPage.Submission1_0ValidationRecords + loggingStep.rowNo + Stage + ".txt");
            }
            Console.WriteLine("SUBMISSION NAME " + _featureContext["SubmissionName_" + loggingStep.rowNo].ToString());
            Log("ADDING SUBMISSION NAME " + _featureContext["SubmissionName_" + loggingStep.rowNo].ToString() + "IN THE SUBMISSION FIELD");
            driver.ClearAndSend(txtSubmission, _featureContext["SubmissionName_" + loggingStep.rowNo].ToString());
        }
        if (SubmissionType.Trim().ToLower().Equals("renewal") && _featureContext["App_Version" + loggingStep.rowNo].ToString().Equals("1"))
        {
            if (_featureContext["TestingType_" + loggingStep.rowNo].ToString().ToLower().Equals("validation"))
            {
                _featureContext["RenewalSubmissionName_" + loggingStep.rowNo] = ReadDataFromFile(SubmissionPage.Renewal1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt");
            }

            Console.WriteLine("RENEWAL SUBMISSION NAME " + _featureContext["RenewalSubmissionName_" + loggingStep.rowNo].ToString());
            Log("RENEWAL SUBMISSION NAME " + _featureContext["RenewalSubmissionName_" + loggingStep.rowNo].ToString());
            driver.ClearAndSend(txtSubmission, _featureContext["RenewalSubmissionName_" + loggingStep.rowNo].ToString());
        }
        System.Threading.Thread.Sleep(3000);
        Assert.IsTrue(driver.WaitForElementToPresent(optnSubmissionValue), "COULD NOT FIND THE VALUE IN THE FIELD");
        driver.JSClick(optnSubmissionValue);
        Log("SUBMISSION NAME IS INPUTED IN THE FILE");
    }

    public void InputValueInSubmission(string scenarioCount, string version)
    {
        string SubmissionName = "";
        Console.WriteLine("Incumbent Insurer Senario count " + scenarioCount);
        driver.WaitAndClick(txtSubmission);
        if (version.Equals("1.0"))
        {
            SubmissionName = SubmissionPage.ReadDataFromFile(SubmissionPage.Renewal1_0Filsepath + scenarioCount + ".txt");
        }

        Console.WriteLine("Inputting Renewal Submission name is ---> " + SubmissionName);
        driver.SendKeysOrText(txtSubmission, SubmissionName);
        driver.WaitForElementToPresent(optnSubmissionValue);
        driver.JSClick(optnSubmissionValue);
        Log("Inputed value in Submission field");
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

    public void VerifyCreatedIncumbentInsurersRecord()
    {
        driver.WaitForElementToPresent(lblInsurerIfOtherValue);
        Assert.AreEqual(driver.GetTextFromElement(lblInsurerIfOtherValue).ToString(), Insurer.ToString(), "Could not create the Incumbent Insurers record");
        Log("Incumbent Insurer record is created successfully");
    }
    public void CreateNewCarrier()
    {
        CarrierName = "Test_Carrier_" + GenerateRandomNumbers();
        string ParentCarrier = "Test_Parent_" + GenerateRandomNumbers();
        Assert.IsTrue(driver.ClearAndSend(txtCarrierName, CarrierName), "Carrier Name is not Inputed");
        Assert.IsTrue(driver.ClearAndSend(txtParentCarrier, ParentCarrier), "Parent Name is not Inputted");
        Assert.IsTrue(driver.CheckTheBox(cbActive), "Checkbox is checked");
        driver.WaitAndClick(btnSaveCarrier);
        Log("Created new Carrier Record");
    }

    public void FillValueInIncumbentInsurer(string SubmissionType, string Stage)
    {
        driver.WaitForElementToPresent(txtInsurerName);
        driver.WaitAndClick(txtInsurerName);
        driver.WaitForElementToPresent(lblNewCarrier);
        driver.WaitAndClick(lblNewCarrier);
        driver.WaitForNextPage();
        Assert.IsTrue(driver.WaitForElementToPresent(txtCarrierName), "COULD NOT CREATE NEW CARRIER");
        CreateNewCarrier();
        InsurerName = driver.GetTextFromElement(txtInsurerName);
        InputValueInSubmissions(SubmissionType, Stage);
        driver.ClearAndSend(txtInsurerShare, SubmissionPage.Generate2DigitRandomNumber());
    }

    public void ThenUserFillTheValueIncumbentInsurersCreationPageForValidation()
    {
        _featureContext["App_Version" + loggingStep.rowNo] = "1";
        _featureContext["TestingType_" + loggingStep.rowNo] = "Validation";
        driver.WaitForElementToPresent(txtInsurerName);
        driver.WaitAndClick(txtInsurerName);
        driver.WaitForElementToPresent(lblNewCarrier);
        driver.WaitAndClick(lblNewCarrier);
        driver.WaitForNextPage();
        Assert.IsTrue(driver.WaitForElementToPresent(txtCarrierName), "COULD NOT CREATE NEW CARRIER");
        CreateNewCarrier();
        InputValueInSubmission("New");
        Log("INPUTED VALUES IN INCUMBENT INSURER CREATION PAGE");
    }

    public void ThenUserVerifyTheErrorMessageForIncumbentInsurerAs(string ErrorMessage)
    {
        string DisplayedErrorMsg = "";
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblClientErrorMsg));
            DisplayedErrorMsg = driver.GetTextFromElement(lblClientErrorMsg);
            Assert.AreEqual(ErrorMessage, DisplayedErrorMsg, "DISPLAYED ERROR MESSAGES ARE NOT EQUAL");
            Console.WriteLine("EXPECTED ERROR MESSAGE IS DISPLAYED");
            Log("EXPECTED ERROR MESSAGE IS DISPLAYED");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            Assert.Fail("EXPECTED ERROR MESSAGE IS NOT DISPLAYED : " + ErrorMessage);
        }
    }



    public void ThenUserFillTheValueIncumbentInsurersCreationPageForRenewalSubmission(string count, string version)
    {
        //    driver.WaitAndClick(txtInsurerName);
        //    driver.WaitForElementToPresent(lblNewCarrier);
        //    driver.WaitAndClick(lblNewCarrier);
        //    driver.WaitForNextPage();
        //    Assert.IsTrue(driver.WaitForElementToPresent(txtCarrierName), "COULD NOT CREATE NEW CARRIER");
        //    CreateNewCarrier();
        //    InsurerName = driver.GetTextFromElement(txtInsurerName);
        //    InputValueInSubmission();
        //    driver.ClearAndSend(txtInsurerShare, SubmissionPage.Generate2DigitRandomNumber());
    }

    public void VerifyIncumbentInsuereRecord()
    {
        driver.WaitForElementToPresent(lblInsurerNameValue);
        Assert.AreEqual(CarrierName.ToString(), driver.GetTextFromElement(lblInsurerNameValue).ToString(), "Could not create the Incumbent Insurer Record");
        Log("Incumbent Insurers page is created successfully");
    }

    // INCUMBENT INSURER VALIDATION A02 REQUIRED METHOD
    public void ThenUserAddValueInField()
    {
        System.Threading.Thread.Sleep(3000);
        _featureContext["App_Version" + loggingStep.rowNo] = "1";
        _featureContext["TestingType_" + loggingStep.rowNo] = "Validation";
        driver.ClearAndSend(txtInsuerIfAny, "Test Insurer");
        Console.WriteLine("ADDED VALUE IN TEST INSURER FIELD");
        Log("ADDED VALUE IN TEST INSURER FIELD");
    }

    //public void VerifyIncumbentInsurerInSubmission(String Version)
    //{
    //    driver.WaitForElementToPresent(lnkSubmissionName);
    //    driver.WaitAndClick(lnkSubmissionName);
    //    if (Version.Equals("1.0"))
    //    {
    //        driver.WaitForElementToPresent(tabSubmissionDetail);
    //        driver.WaitAndClick(tabSubmissionDetail);
    //        driver.WaitForNextPage();
    //        driver.MoveToTheElement(lblIncumbentCucumberInSubmission);
    //        //driver.ScrollByPixel(200);
    //        string AllIncumbentInsurers = driver.GetTextFromElement(lblIncumbentCucumberInSubmission);
    //        Assert.True(AllIncumbentInsurers.Contains(CarrierName), "Assumed insurers is not present in the Submission");
    //    }
    //    if((Version.Equals("2.0")))
    //    {
    //        driver.WaitForElementToPresent(tabPolicy2_0);
    //        driver.WaitAndClick(tabPolicy2_0);
    //        driver.WaitForNextPage();
    //        driver.MoveToTheElement(lblIncumbentCucumberInSubmission);
    //        //driver.ScrollByPixel(200);
    //        string AllIncumbentInsurers = driver.GetTextFromElement(lblIncumbentCucumberInSubmission);
    //        Assert.True(AllIncumbentInsurers.Contains(CarrierName), "Assumed insurers is not present in the Submission");

    //    }
    //    Log("Assumed Insurers present in the Submission ");

    //}
}
