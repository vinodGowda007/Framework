using AngleSharp.Dom;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce3.Steps;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Config;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SalesForce3.Pages;
public class SubmissionPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;

    public static string Directoryath = System.Environment.CurrentDirectory;
    public static bool flagCancelled = false;

    // -- Changes to implement in Ravindra VDI
    public static string BaseURL = Directoryath.Replace("bin\\Debug\\net6.0", "") + "Data/DataSet/", UpdatedProjectName, ActionButtonName;

    public static string Submission1_0ValidationRecords = BaseURL + "Sub1_0_Validation/"; // 1.0 Validation created submisison's RECORD INFORMATION WILL STORE IN THIS LOCATION
    public static string Submission1_0Filsepath = BaseURL + "Sub1_0_Scenario/";  // All 1.0 RECORD INFORMATION WILL STORE IN THIS LOCATION
    public static string Renewal1_0Filsepath = BaseURL + "Renewal1_0/";  // 1.0 RENEWAL RECORD INFORMATION WILL STORE IN THIS LOCATION
    public static string Section_Filsepath = BaseURL + "Section/";  // All 1.0 SECTION RECORD INFORMATION WILL STORE IN THIS LOCATION
    public static string Terrorism_Filsepath = BaseURL + "Terrorism/";  // All 1.0 TERRORISM RECORD INFORMATION WILL STORE IN THIS LOCATION
    public static string Endorsement_Filsepath = BaseURL + "Endorsement/";  // All 1.0 TERRORISM RECORD INFORMATION WILL STORE IN THIS LOCATION

    public static int flag = 0;
    public static IList<IWebElement> ActionField;
    public static IList<IWebElement> SelectLookUpValues;
    public static IList<IWebElement> FieldNames;
    public static IList<IWebElement> AllTabs;
    public static IList<IWebElement> ChildValues;
    public static IList<IWebElement> SelectLookUpValuesClick;

    
    public static int TabCount ;

    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);
    public static string StarrSubmissionNumber = "";

    
    // Release Block
    readonly By lblStarrSubmissionNumber = By.XPath("//p[contains(text(),'Starr Submission Number')]/following::p[1]//lightning-formatted-text");

    readonly By btnReleaseBlock = By.XPath("//input[@value='Release Block']");
    readonly By lblReleasedConfirmationMsg = By.XPath("//div[@escape='False']/h2");

    readonly By lnkStarrSubmissionNumber = By.XPath("//tbody/tr/td[12]/a");
    readonly By tblRow = By.XPath("//tbody/tr");
    readonly By cbStarrSubmissionNumber = By.XPath("./td/input");


    //Tab - PC Coding
    readonly By tabSubmissionDetails2_0 = By.XPath("//a[@id='SubmissionDetailsTab__item']");
    readonly By tabPCCodin2_0 = By.XPath("//a[@id='PCCodingTab__item']");
    readonly By tabReInsurance2_0 = By.XPath("//a[@id='ReinsuranceTab__item']");
    readonly By lblTPA = By.XPath("//label[contains(text(),'TPA')]");
    readonly By lblTreatyApplies = By.XPath("//label[contains(text(),'Treaty Applies')]");
    readonly By picklistProfitCenterValue = By.XPath("//button[@name='Profit_Center__c']/span");
    readonly By lblClientName1_0 = By.XPath("//label[contains(text(),'Client Name')]");

    // Tab - Policy
    readonly By tabPolicy2_0 = By.XPath("//a[@id='customTab3__item']");
    readonly By tabPolicyEditMode2_0 = By.XPath("//a[@id='PolicyTab__item']");


      // Tab - Producer
    readonly By tabProducer2_0 = By.XPath("//a[@id='customTab4__item']");
    readonly By tabProducer1_0 = By.XPath("//li/a[@id='customTab__item']");
    readonly By lblLicensingStatus = By.XPath("//div/span[contains(text(),'Licensing Status')]");

    // Submission 1.0 Web Elements
    readonly By tabCurrentPage = By.XPath("//one-app-nav-bar/nav[@role='navigation']/div//a[@aria-current='page']");
    readonly By tabMainSubmissionTabList = By.XPath("//nav[@role='navigation']/div[@role='list']/one-app-nav-bar-item-root[@aria-hidden='false']/a/span");
    readonly By lnkMore = By.XPath("//a/span[@class='slds-p-right_small']");
    readonly By tabHidenSubmissionTabList = By.XPath("//slot/one-app-nav-bar-menu-item/a/span/span");
    readonly By pages = By.XPath("//table");

    readonly By tabSubmission = By.XPath("//a[@title='Submissions']");

    //  All Tab in create mode
    readonly By TabCreateSubmissionlist = By.XPath("//ul[@class='slds-tabs_scoped__nav']/li/a | //ul[@class='slds-tabs_default__nav']/li/a");
    readonly By TabEditSubmissionList = By.XPath("//ul[@class='slds-tabs_scoped__nav']/li/a");

    // Submission List view page
    readonly By txtSearchSubmission = By.XPath("//div/input[@name='Opportunity-search-input']");
    readonly By btnNewSubmission = By.XPath("//a/div[@title='New']");
    readonly By TableHeaderSubmissionName = By.XPath("//a/span[@title='Submission Name']");
    readonly By listFirstRecord = By.XPath("(//tbody/tr/th/span/a)[1]");
    readonly By btnAllActionsList = By.XPath("(//div[@class='slds-dropdown slds-dropdown_right'])[3]//runtime_platform_actions-action-renderer//a/span");
    readonly By btnShowMoreActions = By.XPath("(//li/lightning-button-menu/button)[2]");

    // Submission Details Page  
    readonly By ddValue = By.XPath("//div/lightning-base-combobox-item/span/span");

    // Record Type Page
    readonly By lblRecordTypePage = By.XPath("//div/h2[contains(text(),'New Submission')]");

    // Selection of Record Type
    readonly By lblRecordType = By.XPath("//div[@class='changeRecordTypeOptionRightColumn']/span");
    readonly By btnNext = By.XPath("//button/span[contains(text(),'Next')]");

    readonly By ddSubmissionCurrencyCurrentValue = By.XPath("//button[@name='CurrencyIsoCode']/span");

    // Tab - Submission details
    readonly By lblsubmissionStatus = By.XPath("//span[contains(text(),'Mark Stage as Complete')] | //span[contains(text(),'Change Closed Stage')]");
    readonly By lblstageValue = By.XPath("((//div/span[contains(text(),'Stage')])[1]/following::div/span/span)[1]");
    readonly By lblErrorMessageInStageProgression = By.XPath("//div[@class='slds-notify__content']/p");
    readonly By lblErrorMessageInStageField = By.XPath("//label[contains(text(),'Stage')]/following::div[@aria-live='assertive'][1]");
    readonly By dateErrorMessageInMidTermBroker = By.XPath("//div[contains(text(),'Your entry does not match the allowed format dd-MMM-yyyy.')]");
    readonly By txtErrorMessageDisplayedField = By.XPath("//div[contains(text(),'Your entry does not match the allowed format dd-MMM-yyyy.')]//parent::lightning-datepicker/div//div/input");

    // Project Name 
    readonly By ddStageSave = By.XPath("//button[@name='StageName']");

    readonly By ddDeclainedReason = By.XPath("//button[@name='LostNTU_or_Declined_Reason__c']");
    readonly By ddDeclainedReasonValue = By.XPath("//lightning-combobox/label[contains(text(),'Declined Reason')]/following-sibling::div//lightning-base-combobox-item/span[2]/span");

    readonly By ddBusinessUnitSave = By.XPath("(//label[contains(text(),'Business Unit')])");

    // Tab - DatesAdditionalInfo
    readonly By txtPolicyEffectiveDate = By.XPath("//input[@name='Effective_Date__c']");

    // Save button
    //readonly By btnSaveSubmission = By.XPath("(//button[@name='save'])[1]");
    readonly By btnSaveSubmission = By.XPath("(//button[@name='save'])[1] | //button[@name='cancel']/parent::div/button[1]");
    readonly By btnSaveUpdateToRenewal = By.XPath("//footer/button/span[contains(text(),'Save')]");
        
    readonly By lblErrorMessages = By.XPath("//div[@class='slds-notify__content']/h2/following::p[1] | (//div[@class='toastContent slds-notify__content']/div/div/following::span[1])[1]");
    readonly By errmsgTypeToRenewal = By.XPath("//ul[@class='errorsList']/li");

    // After Saving submission
    readonly By lblStarSubmissionNo = By.XPath("//h1//lightning-formatted-text");
    readonly By lblStarrSubmissionName = By.XPath("//div/h1/slot/lightning-formatted-text");

    //IceCheckPage Check
    readonly By btnIceCheck = By.XPath("//button[@name='Opportunity.ICE_Check_20']");

    //readonly By btnRefresh = By.XPath("(//button[@name='refresh'])[1]");
    readonly By btnRefresh = By.XPath("//button[contains(text(),'Refresh')]");
    readonly By tabDetails = By.XPath("//ul/li/a[@id='detailTab__item']");

    readonly By tabMoreDetailsForAdmin = By.XPath("(//button[contains(text(),'More')])[2]");

    // Submission View objects Tabs
    readonly By tabListAllTabs = By.XPath("//ul[@class='slds-tabs_default__nav']/li/a");
    readonly By tabMoreDetails = By.XPath("//button[contains(text(),'More')]");
    readonly By tabActionValues = By.XPath("//div[@class='slds-dropdown slds-dropdown_right']//lightning-menu-item/a/span");

    // Licence Check Ojbects
    readonly By btnCloseLincenseCheck = By.XPath("//button[contains(text(),'Close')]");

    // Switch To Apps
    readonly By txtSearchSetup = By.XPath("//div[@class='rightScroll']");
    readonly By btnAppLauncher = By.XPath("//div[@class='slds-r5']");
    readonly By txtSearchAppandItem = By.XPath("//input[contains(@placeholder,'Search apps and items')]");
    readonly By optionAssumedInsurance = By.XPath("(//lightning-formatted-rich-text/span/p/b)[1]");
    readonly By btnNewProject = By.XPath("//a/div[@title='New']");
    readonly By lblAppName = By.XPath("//div[@class='slds-context-bar__primary navLeft']//span[@class='slds-truncate']");
    readonly By lnkLogOut = By.XPath("//div/a[contains(text(),'Log out as')]");


    // Reservation Check information
    readonly By btnMoreActions = By.XPath("//ul[@class='slds-button-group-list']/li//button[@class='slds-button slds-button_icon-border-filled']");
    readonly By btnMoreAction2_0 = By.XPath("(//ul[@class='slds-button-group-list']/li//button)[15]");
    readonly By btnActionNames = By.XPath("//div[@class='slds-dropdown slds-dropdown_right']//runtime_platform_actions-action-renderer//a/span");

    readonly By lblExecutedBy = By.XPath("(//p[@class='slds-p-horizontal--small']/div/div[2])[1]/lightning-formatted-text");
    readonly By lblClientName = By.XPath("(//p[@class='slds-p-horizontal--small']/div/div[2])[3]");
    readonly By lblClientNameInSubmission = By.XPath("//label[contains(text(),'Client Name')]");

    //readonly By lblNearFuzzyMatch = By.XPath("//th[contains(text(),'Total Starr Aggregated Limits')]");
    readonly By lblNearFuzzyMatch = By.XPath("//h5");

    // Read value from field during Detail view
    readonly By lblDetailsTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[1]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblDetailsFieldValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[1]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[1]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");

    // Un/Producer Tab values
    readonly By lblUWProducerTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[2]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblUWProducerFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[2]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[2]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");

    // SGS Tab values
    readonly By lblSGSTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[3]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblSGSFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[3]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[3]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");

    // Prm/Limits Tab values
    readonly By lblPrmLimitsTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[4]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblPrmLimitsFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[4]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[4]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");

    // Forecast Tab values
    readonly By lblForecastTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[5]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblForecastFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[5]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[5]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");

    // Currency Tab values
    readonly By lblCurrencyTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[6]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblCurrencyFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[6]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[6]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");

    // Policy Info Tab values
    readonly By lblPolicyInfoTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[7]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblPolicyInfoFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[7]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[7]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");

    // CheckList/Rating Tab values
    readonly By lblChecklistRatingTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[8]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblChecklistRatingFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[8]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[8]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");

    // 2.0 History Tab values
    readonly By lbl2_0HistoryTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[9]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lbl2_0HistoryFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[9]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[9]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");

    // Sys Info Tab values
    readonly By lblSysInfoTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[10]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblSysInfoFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[10]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[10]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");


    // Sys Info Tab values
    readonly By lblSnapshotInformationTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[11]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblSnapshotInformationFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[11]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[11]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");
    readonly By lblDDValues = By.XPath("//span[contains(text(),'Show All Results for')]");

    // Create Submission Page for 1.0 submission

    readonly By tabListOfAllSubmission1_0Tabs = By.XPath("//lightning-tab-bar/ul/li/a");
    readonly By lblPicklistChildValues = By.XPath("./following::div[2]//span[2]/span");
    readonly By lblLookUpFieldValue = By.XPath("//span[@class='slds-listbox__option-text slds-listbox__option-text_entity']//strong");

    readonly By lblLookUpFieldValue1 = By.XPath("./following::span[@class='slds-listbox__option-text slds-listbox__option-text_entity']//lightning-base-combobox-formatted-text");
    readonly By lblLookUpFieldValue2 = By.XPath("./following::span[@class='slds-listbox__option-text slds-listbox__option-text_entity']//strong");
    //readonly By lblLookupResult = By.XPath("./following::lightning-base-combobox-item[1]//lightning-icon");
    readonly By lblLookupResult = By.XPath("./following::lightning-base-combobox-item[1]//lightning-icon//lightning-primitive-icon/following::span[2]/span[1]/span[contains(text(),'Show All')]");

    readonly By txtLynxDate = By.XPath("(//input[@name='Lynx_Date__c'])[1]");
    readonly By txtLynxTime = By.XPath("(//input[@name='Lynx_Date__c'])[2]");
    // Create Submission Fields
    readonly By txtAreaSubmissionComment = By.XPath("//textarea[@name='Submission_Comments__c']");

    //LookUp Fields
    readonly By lbllkupLookupName1_0 = By.XPath("//form[@class='slds-form']//lightning-grouped-combobox//label");
    readonly By lkupLookupField1_0 = By.XPath("//form[@class='slds-form']//lightning-grouped-combobox//input");

    //Picklist Fields or Select Fields
    readonly By lblslctPickListName1_0 = By.XPath("//form[@class='slds-form']//lightning-picklist//label");
    readonly By slctPickListField1_0 = By.XPath("//form[@class='slds-form']//lightning-picklist//div[@class='slds-form-element__control']/lightning-base-combobox//button");

    readonly By slctEndrosementType = By.XPath("//label/span[contains(text(),'Endorsement Type')]/following::div[1]//select");

    //Date Picker Fields
    readonly By lbltxtDatePickerName1_0 = By.XPath("//form[@class='slds-form']//lightning-datepicker//label");
    readonly By txtDatePickerField1_0 = By.XPath("//form[@class='slds-form']//lightning-datepicker/div//input");

    //Checkbox Fields
    readonly By lblcbCheckBoxName1_0 = By.XPath("//form[@class='slds-form']//lightning-input//label/span");
    readonly By cbCheckBoxField1_0 = By.XPath("//form[@class='slds-form']//lightning-input//span//input");

    // Input Text Field
    readonly By lbltxtInputFieldName1_0 = By.XPath("//form[@class='slds-form']//lightning-input//label[@class='slds-form-element__label slds-no-flex']");
    readonly By txtInputField1_0 = By.XPath("//form[@class='slds-form']//lightning-input//div[@class='slds-form-element__control slds-grow']/input");

    // TextArea - Yet to implement
    readonly By lbltxtAreaFieldName1_0 = By.XPath("//form[@class='slds-form']//lightning-input-field/lightning-textarea/label");
    readonly By txtAreaField1_0 = By.XPath("//form[@class='slds-form']//lightning-input//div[@class='slds-form-element__control slds-grow']/input");


    // Edit Submission Info
    readonly By lblsubmisisonTabEditInfo1_0 = By.XPath("//div[@class='quick-actions-panel']//div[@id='SubmissionDetailsTab']//label");

    readonly By btnClearClient = By.XPath("(//button[@title='Clear Selection'])[1]");
    readonly By btnClearProjectContent = By.XPath("(//button[@title='Clear Selection'])[2]");
    readonly By btnClearAssignedUnderwriter = By.XPath("(//button[@title='Clear Selection'])[3]");
    readonly By btnClearLicensedProducer = By.XPath("//lightning-lookup-desktop//label[contains(text(),'Licensed Producer')]/following::button[1][@title='Clear Selection']");
    readonly By btnClearProducerContact = By.XPath("//lightning-lookup-desktop//label[contains(text(),'Placing Producer Contact')]/following::button[2][@title='Clear Selection']");
    
    readonly By btnClearLocalClientName = By.XPath("//lightning-lookup-desktop//label[contains(text(),'Local Client Name')]/following::button[1][@title='Clear Selection']");

    //readonly By btnViewSubmissionList = By.XPath("//ul/li//lightning-button/button");
    readonly By btnViewSubmissionList = By.XPath("//ul/li//lightning-button/button | (//div[@data-aura-class='forceListViewManagerHeader']/div//ul/li/a[@class='forceActionLink']/div)[1]");

    readonly By btnMoreActionItems = By.XPath("//li/lightning-button-menu/button[@class='slds-button slds-button_icon-border-filled']");
    readonly By btnMoreActionList = By.XPath("//div[@class='slds-dropdown slds-dropdown_right']//slot//a/span");
    readonly By btnSubmissionTabList = By.XPath("(//ul[@class='slds-tabs_default__nav'])[2]/li/a");
    readonly By btnPunitiveDamages = By.XPath("//button[contains(text(),'Punitive Damages')]");

    // Submission link in Incumbent Insurer page
    readonly By lnkSubmissionName = By.XPath("((//span[contains(text(),'Submission')])[3]/following::div)[1]//a//span");

    // Subjectivity WebElements
    readonly By btnAddPreDefinedItem = By.XPath("//input[@value='Add Pre-Defined Item']");
    readonly By btnDeleteItem = By.XPath("//input[@value='Delete Item']");
    readonly By cbSubjectivityItem = By.XPath("(//input[@type='checkbox'])[1]");
    readonly By btnSaveSubjectivity = By.XPath("(//input[@type='submit'])[1]");
    readonly By btnSelectItem = By.XPath("(//input[@type='button'])[1]");
    readonly By slctSubjectivityNotice = By.XPath("(//select)[1]");


    // Wait till the fields present
    readonly By datesOrAdditionalTabFieldInfo = By.XPath("//label[contains(text(),'Effective Date')]");
    readonly By uwOrProducerTabFieldToWait = By.XPath("//label[contains(text(),'Assigned Underwriter')]");
    readonly By sgsSectionTabFieldToWait = By.XPath("//label[contains(text(),'Shell Policy')]");
    readonly By premiumOrlimitsTabFieldToWait = By.XPath("//label[contains(text(),'100 Percent Policy Booked Premium')]");
    readonly By policyInfoTabFieldToWait = By.XPath("//label[contains(text(),'Insurance Type')]");

    readonly By btnStageProgressBarList = By.XPath("//ul[@aria-label='Path Options']/li/a/span[2]");
    readonly By btnMarkAsCurrentStage = By.XPath("//article[@class='slds-card']//button");
    readonly By alertSuccessMsg = By.XPath("//div[contains(@id,'toastDescription')]/span");
    


    readonly FeatureContext _featureContext;

    public SubmissionPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
        _featureContext = featureContext;
    }
    
    public static string GenerateRandomNumbers()
    {
        Random random = new Random();
        return random.Next(99999).ToString();
    }

    public static int GenerateRandomNumbers(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public static int GenerateRandomNumberForPickListValues(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
    public static string Generate2DigitRandomNumber()
    {
        Random random = new Random();
        return random.Next(10, 49).ToString();
    }

    public string GenerateDecimalNumber()
    {
        return GenerateRandomNumbers() + "." + Generate2DigitRandomNumber();
    }

    public void NavigateToSubmissionTab()
    {
        driver.WaitForElement(tabSubmission);
        driver.JSClick(tabSubmission);
        driver.WaitForElementToPresent(txtSearchSubmission);
        Assert.IsTrue(driver.IsDisplayed(txtSearchSubmission), "COULD NOT NAVIGATED TO SUBMISSION PAGE");
        Log("CLICKED ON SUBMISSION TAB ");
    }

    public void NavigateToCreateSubmissionTabs(string TabName)
    {
        Assert.IsTrue(driver.SelectFromListUsingJS(TabCreateSubmissionlist, TabName.Trim()), "COULD NOT CLICK ON THE TAB " + TabName);
        System.Threading.Thread.Sleep(3000);
        Log("CLICKED ON THE TAB " + TabName + "AND NAVIGATED TO SUBMISSION PAGE"); ;
    }

    public void NavigateToEditSubmissionTabs(string TabName)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        driver.ScrollByPixel(-300);
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(TabEditSubmissionList));
        driver.MoveToTheElement(TabEditSubmissionList);
        Assert.IsTrue(driver.SelectFromListUsingJS(TabEditSubmissionList, TabName.Trim()), "COULD NOT CLICK ON THE TAB / TAB IS NOT AVAILABLE " + TabName);
        System.Threading.Thread.Sleep(3000);
        Log("CLICKED ON THE TAB " + TabName + "AND NAVIGATED TO SUBMISSION PAGE"); ;
    }

    public void NavigateToEditSubmissionTabsInAdmin(string TabName)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(TabEditSubmissionList));
        if (driver.SelectFromListUsingJS(TabEditSubmissionList, TabName) == false)
        {
            driver.WaitAndClick(tabMoreDetailsForAdmin);
            if (driver.SelectFromListUsingJS(tabActionValues, TabName) == false)
            {
                Console.WriteLine(TabName + " IS NOT AVAILABLE IN THE LIST");
                Log(TabName + " IS NOT AVAILABLE IN THE LIST");
                Assert.Fail(TabName + " IS NOT AVAILABLE IN THE LIST");
            }
            else
            {
                driver.WaitForNextPage();
                Log("CLICKED ON THE TAB " + TabName);
            }
        }
        else
        {
            driver.WaitForNextPage();
            Log("CLICKED ON THE TAB " + TabName);
        }
    }

    public void ThenUserNavigatedToPage(string TabName)
    {
        System.Threading.Thread.Sleep(2000);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(btnAppLauncher));
            wait.Until(ExpectedConditions.ElementToBeClickable(btnAppLauncher));
            wait.Until(ExpectedConditions.ElementToBeClickable(tabMainSubmissionTabList));
        }
        catch (Exception e)
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(tabMainSubmissionTabList));
        }
        bool flag = driver.SelectFromListUsingJS(tabMainSubmissionTabList, TabName);
        if (flag == false)
        {
            driver.WaitAndClick(lnkMore);
            driver.CaptureScreen(_scenarioContext);
            Assert.IsTrue(driver.SelectFromListUsingJS(tabHidenSubmissionTabList, TabName), "COULD NOT CLICK ON THE TAB " + TabName);
            Log("CLICKED ON THE TAB " + TabName + "AND NAVIGATED TO SUBMISSION PAGE");
        }

        driver.WaitForElementToPresent(tabCurrentPage);

        Log("CLICKED ON THE TAB " + TabName + "AND NAVIGATED TO SUBMISSION PAGE");
    }

    public void NavigateToRecordTypePage()
    {
        driver.WaitAndClick(btnNewSubmission);
        Assert.IsTrue(driver.WaitForElementToPresent(lblRecordTypePage), "COULD NOT NAVIGATE TO RECORD TYPE PAGE");
        Log("NAVIGATED TO RECORD TYPE PAGE");
    }

    public void SearchForSubmission()
    {
        driver.WaitAndClick(txtSearchSubmission);
        driver.ClearAndSend(txtSearchSubmission, "submission Name");
        driver.WaitAndClick(TableHeaderSubmissionName);
        Log("SEARCHED SUBMISSION IS DISPLAYED IN THE LIST ");
    }

    public void SelectFirstRecord()
    {
        driver.WaitForElementToPresent(listFirstRecord);
        driver.WaitAndClick(listFirstRecord);
        driver.WaitForNextPage();
        driver.WaitForElementToPresent(btnShowMoreActions);
        Assert.IsTrue(driver.IsDisplayed(btnShowMoreActions), "COULD NOT CLICKED ON FIRST RECORD");
        Log("CLICKED ON FIRST RECORD");
    }

    public void EditSubmission()
    {
        driver.WaitAndClick(btnShowMoreActions);
        driver.WaitForElementToPresent(btnAllActionsList);
        Boolean flag = SelectValueFromDropdown(btnAllActionsList, "Edit");
        Assert.IsTrue(flag, "COULD NOT CLICK ON EDIT BUTTON");
        Log("CLICKED ON EDIT BUTTON");
    }

    //Method to Select value from the dropdown
    public Boolean SelectValueFromDropdown(By Elements, String Value)
    {
        IList<IWebElement> elements = driver.ListOfElements(Elements);
        foreach (IWebElement element in elements)
        {
            if (element.GetElementText() == Value.ToString())
            {
                element.Click();
                return true;
            }
        }
        return false;
    }

    public Boolean SelectRandomValueFromDropdown(By ddField, By ListElements, string FieldName)
    {
        driver.WaitAndClick(ddField);
        IList<IWebElement> elements = driver.ListOfElements(ListElements);
        Random num = new Random();
        int IndexNo = num.Next(elements.Count);
        if (IndexNo == 0)
        {
            IndexNo++;
        }
        for (int i = 0; i <= elements.Count - 1; i++)
        {
            if (i == IndexNo)
            {
                //System.Threading.Thread.Sleep(3000);
                driver.MoveToTheElementAndClick(elements[i]);
                Log(FieldName + " Is inputed with value " + elements[i].ToString());
                return true;
            }
        }
        Log(FieldName + " Could not Inputed");
        return false;
    }

    public static string GetCurrentDate(string Format)
    {
        string CurrentDate = DateTime.Now.ToString(Format);
        Console.WriteLine(CurrentDate);
        return CurrentDate;
    }

    public static string GetExpirationDate(string Format)
    {
        string ExpireDate = DateTime.Now.AddDays(-365).ToString(Format);
        Console.WriteLine(ExpireDate);
        return ExpireDate;
    }

    public static string GetExpirationDateforFuture(string Format)
    {
        string ExpireDate = DateTime.Now.AddDays(+365).ToString(Format);
        Console.WriteLine(ExpireDate);
        return ExpireDate;
    }

    public static string GetExpirationDateForRenewal(string Format)
    {
        string ExpireDate = DateTime.Now.AddDays(+365).ToString(Format);
        Console.WriteLine(ExpireDate);
        return ExpireDate;
    }

    public string GetCurrentTime()
    {
        string currentTime = DateTime.Now.ToString("dd MMM yyyy hh mm ss");
        return currentTime;
    }

    /* THIS METHOD IS USED TO SELECT RECORD TYPE IN BOTH 1.0 AND 2.0 RECORD TYPE */
    public void SelectRecordType(string RecordTypeValue)
    {
        _featureContext["RecordType_"+ loggingStep.rowNo] = RecordTypeValue;


        if (driver.SelectFromList(lblRecordType, RecordTypeValue) == false)
        {
            Log("CURRENT LOGGED IN USER DON'T HAVE PERMISSION TO VIEW THE RECORD TYPE " + RecordTypeValue);
            Console.WriteLine("CURRENT LOGGED IN USER DON'T HAVE PERMISSION TO VIEW THE RECORD TYPE " + RecordTypeValue);
            Assert.Fail(" COULD NOT FIND THE RECORD TYPE " + RecordTypeValue);
        }
        else
        {
            Console.WriteLine("SELECTED THE RECORD TYPE " + RecordTypeValue);
            Log("SELECTED THE RECORD TYPE " + RecordTypeValue);

            driver.CaptureScreen(_scenarioContext);

            Assert.IsTrue(driver.WaitAndClick(btnNext), "COULD NOT CLICK ON THE NEXT BUTTON");
            Log("CLICKED ON NEXT BUTTON");
            //System.Threading.Thread.Sleep(2000);
        }
    }

    public void FillValuesInSubmissionPagefor_Submission1_0(string[] submissionData, string InsuranceType)
    {
        string Stage = submissionData[0];
        string ProductionOffice = submissionData[1];
        string IssuingCompany = submissionData[2];
        string LineOfBusiness = submissionData[3];
        string Subclass1 = submissionData[4];
        string IssuingOffice = submissionData[5];
        string UWRegion = submissionData[6];
        string DateFormat = submissionData[7];
        string Occupancy = submissionData[8];
        string ShellPolicy = submissionData[9];
        string Affiliated = submissionData[10];
        string PolicyNumber = submissionData[11];
        string SubmissionCurrency = submissionData[12];
        string LicensedProducer = submissionData[13];
        string Program = submissionData[14];
        string CarrierBranch = submissionData[15];
        string UnderWritingCreditBranch = submissionData[16];
        string ProfitCenter = submissionData[17];

        try
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblClientName1_0));

            _featureContext["AttachedClientName_" + loggingStep.rowNo] = ReadDataFromFile(ClientsPage.Clients1_0FilePath + "Client_Create.txt");
            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Used For Testing team", "3.0_Test_Standby");

            PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Client Name", _featureContext["AttachedClientName_" + loggingStep.rowNo].ToString());

            PerformActionOnCreateSubmission("Checkbox", lblcbCheckBoxName1_0, cbCheckBoxField1_0, "Check if no Contractor", "Yes");

            PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "Project Name", _featureContext["AttachedClientName_" + loggingStep.rowNo].ToString());

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Client Region", "Random");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "UDX Currency?", "Yes");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Stage", Stage);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Submission Currency", SubmissionCurrency);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Line of Business", LineOfBusiness);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Issuing Company", IssuingCompany);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Occupancy", Occupancy);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Carrier Branch", CarrierBranch);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Profit Center", ProfitCenter);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Subclass 1", Subclass1);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Subclass 2", "Random");

            if (PolicyNumber.ToLower().Equals("manual"))
            {
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Policy Number - Assignment Type", PolicyNumber);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Number - Current", GenerateRandomNumbers());
            }

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Issuing Office", IssuingOffice);

            PerformActionOnCreateSubmission("Checkbox", lblcbCheckBoxName1_0, cbCheckBoxField1_0, "Clearance Indicator", "Yes");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Program", Program);

            driver.SendKeysOrText(txtAreaSubmissionComment, GenerateRandomNumbers());

            driver.CaptureScreen(_scenarioContext);
            NavigateToCreateSubmissionTabs("Dates/Addtional Info");
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(datesOrAdditionalTabFieldInfo));

            PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Effective Date", GetExpirationDate(DateFormat));

            PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Expiration Date", GetCurrentDate(DateFormat));

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Month", "Random");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Year", "Random");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Inception Month", "Random");

            driver.CaptureScreen(_scenarioContext);
            NavigateToCreateSubmissionTabs("UW/Producer");
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(uwOrProducerTabFieldToWait));

            PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Assigned Underwriter", LoginPage.LooggedInUser);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*UW Region", UWRegion);

            PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Underwriting Assistant", LoginPage.LooggedInUser);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Production Office", ProductionOffice);

            PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Placing Broker", LicensedProducer);

            string PlacingProducerContactName = ReadDataFromFile(ContactsPage.ContactRecordPath);
            PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Placing Producer Contact", PlacingProducerContactName);

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Producer Commission %", Generate2DigitRandomNumber());

            driver.CaptureScreen(_scenarioContext);
            NavigateToCreateSubmissionTabs("SGS Section");
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(sgsSectionTabFieldToWait));

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Local Country", "random");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Shell Policy", ShellPolicy);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Underwriting Credit Branch", UnderWritingCreditBranch);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Affiliated ?", Affiliated);

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "UCB Share %", "0");

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Network Identification (NID)", GenerateRandomNumbers());

            driver.CaptureScreen(_scenarioContext);
            NavigateToCreateSubmissionTabs("Premium/Limits");
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(premiumOrlimitsTabFieldToWait));

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Premium Written Share %", Generate2DigitRandomNumber());

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "PML", Generate2DigitRandomNumber());

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Premium Estimated Signing %", Generate2DigitRandomNumber());

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100% Policy Limit Amount (Currency)", GenerateRandomNumberForPickListValues(30, 50).ToString());

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100 Percent Policy Booked Premium", Generate2DigitRandomNumber());

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "100% TIV", Generate2DigitRandomNumber());

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Starr Share Estimated Forecast Premium", GenerateDecimalNumber());

            driver.CaptureScreen(_scenarioContext);
            NavigateToCreateSubmissionTabs("Policy Info");
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(policyInfoTabFieldToWait));

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Insurance Type", InsuranceType);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Layer", "Excess");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Assumed RI Contract", "Random");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Framework ST Risk Code", "Random");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Maintenance Period", "Random");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchased External FAC Reinsurance", "Yes");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "FAC Basis of Acceptance", "Random");

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "FAC RI PRM", "0");

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Ceding Commission Amount", GenerateDecimalNumber());

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "TRIA/Non Cert Included", "No");

            Log("SSUCCESSFULLY FILLED THE VALUES IN 1.0 SUBMISSION CREATION PAGE");
            Console.WriteLine("SUCCESSFULLY FILLED THE VALUES IN 1.0 SUBMISSION CREATION PAGE");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Log("COULD NOT FILL THE VALUES IN SUBMISSION PAGE ");
            Console.WriteLine("COULD NOT FILL THE VALUES IN SUBMISSION PAGE");
        }
    }


    //public void FillValuesInSubmissionPagefor_Submission1_0(string[] submissionData, string InsuranceType)
    //{
    //    string Stage = submissionData[0];
    //    string ProductionOffice = submissionData[1];
    //    string IssuingCompany = submissionData[2];
    //    string LineOfBusiness = submissionData[3];
    //    string Subclass1 = submissionData[4];
    //    string IssuingOffice = submissionData[5];
    //    string UWRegion = submissionData[6];
    //    string DateFormat = submissionData[7];
    //    string Occupancy = submissionData[8];
    //    string ShellPolicy = submissionData[9];
    //    string Affiliated = submissionData[10];
    //    string PolicyNumber = submissionData[11];
    //    string SubmissionCurrency = submissionData[12];
    //    string LicensedProducer = submissionData[13];
    //    string Program = submissionData[14];
    //    string CarrierBranch = submissionData[15];
    //    string UnderWritingCreditBranch = submissionData[16];
    //    string ProfitCenter = submissionData[17];

    //    try
    //    {
    //        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
    //        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblClientName1_0));

    //        _featureContext["AttachedClientName_" + loggingStep.rowNo] = ReadDataFromFile(ClientsPage.Clients1_0FilePath + "Client_Create.txt");

    //        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Client Name", _featureContext["AttachedClientName_" + loggingStep.rowNo].ToString());

    //        PerformActionOnCreateSubmission("Checkbox", lblcbCheckBoxName1_0, cbCheckBoxField1_0, "Check if no Contractor", "Yes");

    //        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "Project Name", _featureContext["AttachedClientName_" + loggingStep.rowNo].ToString());

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Client Region", "Random");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Stage", Stage);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "UDX Currency?", "Yes");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Submission Currency", SubmissionCurrency);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Line of Business", LineOfBusiness);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Issuing Company", IssuingCompany);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Occupancy", Occupancy);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Carrier Branch", CarrierBranch);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Profit Center", ProfitCenter);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Subclass 1", Subclass1);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Subclass 2", "Random");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Policy Number - Assignment Type", PolicyNumber);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Division", "Random");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Subdivision", "Random");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Issuing Office", IssuingOffice);

    //        PerformActionOnCreateSubmission("Checkbox", lblcbCheckBoxName1_0, cbCheckBoxField1_0, "Clearance Indicator", "Yes");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Program", Program);

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Local Policy Number – Current", GenerateRandomNumbers());

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Number - Current", GenerateRandomNumbers());

    //        driver.SendKeysOrText(txtAreaSubmissionComment, GenerateRandomNumbers());

    //        driver.CaptureScreen(_scenarioContext);
    //        NavigateToCreateSubmissionTabs("Dates/Addtional Info");
    //        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(datesOrAdditionalTabFieldInfo));

    //        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Effective Date", GetExpirationDate(DateFormat));

    //        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Expiration Date", GetCurrentDate(DateFormat));

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Month", "Random");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Year", "Random");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Inception Month", "Random");

    //        driver.CaptureScreen(_scenarioContext);
    //        NavigateToCreateSubmissionTabs("UW/Producer");
    //        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(uwOrProducerTabFieldToWait));

    //        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Assigned Underwriter", LoginPage.LooggedInUser);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*UW Region", UWRegion);

    //        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Underwriting Assistant", LoginPage.LooggedInUser);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Production Office", ProductionOffice);

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Producer Commission %", Generate2DigitRandomNumber());

    //        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Placing Broker", LicensedProducer);

    //        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Placing Producer Contact", ReadDataFromFile(ContactsPage.ContactRecordPath));

    //        driver.CaptureScreen(_scenarioContext);
    //        NavigateToCreateSubmissionTabs("SGS Section");
    //        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(sgsSectionTabFieldToWait));

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Local Country", "random");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Shell Policy", ShellPolicy);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Underwriting Credit Branch", UnderWritingCreditBranch);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Affiliated ?", Affiliated);

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "UCB Share %", "0");

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Network Identification (NID)", GenerateRandomNumbers());

    //        driver.CaptureScreen(_scenarioContext);
    //        NavigateToCreateSubmissionTabs("Premium/Limits");
    //        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(premiumOrlimitsTabFieldToWait));

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Premium Written Share %", Generate2DigitRandomNumber());

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "PML", Generate2DigitRandomNumber());

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Premium Estimated Signing %", Generate2DigitRandomNumber());

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100% Policy Limit Amount (Currency)", GenerateRandomNumberForPickListValues(30, 50).ToString());

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100 Percent Policy Booked Premium", Generate2DigitRandomNumber());

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "100% TIV", Generate2DigitRandomNumber());

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Starr Share Estimated Forecast Premium", GenerateDecimalNumber());

    //        driver.CaptureScreen(_scenarioContext);
    //        NavigateToCreateSubmissionTabs("Policy Info");
    //        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(policyInfoTabFieldToWait));

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Insurance Type", InsuranceType);

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Layer", "Excess");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Assumed RI Contract", "Random");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Framework ST Risk Code", "Random");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Maintenance Period", "Random");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchased External FAC Reinsurance", "Yes");

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "FAC Basis of Acceptance", "Random");

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "FAC RI PRM", "0");

    //        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Ceding Commission Amount", GenerateDecimalNumber());

    //        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "TRIA/Non Cert Included", "No");

    //        Log("SSUCCESSFULLY FILLED THE VALUES IN 1.0 SUBMISSION CREATION PAGE");
    //        Console.WriteLine("SUCCESSFULLY FILLED THE VALUES IN 1.0 SUBMISSION CREATION PAGE");
    //    }
    //    catch (Exception e)
    //    {
    //        Log(e.Message.ToString());
    //        Log(e.StackTrace);
    //        Log(e.Source);
    //        Assert.Fail("COULD NOT FILL THE VALUES IN SUBMISSION PAGE" + e.Message.ToString() + '\n' + e.Source.ToString() + '\n' + e.StackTrace);
    //    }
    //}



    public void FillValuesInSubmissionPagefor_Submission2_0(string[] submissionData, String InsuranceType)
    {

        string Stage2_0 = submissionData[0];
        string DateFormat = submissionData[1];
        string LicensedProducer = submissionData[2];
        string PlacingProducerContact = submissionData[3];
        string ProfitCenter = submissionData[4];
        string ProductProfitCenter = submissionData[5];
        string BusinessUnit = submissionData[6];
        string IssuingCompany = submissionData[7];
        string UnderWriter = submissionData[8];
        string SubmissionCurrency = submissionData[9];
        string PlacingBroaker =   submissionData[10];

        _featureContext["ExecutedByName_" + loggingStep.rowNo] = LoginPage.LooggedInUser;

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblClientName1_0));

        driver.WaitForElementToPresent(lblClientName1_0);

        _featureContext["AttachedClientName_" + loggingStep.rowNo] = ReadDataFromFile(ClientsPage.Clients2_0FilePath+ "Client_Create.txt");
        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Client Name", _featureContext["AttachedClientName_" + loggingStep.rowNo].ToString());

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Stage", Stage2_0);

        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Assigned Underwriter", UnderWriter);

        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Effective Date", GetExpirationDate(DateFormat));

        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Placing Underwriter", UnderWriter);

        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Expiration Date", GetCurrentDate(DateFormat));

        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Underwriting Assistant", UnderWriter);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Pending or Block Reason", "Random");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Primary/XS", "Excess");

        if (!_featureContext["RecordType_" + loggingStep.rowNo].ToString().Trim().Equals("Starr Casualty International"))
        {
            driver.CaptureScreen(_scenarioContext);
            NavigateToCreateSubmissionTabs("PC Coding");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Business Unit", BusinessUnit);

            if (_featureContext["RecordType_" + loggingStep.rowNo].ToString().Trim().Equals("Starr Marine"))
            {
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*UW Region", "Random");

                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Production Office", "Random");

            }

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Issuing Office", "Random");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Profit Center", ProfitCenter);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Issuing Company", IssuingCompany);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Product Profit Center", "Random");

            NavigateToCreateSubmissionTabs("Policy");
            NavigateToCreateSubmissionTabs("PC Coding");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Line of Business", "Random");

            driver.CaptureScreen(_scenarioContext);
            NavigateToCreateSubmissionTabs("Policy");

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Insurance Type", InsuranceType);

            driver.CaptureScreen(_scenarioContext);
            NavigateToCreateSubmissionTabs("Producer");

            PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "Licensed Producer", LicensedProducer);

            PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Placing Producer Contact", PlacingProducerContact);

            PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Placing Broker", PlacingBroaker);

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*Commission Percent", Generate2DigitRandomNumber());

            driver.CaptureScreen(_scenarioContext);
            NavigateToCreateSubmissionTabs("Premium/Limits");

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100% Policy Limit Amount (Currency)", Generate2DigitRandomNumber());

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Submission Currency", SubmissionCurrency);

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100 Percent Policy Booked Premium", Generate2DigitRandomNumber());

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*Additional Limit", Generate2DigitRandomNumber());

            //PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Policy Limit Type", "Random");

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*Policy Premium Estimated Signing %", Generate2DigitRandomNumber());
            driver.CaptureScreen(_scenarioContext);

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Additional Limit Type", "Random");

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*Policy Premium Written Share %", Generate2DigitRandomNumber());


            if (!_featureContext["RecordType_" + loggingStep.rowNo].ToString().Trim().Equals("Starr Accident & Health International"))
            {
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100% Policy Limit", Generate2DigitRandomNumber());
            }

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*XS Attachment Point", Generate2DigitRandomNumber());

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Starr Share Estimated Forecast Premium", GenerateRandomNumbers());
        }
        Log("SUCCESSFULLY FILLED THE VALUES IN 2.0 SUBMISSION CREATION PAGE");
        Console.WriteLine("SUCCESSFULLY FILLED THE VALUES IN 2.0 SUBMISSION CREATION PAGE");
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

    public static string ReadSubmissionLinkFromFile(string Filepath)
    {
        string[] LInes = File.ReadAllLines(Filepath);
        string[] value;
        string[] SubmissionLInk;
        string returnValue;
        foreach (string str in LInes)
        {
            value = str.Split("=");
            SubmissionLInk = value[0].Split("#");
            Console.WriteLine("SUBMISSION LINK IS  = " + SubmissionLInk);
            Console.WriteLine("SUBMISSION LINK INDEX COUNT  = " + SubmissionLInk.Length);
            for (int i = 0; i < SubmissionLInk.Length; i++)
            {
                Console.WriteLine("out put of " + i + " = ", SubmissionLInk[i]);
            }
            returnValue = SubmissionLInk[1];
            return returnValue;
        }
        Console.WriteLine("COULD NOT FETCH VALUE FROM THE FIELD");
        return null;
    }

    public static string ReadRecordTypeFromFile(string Filepath)
    {
        string[] LInes = File.ReadAllLines(Filepath);
        string[] value;
        string returnValue;
        foreach (string str in LInes)
        {
            value = str.Split("#");
            for (int i = 0; i < value.Length; i++)
            {
                Console.WriteLine("OUT PUT OF  " + i + " = ", value[i]);
            }
            returnValue = value[0];
            return returnValue;
        }
        Console.WriteLine("COULD NOT FETCH THE RECORD TYPEFROM THE FILE");
        return null;
    }

    public void ClickOnNewButton()
    {
        Assert.IsTrue(driver.WaitAndClick(btnNewSubmission), "Could not click on New button");
        Assert.IsTrue(driver.WaitForElementToPresent(lblRecordTypePage), "Record Type page is not displayed");
        Log("Clicked on New button and Navigated to Record Type page");
    }

    public void ClickOnSaveButton()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(btnSaveSubmission));
            driver.MoveToTheElement(btnSaveSubmission);
            driver.JSClick(btnSaveSubmission);
            System.Threading.Thread.Sleep(4000);
            Console.WriteLine("CLICKED ON THE SAVE SUBMISSION BUTTON");
            Log("CLICKED ON THE SAVE SUBMISSION BUTTON");
        }
        catch (Exception e)
        {
            Console.WriteLine("COULD NOT CLICK ON THE SAVE SUBMISSION BUTTON");
            Log("COULD NOT CLICK ON THE SAVE SUBMISSION BUTTON");
            Assert.Fail("COULD NOT CLICK ON THE SAVE SUBMISSION BUTTON");
        }

        if (driver.IsDisplayed(dateErrorMessageInMidTermBroker))
        {
            driver.ScrollToCenter(txtErrorMessageDisplayedField);
            driver.ClearAndSend(txtErrorMessageDisplayedField, GetCurrentDate("dd-MMM-yyyy").ToString());
            NavigateToEditSubmissionTabs("PC Coding");
            ClickOnSaveButton();
        }

        // ANY ERROR MESSAGES ARE DISPLAYED THEN EXECUTION WILL FAIL
        if (driver.IsDisplayed(lblErrorMessageInStageProgression))
        {
            driver.ScrollToCenter(lblErrorMessages);
            driver.CaptureScreen(_scenarioContext);
            if (driver.IsDisplayed(lblErrorMessages))
            {
                string DisplayedErrorMessage = driver.GetTextFromElement(lblErrorMessages);
                if (DisplayedErrorMessage.Contains("You cannot create submission with Inactive Profit Center"))
                {
                    ChangeProfitCenter();
                    Assert.IsTrue(driver.WaitAndClick(btnSaveSubmission), "COULD NOT CLICK ON SAVE BUTTON");
                }
                else if (DisplayedErrorMessage.Contains("Carrier Branch is Required for this Issuing Office or Production Office ( Submission Details Tab )"))
                {
                    PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Carrier Branch", "Random");
                    driver.CaptureScreen(_scenarioContext);
                    driver.ScrollToCenter(btnSaveSubmission);
                    Assert.IsTrue(driver.WaitAndClick(btnSaveSubmission), "COULD NOT CLICK ON SAVE BUTTON");
                }
                else if (DisplayedErrorMessage.Contains("Opportunity Master Flow"))
                {
                    Assert.Fail("OPPORTUNITY MASTER FLOW ERROR MESSAGE IS DISPLAYED " + driver.GetTextFromElement(lblErrorMessages));
                }
                else
                {
                    Assert.Fail("ERROR MESSAGE IS DISPLAYED AS " + driver.GetTextFromElement(lblErrorMessages));
                }
            }
            else
            {
                Assert.Fail("ERROR MESSAGE IS DISPLAYED as " + driver.GetTextFromElement(lblErrorMessages));
            }
        }

        if (wait.Until(ExpectedConditions.InvisibilityOfElementLocated(btnSaveSubmission)) == false)
        {
            Assert.Fail("THERE ARE SOME ERROR IN THE SUBMISSION CREATION, HENCE CLOSING THE EXECUTION ");
        }
        else
        {
            Log("CLICKED ON SAVE BUTTON TO SAVE THE SUBMISSION");
            Console.WriteLine("CLICKED ON SAVE BUTTON TO SAVE THE SUBMISSION");
        }
    }

    public void ThenUserClickedOnSaveButtonInUpdateSubmissionToRenewal()
    {
        driver.JSClick(btnSaveUpdateToRenewal);
        Log("CLICKED ON UPDATE RENEWAL SUBMISSION");
    }

    public void VerifyErrorMessageUpdateRenewalSubmissionTypeToRenewal(string errorMsg)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(errmsgTypeToRenewal));
        string DisplayedErrorMsg = driver.GetTextFromElement(errmsgTypeToRenewal);
        Assert.AreEqual(errorMsg, DisplayedErrorMsg, "EXPECTED ERROR MESSAGE IS NOT DISPLAYED");
        Log("EXPECTED ERROR MESSAGE " + errorMsg);
        Log("DISPLAYED ERROR MESSAGE " + errorMsg);
    }


    public void ClickOnSaveButtonDuringVerificationOfLicenceCheck()
    {
        driver.ScrollToCenter(btnSaveSubmission);
        Assert.IsTrue(driver.WaitAndClick(btnSaveSubmission), "COULD NOT CLICK ON SAVE BUTTON");
        System.Threading.Thread.Sleep(4000);
    }

    public void ChangeProfitCenter()
    {
        Assert.IsTrue(driver.WaitAndClick(tabPCCodin2_0), "COULD NOT CLICK ON PC CODING TAB");
        string ExistingValue = driver.GetTextFromElement(picklistProfitCenterValue);
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Profit Center", "Random");
        string SelectedValue = driver.GetTextFromElement(picklistProfitCenterValue);
        if (ExistingValue.Equals(SelectedValue))
        {
            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Profit Center", "Random");
        }
        Log("CHANGED THE PROFIT CENTER VALUE");
    }


    public void VerifySubmissionCreation()
    {
        string RecordInfo;

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        if (_featureContext["App_Version"+ loggingStep.rowNo].ToString().Equals("1"))
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblDetailsTabFieldsName));
            _featureContext["CreatedSubmissionName_" + loggingStep.rowNo] = GetValueUsingFieldName("Submission Name", lblDetailsTabFieldsName, lblDetailsFieldValue);
            _featureContext["AssignmentType_" + loggingStep.rowNo] = GetValueUsingFieldName("Policy Number - Assignment Type", lblDetailsTabFieldsName, lblDetailsFieldValue);
            driver.CaptureScreen(_scenarioContext);
            RecordInfo = _featureContext["RecordType_" + loggingStep.rowNo].ToString() + " # " + driver.Url +" = "+ _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString();
            File.WriteAllText(Submission1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt", RecordInfo);
        }
        
     Log("SUCCESSFULLY CREATED THE SUBMISSION " + _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString());
        Console.WriteLine("SUCCESSFULLY CREATED THE SUBMISSION AND SUBMISSSION NAME = " + _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString());

        _featureContext["URL_" + loggingStep.rowNo] = driver.Url;
        _featureContext["SubmissionName_" + loggingStep.rowNo] =  _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString();
    }

    public void VerifySubmissionCreation(string TestingType)
    {
        string RecordInfo;
        _featureContext["TestingType_" + loggingStep.rowNo] = TestingType;

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        if (_featureContext["App_Version" + loggingStep.rowNo].ToString().Equals("1"))
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblDetailsTabFieldsName));
            _featureContext["CreatedSubmissionName_" + loggingStep.rowNo] = GetValueUsingFieldName("Submission Name", lblDetailsTabFieldsName, lblDetailsFieldValue);
            _featureContext["AssignmentType_" + loggingStep.rowNo] = GetValueUsingFieldName("Policy Number - Assignment Type", lblDetailsTabFieldsName, lblDetailsFieldValue);
            driver.CaptureScreen(_scenarioContext);
            RecordInfo = _featureContext["RecordType_" + loggingStep.rowNo].ToString() + " # " + driver.Url + " = " + _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString();
            if (_featureContext["TestingType_" + loggingStep.rowNo].ToString().Trim().Equals("Validation"))
            {
                File.WriteAllText(Submission1_0ValidationRecords + loggingStep.rowNo + ".txt", RecordInfo);
            }
            else
            {
                File.WriteAllText(Submission1_0Filsepath + loggingStep.rowNo+ loggingStep.FeatureFileName+ ".txt", RecordInfo);
            }
        }
        Log("SUCCESSFULLY CREATED THE SUBMISSION " + _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString());
        Console.WriteLine("SUCCESSFULLY CREATED THE SUBMISSION AND SUBMISSSION NAME = " + _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString());
    }

    public void VerifySubmissionCreationforValidation(string TestingType,string stageNme)
    {
        string RecordInfo;
        _featureContext["TestingType_" + loggingStep.rowNo] = TestingType;

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        if (_featureContext["App_Version" + loggingStep.rowNo].ToString().Equals("1"))
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblDetailsTabFieldsName));
            _featureContext["CreatedSubmissionName_" + loggingStep.rowNo] = GetValueUsingFieldName("Submission Name", lblDetailsTabFieldsName, lblDetailsFieldValue);
            _featureContext["AssignmentType_" + loggingStep.rowNo] = GetValueUsingFieldName("Policy Number - Assignment Type", lblDetailsTabFieldsName, lblDetailsFieldValue);
            driver.CaptureScreen(_scenarioContext);
            RecordInfo = _featureContext["RecordType_" + loggingStep.rowNo].ToString() + " # " + driver.Url + " = " + _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString();
            if (_featureContext["TestingType_" + loggingStep.rowNo].ToString().Trim().Equals("Validation"))
            {
                File.WriteAllText(Submission1_0ValidationRecords + loggingStep.rowNo + stageNme+ ".txt", RecordInfo);
            }
            else
            {
                File.WriteAllText(Submission1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt", RecordInfo);
            }
        }
        Log("SUCCESSFULLY CREATED THE SUBMISSION " + _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString());
        Console.WriteLine("SUCCESSFULLY CREATED THE SUBMISSION AND SUBMISSSION NAME = " + _featureContext["CreatedSubmissionName_" + loggingStep.rowNo].ToString());
    }

    public void ClickOnIceCheckButton()
    {
        driver.WaitForElementToPresent(btnIceCheck);
        Assert.IsTrue(driver.WaitAndClick(btnIceCheck), "COULD NOT CLICK ON ICE CHECK BUTTON");
        try
        {
            driver.WaitForElementToPresent(btnRefresh);
        }
        catch (Exception e)
        {
            driver.Refresh();
            driver.WaitForElementToPresent(btnIceCheck);
            Assert.IsTrue(driver.WaitAndClick(btnIceCheck), "COULD NOT CLICK ON ICE CHECK BUTTON");
            driver.WaitForElementToPresent(btnRefresh);
        }
        Log("ICE CHECK BUTTON IS CLICKED");
    }

    public void ThenUserClickedOnButton(string ButtonName)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        try
        {
            System.Threading.Thread.Sleep(2000);
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(btnViewSubmissionList));
            wait.Until(ExpectedConditions.ElementToBeClickable(btnViewSubmissionList));
        }
        catch (Exception e)
        {
            Console.WriteLine("COULD NOT FIND THE ACTION BUTTON ITEMS IN THE PAGE " + ButtonName);
            Log("COULD NOT FIND THE ACTION BUTTON ITEMS IN THE PAGE " + ButtonName);
            Assert.Fail("COULD NOT FIND THE ACTION BUTTON ITEMS IN THE PAGE " + ButtonName);
        }
        _featureContext["ParentWindow_" + loggingStep.rowNo] = driver.CurrentWindowHandle;

        ActionButtonName = ButtonName;

        if (driver.SelectFromListUsingJS(btnViewSubmissionList, ButtonName) == false)
        {
            System.Threading.Thread.Sleep(2000);
            driver.JSClick(btnMoreActionItems);
            if (driver.SelectFromListUsingJS(btnMoreActionList, ButtonName.Trim()) == true)
            {
                driver.WaitForNextPage();
                Log("CLICKED ON " + ButtonName);
                _featureContext["ReservationCheckFlag_" + loggingStep.rowNo] = true;
            }
            else
            {
                if (ButtonName.Contains("Reservation"))
                {
                    Console.WriteLine(ButtonName + "IS NOT APPLICABLE FOR THE RECORD TYPE " + _featureContext["RecordType_" + loggingStep.rowNo].ToString());
                    Log(ButtonName + " IS NOT APPLICABLE FOR THE RECORD TYPE " + _featureContext["RecordType_" + loggingStep.rowNo].ToString());
                    _featureContext["ReservationCheckFlag_" + loggingStep.rowNo] = false;
                }
                else
                {
                    Log(ButtonName + "IS NOT AVAILABLE ");
                    Assert.Fail(ButtonName + " IS NOT AVAILABLE");
                }
            }
        }
        else
        {
            _featureContext["ReservationCheckFlag_" + loggingStep.rowNo] = true;
        }

        //if(driver.IsDisplayed(errorNotification))
        //{
        //    Log(" ERROR MESSAGE IS DISPLAYED " + driver.GetTextFromElement(errorNotification));
        //    Assert.Fail(" ERROR MESSAGE IS DISPLAYED " + driver.GetTextFromElement(errorNotification));
        //}
    }


    public void ThenUserVerifyButtonFromTheViewLayout(string ButtonName)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(btnViewSubmissionList));
        if (VerifyItemsFromTheList(btnViewSubmissionList, ButtonName) == false)
            {
                driver.JSClick(btnMoreActionItems);
                if (VerifyItemsFromTheList(btnMoreActionList, ButtonName.Trim()) == true)
                {
                    driver.CaptureScreen(_scenarioContext);
                    Log(ButtonName + "BUTTON IS AVAILABLE");
                }
                else
                {
                    driver.CaptureScreen(_scenarioContext);
                    Log(ButtonName + "BUTTON IS NOT AVAILABLE");
                }
            }
            else
            {
                driver.CaptureScreen(_scenarioContext);
                Log(ButtonName + "BUTTON IS AVAILABLE");
            }
        }
        catch(Exception e)
        {
            Log("COULD NOT FIND THE TAB " + ButtonName);
            Assert.Fail("COULD NOT FIND THE TAB " + ButtonName);
        }
    }


    public bool VerifyItemsFromTheList(By by, string matchingText)
    {
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(20));
        var lst = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        foreach (IWebElement item in lst)
        {
            if (item.Text.ToLower().Equals(matchingText.ToLower()))
            {
                return true;
            }
        }
        return false;
    }

    public void IceCheckRefresh()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(btnRefresh));
        Assert.IsTrue( driver.IsDisplayed(btnRefresh),"REFRESH BUTTON IS NOT DISPLAYED");
        driver.CaptureScreen(_scenarioContext);
        driver.JSClick(btnRefresh);
        System.Threading.Thread.Sleep(2000);
        Log("Clicked on Refresh button");
    }

    public void ThenClickedOnCloseButtonDuringLicenceCheck()
    {
        driver.CaptureScreen(_scenarioContext);
        driver.WaitForElementToPresent(btnCloseLincenseCheck);
        driver.WaitAndClick(btnCloseLincenseCheck);
        Log("Clicked on Refresh button");
    }

    public void VerifyIceCheckUpdate(string iceCheckAction)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        String IceStatus = "";
        if (_featureContext["App_Version"+ loggingStep.rowNo].ToString().Trim().Equals("1"))
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(tabDetails));
            driver.ScrollToCenter(tabDetails);
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblDetailsTabFieldsName));
            IceStatus = GetValueUsingFieldName("ICE Status", lblDetailsTabFieldsName, lblDetailsFieldValue);
        }
        if (_featureContext["App_Version"+ loggingStep.rowNo].ToString().Trim().Equals("2"))
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(TabCreateSubmissionlist));
            int tabCount = GetListIndexFromList("Policy", TabCreateSubmissionlist);
            NavigateToCreateSubmissionTabs("Policy");
            if (tabCount == 1)
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblUWProducerTabFieldsName));
                IceStatus = GetValueUsingFieldName("ICE Status", lblUWProducerTabFieldsName, lblUWProducerFieldsValue);
            }
            if (tabCount == 2)
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblSGSTabFieldsName));
                IceStatus = GetValueUsingFieldName("ICE Status", lblSGSTabFieldsName, lblSGSFieldsValue);
            }
        }
        if(iceCheckAction.Trim().ToLower().ToString().Equals("before"))
        {
            Assert.IsTrue(IceStatus.Equals("Unchecked"), "ICE CHECK FIELD IS NOT UPDATED AND THE DISPLAYED VALUE IS = " + IceStatus);
        }
        if (iceCheckAction.Trim().ToLower().ToString().Equals("after"))
        {
            Assert.IsTrue(IceStatus.Equals("Pass"), "ICE CHECK FIELD IS NOT UPDATED AND THE DISPLAYED VALUE IS = " + IceStatus);
        }
        Log("ICE CHECK FIELD IS UPDATED WITH THE VALUE " + IceStatus);
    }

   
    public void ThenUserLicenceCheckStatusIsUpdatedInTheSubmissionDetailsPage(string SubmissionVersion)
    {
        string LicenseCheckStatus = "";
        if (SubmissionVersion.ToLower().Equals("submission1_0"))
        {
            driver.WaitForElementToPresent(tabProducer1_0);
            driver.WaitAndClick(tabProducer1_0);
            driver.WaitForElementToPresent(lblLicensingStatus);
            LicenseCheckStatus = GetValueUsingFieldName("Licensing Status", lblUWProducerTabFieldsName, lblUWProducerFieldsValue);
            //Assert.IsTrue(LicenseCheckStatus.Equals("Pass"), "License CHECK FIELD IS NOT UPDATED AND THE DISPLAYED VALUE IS = " + LicenseCheckStatus);
        }

        if (SubmissionVersion.ToLower().Equals("submission2_0"))
        {
            NavigateToCreateSubmissionTabs("Producer");
            driver.WaitForElementToPresent(lblLicensingStatus);
            LicenseCheckStatus = GetValueUsingFieldName("Licensing Status", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue);
        }

        if (LicenseCheckStatus.ToLower().Equals("pass") || LicenseCheckStatus.ToLower().Equals("not nequired"))
        {
            Log("License CHECK FIELD IS UPDATED AND THE DISPLAYED VALUE IS = " + LicenseCheckStatus);
        }
        else
        {
            Log("License CHECK FIELD IS NOT UPDATED AND THE DISPLAYED VALUE IS = " + LicenseCheckStatus);
        }          
        Log("ICE CHECK FIELD IS UPDATED WITH THE VALUE " + LicenseCheckStatus);
    }

    // THIS METHOD IS USED TO GET THE URL FROM THE FEATURE CONTEXT DURING RUN TIME AND NAVIGATE TO SPECIFIC URL BASED ON THE VERSION
    public void NavigateToCreatedSubmissionRecord(string Version)
    {
        string submissionUrl = "";
        switch (Version)
        {
            case "1.0":
                 _featureContext["URL_" + loggingStep.rowNo] =  ReadSubmissionLinkFromFile(Submission1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt");
                submissionUrl = _featureContext["URL_" + loggingStep.rowNo].ToString();
                _featureContext["App_Version" + loggingStep.rowNo] = "1";
                _featureContext["RecordType_" + loggingStep.rowNo] = ReadRecordTypeFromFile(Submission1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt");
                break;

            case "Renewal 1.0":
                _featureContext["URL_" + loggingStep.rowNo] = ReadSubmissionLinkFromFile(Renewal1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt");
                submissionUrl = _featureContext["URL_" + loggingStep.rowNo].ToString();
                _featureContext["App_Version" + loggingStep.rowNo] = "1";
                _featureContext["RecordType_" + loggingStep.rowNo] = ReadRecordTypeFromFile(Renewal1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt");
                break;

            default:
                Console.WriteLine("COULD NOT FIND THE VERSION PROVIDED " + Version);
                break;
        }
        Console.WriteLine("Submission URL " + submissionUrl);
        driver.Navigate().GoToUrl(submissionUrl);
        driver.WaitForNextPage();
        System.Threading.Thread.Sleep(3000);
    }

    // THIS METHOD IS USED TO GET THE URL FROM FILE DURING OFFLINE MODE AND NAVIGATE TO THE URL
    public void NavigateToCreatedSubmissionRecord(string Version, string ExecutionType)
    {
        string submissionUrl = "";

        if (ExecutionType.ToLower().Trim().Equals("validation") & Version.Equals("1.0"))
        {
            submissionUrl = ReadSubmissionLinkFromFile(Submission1_0ValidationRecords + loggingStep.rowNo + ".txt");
            _featureContext["App_Version" + loggingStep.rowNo] = "1";
            _featureContext["TestingType_" + loggingStep.rowNo] = "Validation";
            _featureContext["RecordType_" + loggingStep.rowNo] = ReadRecordTypeFromFile(Submission1_0ValidationRecords + loggingStep.rowNo + ".txt");
        }
        else if (ExecutionType.ToLower().Trim().Equals("validation") & Version.Equals("Renewal 1.0"))
        {
            submissionUrl = ReadSubmissionLinkFromFile(Renewal1_0Filsepath + loggingStep.rowNo + ".txt");
            _featureContext["App_Version" + loggingStep.rowNo] = "1";
            _featureContext["TestingType_" + loggingStep.rowNo] = "Validation";
            //_featureContext["RecordType_" + loggingStep.rowNo] = ReadRecordTypeFromFile(Renewal1_0Filsepath + loggingStep.rowNo  + ".txt");
        }
        else
        {
            switch (Version)
            {
                case "1.0":
                    submissionUrl = ReadSubmissionLinkFromFile(Submission1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt");
                    _featureContext["App_Version" + loggingStep.rowNo] = "1";
                    _featureContext["AssignmentType_" + loggingStep.rowNo] = GetValueUsingFieldName("Policy Number - Assignment Type", lblDetailsTabFieldsName, lblDetailsFieldValue);
                    _featureContext["TestingType_" + loggingStep.rowNo] = "Functional";
                    break;

                case "Renewal 1.0":
                    submissionUrl = ReadSubmissionLinkFromFile(Renewal1_0Filsepath + loggingStep.rowNo + loggingStep.FeatureFileName + ".txt");
                    _featureContext["AssignmentType_" + loggingStep.rowNo] = GetValueUsingFieldName("Policy Number - Assignment Type", lblDetailsTabFieldsName, lblDetailsFieldValue);
                    _featureContext["App_Version" + loggingStep.rowNo] = "1";
                    _featureContext["AssignmentType_" + loggingStep.rowNo] = GetValueUsingFieldName("Policy Number - Assignment Type", lblDetailsTabFieldsName, lblDetailsFieldValue);
                    _featureContext["TestingType_" + loggingStep.rowNo] = "Functional";
                    break;

                default:
                    Console.WriteLine("COULD NOT FIND THE VERSION PROVIDED " + Version);
                    break;
            }
        }
        Log("NAVIGATING TO URL " + submissionUrl);
        Console.WriteLine("Submission URL " + submissionUrl);
        driver.Navigate().GoToUrl(submissionUrl);
        driver.WaitForNextPage();
        System.Threading.Thread.Sleep(3000);
    }

    public void NavigateToCreatedSubmissionRecordforthestageName(string Version, string ExecutionType, string stageName)
    {
        string submissionUrl = "";

        if (ExecutionType.ToLower().Trim().Equals("validation") & Version.Equals("1.0"))
        {
            submissionUrl = ReadSubmissionLinkFromFile(Submission1_0ValidationRecords + loggingStep.rowNo+ stageName + ".txt");
            _featureContext["App_Version" + loggingStep.rowNo] = "1";
            _featureContext["TestingType_" + loggingStep.rowNo] = "Validation";
            _featureContext["RecordType_" + loggingStep.rowNo] = ReadRecordTypeFromFile(Submission1_0ValidationRecords + loggingStep.rowNo +stageName+ ".txt");
        }
        else if (ExecutionType.ToLower().Trim().Equals("validation") & Version.Equals("Renewal 1.0"))
        {
            submissionUrl = ReadSubmissionLinkFromFile(Renewal1_0Filsepath + loggingStep.rowNo + ".txt");
            _featureContext["App_Version" + loggingStep.rowNo] = "1";
            _featureContext["TestingType_" + loggingStep.rowNo] = "Validation";
            //_featureContext["RecordType_" + loggingStep.rowNo] = ReadRecordTypeFromFile(Renewal1_0Filsepath + loggingStep.rowNo  + ".txt");
        }
 
        Log("NAVIGATING TO URL " + submissionUrl);
        Console.WriteLine("Submission URL " + submissionUrl);
        driver.Navigate().GoToUrl(submissionUrl);
        driver.WaitForNextPage();
        System.Threading.Thread.Sleep(3000);
    }


    public void ThenUserNavigatedToExistingRecord(string ExistingURL)
    {
        Console.WriteLine("Existing URL : " + ExistingURL);
        driver.Navigate().GoToUrl(ExistingURL);
        Log("NAVIGATED TO EXISTING URL");
    }

    public void NavigateToApp(string AppName)
    {
        Console.WriteLine(driver.GetTextFromElement(lblAppName).ToString());
        Console.WriteLine(AppName.ToString());
        if (driver.GetTextFromElement(lblAppName).ToString() != AppName.ToString())
        {
            driver.WaitForElementToPresent(txtSearchSetup);
            driver.WaitTillElementIsClickable(btnAppLauncher);
            driver.WaitAndClick(btnAppLauncher);
            driver.WaitForElementToPresent(txtSearchAppandItem);
            driver.ClearAndSend(txtSearchAppandItem, AppName);
            driver.WaitTillElementIsClickable(optionAssumedInsurance);
            driver.JSClick(optionAssumedInsurance);
            driver.WaitTillElementIsClickable(btnNewProject);
            Assert.IsTrue(driver.IsDisplayed(btnNewProject), "COULDN'T NAVIGATE TO " + AppName + " App");
            Log("NAVIGATED TO " + AppName + "App");
        }
        Log("NAVIGATED TO " + driver.GetTextFromElement(lblAppName) + "App");
    }

    public void StageProgression(string BeforeStageValue, string AfterStageValue)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblClientNameInSubmission));
            System.Threading.Thread.Sleep(2000);
        }
        catch (Exception e)
        {
            Console.WriteLine("COULD NOT FIND THE FIELDS");
            Log("COULD NOT FIND THE FIELDS");
            Assert.Fail("COULD NOT FIND THE FIELDS");
        }
        driver.ScrollByPixel(100);
        Assert.IsTrue(GetValueUsingFieldName("*Stage", lblslctPickListName1_0, slctPickListField1_0).Equals(BeforeStageValue), "STAGE IS NOT IN " + BeforeStageValue);
        driver.CaptureScreen(_scenarioContext);
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Stage", AfterStageValue);
        driver.CaptureScreen(_scenarioContext);
        Assert.IsTrue(GetValueUsingFieldName("*Stage", lblslctPickListName1_0, slctPickListField1_0).Equals(AfterStageValue), "COULD NOT SELECT THE VALUE IN STAGE FIELD");
        Log("STAGE VALUE IS SELECTED TO " + AfterStageValue);

        // UPDATE THE VALUE OF FAC RI PRM VALUE DURING BOUND STAGE
        if (AfterStageValue.Equals("Bound") && (_featureContext["App_Version" + loggingStep.rowNo].ToString().Equals("1")))
        {
            ThenUserUpdatedRequiredField("FAC RI PRM", "Calculated Amount");
        }
    }

    public void StageProgression2_0(string BeforeStageValue, string AfterStageValue)
    {
        Assert.IsTrue(GetValueUsingFieldName("*Stage", lblslctPickListName1_0, slctPickListField1_0).Equals(BeforeStageValue), "STAGE IS NOT IN " + BeforeStageValue);
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Stage", AfterStageValue);
        Assert.IsTrue(GetValueUsingFieldName("*Stage", lblslctPickListName1_0, slctPickListField1_0).Equals(AfterStageValue), "COULD NOT SELECT THE VALUE IN STAGE FIELD");
        Log("STAGE VALUE IS SELECTED TO " + AfterStageValue);
    }
    public void ProvideDeclainedReason()
    {
        driver.ScrollToCenter(ddDeclainedReason);
        Assert.IsTrue(SelectRandomValueFromDropdown(ddDeclainedReason, ddDeclainedReasonValue, "LostNTU or Declined Reason"), "Could not select values for Declained Reason");
        Log("Successfully selected the value for Declained Reason");
    }

    public void ThenUserFillTheValueInField(string FieldName)
    {
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName, "Random");
        Log("Updated value in the field "+ FieldName);
        driver.CaptureScreen(_scenarioContext);
    }

    public void ThenUserFillTheValueInField(string FieldName, string FieldValue)
    {
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName, FieldValue);
        Log("Updated value in the field " + FieldName);
        driver.CaptureScreen(_scenarioContext);
    }

    public void ThenUserFillTheValueInSubmissionRequiredForBoundStage(string DateFormat)
    {
        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Number - Current", GenerateRandomNumbers());

        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "Policy Bound Date", GetCurrentDate(DateFormat));

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Renewable", "Yes");
        
        driver.ScrollToCenter(tabPCCodin2_0);
        NavigateToEditSubmissionTabs("PC Coding");
        //driver.ScrollToCenter(tabPCCodin2_0);
        //driver.WaitAndClick(tabPCCodin2_0);
        driver.WaitForElementToPresent(lblTPA);
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "TPA", "Random");
        driver.CaptureScreen(_scenarioContext);

        //if (RecordType.Equals("Political Risk"))
        //{
            driver.ScrollToCenter(tabPCCodin2_0);
            NavigateToEditSubmissionTabs("Reinsurance");
            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Treaty Applies", "No");
            driver.CaptureScreen(_scenarioContext);
        //}

        if (_featureContext["RecordType_"+ loggingStep.rowNo].ToString().Equals("Starr Accident & Health"))
        {
            driver.ScrollToCenter(tabPCCodin2_0);
            NavigateToEditSubmissionTabs("Policy");
            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Covered Lives", GenerateRandomNumbers());
            driver.CaptureScreen(_scenarioContext);
        }

        driver.ScrollToCenter(tabPCCodin2_0);
        NavigateToEditSubmissionTabs("Submission Details");
        //driver.ScrollToCenter(tabSubmissionDetails2_0);
        //driver.WaitAndClick(tabSubmissionDetails2_0);
        driver.WaitForNextPage();
        Log("FILLED THE VALUES IN MANDATORY FIELDS WHICH ARE REQIRED FOR BOUND STAGE");
    }

    public void ThenUserFillTheValueInSubmissionRequiredForCancelledStage(string DateFormat)
    {
        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "Policy Cancelled Date", GetCurrentDate(DateFormat));
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Cancellation Reason/Type", "Random");
    }


    public void VerifySubmissionStatus(string status)
    {
        System.Threading.Thread.Sleep(2000);
        if (_featureContext["App_Version" + loggingStep.rowNo].ToString().Equals("1"))
        {
            NavigateToCreateSubmissionTabs("Details");
        }
        if (_featureContext["App_Version" + loggingStep.rowNo].ToString().Equals("2"))
        {
            NavigateToCreateSubmissionTabs("Submission Details");
        }
        
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblDetailsTabFieldsName));
        string StageValue = GetValueUsingFieldName("Stage", lblDetailsTabFieldsName, lblDetailsFieldValue);
        Assert.IsTrue(StageValue.Equals(status), "SUBMISSION STATUS IS NOT CHAGED TO " + status);
        Log("SUBMISSION STATUS IS CHANGED TO  " + status);
    }
    public void ThenUserVerifyUpdatedClientIsUpdated(string clientvalue)
    {
        string updatedValue = GetValueUsingFieldName("Client Name", lblDetailsTabFieldsName, lblDetailsFieldValue);
        Assert.AreEqual(updatedValue.Trim(), clientvalue.Trim(), "Client value is not updated");
        Log("Client value is updated successfully");
        Console.WriteLine("Client value is updated successfully");
    }
    public void ThenUserVerifiedSubmissionRecordStatusChangedToIn(string status)
    {
        Assert.IsTrue(GetValueUsingFieldName("Stage", lblDetailsTabFieldsName, lblDetailsFieldValue).Equals(status), "SUBMISSION STATUS IS NOT CHANGED TO " + status);
        Log("SUBMISSION STATUS IS CHANGED TO " + status);
    }

    public void VerifyErrormessage(string errormessage)
    {
        string[] ValidationErrorMessage = errormessage.Split("$");
        Console.WriteLine("Length of Error message " + ValidationErrorMessage.Length);
        if (ValidationErrorMessage.Length == 1)
        {
            string DisplayedErrorMessage = "";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
            try
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblErrorMessages));
                driver.ScrollToCenter(lblErrorMessages);
                driver.CaptureScreen(_scenarioContext);
                DisplayedErrorMessage = driver.GetTextFromElement(lblErrorMessages);
                Console.WriteLine("EXPECTED ERROR MESSAGE  - " + errormessage);
                Console.WriteLine("Displayed Error Message - " + DisplayedErrorMessage);                
                Assert.AreEqual(errormessage.ToString(), DisplayedErrorMessage.ToString(), "DISPLAYED ERROR MESSAGE IS NOT VALID");
                Log("EXPECTED ERROR MESSAGE IS DISPLAYED \n" + DisplayedErrorMessage);
                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                driver.CaptureScreen(_scenarioContext);
                Console.WriteLine("EXPECTED ERROR MESSAGE  - " + errormessage);
                Console.WriteLine("Displayed Error Message - " + DisplayedErrorMessage);
                Log("EXPECTED ERROR MESSAGE  - " + errormessage);
                Log("Displayed Error Message - " + DisplayedErrorMessage);
                Assert.Fail("EXPECTED ERROR MESSAGE IS NOT DISPLAYED \n" + errormessage);
            }
        }
        if (ValidationErrorMessage.Length > 1)
        {
            string DisplayedErrorMessage = "";
            bool flag = false;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
            try
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblErrorMessages));
                driver.ScrollToCenter(lblErrorMessages);
                driver.CaptureScreen(_scenarioContext);

                DisplayedErrorMessage = driver.GetTextFromElement(lblErrorMessages);
                for (int i = 0; i < ValidationErrorMessage.Length; i++)
                {
                    if (ValidationErrorMessage[i].Trim().ToString().Equals(DisplayedErrorMessage.ToString()))
                    {
                        Log("EXPECTED ERROR MESSAGE IS DISPLAYED \n" + ValidationErrorMessage[i].Trim().ToString());
                        System.Threading.Thread.Sleep(2000);
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    driver.CaptureScreen(_scenarioContext);
                    Console.WriteLine("EXPECTED ERROR MESSAGE  - " + errormessage);
                    Console.WriteLine("Displayed Error Message - " + DisplayedErrorMessage);
                    Log("EXPECTED ERROR MESSAGE  - " + errormessage);
                    Log("Displayed Error Message - " + DisplayedErrorMessage);
                    Assert.Fail("EXPECTED ERROR MESSAGE IS NOT DISPLAYED \n" + errormessage);
                }
            }
            catch (Exception e)
            {
                driver.CaptureScreen(_scenarioContext);
                Console.WriteLine("EXPECTED ERROR MESSAGE  - " + errormessage);
                Console.WriteLine("Displayed Error Message - " + DisplayedErrorMessage);
                Log("EXPECTED ERROR MESSAGE  - " + errormessage);
                Log("Displayed Error Message - " + DisplayedErrorMessage);
                Assert.Fail("EXPECTED ERROR MESSAGE IS NOT DISPLAYED \n" + errormessage);
            }
        }
    }

    public void ThenVerifyErrorMessageIsDisplayed(string errormessage)
    {
        Assert.IsTrue(driver.WaitForElementToPresent(lblErrorMessageInStageProgression), "ERROR MESSAGE IS DISPLAYED");
        driver.ScrollToCenter(lblErrorMessageInStageProgression);
        driver.CaptureScreen(_scenarioContext);
        string DisplayedErrorMessage = driver.GetTextFromElement(lblErrorMessageInStageProgression);
        Assert.AreEqual(errormessage.ToString(), DisplayedErrorMessage.ToString(), "DISPLAYED ERROR MESSAGE IS NOT VALID");
        Log("DISPLAYED VALID ERROR MESSAGE");
    }

    public void ThenVerifyErrorMessageIsDisplayedUnderField(string errormessage)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblErrorMessageInStageField));
            driver.ScrollToCenter(lblErrorMessageInStageField);
            driver.CaptureScreen(_scenarioContext);
            string DisplayedErrorMessage = driver.GetTextFromElement(lblErrorMessageInStageField);
            Assert.AreEqual(errormessage.ToString(), DisplayedErrorMessage.ToString(), "DISPLAYED ERROR MESSAGE IS NOT VALID");
            Log("DISPLAYED VALID ERROR MESSAGE");
        }
        catch(Exception e)
        {
            Log("EXPECTED ERROR MESSAGE IS NOT DISPLAYED " + errormessage);
            Assert.Fail("EXPECTED ERROR MESSAGE IS NOT DISPLAYED " + errormessage);
        }
       
    }

    public void ReservationChecks(string ReservationCheckName)
    {
        //ParentWindow = driver.CurrentWindowHandle;
        driver.WaitForElementToPresent(btnMoreActions);
        driver.WaitAndClick(btnMoreActions);
        System.Threading.Thread.Sleep(1000);
        Assert.IsTrue(driver.SelectFromListUsingJS(btnActionNames, ReservationCheckName), "COULD NOT SELECT THE VALUE " + ReservationCheckName);
        System.Threading.Thread.Sleep(3000);
    }

    public void VerifyReservationCheckPages(string ReservationCheckPagese)
    {
        if (_featureContext["ReservationCheckFlag_"+ loggingStep.rowNo].Equals(true))
        {
            for(int Window=0; Window < 3; Window++)
            {
                if(driver.WindowHandles.Count == 2)
                {
                    Console.WriteLine("RESERVATION WINDOW IS DISPLAYED");
                    break;
                }
                System.Threading.Thread.Sleep(3000);
            }
            Assert.AreEqual(2, driver.WindowHandles.Count, "RESERVATION CHECK PAGE IS NOT DISPLAYED");
            Assert.IsTrue(SwitchToWindow(), "COULD NOT NAVIGATE TO ANOTHER WINDOW");
            Assert.IsTrue(driver.WaitForTitleContains("Reservation Check"), "COULD NOT NAVIGATE TO RESERVATION CHECK WINDOW");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblExecutedBy));
            System.Threading.Thread.Sleep(2000);
            //driver.WaitForElementToPresent(lblExecutedBy);
            Log("SWITCH TO RESERVATION CHECK PAGE");
            //Assert.IsTrue(driver.GetTextFromElement(lblExecutedBy).Trim().Equals(ExecutedByName.Trim()), "MISMATCH IN EXECUTED BY " + ExecutedByName);
            Assert.IsTrue(driver.GetTextFromElement(lblExecutedBy).Trim().Equals(LoginPage.LooggedInUser.Trim()), "MISMATCH IN EXECUTED BY " + LoginPage.LooggedInUser);

            Log("EXECUTED BY VALUES ARE MATCHING");
            //string ActualClientName = driver.GetTextFromElement(lblClientName).Trim();
            //Assert.IsTrue(ActualClientName.Equals(AttachedClientName.Trim()), "MISMATCH IN CLIENT NAME " + AttachedClientName);
            //Log("CLIENT NAME VALUES ARE MATCHING");
        }
        else
        {
            Console.WriteLine("RESERVATION CHECK IS NOT APPLICABLE FOR THIS RECORD TYPE " + _featureContext["RecordType_"+ loggingStep.rowNo].ToString());
            Log(ActionButtonName +" BUTTON IS NOT APPLICABLE FOR THIS RECORD TYPE " + _featureContext["RecordType_"+ loggingStep.rowNo].ToString());
        }
    }

    public void VerifyFuzzyMatches()
    {
        if (_featureContext["ReservationCheckFlag_"+ loggingStep.rowNo].Equals(true))
        {
            driver.SwitchToFrame(0);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblNearFuzzyMatch));
            driver.MoveToTheElement(lblNearFuzzyMatch);
            driver.ScrollByPixel(300);

            //Assert.IsTrue(driver.GetTextFromElement(lblFuzzyMatch).ToString().Equals("Potential Fuzzy Matches"));
            Log("VERIFIED THE FUZZY MATCH");
        }
        else
        {
            Console.WriteLine("RESERVATION CHECK IS NOT APPLICABLE FOR THIS RECORD TYPE " + _featureContext["RecordType_"+ loggingStep.rowNo].ToString());
            Log(ActionButtonName + " BUTTON IS NOT APPLICABLE FOR THIS RECORD TYPE " + _featureContext["RecordType_"+ loggingStep.rowNo].ToString());
        }
    }

    public void ThenNavigateBackToParentSubmission()
    {
        string ParentWindow = _featureContext["ParentWindow_"+ loggingStep.rowNo].ToString();
        if (_featureContext["ReservationCheckFlag_"+ loggingStep.rowNo].Equals(true))
        {
            driver.Close();
            System.Threading.Thread.Sleep(1000);
            driver.SwitchTo().Window(ParentWindow);
            Log("CLOSING THE CHILD WINDOW AND NAVIGATING BACK TO PARENT SUBMISSION");
        }
        else
        {
            Console.WriteLine("RESERVATION CHECK IS NOT APPLICABLE FOR THIS RECORD TYPE " + _featureContext["RecordType_"+ loggingStep.rowNo].ToString());
            Log(ActionButtonName + " BUTTON IS NOT APPLICABLE FOR THIS RECORD TYPE " + _featureContext["RecordType_"+ loggingStep.rowNo].ToString());
        }
    }

    public bool SwitchToWindow()
    {
        string originalWindow = driver.CurrentWindowHandle;
        Console.WriteLine("Original Window = " + originalWindow);
        foreach (string window in driver.WindowHandles)
        {
            Console.WriteLine("Window = " + window);
            if (originalWindow != window)
            {
                driver.SwitchTo().Window(window);
                Log("SWITCH TO ANOTHER WINDOW");
                return true;
            }
        }
        return false;
    }


    public void PerformActionOnCreateSubmission(string FieldType, By ListFieldNames, By ActionFieldItems, string FieldName, string FieldValue)
    {
        Dictionary<string, int> FieldIndexs = new Dictionary<string, int>();
        FieldIndexs.Clear();
        int FieldIndex;
        var lists = driver.ListOfElements(ListFieldNames);
        for (int i = 0; i < lists.Count; i++)
        {
            if (!FieldIndexs.ContainsKey((lists[i].Text.ToString())))
            {
                FieldIndexs.Add(lists[i].Text.ToString(), i);
            }
        }

        if (!FieldIndexs.ContainsKey(FieldName))
        {
            string[] value = FieldName.Split("*");
            FieldIndex = FieldIndexs[value[1]];
        }
        else
        {
            FieldIndex = FieldIndexs[FieldName];
        }
        try
        {
            ActionField = driver.ListOfElements(ActionFieldItems);

            if (FieldIndex != -1)
            {
                if (FieldType.ToLower().Equals("date"))
                {
                    ActionField = driver.ListOfElements(ActionFieldItems);
                    driver.MoveToTheElement(ActionField[FieldIndex]);
                    driver.ScrollByPixel(40);
                    ActionField[FieldIndex].ClearAndTypeText(FieldValue);
                    Log("SUCCESSFULLY INPUTTED THE VALUE IN THE FIELD  " + FieldName);
                }
                if (FieldType.ToLower().Equals("input"))
                {
                    ActionField = driver.ListOfElements(ActionFieldItems);
                    driver.MoveToTheElement(ActionField[FieldIndex]);
                    driver.ScrollByPixel(40);
                    ActionField[FieldIndex].ClearAndTypeText(FieldValue);
                    Log("SUCCESSFULLY INPUTTED THE VALUE IN THE FIELD  " + FieldName);
                }
                if (FieldType.ToLower().Equals("lookup"))
                {
                    ActionField = driver.ListOfElements(ActionFieldItems);
                    driver.MoveToTheElement(ActionField[FieldIndex]);
                    driver.ScrollByPixel(40);
                    ActionField[FieldIndex].Click();
                    ActionField[FieldIndex].SendKeys(FieldValue);

                    string EnteredValue = ActionField[FieldIndex].GetAttribute("data-value");
                    Assert.AreEqual(FieldValue.Trim(), EnteredValue.Trim(), "ENTERED VALUES ARE NOT EQUAL");
                    System.Threading.Thread.Sleep(1000);
                    ActionField[FieldIndex].Click();
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                    try
                    {
                        System.Threading.Thread.Sleep(2000);
                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(ActionField[FieldIndex].FindElements(lblLookupResult)));
                        SelectLookUpValues = ActionField[FieldIndex].FindElements(lblLookUpFieldValue1);
                    }
                    catch (Exception e)
                    {
                        Log(FieldValue + " SEARCHED VALUES ARE NOT DISPLAYED IN THE FIELD " + FieldName);
                        Assert.Fail(FieldValue + " SEARCHED VALUES ARE NOT DISPLAYED IN THE FIELD " + FieldName);
                    }

                    SelectLookUpValuesClick = ActionField[FieldIndex].FindElements(lblLookUpFieldValue2);
                    foreach (IWebElement child in SelectLookUpValues)
                    {
                        if (child.GetAttribute("title").Trim().Equals(FieldValue.Trim()))
                        {
                            child.Click();
                            break;
                        }
                    }

                    string ActualValue = ActionField[FieldIndex].GetAttribute("placeholder");
                    Assert.AreEqual(ActualValue.Trim(), FieldValue.Trim(), "COULD NOT INPUT PROPER VALUE IN THE FIELD " + FieldName);
                    Log("SUCCESSFULLY INPUTTED THE VALUE IN THE FIELD  " + FieldName);
                }
                if (FieldType.ToLower().Equals("picklist"))
                {
                    ActionField = driver.ListOfElements(ActionFieldItems);
                    driver.MoveToTheElement(ActionField[FieldIndex]);
                    driver.ScrollByPixel(40);
                    if (!ActionField[FieldIndex].GetAttribute("class").Contains("disabled"))
                    {
                        string picklistValue = ActionField[FieldIndex].GetAttribute("data-value");

                        if (!picklistValue.Trim().Equals(FieldValue.Trim()))
                        {
                            ActionField[FieldIndex].Click();
                            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
                            ChildValues = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(ActionField[FieldIndex].FindElements(lblPicklistChildValues)));
                            if (ChildValues.Count > 1)
                            {
                                string value = "";
                                bool flag = false;
                                // IF THE FIELD VALUE IS EQUAL TO RANDOM THEN IT SELECT RANDOM VALUE IN THE PICKLIST 
                                if (FieldValue.ToLower().Trim().Equals("random"))
                                {
                                    int RandomeValue = GenerateRandomNumberForPickListValues(1, ChildValues.Count - 1);
                                    string RandomSelectingValue = ChildValues[RandomeValue].GetElementText();

                                    if (RandomSelectingValue.Trim().Equals("Columbus"))
                                    {
                                        RandomSelectingValue = ChildValues[RandomeValue + 1].GetElementText();
                                    }

                                    foreach (IWebElement child in ChildValues)
                                    {
                                        if (child.GetElementText().Trim().Equals(RandomSelectingValue.Trim()))
                                        {
                                            driver.MoveToTheElementAndClick(child);
                                        }
                                    }
                                }
                                else
                                {
                                    // IF THE FIELD VALUE IS NOT EQUAL TO RANDOM THEN IT SELECT PROVIDED DATA FROM THE EXCEL
                                    string[] FieldValues = FieldValue.Split("#");
                                    if (FieldValues.Length > 1)
                                    {
                                        value = FieldValues[GenerateRandomNumbers(1, FieldValues.Length)];
                                    }
                                    else
                                    {
                                        value = FieldValue;
                                    }
                                    foreach (IWebElement child in ChildValues)
                                    {
                                        if (child.GetElementText().Trim().Equals(value))
                                        {
                                            driver.MoveToTheElementAndClick(child);
                                            flag = true;
                                            Log(value + " PICKLIST VALUES ARE INPUTED");
                                            break;
                                        }
                                    }
                                    if (flag == false)
                                    {
                                        Assert.Fail(value + " IS NOT PRESENT IN THE PICKLIST" + FieldName);
                                    }
                                }
                            }
                            else
                            {
                                ActionField[FieldIndex].Click();
                            }
                        }
                        else
                        {
                            Log("PICKLIST VALUE IS ALREADY SELECTED AS " + FieldValue);
                            Console.WriteLine("PICKLIST VALUE IS ALREADY SELECTED AS " + FieldValue);
                        }
                    }
                    else
                    {
                        Log(" PICKLIST IS DISABLED COULD NOT INPUT VALUES IN THE FIELD  " + FieldName);
                        Console.WriteLine(" PICKLIST IS DISABLED COULD NOT INPUT VALUES IN THE FIELD  " + FieldName);
                    }
                }
                if (FieldType.ToLower().Equals("checkbox"))
                {
                    ActionField = driver.ListOfElements(ActionFieldItems);
                    driver.MoveToTheElement(ActionField[FieldIndex]);
                    // FOR CHECKBOX , IF CHECKBOX NEED TO BE CHECKED THEN PROVIDING 'YES' AS INPUT WILL CLICK ON THE CHECKBOX
                    if (FieldValue.Trim().ToLower().Equals("yes"))
                    {
                        if (ActionField[FieldIndex].Selected == false)
                        {
                            driver.MoveToTheElement(ActionField[FieldIndex]);
                            ActionField[FieldIndex].Click();
                            Assert.IsTrue(ActionField[FieldIndex].Selected, "COULD NOT SELECT THE CHECKBOX FOR THE FIELD " + FieldName);
                        }
                    }
                    // FOR CHECKBOX , IF CHECKBOX NEED TO BE UNCHECKED THEN PROVIDING 'NO' AS INPUT WILL UNCHECK THE CHECKBOX
                    if (FieldValue.Trim().ToLower().Equals("no"))
                    {
                        if (ActionField[FieldIndex].Selected == true)
                        {
                            ActionField[FieldIndex].Click();
                            Assert.IsTrue(!ActionField[FieldIndex].Selected, "UNCHECKED THE CHECKBOX " + FieldName);
                        }
                    }
                    Log("CHECKBOX IS SELECTED FOR THE FIELD " + FieldName);
                }
            }
            else
            {
                Assert.Fail(" COULD NOT FIND THE FIELD " + FieldName);
            }
        }
        catch (Exception e)
        {
            Log("TRIED TO FILL VALUES IN THE FIELD ' " + FieldName + "' FOLLOWING ERROR ENCOUNTERED " + e.Message.ToString());
            Assert.Fail("TRIED TO FILL VALUES IN THE FIELD ' " + FieldName + "' FOLLOWING ERROR ENCOUNTERED " + e.Message.ToString());

        }
    }


    public string GetValueUsingFieldName(string FieldName, By ListFieldName, By ListFields)
    {

        IList<IWebElement> FieldNamesList = driver.ListOfElements(ListFieldName);
        IList<IWebElement> FieldsList = driver.ListOfElements(ListFields);

        for (int FieldCount = 0; FieldCount < FieldNamesList.Count - 1; FieldCount++)
        {
            string ActualFieldname = FieldNamesList[FieldCount].GetElementText();
            //Console.WriteLine(ActualFieldname + " == " + FieldName);
            if (ActualFieldname.Trim().Equals(FieldName.Trim()))
            {
                driver.MoveToTheElement(FieldsList[FieldCount]);
                string FieldValue = FieldsList[FieldCount].GetElementText();
                return FieldValue;
            }
        }
        //Console.WriteLine("COULD NOT FIND THE FIELD " + FieldName);
        Log("COULD NOT FIND THE FIELD " + FieldName);
        return null;
    }

    public int GetListIndexFromList(string FieldName, By ListFieldName)
    {

        IList<IWebElement> FieldNamesList = driver.ListOfElements(ListFieldName);

        for (int FieldCount = 0; FieldCount < FieldNamesList.Count - 1; FieldCount++)
        {
            string ActualFieldname = FieldNamesList[FieldCount].GetElementText();
            //Console.WriteLine(ActualFieldname + " == " + FieldName);
            if (ActualFieldname.Trim().Equals(FieldName.Trim()))
            {
                return FieldCount;
            }
        }
        return -1;
    }


    public void ThenUserFillTheValuesInEndorsementCreationPageFor(string[] data, string EndorsementType)
    {
        string ProductionMonth = data[0];
        string ProductionYear = data[1];
        string UnderwritingCreditBranch = data[2];
        string UCBShare = data[3];
        string PurchaseExternalFAC = data[4];
        string PercentPolicyBookedPremium = data[5];
        string PurchasedExternalFACReinsurance = data[6];
        string FACBasisofAcceptance = data[7];
        string FACRIPRM = data[8];
        string DateFormat = data[9];

        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Effective Date", GetCurrentDate(DateFormat) );

        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Expiration Date", GetExpirationDateforFuture(DateFormat));

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Month", ProductionMonth);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Year", ProductionYear);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "MAS", "Random");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Underwriting Credit Branch", UnderwritingCreditBranch);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchase External FAC (UCB)", PurchaseExternalFAC);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Shell Policy", "Random");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchased External FAC Reinsurance", PurchasedExternalFACReinsurance);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "FAC Basis of Acceptance", FACBasisofAcceptance);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "TRIA/Non Cert Included", "No");

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "UCB Share %", UCBShare);

        driver.SelectValueFromDropDownListUsingValue(slctEndrosementType, EndorsementType);

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Premium Written Share %", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "PML", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Premium Estimated Signing %", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100% Policy Limit Amount (Currency)", Generate2DigitRandomNumber());

        if(EndorsementType.ToLower().ToString().Equals("endorsement cancellation"))
        {
            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100 Percent Policy Booked Premium", PercentPolicyBookedPremium);
        }
        else
        {
            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100 Percent Policy Booked Premium", Generate2DigitRandomNumber());
        }        

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "100% TIV", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Starr Share Estimated Forecast Premium", GenerateRandomNumbers());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "FAC RI PRM", FACRIPRM);

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Ceding Commission Amount", GenerateDecimalNumber());

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Master/Local", "Random");
    }

    public void FillValuesInSection1_0(string[] SectionData)
    {
        string DateFormat = SectionData[0];
        string ProductionMonth = SectionData[1];
        string ProductionYear = SectionData[2];
        string UnderwritingCreditBranch = SectionData[3];
        string UCBShare =  SectionData[4];
        string Affiliated = SectionData[5];
        string PurchaseExternalFAC = SectionData[6];
        string PurchasedExternalFACReinsurance = SectionData[7];
        string FACBasisofAcceptance = SectionData[8];
        string FACRIPRM = SectionData[9];


        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Effective Date", GetExpirationDate(DateFormat));

        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Expiration Date", GetCurrentDate(DateFormat));

        if(_featureContext["AssignmentType_" + loggingStep.rowNo].ToString().ToLower().Equals("manual"))
        {

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Policy Number - Assignment Type", "Manual");

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Number - Current", GenerateRandomNumbers());
        }
        
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Month", ProductionMonth);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Year", ProductionYear);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "MAS", "Random");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Underwriting Credit Branch", UnderwritingCreditBranch);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Local Country", "random");

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "UCB Share %", UCBShare);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Carrier Branch", _featureContext["Carrier Branch" + loggingStep.rowNo].ToString());       

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Affiliated ?", Affiliated);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchase External FAC (UCB)", PurchaseExternalFAC);

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "PML", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Premium Estimated Signing %", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100% Policy Limit Amount (Currency)", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Shell Policy", "Random");

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100 Percent Policy Booked Premium", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "100% TIV", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchased External FAC Reinsurance", PurchasedExternalFACReinsurance);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "FAC Basis of Acceptance", FACBasisofAcceptance);

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "FAC RI PRM", FACRIPRM);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "TRIA/Non Cert Included", "No");

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Ceding Commission Amount", GenerateDecimalNumber());

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Master/Local", "Random");

    }

    public void FillValuesInTerrorism1_0(string[] TerrorismData)
    {
        string DateFormat = TerrorismData[0];
        string ProductionMonth = TerrorismData[1];
        string ProductionYear = TerrorismData[2];
        string UnderwritingCreditBranch = TerrorismData[3];
        string UCBShare = TerrorismData[4];
        string Affiliated = TerrorismData[5];
        string PurchaseExternalFAC = TerrorismData[6];
        string PurchasedExternalFACReinsurance = TerrorismData[7];
        string FACBasisofAcceptance = TerrorismData[8];
        string FACRIPRM = TerrorismData[9];

        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Effective Date", GetExpirationDate(DateFormat));

        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Expiration Date", GetCurrentDate(DateFormat));

        if (_featureContext["AssignmentType_" + loggingStep.rowNo].ToString().ToLower().Equals("manual"))
        {

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Policy Number - Assignment Type", "Manual");

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Number - Current", GenerateRandomNumbers());
        }

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Month", ProductionMonth);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Year", ProductionYear);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "MAS", "Random");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Underwriting Credit Branch", UnderwritingCreditBranch);

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "UCB Share %", UCBShare);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Affiliated ?", Affiliated);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchase External FAC (UCB)", PurchaseExternalFAC);

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Premium Written Share %", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "PML", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Premium Estimated Signing %", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100% Policy Limit Amount (Currency)", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Shell Policy", "Random");

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100 Percent Policy Booked Premium", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "100% TIV", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchased External FAC Reinsurance", PurchasedExternalFACReinsurance);

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "FAC Basis of Acceptance", FACBasisofAcceptance);

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "FAC RI PRM", FACRIPRM);

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Ceding Commission Amount", GenerateDecimalNumber());

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Master/Local", "Random");
    }

    public void ThenUserFillTheValuesInRenewalSubmissionCreationPage(string[] Data)
    {

        string DateFormat = Data[0];

        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Effective Date", GetCurrentDate(DateFormat));

        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*Expiration Date", GetExpirationDateForRenewal(DateFormat));

        if (_featureContext["AssignmentType_" + loggingStep.rowNo].ToString().ToLower().Equals("manual"))
        {

            PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Policy Number - Assignment Type", _featureContext["AssignmentType_" + loggingStep.rowNo].ToString());

            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Number - Current", GenerateRandomNumbers());
        }

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Policy Number - Assignment Type", "Manual");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Month", "Random");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*QA/Production Year", "Random");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Inception Month", "Random");

        PerformActionOnCreateSubmission("Checkbox", lblcbCheckBoxName1_0, cbCheckBoxField1_0, "Clearance Indicator", "Yes");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Underwriting Credit Branch", "Random");

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "UCB Share %", "0");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Shell Policy", "Random");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Affiliated ?", "No");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchase External FAC (UCB)", "No");

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Premium Written Share %", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "PML", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Premium Estimated Signing %", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100% Policy Limit Amount (Currency)", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*100 Percent Policy Booked Premium", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "100% TIV", Generate2DigitRandomNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Starr Share Estimated Forecast Premium", GenerateRandomNumbers());

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchased External FAC Reinsurance", "Yes");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "FAC Basis of Acceptance", "Random");

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "FAC RI PRM", "0");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Slip Position", "Random");

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Ceding Commission Amount", GenerateDecimalNumber());

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Actual Rate Change", Generate2DigitRandomNumber());
    }

    public void ThenUserFillTheValuesInCloneSubmissionPage()
    {

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchased External FAC Reinsurance", "Yes");

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "FAC Basis of Acceptance", "Random");

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "FAC RI PRM", "0");

        PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Ceding Commission Amount", GenerateDecimalNumber());

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Purchase External FAC (UCB)", "No");
    }

    public void ThenUserUpdatedTheFieldsRequiredForSnapshotInformaiton(string updateValue)
    {
        UpdatedProjectName = updateValue;
        driver.WaitForElementToPresent(btnClearProjectContent);
        driver.WaitAndClick(btnClearProjectContent);
        _featureContext["UpdatedProjectName" + loggingStep.rowNo] = ReadDataFromFile(ClientsPage.Clients1_0FilePath + "Client_Update.txt");
        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "Project Name", _featureContext["UpdatedProjectName" + loggingStep.rowNo].ToString());
        Log("USER UPDATED THE PROJECT NAME FIELD");
    }

    public void ThenUserUpdatedTheClientInformation(string UpdateValue)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(btnClearClient));
            driver.WaitAndClick(btnClearClient);
            System.Threading.Thread.Sleep(3000);
            PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Client Name", ReadDataFromFile(ClientsPage.Clients1_0FilePath + "Client_Update.txt"));
            Log("USER UPDATED THE Client NAME FIELD");
        }
        catch(Exception e)
        {
            Log("COULD NOT CLEAR THE CLIENT FIELD TO UPDATE CLIENT");
            Assert.Fail("COULD NOT CLEAR THE CLIENT FIELD TO UPDATE CLIENT");
        }
       
    }

    public void ThenUserVerifyUpdatedClientIsUpdated()
    {
        Log("Client is updated successfully");
    }


    public void ThenCloneSubmissionWindowIsDisplayedAndUserClickedOnPunitiveDamagesButton()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(btnPunitiveDamages), "CLONE SUBMISSION WINDOW IS NOT DISPLAYED");
        Log("CLONE SUBMISSION WINDOW IS DISPLAYED");
        Assert.IsTrue(driver.WaitAndClick(btnPunitiveDamages), "COULD NOT CLICKED ON PUNITIVE DAMAGES");
        Log("CLICKED ON PUNITIVE DAMAGE BUTTON");
    }

    public void ThenClonedSubmissionRecordIsCreatedSuccessfully()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(lblStarrSubmissionName), "CLONED SUBMISSION IS NOT CREATED");
        Log("CLONED SUBMISSION IS CREATED SUCCESSFULLY");
    }

    public void ThenUserVerifyClonedSubmissionData()
    {
        driver.SelectFromList(btnSubmissionTabList, "PC Coding");
        string BusinessUnitValue = GetValueUsingFieldName("Business Unit", lblUWProducerTabFieldsName, lblUWProducerFieldsValue);
        string ProfitCenterValue = GetValueUsingFieldName("Profit Center", lblUWProducerTabFieldsName, lblUWProducerFieldsValue);
        string ProductionOfficeValue = GetValueUsingFieldName("Production Office", lblUWProducerTabFieldsName, lblUWProducerFieldsValue);
        string IssuingOfficeValue = GetValueUsingFieldName("Issuing Office", lblUWProducerTabFieldsName, lblUWProducerFieldsValue);
        Assert.AreEqual(BusinessUnitValue, "Starr Casualty International - 10110", " BUSINESS UNIT VALUE IS NOT CHANGED TO EXPECTED VALUE");
        Log("BUSINESS UNIT VALUE IS CHANGED TO " + BusinessUnitValue);
        Assert.AreEqual(ProfitCenterValue, "General Liability - 20610", "PROFIT CENTER IS NOT CHANGED TO EXPECTED VALUE");
        Log("PROFIT CENTER VALUE IS CHANGED TO " + ProfitCenterValue);
        Assert.AreEqual(ProductionOfficeValue, "Hamilton", "PRODUCTION OFFICE VALUE IS NOT CHANGED TO EXPECTED VALUE");
        Log("PRODUCTION OFFICE VALUE IS CHANGED TO " + ProductionOfficeValue);
        Assert.AreEqual(IssuingOfficeValue, "Hamilton", "ISSUING OFFICE VALUE IS NOT CHANGED TO EXPECTED VALUE");
        Log("ISSUING OFFICE VALUE IS CHANGED TO " + IssuingOfficeValue);
        Log("SUCCESSFULLY VERIFIED CLONED SUBMISSION DETAILS");
    }

    public void ThenUserPerformIceCheckInSubmissionPage()
    {
        driver.WaitForElementToPresent(btnMoreAction2_0);
        driver.WaitAndClick(btnMoreAction2_0);
        System.Threading.Thread.Sleep(1000);
        Assert.IsTrue(driver.SelectFromListUsingJS(btnActionNames, "ICE Check"), "COULD NOT SELECT THE VALUE " + "ICE Check");
        Log("CLICKED ON ICE CHECK BUTTON");
    }

    public void VerifyIncumbentInsurerInSubmission(String Version)
    {
        string AllIncumbentInsurers = "";
        driver.WaitForElementToPresent(lnkSubmissionName);
        driver.WaitAndClick(lnkSubmissionName);
        System.Threading.Thread.Sleep(3000);
        driver.Refresh();

        if (Version.Equals("1.0"))
        {
            driver.WaitForElementToPresent(tabDetails);
            driver.WaitAndClick(tabDetails);
            driver.WaitForNextPage();
            AllIncumbentInsurers = GetValueUsingFieldName("Incumbent Insurers", lblDetailsTabFieldsName, lblDetailsFieldValue);
        }

        if ((Version.Equals("2.0")))
        {
            int tabCount = GetListIndexFromList("Policy", TabCreateSubmissionlist);
            NavigateToCreateSubmissionTabs("Policy");
            if (tabCount == 1)
            {
                AllIncumbentInsurers = GetValueUsingFieldName("Incumbent Insurers", lblUWProducerTabFieldsName, lblUWProducerFieldsValue);
            }
            if (tabCount == 2)
            {  
                AllIncumbentInsurers = GetValueUsingFieldName("Incumbent Insurers", lblSGSTabFieldsName, lblSGSFieldsValue);
            }
        }
        Assert.True(AllIncumbentInsurers.Contains(IncumbentInsurersPage.CarrierName), "Assumed insurers is not present in the Submission");
        Log("Assumed Insurers present in the Submission ");
    }

    public void ThenUserFillTheDetailsInPolicyTab(string DateFormat)
    {
        System.Threading.Thread.Sleep(2000);
        NavigateToEditSubmissionTabs("Policy");
        //Assert.IsTrue(driver.WaitForElementToPresent(tabPolicyEditMode2_0),"POLICY TAB IS NOT DISPLAYED");
        //driver.WaitAndClick(tabPolicyEditMode2_0);
        PerformActionOnCreateSubmission("Checkbox", lblcbCheckBoxName1_0, cbCheckBoxField1_0, "Policy Issued", "Yes");
        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "Policy Issued Date", GetCurrentDate(DateFormat));
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Policy Issued By", "Random");
        Log("SUCCESSFULLY FILLED THE VALUES IN POLICY TAB");
    }

    public bool SelectCheckBoxBasedOnTheList(By List1, string ItemToMatch, By List2)
    {
        driver.SwitchToFrame(0);

        IList<IWebElement> ListOfItem1 = driver.ListOfElements(List1);
        IList<IWebElement> ListOfItem2 = driver.ListOfElements(List2);

        for (int i = 0; i < ListOfItem1.Count; i++)
        {
            driver.MoveToTheElement(ListOfItem1[i]);
            if (ListOfItem1[i].GetElementText().ToString().Equals(ItemToMatch.Trim().ToString()))
            {
                driver.MoveToTheElement(ListOfItem2[i].FindElement(cbStarrSubmissionNumber));
                ListOfItem2[i].FindElement(cbStarrSubmissionNumber).Click();
                driver.CaptureScreen(_scenarioContext);
                return true;
                
            }
        }
        //Console.WriteLine("COULD NOT FIND THEITEM FROM THE LIST");
        return false;
    }

    public void ThenUserCaptureTheStarrSubmissionNumberIn()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(lblStarrSubmissionNumber),"COULD NOT FIND THE STARR SUBMISSION NUMBER FIELD");
        _featureContext["StarrSubmissionNumber" + loggingStep.rowNo] = driver.GetTextFromElement(lblStarrSubmissionNumber);
        Log("STARR SUBMISSION NUMBER IS STORED SUCCESSFULLY");
    }
    public void WhenUserSelectedTheSubmissionWhichIsInBlockStage()
    {
        Assert.IsTrue(SelectCheckBoxBasedOnTheList(lnkStarrSubmissionNumber, _featureContext["StarrSubmissionNumber" + loggingStep.rowNo].ToString(), tblRow), "COULD NOT SELECT THE SUBMISSION CHECKBOX");
        Log("SELECTED THE SUBMISSION CHECKBOX");
    }

    public void ThenUserClickedOnReleaseBlockButtonInReleaseBlockPage()
    {
        driver.ScrollToCenter(btnReleaseBlock);
        Assert.IsTrue(driver.WaitAndClick(btnReleaseBlock),"COULD NOT CLICK ON THE RELEASE BLOCK BUTTON ");
        driver.CaptureScreen(_scenarioContext);
        driver.WaitForElementToPresent(lblReleasedConfirmationMsg);
        Assert.IsTrue(driver.GetTextFromElement(lblReleasedConfirmationMsg).Trim().Equals("Now that you have released the submission, please update stage to \"Declined\" if appropriate."));
        Assert.IsTrue(driver.SelectFromList(lnkStarrSubmissionNumber, _featureContext["StarrSubmissionNumber" + loggingStep.rowNo].ToString()), "COULD NOT SELECTE THE STARR SUBMISSION LINK");
        System.Threading.Thread.Sleep(3000);
        driver.WaitForNextPage();
        Log("CLICKED ON THE RELEASE BLOCK BUTTON");
    }

    public void ThenUserNavigatedToSubmissionWhichIsReleased()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(lblStarrSubmissionNumber),"COULD NOT NAVIGATED TO RELEASED SUBMISSION PAGE");
        Log("CLICKED ON THE STARR SUBMISSION NUMBER");
    }

    public void ThenUserFillTheDetailsRequiredForMidTermBrokerByPassingNewProducerInformation(string[] data)
    {
        string NewLicensedProducer = data[0];
        string NewProducerContact = data[1];
        string DateFormat = data[2];

        NavigateToEditSubmissionTabs("Producer");
        PerformActionOnCreateSubmission("Checkbox", lblcbCheckBoxName1_0, cbCheckBoxField1_0, "Midterm Broker of Record Change", "Yes");
        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "New Licensed Producer Name", NewLicensedProducer);
        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "New Placing Producer", NewLicensedProducer);
        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "New Placing Producer Contact", NewProducerContact);
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "Originating Broker Comm Forfeit (Y/N)", "No");
        PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "BOR Change effective date", GetCurrentDate(DateFormat));

    }

    //readonly By lblPrmLimitsTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[4]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    //readonly By lblPrmLimitsFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[4]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[4]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");


    public void ThenUserVerifyTheFieldsWhichAreUpdatedDuringMidtermBroker(string[] data)
    {
        string NewLicensedProducer = data[0];
        string NewProducerContact = data[1];

        Assert.AreEqual(NewLicensedProducer.Trim(), GetValueUsingFieldName("New Licensed Producer Name", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue).Trim().ToString(),"PRODUCER VALUE ARE NOT EQUAL");
        Assert.AreEqual(NewLicensedProducer.Trim(), GetValueUsingFieldName("New Placing Producer", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue).Trim().ToString(),"PLACING PRODUCER ARE NOT EQUAL");
        Assert.AreEqual(NewProducerContact.Trim(), GetValueUsingFieldName("New Placing Producer Contact", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue).Trim().ToString(),"PRODUCER CONTACT ARE NOT EQUAL");
    }

    public void ThenUserVerifyPolicyNumberIsAssignedInTheSubmission()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblDetailsFieldValue));
        string AssignedPolicyNumber = GetValueUsingFieldName("Policy Number - Current", lblDetailsTabFieldsName, lblDetailsFieldValue);
        if(string.IsNullOrEmpty(AssignedPolicyNumber))
        {
            Assert.Fail("POLICY NUMBER IS NOT GENERATED");
        }
        else
        {
            Log("POLICY NUMBER IS GENERATED");
        }
    }


    public void ThenUserAddedPreDefinedItem()
    {
        System.Threading.Thread.Sleep(4000);
        // Switch To Frame
        driver.SwitchToFrame(0);

        // Subjectivity Notice
        driver.ScrollToCenter(slctSubjectivityNotice);
        driver.SelectValueFromDropDownListUsingText(slctSubjectivityNotice , "Daily");
        driver.CaptureScreen(_scenarioContext);

        // ADDING PRE DEFINED ITEMS
        driver.WaitForElementToPresent(btnAddPreDefinedItem);
        driver.ScrollToCenter(btnAddPreDefinedItem);
        driver.WaitAndClick(btnAddPreDefinedItem);

        // SELECTING THE CHECKBOX OF SUBJECTIVITY ITEM
        driver.WaitForElementToPresent(cbSubjectivityItem);
        driver.ScrollToCenter(cbSubjectivityItem);
        driver.CheckTheBox(cbSubjectivityItem);
        driver.CaptureScreen(_scenarioContext);

        // SELECT THE ITEMS
        driver.MoveToTheElementAndClick(btnSelectItem);

        // SAVE THE SUBJECTIVITY
        driver.ScrollToCenter(btnSaveSubjectivity);
        Assert.IsTrue( driver.WaitAndClick(btnSaveSubjectivity),"COULD NOT CLICK ON THE SAVE BUTTON");
        Log("SUBJECTIVITY ITEM IS ADDED SUCCESSFULLY");
    }

    public void ThenUserDeletedThePreDefinedItem()
    {
        System.Threading.Thread.Sleep(4000);
        driver.SwitchToFrame(0);

        // SELECT THE ITEM WHICH IS SELECTED ALREADY
        driver.ScrollToCenter(cbSubjectivityItem);
        Assert.IsTrue(driver.CheckTheBox(cbSubjectivityItem),"COULD NOT SELECT THE ITEMS");
        driver.CaptureScreen(_scenarioContext);

        // DELETING THE SUBJECTIVITY ITEM
        driver.ScrollToCenter(btnDeleteItem);
        Assert.IsTrue(driver.WaitAndClick(btnDeleteItem),"COULD NOT CLICK ON THE DELETE BUTTON");

        // HANDLING ALERT POPUP
        driver.AcceptAlert();
        Log("SUBJECTIVITY ITEM IS DELETED SUCCESSFULLY");
    }


    public void UpdateClientFieldValue(string ValidaitonName)
    {
        driver.WaitAndClick(btnClearClient);
        System.Threading.Thread.Sleep(2000);
        PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Client Name", ReadDataFromFile(ClientsPage.Clients1_0FilePath + "Client_Update.txt"));
        Log("UPDAED CLIENT VALUE IN CLIENT FIELD");
        if (ValidaitonName.Contains("A65"))
        {
            PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Local Policy Number – Expiring", GenerateRandomNumbers());
            Log("UPDAED Local Policy Number – Expiring VALUE IN CLIENT FIELD");
        }        
    }
    public void ThenUserUpdatedRequiredField(string FieldName)
    {
        string ResetPicklist = "--None--";
        string blankField = "";
        string[] Fields = FieldName.Split(",");
        string valueToUpdate="";

        switch (FieldName)
        {
            case "UDX Currency?":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName, "No");
                break;

            case "Affiliated ?":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName, "Yes");
                break;

            case "Clearance Indicator":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Checkbox", lblcbCheckBoxName1_0, cbCheckBoxField1_0, "Clearance Indicator", "No");
                break;

            case "Issuing Company":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName, "Random");
                break;

            case "Production Office":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*"+FieldName, "Random");
                break;

            case "Carrier Branch":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName, ResetPicklist);
                break;

            case "Starr Share Estimated Forecast Premium":
                NavigateToEditSubmissionTabs("Premium/Limits");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, FieldName, blankField);
                break;

            case "Submission Details":               
                NavigateToEditSubmissionTabs("Premium/Limits");
                NavigateToEditSubmissionTabs("Submission Details");
                break;

            case "Policy Premium Written Share %":
                NavigateToEditSubmissionTabs("Premium/Limits");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, FieldName, blankField);
                break;

            case "Policy Premium Estimated Signing %":
                NavigateToEditSubmissionTabs("Premium/Limits");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, FieldName, blankField);
                break;
                
            //case "Producer Commission %":
            //    NavigateToEditSubmissionTabs("UW/Producer");
            //    PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, FieldName, blankField);
            //    driver.CaptureScreen(_scenarioContext);
            //    break;

            case "Inception Month,Non-renewable,Layer":
                NavigateToEditSubmissionTabs("Dates/Addtional Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], ResetPicklist);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[1], ResetPicklist);
                driver.CaptureScreen(_scenarioContext);
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[2], ResetPicklist);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "FAC Basis of Acceptance,FAC RI PRM,Policy Ceding Commission Amount":
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], ResetPicklist);
                driver.CaptureScreen(_scenarioContext);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[1], blankField);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[2], blankField);
                break;

            case "Purchase External FAC (UCB),Underwriting Credit Branch":
                NavigateToEditSubmissionTabs("SGS Section");
                driver.CaptureScreen(_scenarioContext);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], "Yes");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[1], "Random");
                break;

            case "Shell Policy,Affiliated ?":
                NavigateToEditSubmissionTabs("SGS Section");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], ResetPicklist);
                driver.CaptureScreen(_scenarioContext);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[1], ResetPicklist);
                break;

            case "Issuing Office,UW Region,Production Office":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], "Middle Market");
                driver.CaptureScreen(_scenarioContext);
                NavigateToEditSubmissionTabs("UW/Producer");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], "North America");
                driver.CaptureScreen(_scenarioContext);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[2], "Middle Market");
                break;

            case "Purchase External FAC (UCB),FAC RI PRM (UCB),Policy Ceding Commission (UCB)":              
                NavigateToEditSubmissionTabs("SGS Section");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], "No");
                driver.CaptureScreen(_scenarioContext);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[1], GenerateRandomNumbers());
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[2], GenerateRandomNumbers());
                break;

            case "Underwriting Credit Branch":
                NavigateToEditSubmissionTabs("UW/Producer");
                NavigateToEditSubmissionTabs("SGS Section");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName, "Random");
                break;

            case "UCB Share %":
                NavigateToEditSubmissionTabs("UW/Producer");
                NavigateToEditSubmissionTabs("SGS Section");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, FieldName, Generate2DigitRandomNumber());
                break;   

            case "Policy Number - Current,Local Policy Number – Current,Local Policy Number – Expiring":
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[0], "ASDF123!@#$%^&*()");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[1], "@#$%^&*()");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[2], "ASDF123!@#$%^&*()ASDF123");
                break;




            default: Console.WriteLine("COULD NOT FIND THE MATCHING FIELD, CHECK FOR THE INPUT");
                break;
        }
    }

    public decimal RemoveUnwantedFromString(string value)
    {
        string[] inputValue = value.Split(" ");
        decimal returnvalue;
        if (inputValue.Length > 1)
        {
            returnvalue = decimal.Parse(inputValue[1]);
        }
        else
        {
            returnvalue = decimal.Parse(inputValue[1]);
        }

        //Log("Split the string and value is " + returnvalue);
        return returnvalue;
    }


    public void ThenUserCaptureTheValueOfField()
    {
        System.Threading.Thread.Sleep(3000);
        _featureContext["AssignmentType_" + loggingStep.rowNo] = GetValueUsingFieldName("Policy Number - Assignment Type", lblDetailsTabFieldsName, lblDetailsFieldValue);
        _featureContext["Carrier Branch" + loggingStep.rowNo] = GetValueUsingFieldName("Carrier Branch", lblDetailsTabFieldsName, lblDetailsFieldValue);
        NavigateToCreateSubmissionTabs("Prm/Limits");
        string StarrEPIGWPvalue = GetValueUsingFieldName("Starr EPI/GWP", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue);
        decimal value = RemoveUnwantedFromString(StarrEPIGWPvalue);
        if (value > 1)
        {
            _featureContext["FAC RI PRM" + loggingStep.rowNo] = (value - 1);
            //Console.WriteLine("Starr EPI/GWP value is " + value);
            //Console.WriteLine("Stored FAC RI PRM value is " + _featureContext["FAC RI PRM" + loggingStep.rowNo].ToString());
        }
        else
        {
            _featureContext["FAC RI PRM" + loggingStep.rowNo] = 0;
        }
    }

    public void ThenUserUpdatedTheValueForTheField(string FieldName, string Data)
    {
        string LOB = Data;
        NavigateToEditSubmissionTabs("Submission Details");
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + FieldName, LOB);
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Subclass 1", "Random");
    }

    public void ThenUserUpdatedRequiredField(string FieldName, string FieldValue)
    {
        string ResetPicklist = "--None--";
        string blankField = "";
        string[] Fields = FieldName.Split(",");
        string[] Values = FieldValue.Split(",");

        switch (FieldName)
        {
            case "Submission Currency": 
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "UDX Currency?", "Yes");
                driver.CaptureScreen(_scenarioContext);
                System.Threading.Thread.Sleep(1000);
                break;

            case "Starr Share Estimated Forecast Premium":
                NavigateToEditSubmissionTabs("Premium/Limits");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, FieldName, FieldValue);
                break;

            case "Line of Business,Issuing Office,Occupancy,UW Region,Production Office":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], Values[1]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[2], Values[2]);
                
                NavigateToEditSubmissionTabs("UW/Producer");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[3], Values[3]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[4], Values[4]);
                break;

            case "Line of Business,Issuing Office,Occupancy":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], Values[1]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[2], Values[2]);
                break;

            case "Program":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + FieldName, FieldValue);
                break;

            case "Division":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                break;

            case "Division,Subdivision,Program":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], Values[1]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[2], Values[2]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + "Client Region", "Random");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + "Subclass 1", "MAJOR");
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Issuing Office,Carrier Branch":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[1], Values[1]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Issuing Office,TRIA/Non Cert Included":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[1], Values[1]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "TRIA/Non Cert Included":
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], Values[0]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "UW Region,Production Office,TRIA/Non Cert Included":
                NavigateToEditSubmissionTabs("UW/Producer");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], Values[1]);
                driver.CaptureScreen(_scenarioContext);
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[2], Values[2]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "UW Region,Production Office":
                NavigateToEditSubmissionTabs("UW/Producer");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], Values[1]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Line Of Business,Occupancy,Subclass 1":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                driver.CaptureScreen(_scenarioContext);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], Values[1]);
                driver.CaptureScreen(_scenarioContext);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[2], Values[2]);
                break;

            case "Issuing Company,Carrier Branch":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[1], Values[1]);
                break;

            case "Policy Number - Current,Subclass 1":
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Number - Current", "");
                driver.CaptureScreen(_scenarioContext);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], "Random");
                break;

            case "Occupancy,Subclass 1":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                driver.CaptureScreen(_scenarioContext);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], Values[1]);
                break;

            case "Framework ST Risk Code":
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName, FieldValue);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Slip Position":
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName, FieldValue);
                driver.CaptureScreen(_scenarioContext);
                break;                

            case "Client Name,Issuing Office,Licensed Producer":
                driver.WaitAndClick(btnClearClient);
                System.Threading.Thread.Sleep(3000);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*" + Fields[0], ReadDataFromFile(ClientsPage.Clients1_0FilePath + "Client_Create.txt"));
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], Values[1]);
                NavigateToEditSubmissionTabs("UW/Producer");
                driver.ScrollToCenter(btnClearLicensedProducer);
                driver.WaitAndClick(btnClearLicensedProducer);
                System.Threading.Thread.Sleep(3000);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, Fields[2], Values[2]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Client Name,Issuing Office":
                NavigateToEditSubmissionTabs("Submission Details");
                driver.WaitAndClick(btnClearClient);
                System.Threading.Thread.Sleep(3000);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*" + Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], Values[1]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Licensed Producer,Placing Producer Contact":
                NavigateToEditSubmissionTabs("UW/Producer");
                driver.WaitAndClick(btnClearLicensedProducer);
                System.Threading.Thread.Sleep(3000);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*" + Fields[0], Values[0]);
                driver.CaptureScreen(_scenarioContext);
                driver.ScrollToCenter(btnClearProducerContact);
                driver.WaitAndClick(btnClearProducerContact);
                System.Threading.Thread.Sleep(3000);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*" + Fields[1], Values[1]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Issuing Office,Placing Producer Contact":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                NavigateToEditSubmissionTabs("UW/Producer");
                driver.ScrollToCenter(btnClearProducerContact);
                driver.WaitAndClick(btnClearProducerContact);
                System.Threading.Thread.Sleep(3000);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*" + Fields[1], Values[1]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Issuing Office,Licensed Producer":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                NavigateToEditSubmissionTabs("UW/Producer");
                driver.ScrollToCenter(btnClearLicensedProducer);
                driver.WaitAndClick(btnClearLicensedProducer);
                System.Threading.Thread.Sleep(3000);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, Fields[1], Values[1]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "*Stage":
                driver.ScrollToCenter(btnSaveSubmission);
                NavigateToCreateSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName, FieldValue);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Camilion Created Date":
                string value = "";
                if (FieldValue.Equals("Date"))
                {
                    value = GetCurrentDate("MMM dd, yyyy");
                }
                if(FieldValue.Equals("Blank"))
                {
                    value = blankField;
                }
                NavigateToEditSubmissionTabsInAdmin("System Information");
                PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, FieldName, value);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Lynx Date":
                if (FieldValue.Equals("Blank"))
                {
                    NavigateToEditSubmissionTabsInAdmin("System Information");
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(txtLynxDate));
                    driver.ClearAndSend(txtLynxDate, "");
                    driver.ClearAndSend(txtLynxTime, "");
                    driver.CaptureScreen(_scenarioContext);
                    break;
                }
                else
                {
                    NavigateToEditSubmissionTabsInAdmin("System Information");
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(txtLynxDate));
                    driver.ClearAndSend(txtLynxDate, GetCurrentDate("MMM d, yyyy"));
                    driver.ClearAndSend(txtLynxTime, "12:00 AM");
                    driver.CaptureScreen(_scenarioContext);
                    break;
                }

            case "Camilion Issued Date":
                string valueIssueDeate = "";
                if (FieldValue.Equals("Date"))
                {
                    valueIssueDeate = GetCurrentDate("MMM dd, yyyy");
                }
                if (FieldValue.Equals("Blank"))
                {
                    valueIssueDeate = blankField;
                }
                NavigateToEditSubmissionTabsInAdmin("System Information");
                PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, FieldName, valueIssueDeate);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "TRIA/Non Cert Included,Starr TRIA Premium,Starr Non Cert Premium":
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[1], Values[1]);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[2], Values[2]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Actual Rate Change":
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[0], blankField);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Endorsement Ref":
                NavigateToEditSubmissionTabsInAdmin("System Information");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[0], Values[0]);
                driver.CaptureScreen(_scenarioContext);
                break;



            case "*Policy Number - Expiring":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "Policy Number - Expiring", blankField);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "*Client Name":
                driver.WaitAndClick(btnClearClient);
                System.Threading.Thread.Sleep(3000);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Client Name", ReadDataFromFile(ClientsPage.Clients1_0FilePath + "Client_Validation.txt"));
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Issuing Company,License Number":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], Values[0]);
                NavigateToEditSubmissionTabs("UW/Producer");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[1], blankField);
                break;

            case "Affiliated ?":
                NavigateToEditSubmissionTabs("SGS Section");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName, FieldValue);
                break;

            case "Issuing Office,Assumed RI Contract":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[1], Values[1]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Expiration Date":
                PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*" + Fields[0], FieldValue);
                driver.CaptureScreen(_scenarioContext);
                break;


            case "Line of Business,Issuing Office,Subclass 1":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], Values[1]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[2], Values[2]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Line of Business":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "100 Percent Policy Booked Premium":
                //NavigateToEditSubmissionTabs("Premium/Limits");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*" + Fields[0], Values[0]);
                break;

            case "Policy Number - Current":
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "*" + Fields[0], blankField);
                break;

            case "Endorsement Type":
                NavigateToEditSubmissionTabs("System Information");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], Values[0]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Submission Type":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*"+Fields[0], Values[0]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Stage":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                if (Values[0].Equals("Lost/NTU"))
                {
                    PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*LostNTU or Declined Reason", "Random");
                }
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Issuing Office,Local Policy Number – Current":
                string LocalValue = "";
                if (Values[0].ToString().ToLower().Equals("blank"))
                {
                    LocalValue = "";
                }
                else
                {
                    LocalValue = GenerateRandomNumbers();
                }
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[1], LocalValue);
                driver.CaptureScreen(_scenarioContext);
                System.Threading.Thread.Sleep(1000);
                break;

            case "Licensed Producer":
                NavigateToEditSubmissionTabs("UW/Producer");
                driver.WaitAndClick(btnClearLicensedProducer);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, FieldName, FieldValue);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Local Client Name":
                NavigateToEditSubmissionTabs("SGS Section");
                if(driver.IsDisplayed(btnClearLocalClientName))
                {
                    driver.WaitAndClick(btnClearLocalClientName);
                }                
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, FieldName, ReadDataFromFile(ClientsPage.Clients1_0FilePath + "Client_Create.txt"));
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Client Name,Client Region,Stage,Line of Business,Occupancy,Profit Center,Division,Subdivision,Issuing Office,Program":
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Client Name", ReadDataFromFile(ClientsPage.Clients1_0FilePath + "Client_Create.txt"));
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Client Region", "Random");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Stage", "Prospect");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Line of Business", "Random");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Occupancy", "Random");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Profit Center", "Random");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Subdivision", "Random");                
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Subclass 1", "Random");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Issuing Office", "Random");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Subclass 2", "Random");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Program", "Random");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Division", "Random");
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Policy Ceding Commission Amount,FAC RI PRM,Purchased External FAC Reinsurance,Forecast FAC RI PRM":
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[0], GenerateRandomNumbers());
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[1], GenerateRandomNumbers());
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[2], "--None--");
                NavigateToEditSubmissionTabs("Premium/Limits");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[3], GenerateRandomNumbers());
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Production Office Share %,UCB Share %,Purchase External FAC (UCB)":
                NavigateToEditSubmissionTabs("SGS Section");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[1], Values[1]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[2], "--None--");
                break;

            case "Effective Date":
                NavigateToEditSubmissionTabs("Dates/Addtional Info");
                PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, "*" + Fields[0], GetExpirationDateforFuture("MMM dd, yyyy"));
                driver.CaptureScreen(_scenarioContext);
                break;

            case "*Effective Date":
                NavigateToEditSubmissionTabs("Dates/Addtional Info");
                PerformActionOnCreateSubmission("Date", lbltxtDatePickerName1_0, txtDatePickerField1_0, Fields[0], GetCurrentDate("MMM dd, yyyy"));
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Local Country,Shell Policy":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[1], Values[1]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "LATAM Risk Code":
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], Values[0]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Policy Number - Assignment Type,Policy Number - Current":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[1], blankField);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Production Office Share %":
                NavigateToEditSubmissionTabs("SGS Section");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[0], blankField);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Issuing Company":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                break;

            case "Underwriting Credit Branch,UCB Share %":
                NavigateToEditSubmissionTabs("SGS Section");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0],"Random");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[1], blankField);

                break;

            case "New York Free Trade Zone":
                NavigateToEditSubmissionTabs("Dates/Addtional Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "100% TIV":
                NavigateToEditSubmissionTabs("Premium/Limits ");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "100% TIV", "");
                NavigateToEditSubmissionTabs("Submission Details");
                break;

            case "Project Name":
                driver.JSClick(btnClearProjectContent);
                break;

            case "LostNTU or Declined Reason,Submission Comments":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                driver.ScrollByPixel(100);
                driver.ClearAndSend(txtAreaSubmissionComment, "");
                driver.CaptureScreen(_scenarioContext);
                break;

            case "PML":
                NavigateToEditSubmissionTabs("Premium/Limits");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, FieldName, blankField);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Client Name":
                driver.JSClick(btnClearClient);
                System.Threading.Thread.Sleep(1000);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*" + FieldName, ReadDataFromFile(ClientsPage.Clients1_0FilePath + "Client_Update.txt"));
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Client Name Update":
                driver.JSClick(btnClearClient);
                System.Threading.Thread.Sleep(1000);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*Client Name", FieldValue);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Network Identification (NID)":
                NavigateToEditSubmissionTabs("SGS Section");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, FieldName, FieldValue);
                driver.CaptureScreen(_scenarioContext);
                System.Threading.Thread.Sleep(1000);
                break;

            case "Line of Business,Occupancy,Subclass 1":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[1], Values[1]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*" + Fields[2], Values[2]);
                System.Threading.Thread.Sleep(1000);
                break;

            case "Carrier Branch,MAS":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0,  Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[1], Values[1]);
                break;

            case "Line of Business,Issuing Company,Occupancy,Division,Subclass 1,Program":
                NavigateToEditSubmissionTabs("Submission Details");
                for (int i=0;i<Fields.Length;i++)
                {
                    PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*"+Fields[i], "Random");
                }
                break;

            case "Program,Purchased External FAC Reinsurance,FAC Basis of Acceptance,FAC RI PRM,Policy Ceding Commission Amount":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*"+Fields[0], Values[0]);
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[1], Values[1]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[2], Values[2]);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[3], Values[3]);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[4], Values[4]);
                break;

            case "Purchased External FAC Reinsurance,FAC Basis of Acceptance,FAC RI PRM,Policy Ceding Commission Amount":
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], Values[0]);
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[1], Values[1]);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[2], Values[2]);
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[3], Values[3]);
                break;

            case "FAC RI PRM":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*Issuing Office", "Atlanta");
                NavigateToEditSubmissionTabs("Policy Info");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[0], _featureContext["FAC RI PRM" + loggingStep.rowNo].ToString());
                break;

            case "FAC RI PRM In Child Records":
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, "FAC RI PR", _featureContext["FAC RI PRM" + loggingStep.rowNo].ToString());
                break;

            case "Underwriting Credit Branch":
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, Fields[0], "--None--");
                break;

            case "Issuing Office":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, "*"+ Fields[0], Values[0]);
                break;

            case "Producer Commission %":
                NavigateToEditSubmissionTabs("UW/Producer");
                if (FieldValue.Trim().ToLower().Equals("blank"))
                {
                    PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, FieldName, "");
                }
                else
                {
                    PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, FieldName, GenerateRandomNumberForPickListValues(10, 20).ToString());
                }
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Local Policy Number – Current":
                NavigateToEditSubmissionTabs("Submission Details");
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, FieldName, blankField);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Policy Number - Current,Local Policy Number – Current":
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[0], GenerateRandomNumbers());
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[1], GenerateRandomNumbers());
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Assigned Underwriter":
                NavigateToEditSubmissionTabs("UW/Producer");
                driver.JSClick(btnClearAssignedUnderwriter);
                System.Threading.Thread.Sleep(1000);
                PerformActionOnCreateSubmission("Lookup", lbllkupLookupName1_0, lkupLookupField1_0, "*" + FieldName, FieldValue);
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Policy Number - Expiring":
                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[0], GenerateRandomNumbers());
                driver.CaptureScreen(_scenarioContext);
                break;

            case "Local Policy Number – Expiring":
                string valuePolicy = "";
                if (FieldValue.Equals("Blank"))
                {
                    valuePolicy = blankField;
                }
                else
                {
                    valuePolicy = FieldValue;
                }

                PerformActionOnCreateSubmission("Input", lbltxtInputFieldName1_0, txtInputField1_0, Fields[0], valuePolicy);
                driver.CaptureScreen(_scenarioContext);
                break;


            default:
                Console.WriteLine("COULD NOT FIND THE MATCHING FIELD, CHECK FOR THE INPUT");
                break;
        }
    }

    public void ThenUserUpdatedTheValueForAnd(string FieldName1, string FieldName2, string data)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName1, "Random");

        driver.CaptureScreen(_scenarioContext);
        NavigateToEditSubmissionTabs("UW/Producer");
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(uwOrProducerTabFieldToWait));

        PerformActionOnCreateSubmission("Picklist", lblslctPickListName1_0, slctPickListField1_0, FieldName2, "Random");

        Log("Entered value in " + FieldName1 + FieldName2 + "Fields");
    }


    public void ThenUserSelectedTheStageAsFromViewModeAndClickedOnMarkAsCurrentStageButton(string StageName)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        if(driver.IsDisplayed(alertSuccessMsg))
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(alertSuccessMsg));
        }
        driver.ScrollToCenter(btnMarkAsCurrentStage);
        if (driver.SelectFromListUsingJS(btnStageProgressBarList, StageName) == true)
        {
            try
            {
                driver.JSClick(btnMarkAsCurrentStage);
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(alertSuccessMsg));
                driver.CaptureScreen(_scenarioContext);
                Log("SELECTED THE OPTION AND CLICKED ON MARK AS CURRENT STAGE BUTTON");
                System.Threading.Thread.Sleep(3000);
            }
            catch (Exception e)
            {
                Assert.Fail("SELECTED THE OPTION AND CLICKED ON MARK AS CURRENT STAGE BUTTON");
            }
        }
        else
        {
            Assert.Fail("COULT NOT CLICK ON THE STAGE PROGRESSION BAR");
            Log("COULT NOT CLICK ON THE STAGE PROGRESSION BAR");
        }   
    }

    public int VerifyItemsFromArray(string[] array, string matchingText)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Equals(matchingText.Trim()))
            {
                Console.WriteLine("STAGE POSITION " + i);
                return i;
            }
        }
        return -1;
    }

    public void LogoutFromUser()
    {
        System.Threading.Thread.Sleep(2000);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lnkLogOut));
        Assert.IsTrue(driver.WaitAndClick(lnkLogOut), "COULD NOT CLICK ON THE LOGOUT BUTTON");
        Log("CLICKED ON LOGOUT BUTTON");
        wait.Until(ExpectedConditions.ElementToBeClickable(btnAppLauncher));
        driver.WaitTillElementIsClickable(btnAppLauncher);
    }

    public void ResetSubmissionStageForExecution(string ResetStage)
    {
        string[] AllStages = { "Prospect", "Working", "Quoted", "Bound", "Cancelled" };
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblDetailsTabFieldsName));
        string StageValue = GetValueUsingFieldName("Stage", lblDetailsTabFieldsName, lblDetailsFieldValue);
        int currentPosition = VerifyItemsFromArray(AllStages, StageValue);
        int ResetStagePosition = VerifyItemsFromArray(AllStages, ResetStage);
        if (!StageValue.Equals(ResetStage.Trim()))
        {
            if (currentPosition != -1)
            {
                if (currentPosition == ResetStagePosition)
                {
                    Log("CURRENT POSITION is " + AllStages[0].ToString());
                }
                else if (currentPosition > ResetStagePosition)
                {
                    for (int j = currentPosition; j > ResetStagePosition; j--)
                    {
                        ThenUserSelectedTheStageAsFromViewModeAndClickedOnMarkAsCurrentStageButton(AllStages[j - 1].ToString());
                        if (GetValueUsingFieldName("Stage", lblDetailsTabFieldsName, lblDetailsFieldValue).Equals(ResetStage))
                        {
                            Log("SUBMISSION PAGE IS RESET BACK TO REQUESTED STAGE " + ResetStage);
                            break;
                        }
                        Log("CURRENT SUBMISSION PAGE STATUS IS " + GetValueUsingFieldName("Stage", lblDetailsTabFieldsName, lblDetailsFieldValue).ToString());
                        currentPosition--;
                    }
                }
                else if (currentPosition < ResetStagePosition)
                {
                    for (int j = currentPosition; j < ResetStagePosition; j++)
                    {
                        ThenUserSelectedTheStageAsFromViewModeAndClickedOnMarkAsCurrentStageButton(AllStages[j + 1].ToString());
                        if (GetValueUsingFieldName("Stage", lblDetailsTabFieldsName, lblDetailsFieldValue).Equals(ResetStage))
                        {
                            Log("SUBMISSION PAGE IS RESET BACK TO REQUESTED STAGE " + ResetStage);
                            break;
                        }
                        Log("CURRENT SUBMISSION PAGE STATUS IS " + GetValueUsingFieldName("Stage", lblDetailsTabFieldsName, lblDetailsFieldValue).ToString());
                        currentPosition++;
                    }
                }
            }
            else
            {
                Log("CURRENT POSITION IS NOT LISTED IN THE ARRAY");
                Assert.Fail("CURRENT POSITION IS NOT LISTED IN THE ARRAY");
            }
        }
        else
        {
            Log("CURRENT STAGE OF SUBMISSION IS ALREADY IN " + ResetStage);
        }
    }

    public void ThenUserGetTheValueOfTheField(string FieldName)
    {
        NavigateToCreateSubmissionTabs("SGS");
        _featureContext["Affiliated_" + loggingStep.rowNo] =  GetValueUsingFieldName("Affiliated ?", lblSGSTabFieldsName, lblSGSFieldsValue);
        Log("VALUE IN THE AFFILIATED FIELD IS " + _featureContext["Affiliated_" + loggingStep.rowNo].ToString());
        //Console.WriteLine("VALUE IN THE AFFILIATED FIELD IS " + _featureContext["Affiliated_" + loggingStep.rowNo].ToString());
    }




}