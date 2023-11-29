using AventStack.ExtentReports.Gherkin.Model;
using Gherkin.CucumberMessages.Types;
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
public class ClientsPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    public static string ClientName;
    public static string ScenarioCount, SelectedClientType, Source;
    public static string Clients1_0FilePath = SubmissionPage.BaseURL + "Clients1_0/";
    public static string Clients2_0FilePath = SubmissionPage.BaseURL + "Clients2_0/";
    public static IList<IWebElement> ChildValues;

    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);


    // Selecting REcord Type
    readonly By btnClientRecordTypeNext = By.XPath("//button/span[contains(text(),'Next')]");
    readonly By rbClientType = By.XPath("(//div[@class='changeRecordTypeRightColumn slds-form-element__control']//span[@class='slds-radio--faux'])[1]");
    

    //SF HOMEPAGE

    readonly By mnuItems = By.XPath("(//div[@role='menu'])[3]/slot/one-app-nav-bar-menu-item/a/span/span");
    //readonly By lnkMore = By.XPath("(//a/span[contains(text(),'More')])[1]");
    readonly By lnkMore = By.XPath("//a/span[@class='slds-p-right_small']");
    readonly By tabClients = By.XPath("//a[@title='Clients']");
    readonly By lblAppName = By.XPath("//div[@class='slds-context-bar__primary navLeft']/div/span/span");

    // CLIENT PAGE
    readonly By txtBusinessName = By.XPath("//label[@id='labelBusinessName']/following::div[1]//div/input");
    readonly By btnSearchClient = By.XPath("//div[@class='DNBoptimizerSbcSearch']//button[contains(text(),'Search')]");
    readonly By btnCreateNew = By.XPath("//div[@class='DNBoptimizerSbcSearch']//button[contains(text(),'Create New')]");

    readonly By txtDunsNumber = By.XPath("//input[@name='DUNS_Number__c']");
    readonly By ddClientStatus = By.XPath("//label[contains(text(),'Client Status')]");
    readonly By btnNewClient = By.XPath("//a/div[@title='New']");
    readonly By txtClientName = By.XPath("//input[@name='Name']");
    readonly By txtMailingStreet = By.XPath("//input[@name='Mailing_Street__c']");
    readonly By txtBillingStreet = By.XPath("//textarea[@name='street']");
    readonly By txtBillingCity = By.XPath("//input[@name='city']");
    readonly By txtBillingProvince = By.XPath("//input[@name='province']");
    readonly By ddBillingProvincelist = By.XPath("//label[text()='Billing State/Province']/following::lightning-base-combobox-item/span[2]/span");

    readonly By ddStatusChile = By.XPath("//label[text()='Status']/following::button[1]");
    readonly By ddStatusList = By.XPath("//label[text()='Status']/following::lightning-base-combobox-item/span[2]/span");

    readonly By txtCUITNumber = By.XPath("//input[@name='C_U_I_T_Number__c']");
    readonly By txtBillingPostalCode = By.XPath("//input[@name='postalCode']");
    readonly By txtBillingCountry = By.XPath("//input[@name='country']");
    readonly By txtMailingCity = By.XPath("//input[@name='Mailing_City__c']");
    readonly By txtMailingPostalCode = By.XPath("//input[@name='Mailing_ZIP_Postal_Code__c']");
    readonly By btnClientSave = By.XPath("(//button[@name='SaveEdit'])[1]");
    readonly By btnClientSaveAndNew = By.XPath("//button[@name='SaveAndNew']");
    readonly By btnClientCancel = By.XPath("//button[@name='CancelEdit']");
    readonly By lblCreatedClient = By.XPath("//lightning-formatted-text[@class='custom-truncate']");
    readonly By clientFirstRecordlink = By.XPath("(//tr/th/span/a)[1]");
    readonly By btnEditRecord = By.XPath("//button[@name='Edit']");
    readonly By btnEditAction = By.XPath("//ul[@class='slds-button-group-list']/li//button[contains(text(),'Edit')]");
    readonly By btnMoreAction1_0 = By.XPath("(//ul[@class='slds-button-group-list']/li//button)[6]");
    readonly By btnMoreAction2_0 = By.XPath("(//ul[@class='slds-button-group-list']/li//button)[7]");
    readonly By btnMoreActionItems = By.XPath("//div[@class='slds-dropdown slds-dropdown_right']//runtime_platform_actions-action-renderer//a/span");
    readonly By txtFEINTaxID = By.XPath("//input[@name='Tax_ID__c']");
    readonly By lblFEINTaxID = By.XPath("//label[contains(text(),'FEIN / Tax ID')]");
    readonly By lblNewClientPage = By.XPath("//records-lwc-detail-panel/div/h2[contains(text(),'New Client: Client')]");
    readonly By btnAddressValidation = By.XPath("(//input[@value='Validate Address'])[1]");
    readonly By btnUpdateAddress = By.XPath("(//input[@value='Update'])[1]");
    readonly By lblAddressValidationmsg = By.XPath("(//b)[2]");
    
    readonly By btnCreditReport = By.XPath("(//button[contains(text(),'Credit Report')])[1] | (//button[contains(text(),'Credit Report')])[2]");
    readonly By creditReportPageContent = By.XPath("//div[@id='divReport']/h1[contains(text(),'Credit Report : ')]");
    readonly By txtSearchClient = By.XPath("//input[@name='Account-search-input']");
    readonly By lblSearchClient = By.XPath("//th/div/a/span[contains(@title,'Client Name')]");
    readonly By btnClose = By.XPath("//button[@title='Close this window']");
    readonly By lblDuplicateErrorMessage = By.XPath("//h2[@title='Similar Records Exist']");
    readonly By lblErrorWeHitASnag = By.XPath("//h2[text()='We hit a snag.']");
    readonly By lblSource = By.XPath("(//span[contains(text(),'Source')])[1]");
    readonly By lblSourceValue = By.XPath("//span[contains(text(),'Source')]/following::div[2]/span/span");
    readonly By btnSaveInViewMode = By.XPath("//span[contains(text(),'Save')]");
    readonly By cbAllAddressUpdate = By.XPath("//tbody/tr/td/input[@type='checkbox']");
    readonly By lblClientErrorMsg = By.XPath("//div[@class='slds-form-element__help slds-m-left_none']");
    readonly By lblClientErrorMsgInPageLevel = By.XPath("//div[@class='pageLevelErrors']//ul/li");

    // Treaty info
    readonly By txtInputQuickfind = By.XPath("//input[@placeholder='Quick Find']");
    readonly By lnkMetadata = By.XPath("//a/mark[text()='Custom Metadata Types']");
    readonly By btnNewTreaty = By.XPath("//span/input[@name='new']");
    readonly By txtTreatyStartDate = By.XPath("(//span[@class='dateInput dateOnlyInput']/input)[1]");
    readonly By txtTreatyEndDate = By.XPath("(//span[@class='dateInput dateOnlyInput']/input)[2]");
    readonly By txtLabel = By.XPath("//input[@id='MasterLabel']");
    readonly By txtTreatyYearTableName = By.XPath("//input[@id='DeveloperName']");
    readonly By btnSaveTreaty = By.XPath("(//input[@name='save'])[1]");
    readonly By lblErrorMsgTreatyYear = By.XPath("//div[@id='errorDiv_ep']");

    // Chile 
    readonly By txtCorreo = By.XPath("//input[@name='Correo_Electronico_Representante_Legal__c']");
    readonly By txtInsured = By.XPath("//input[@name='Insured__c']");
    readonly By txtRUTInsured = By.XPath("//input[@name='RUT_Insured__c']");
    readonly By txtEffectiveDate = By.XPath("//input[@name='Effective_Date__c']");
    readonly By txtExpiredDate = By.XPath("//input[@name='Expiration_Date__c']");
    readonly By txtRepresentateLegal = By.XPath("//input[@name='Representante_Legal__c']");
    readonly By txtLineOfBusiness = By.XPath("//input[@name='Line_of_Business__c']");
    readonly By txtCorreoRepresentateLegal = By.XPath("//input[@name='Correo_Electronico_Representante_Legal__c']");


    //dropdown
    readonly By ddClientType = By.XPath("//label[contains(text(),'Client Type')]/following::button[1]");
    readonly By ddSelectedClientType = By.XPath("//label[contains(text(),'Client Type')]/following::button[1]/span");
    //readonly By ddClientType = By.XPath("//label[contains(text(),'Client Type')]");

    readonly By ddClientTypeValue = By.XPath("//label[contains(text(),'Client Type')]/following::div//button/parent::div/following-sibling::div//span[@class='slds-media__body']/span");

    //readonly By ddmailingCountry = By.XPath("//label[starts-with(text(),'Mailing Country')]");
    readonly By ddBillingCountry = By.XPath("//label[text()='Billing Country']");
    readonly By ddBillingCountryValue = By.XPath("//label[text()='Billing Country']/following::div//span[@class='slds-media__body']/span");

    // chile
    readonly By ddStatus = By.XPath("//label[text()='Status']");
    readonly By ddStatusValue = By.XPath("//label[text()='Status']/following::div//span[@class='slds-media__body']/span");

    readonly By ddMailingCountryValue = By.XPath("//label[contains(text(),'Mailing Country')]/following::div//span[@class='slds-media__body']/span");

    readonly By ddValue = By.XPath("//div/lightning-base-combobox-item/span/span");

    readonly By ddMailingState = By.XPath("//label[contains(text(),'Mailing State/Province (US & Canada)')]");
    readonly By ddMailingStateValue = By.XPath("//label[contains(text(),'Mailing State/Province (US & Canada)')]/following::div//span[@class='slds-media__body']/span");

    // Error messages
    readonly By errorMessage = By.XPath("//div[starts-with(@id,'help-message')]");
    readonly By closeErrorDialog = By.XPath("//button[starts-with(@title,'Close error dialog')]");
    readonly By errorDialogMessage = By.XPath("//records-record-edit-error//ul/li");
    readonly By fraSetup = By.XPath("//iframe[starts-with(@name,'vfFrameId')]");
    readonly By errorMsgBox = By.XPath("//div/h2[contains(@title,'We hit a snag')]");

    readonly By lblSourceValueInProfile = By.XPath("//span[contains(text(),'Source')]/following::td[1]");

    public ClientsPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
    }

    public static int GenerateRandomNumbers()
    {
        Random random = new Random();
        return random.Next(999999999);
    }

    public static int GenerateRandomNumbers(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public void NavigateToClientsPage()
    {
        Boolean flag = false;
        driver.WaitForElementToPresent(lblAppName);
        if ((driver.GetTextFromElement(lblAppName) == "Starr Companies Underwriting") || (driver.GetTextFromElement(lblAppName) == "Starr App"))
        {
            driver.JSClick(tabClients);
        }

        if (driver.GetTextFromElement(lblAppName) == "Starr Underwriting Lightning")
        {
            driver.WaitAndClick(lnkMore);
            flag = driver.SelectFromListUsingJS(mnuItems, "Clients");
            if (flag == false)
            {
                driver.JSClick(tabClients);
            }
        }
        Assert.IsTrue(driver.WaitForElementToPresent(btnNewClient), "Could not navigate to Clients Page");
        Log("NAVIGATED TO CLIENTS PAGE");
    }

    public void ClickOnEditButton()
    {
        Boolean flag = false;
        flag = driver.IsDisplayed(btnEditAction);
        if (flag == true)
        {
            driver.WaitAndClick(btnEditAction);
        }
        if (flag == false)
        {
            if ((driver.GetTextFromElement(lblAppName) == "Starr Companies Underwriting") || (driver.GetTextFromElement(lblAppName) == "Starr App"))
            {
                driver.WaitAndClick(btnMoreAction1_0);
            }

            if (driver.GetTextFromElement(lblAppName) == "Starr Underwriting Lightning")
            {
                driver.WaitAndClick(btnMoreAction2_0);
            }
            driver.SelectFromListUsingJS(btnMoreActionItems, "Edit");
        }

        Assert.IsTrue(driver.WaitForElementToPresent(txtClientName), "Could not navigate to Client Edit Page");
        Log("CLIENT RECORD Edit PAGE IS DISPLAYED");
    }


    public void ValidateAddressValidationPage()
    {
        //driver.WaitForElementToPresent(fraSetup);

        System.Threading.Thread.Sleep(3000);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(fraSetup));
        driver.SwitchToFrame(0);

        Assert.IsTrue(driver.IsDisplayed(btnUpdateAddress), "COULD NOT NAVIGATE TO ADDRESS VALIDATION PAGE");
        Log("NAVIGATED TO ADDRESS VALIATION PAGE");
    }

    public void ClickOnAddressValidationButton()
    {
        driver.WaitForElement(btnAddressValidation);
        driver.WaitAndClick(btnAddressValidation);
        driver.WaitForNextPage();
        Log("CLICKED ON ADDRESS VALIDATION PAGE");
    }



    public void NavigateToClientsPagefromClientDetailsPage()
    {
        driver.WaitForElementToPresent(tabClients);
        driver.WaitAndClick(tabClients);
        Assert.AreEqual(driver.Title.ToString(), "Recently Viewed | Clients | Salesforce", "COULD NOT NAVIGATED TO CLIENT PAGE");
        Log("NAVIGATED TO CLIENTS PAGE");
    }

    public void OpenNewClientDetailsPage()
    {
        driver.CaptureScreen(_scenarioContext);
        driver.WaitForElementToPresent(btnNewClient);
        driver.WaitAndClick(btnNewClient);


    }

    public void FillDetailsInClientsCreationPage(string[] dataAdd, string SubmissionVersion)
    {
        _scenarioContext.Add("CurrentSubmissionVersion", SubmissionVersion);
        int recordValue = GenerateRandomNumbers();
        ClientName = dataAdd[0] + recordValue;
        string BillingStreet = dataAdd[1];
        string BillingCity = dataAdd[2];
        string BillingProvince = dataAdd[3];
        string BillingPostalCode = dataAdd[4];
        string BillingCountry = dataAdd[5];
        string FEINTaxID = dataAdd[6];
        ScenarioCount = dataAdd[7];
        string ClientType = dataAdd[8];

        driver.WaitForElementToPresent(txtBusinessName);
        driver.ClearAndSend(txtBusinessName, "Test");
        driver.CaptureScreen(_scenarioContext);

        driver.WaitAndClick(btnSearchClient);

        driver.WaitTillElementIsClickable(btnCreateNew);
        driver.CaptureScreen(_scenarioContext);

        driver.WaitAndClick(btnCreateNew);
        Assert.IsTrue(driver.WaitForElementToPresent(txtClientName), "NEW CLIENT CREATION PAGE IS NOT DISPLAYED");
        Log("CLIENT CREATION PAGE IS DISPLAYED");


        if (ClientName != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtClientName, ClientName), "ClientName Was NOT Inputed");
        _scenarioContext["ClientName" + loggingStep.rowNo] = ClientName;

        if (SubmissionVersion.Equals("Submission1_0"))
        {
            SelectRandomValueFromDropdown(ddClientType, ddClientTypeValue, "Project Name");
            _scenarioContext.Add("SelectedClientType", driver.GetTextFromElement(ddSelectedClientType));
        }
        if (SubmissionVersion.Equals("Submission2_0"))
        {
            string[] ClientType2_0 = ClientType.Split(",");
            SelectRandomValueFromDropdown(ddClientType, ddClientTypeValue, ClientType2_0[GenerateRandomNumbers(1, ClientType2_0.Length)]);
            _scenarioContext.Add("SelectedClientType", driver.GetTextFromElement(ddSelectedClientType));
        }

        SelectRandomValueFromDropdown(ddBillingCountry, ddBillingCountryValue, BillingCountry);

        driver.ScrollToCenter(txtBillingProvince);
        //driver.WaitAndClick(txtBillingProvince);
        //System.Threading.Thread.Sleep(2000);
        //driver.SelectFromListUsingJS(ddBillingProvincelist, BillingProvince);
        SelectRandomValueFromDropdown(txtBillingProvince, ddBillingProvincelist, BillingProvince);

        driver.ScrollToCenter(txtBillingStreet);
        Assert.IsTrue(driver.ClearAndSend(txtBillingStreet, BillingStreet), "Billing Street NOT Inputed");

        driver.ScrollToCenter(txtBillingCity);
        Assert.IsTrue(driver.ClearAndSend(txtBillingCity, BillingCity), "BillingCity NOT Inputed");

        //driver.ScrollToCenter(txtBillingProvince);
        ////driver.WaitAndClick(txtBillingProvince);
        ////System.Threading.Thread.Sleep(2000);
        ////driver.SelectFromListUsingJS(ddBillingProvincelist, BillingProvince);
        //SelectRandomValueFromDropdown(txtBillingProvince, ddBillingProvincelist, BillingProvince);

        driver.ScrollToCenter(txtBillingPostalCode);
        Assert.IsTrue(driver.ClearAndSend(txtBillingPostalCode, BillingPostalCode), "Billing Postal Code NOT Inputed");

        if (!_scenarioContext["CurrentSubmissionVersion"].ToString().Equals("Submission2_0"))
        {
            Assert.IsTrue(driver.ClearAndSend(txtCUITNumber, GenerateRandomNumbers().ToString()), "C.U.I.T NUMBERS ARE NOT INPUTED");
        }

        //driver.ScrollToCenter(txtDunsNumber);
        //Assert.IsTrue(driver.ClearAndSend(txtDunsNumber, GenerateRandomNumbers().ToString()), "DUNS NUMBER NOT INPUTED");
        //driver.CaptureScreen(_scenarioContext);


        Log("CLIENT DETAILS ARE FILLED SUCCESSFULLY");
    }

    public void SelectRandomValueFromDropdown(By ddField, By ListElements, string FieldValue)
    {
        driver.JSClick(ddField);
        driver.WaitForElementToPresent(ListElements);
        ChildValues = driver.ListOfElements(ListElements);
        if (FieldValue.ToLower().Trim().Equals("random"))
        {
            Console.WriteLine("------------------Random----------------------------");
            Console.WriteLine("Picklist Count = " + ChildValues.Count);
            int RandomeValue = GenerateRandomNumberForPickListValues(1, ChildValues.Count - 1);
            Console.WriteLine("Randome number is = " + RandomeValue);
            Console.WriteLine("Selecting Randomevalue is = " + ChildValues[RandomeValue].GetElementText());
            string RandomSelectingValue = ChildValues[RandomeValue].GetElementText();

            foreach (IWebElement child in ChildValues)
            {
                Console.WriteLine(child.GetElementText());
                if (child.GetElementText().Trim().Equals(RandomSelectingValue.Trim()))
                {
                    driver.MoveToTheElementAndClick(child);
                }
            }
        }
        else
        {
            Console.WriteLine("------------------Not a Random Value----------------------------");
            foreach (IWebElement child in ChildValues)
            {
                Console.WriteLine(child.GetElementText());
                if (child.GetElementText().Trim().Equals(FieldValue))
                {
                    driver.MoveToTheElement(child);
                    driver.MoveToTheElementAndClick(child);
                }
            }
        }
    }

    public static int GenerateRandomNumberForPickListValues(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    //Method to Select value from the dropdown
    public bool SelectValueFromDropdown(By Elements, String Value)
    {
        IList<IWebElement> elements = driver.ListOfElements(Elements);
        foreach (IWebElement element in elements)
        {
            if (element.GetElementText() == Value.ToString())
            {
                driver.MoveToTheElementAndClick(element);
                return true;
            }
        }
        return false;
    }

    public bool SelectAllItemFromElement(By Elements)
    {
        IList<IWebElement> elements = driver.ListOfElements(Elements);
        foreach (IWebElement element in elements)
        {
             Assert.IsTrue(driver.WaitAndClick(element),"COULD NOT CLICK ON THE ITEM");
            if(!element.Selected)
            {
                return false;
            }
        }
        return true;
    }

    public void ClickOnSaveButton()
    {
        Assert.IsTrue(driver.WaitAndClick(btnClientSave), "COULD NOT Clicked on Save button");
        System.Threading.Thread.Sleep(2000);
        if (driver.IsDisplayed(lblDuplicateErrorMessage))
        {
            if (driver.IsDisplayed(btnClientSave))
            {
                driver.WaitAndClick(btnClientSave);
            }
        }
        if (driver.IsDisplayed(lblErrorWeHitASnag))
        {
            Log("UNEXPECTED ERROR MESSAGE IS DISPLAYED ");
            Assert.Fail("UNEXPECTED ERROR MESSAGE IS DISPLAYED ");
        }
        driver.WaitTillOverlayDisappears(btnClientSave);
        Log("CLICKED ON SAVE BUTTON");
    }

    public void ClickedOnSaveButtonToVerifyErrorMessage()
    {
        Assert.IsTrue(driver.WaitAndClick(btnClientSave), "COULD NOT Clicked on Save button");
        System.Threading.Thread.Sleep(2000);
        Log("CLICKED ON SAVE BUTTON");
    }

    public void ClickOnCancelButton()
    {
        driver.WaitAndClick(btnClientCancel);
        Log("CLICKED ON CANCEL BUTTON");
    }

    public void ClickOnSaveAndNewButton()
    {
        driver.WaitAndClick(btnClientSaveAndNew);
        Log("CLICKED ON SAVE AND NEW BUTTON");
    }

    public void VerifyCreatedClient()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblCreatedClient));
            driver.CaptureScreen(_scenarioContext);
            Assert.AreEqual(ClientName.ToString(), driver.GetTextFromElement(lblCreatedClient), "CLIENT RECORD IS NOT CREATED");
            Log("CLIENT RECORD IS CREATED SUCCESSFULLY");
            CreatePropertyFile();
        }
        catch (Exception e)
        {
            Log("CLIENT RECORD IS NOT CREATED");
            Console.WriteLine("CLIENT RECORD IS NOT CREATED");
        }


    }

    public void CreatePropertyFile()
    {
        if ( _scenarioContext["CurrentSubmissionVersion"].ToString().Equals("Submission1_0"))
        {
            if(_loggingStep.rowNo.Contains("1"))
            {
                File.WriteAllText(Clients1_0FilePath + "Client_Create.txt", "ClientName = " + ClientName);
            }
            if (_loggingStep.rowNo.Contains("2"))
            {
                File.WriteAllText(Clients1_0FilePath + "Client_Update.txt", "ClientName = " + ClientName);
            }
            if (_loggingStep.rowNo.Contains("3"))
            {
                File.WriteAllText(Clients1_0FilePath + "Client_Validation.txt", "ClientName = " + ClientName);
            }

        }

        if (_scenarioContext["CurrentSubmissionVersion"].ToString().Equals("Submission2_0"))
        {
            if (_loggingStep.rowNo.Contains("1"))
            {
                File.WriteAllText(Clients2_0FilePath + "Client_Create.txt", "ClientName = " + ClientName);
            }
            if (_loggingStep.rowNo.Contains("2"))
            {
                File.WriteAllText(Clients2_0FilePath + "Client_Update.txt", "ClientName = " + ClientName);
            }
        }

    }

    public static string ReadDataFromFile(string FilePath)
    {
        string[] AllLines = File.ReadAllLines(FilePath);
        string[] value;
        string returnValue;
        foreach (string lines in AllLines)
        {
            value = lines.Split("=");
            returnValue = value[1];
            return returnValue;
        }
        Console.WriteLine("Could not fetch Data from the file");
        return null;
    }


    public void VerifyUpdatedClientRecord()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        try
        {
           Log(driver.GetTextFromElement(lblCreatedClient) + "  CLIENT RECORD IS UPDATED SUCCESSFULLY");
        }
        catch(Exception e)
        {
            Log("CLIENT RECORD IS NOT UPDATED");
        }
        
       
    }

    public void ClickOnFirstClientRecord()
    {
        driver.WaitAndClick(clientFirstRecordlink);
        Log("CLIENT RECORD IS DISPLAYED");
    }

    public void ClickAndEditFirstClientRecord()
    {
        driver.WaitAndClick(clientFirstRecordlink);
        ClickOnEditButton();
        //driver.WaitForElement(btnEditRecord);
        //driver.JSClick(btnEditRecord);
        //driver.WaitForElement(txtClientName);
        //Log("CLIENT RECORD DETAIL PAGE IS DISPLAYED");
    }

    public void SelectClientType(string clientType)
    {
        driver.ScrollToCenter(ddClientType);
        driver.WaitAndClick(ddClientType);
        SelectValueFromDropdown(ddValue, clientType.ToString());
        Log("CLIENT TYPE IS SELECTED AS " + clientType);
    }

    public void VerifyFEINErrorMessage()
    {
        driver.ScrollToCenter(closeErrorDialog);
        Assert.IsTrue(driver.IsDisplayed(closeErrorDialog), "Error Dialog box is not displayed");
        driver.WaitAndClick(closeErrorDialog);
        driver.ScrollToCenter(errorMessage);
        Assert.AreEqual(driver.GetTextFromElement(errorMessage).ToString(), "FEIN/TAX ID must be blank if the Client Type is Individual.", "Expected Error message is not displayed");
        Log("FEIN/TAX ID - Error message is displayed");
    }

    public void ClearValueFromTextbox()
    {
        driver.WaitForElement(txtFEINTaxID).Clear();
        //Screenshot screenshot = driver.TakeScreenshot();
        Log("Value from FEINT textbox is cleared");
    }

    public void UpdateClientsDetails(string[] dataUpdate)
    {
        int randomeNumber = GenerateRandomNumbers();
        string FEINTaxId = dataUpdate[0] + randomeNumber;

        if (!_scenarioContext["SelectedClientType"].ToString().Equals("Individual"))
        {

            driver.ScrollToCenter(lblFEINTaxID);
            Assert.IsTrue(driver.IsDisplayed(txtFEINTaxID), "FEIN / Tax ID field is not displayed");
            Assert.IsTrue(driver.ClearAndSend(txtFEINTaxID, FEINTaxId), "FEINTaxId NOT Inputed");
            Log("CLIENT DETAILS ARE UPDATED SUCCESSFULLY");

        }
    }


    public void VerifyErrorMessage()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(errorMsgBox), "Error message box is not displayed");
        Log("Error message box is displayed");
        driver.ScrollToCenter(errorDialogMessage);
        Assert.AreEqual(driver.GetTextFromElement(errorDialogMessage).ToString(), "Mailing Street/Mailing City/Mailing State Province for US and Canada/Mailing Country/Mailing Zip Postal Code Information is required.", "Error message is not displayed");
        Log("Error message is displayed");
    }

    public void EnterClientsDetailsToVerifyErrorMsg(string[] data)
    {
        int recordValue = GenerateRandomNumbers();
        ClientName = data[0] + recordValue;
        string MailingStreet = data[1] + recordValue;
        string BillingStreet = data[2];
        string BillingCity = data[3];
        string BillingProvince = data[4];
        string BillingPostalCode = data[5];
        string BillingCountry = data[6];
        string MailingCity = data[7] + recordValue;
        string MailingPostalCode = data[8];

        if (ClientName != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtClientName, ClientName), "ClientName Was NOT Inputed");
        if (MailingStreet != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtMailingStreet, MailingStreet), "Mailing Street Was NOT Inputed");
        if (BillingStreet != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtBillingStreet, BillingStreet), "Billing Street NOT Inputed");
        if (BillingCity != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtBillingCity, BillingCity), "BillingCity NOT Inputed");
        if (BillingProvince != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtBillingProvince, BillingProvince), "BillingProvince NOT Inputed");
        if (BillingPostalCode != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtBillingPostalCode, BillingPostalCode), "Billing Postal Code NOT Inputed");
        if (BillingCountry != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtBillingCountry, BillingCountry), "Billing Country NOT Inputed");
        if (MailingCity != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtMailingCity, MailingCity), "Billing Country NOT Inputed");
        if (MailingPostalCode != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtMailingPostalCode, MailingPostalCode), "Mailing Postal Code NOT Inputed");
        Log("Values are filled in the clients page");
    }

    public void ClickOnCreditReport()
    {

        driver.JSClick(btnCreditReport);
        System.Threading.Thread.Sleep(2000);
        Log("Credit Report button is clicked");
    }

    public void VerifyCreditReportPage()
    {
        driver.WaitForNextPage();
        SwitchToWindow();
        Log("Switch to Credit Report Page");
        Assert.IsTrue(driver.WaitForElementToPresent(creditReportPageContent), "Credit Report Page is not displayed");
        Log("Navigated to Credit Report Page");
    }


    public void SwitchToWindow()
    {
        string originalWindow = driver.CurrentWindowHandle;
        foreach (string window in driver.WindowHandles)
        {
            if (originalWindow != window)
            {
                driver.SwitchTo().Window(window);
                break;
            }
        }
    }

    public void SearchForClientRecord()
    {
        driver.WaitAndClick(txtSearchClient);
        driver.ClearAndSend(txtSearchClient, "GSM Holdings Limited &/or GSM International Limited");
        driver.JSClick(lblSearchClient);
        System.Threading.Thread.Sleep(3000);

    }

    public void ThenUserVerifiedDuplicateErrorMessage()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(lblDuplicateErrorMessage),"DUPLICATE ERROR MESSAGE IS NOT DISPLAYED");
        Log("DUPLICATE ERROR MESSAGE IS DISPLAYED");
        Console.WriteLine("DUPLICATE ERROR MESSAGE IS DISPLAYED");
    }

    public void ThenUserVerifySourceFieldIsNotEditable()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(lblSourceValue),"SOURCE FIELD IS NOT AVAILABLE");
        driver.MoveToTheElementAndDoubleClick(lblSourceValue);
        Assert.IsFalse(driver.IsDisplayed(btnSaveInViewMode), "SOURCE FIELD IS EDITABLE");
        Log("SOURCE FIELD IS NOT EDITABLE");
    }

    public void ThenUserSelectedAllTheCheckboxes()
    {
        Assert.IsTrue(SelectAllItemFromElement(cbAllAddressUpdate),"CHECKBOXES ARE NOT SELECTED");
        Log("ALL CHECKBOXES ARE SELECTED");
    }

    public void ThenUserClickedOnUpdateButton()
    {
        Assert.IsTrue(driver.WaitAndClick(btnUpdateAddress),"COULD NOT CLICK ON THE UPDATE BUTTON");
        System.Threading.Thread.Sleep(2000);
        Log("CLICKED ON UPDATE BUTTON");
    }

    public void ThenUserClickedOnValidateAddressButton()
    {
        System.Threading.Thread.Sleep(2000);
        Assert.IsTrue(driver.WaitAndClick(btnAddressValidation), "COULD NOT CLICK ON THE UPDATE BUTTON");
        Log("CLICKED ON UPDATE BUTTON");
    }
    public static string GetCurrentDate(string Format)
    {
        string CurrentDate = DateTime.Now.ToString(Format);
        Console.WriteLine(CurrentDate);
        return CurrentDate;
    }

    public void ThenUserVerifyAddressValidateUpdatedDate()
    {
        System.Threading.Thread.Sleep(2000);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(fraSetup));
        driver.SwitchToFrame(0);
        string AddressValidaitonmsg = driver.GetTextFromElement(lblAddressValidationmsg);
        Console.WriteLine("DISPLAYED ADDRESS VALIDATION MESSAGE = " +AddressValidaitonmsg);
        Assert.AreEqual(AddressValidaitonmsg, "Address Validation was last performed on: " + GetCurrentDate("yyyy-MM-dd"), "DISPLAYED MESSAGE ARE NOT EQUAL");
    }

    public void ThenUserCheckForTheSourceValueInProfilePage()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(lblSourceValueInProfile));
        driver.MoveToTheElement(lblSourceValue);
        Source = driver.GetTextFromElement(lblSourceValueInProfile);
        Log("CAPTURED SOURCE VALUE UNDER THIS PROFILE");
    }


    public void ThenUserVerifyTheSourceValueFor(string SFSourceValue)
    {
        driver.WaitForElementToPresent(lblSource);
        string clientSourceValue = "";
        if (SFSourceValue.Equals("1.0"))
        {
            clientSourceValue = driver.GetTextFromElement(lblSourceValue);
            Assert.IsTrue(clientSourceValue.Equals(SFSourceValue), "SOURCE VALUE IS NOT VALID");
            Log("SOURCE VALUE IS SF 1.0");
            Console.WriteLine("SOURCE VALUE IS SF 1.0");
        }
        if (SFSourceValue.Equals("2.0"))
        {
            clientSourceValue = driver.GetTextFromElement(lblSourceValue);
            Assert.IsTrue(clientSourceValue.Equals(SFSourceValue), "SOURCE VALUE IS NOT VALID");
            Log("SOURCE VALUE IS SF 2.0");
            Console.WriteLine("SOURCE VALUE IS SF 2.0");
        }
        if (SFSourceValue.Equals("Multiple"))
        {
            if(!driver.IsDisplayed(lblSourceValue))
            {
                clientSourceValue = driver.GetTextFromElement(lblSourceValue);
                Assert.IsTrue(clientSourceValue.Equals(""), "SOURCE VALUE IS NOT VALID");
                Log("SOURCE VALUE IS MULTIPLE");
                Console.WriteLine("SOURCE VALUE IS MULTIPLE");
            }
            //clientSourceValue = driver.GetTextFromElement(lblSourceValue);
            //Assert.IsTrue(clientSourceValue.Equals(""), "SOURCE VALUE IS NOT VALID");
            
        }
    }

    public void ThenUserVerifyTheErrorMessageForClientAs(string ErrorMessage)
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
        catch(Exception e)
        {
            Console.WriteLine(e.ToString());
            Assert.Fail("EXPECTED ERROR MESSAGE IS NOT DISPLAYED : "+ ErrorMessage);
        }        
    }

    public void ThenUserVerifyTheErrorMessageForClientInPageLevel(string ErrorMessage)
    {
        string DisplayedErrorMsg = "";
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblClientErrorMsgInPageLevel));
            DisplayedErrorMsg = driver.GetTextFromElement(lblClientErrorMsgInPageLevel);
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

    public void ThenUserSearchForAndNavigateTo(string searchValue, string displayedValue)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(txtInputQuickfind));
        driver.SendKeysOrText(txtInputQuickfind, searchValue);
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lnkMetadata));
        driver.JSClick(lnkMetadata);
        Log("NAVIGATED TO CUSTOM META DATA TYPES");
    }

    public void ThenUserClickedOnNewButtonInTreatyYearTables()
    {
        System.Threading.Thread.Sleep(2000);
        driver.SwitchToFrame(0);
        driver.CaptureScreen(_scenarioContext);
        driver.JSClick(btnNewTreaty);
        System.Threading.Thread.Sleep(2000);
        Console.WriteLine("CLICKED ON NEW TREATY BUTTON");
        Log("CLICKED ON NEW TREATY BUTTON");
    }

    public void ThenUserFillTheValuesInTreatyTableFields()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

        try
        {
            System.Threading.Thread.Sleep(2000);
            driver.SwitchToFrame(0);
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(txtTreatyStartDate));
            driver.ClearAndSend(txtTreatyStartDate, "1/1/2025");
            driver.ClearAndSend(txtTreatyEndDate, "1/1/2023");
            driver.ClearAndSendTextAndPressTab(txtLabel, "test");
            Console.WriteLine("FILLED THE DATA IN TREATY CREATION FORM");
            Log("FILLED THE DATA IN TREATY CREATION FORM");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("COULD NOT FIND THE FIELD START DATE");
            Log("COULD NOT FIND THE FIELD START DATE");
            Assert.Fail("COULD NOT FIND THE FIELD START DATE");
        }
    }

    public void ThenUserClickedOnSaveButtonInTreatTableFields()
    {
        driver.JSClick(btnSaveTreaty);
        Console.WriteLine("CLICKED ON SAVE BUTTON IN TREATY YEAR");
    }

    public void ThenUserVerifyTheErrorMessageInTreatyTableAs(string errorMsg)
    {
        WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblErrorMsgTreatyYear));
            string DisplayederrorMsg = driver.GetTextFromElement(lblErrorMsgTreatyYear);
            Assert.IsTrue(DisplayederrorMsg.Contains(errorMsg), "DISPLAYED ERROR MESSAGE ARE NOT EQUAL");
            Log("EXPECTED ERROR MESSAGE IS DISPLAYED " + errorMsg);
        }
        catch(Exception e)
        {
            driver.CaptureScreen(_scenarioContext);
            Log("TREATY YEAR CREATED SUCCESSFULLY WITHOUT THORWING EXPECTED ERROR");
            Assert.Fail("TREATY YEAR CREATED SUCCESSFULLY WITHOUT THORWING EXPECTED ERROR");
        }
    }

    public void ThenUserFillTheValueInChileManualPolicyNumberLog()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(txtLineOfBusiness));
        driver.ClearAndSend(txtInsured, "Polpaico");
        driver.ClearAndSend(txtRUTInsured, GenerateRandomNumbers().ToString());
        driver.ClearAndSend(txtEffectiveDate, "7/11/2022");
        driver.ClearAndSend(txtExpiredDate, "7/11/2025");
        driver.ScrollToCenter(txtExpiredDate);
        driver.ClearAndSend(txtRepresentateLegal, "Test");
        driver.ScrollToCenter(txtLineOfBusiness);
        driver.ClearAndSend(txtLineOfBusiness, "Test");
        driver.ScrollToCenter(txtCorreoRepresentateLegal);
        //driver.ClearAndSend(txtCorreoRepresentateLegal, "Tkc");
        SelectRandomValueFromDropdown(ddStatusChile, ddStatusList, "Active");

        System.Threading.Thread.Sleep(5000);
    }

    public void ThenUserAddedTheEmailIdAs(string email)
    {
        
        //SelectRandomValueFromDropdown(ddStatus,ddStatusValue, "Active");
        
        driver.ScrollToCenter(txtCorreo);
        driver.ClearAndSend(txtCorreo, email);
        SelectRandomValueFromDropdown(ddStatusChile, ddStatusList, "Active");
        Console.WriteLine("ENTERED VALUE IN CORREO FIELD");
    }

    public void ThenUserFillRemainngFieldsInChileManualPolicyNumberLogForm()
    {
        WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(txtLineOfBusiness));
        driver.ClearAndSend(txtInsured, "Polpaico");
        driver.ClearAndSend(txtRUTInsured, GenerateRandomNumbers().ToString());
        driver.ClearAndSend(txtEffectiveDate, "7/11/2022");
        driver.ClearAndSend(txtExpiredDate, "7/11/2025");
        driver.ScrollToCenter(txtExpiredDate);
        driver.ClearAndSend(txtRepresentateLegal, "Test");
        driver.ScrollToCenter(txtLineOfBusiness);
        driver.ClearAndSend(txtLineOfBusiness, "Test");
        driver.ScrollToCenter(txtCorreoRepresentateLegal);
        driver.ClearAndSend(txtCorreoRepresentateLegal, "Test@gmail.com");
        SelectRandomValueFromDropdown(ddStatusChile, ddStatusList, "Active");

        System.Threading.Thread.Sleep(5000);
    }

    public void ThenUserUpdatedTheFieldInChileManualPolicyRecord()
    {
        System.Threading.Thread.Sleep(3000);
        driver.ClearAndSend(txtInsured, "Polpaico Test");
    }
}
