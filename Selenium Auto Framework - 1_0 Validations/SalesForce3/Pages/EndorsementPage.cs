using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace SalesForce3.Pages;
public class EndorsementPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    public static string ScenarioCount, PolicyLimitId, ExpectedParentSubmissionNumber;
    public static int DisplayDecimalDigit;
    public static bool flag = false;
    public static string EndorsementFilepath = SubmissionPage.BaseURL + "Endorsement/Endorsement1_0.txt";

    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

    // Endorsement Webelements
    readonly By btnNewEndrosement = By.XPath("//button[@name='Opportunity.STARR_New_Endorsements']");
    readonly By pageEndorsement = By.XPath("//h2/span[contains(text(),'Endorsement')]");
    readonly By btnSave = By.XPath("//button[@name='cancel']/parent::div/button[1]");
    readonly By lblCreatedEndorsement = By.XPath("//span[contains(text(),'Submission Name')]/following::div[1]/span/span");

    readonly By lblErrorMessages = By.XPath("//div[@class='slds-notify__content']/h2/following::p[1]");
    readonly By lblErrorMessageInStageProgression = By.XPath("//div[@class='slds-notify__content']/h2");

    readonly By slctEndorsementType = By.XPath("//select[@name='select']");

    readonly By ddQAProductionMonth = By.XPath("//button[@name='QA_Month__c']");
    readonly By ddQAProductionMonthValue = By.XPath("//lightning-combobox/label[contains(text(),'QA/Production Month')]/following-sibling::div//lightning-base-combobox-item/span[2]/span");

    readonly By ddQAProductionYear = By.XPath("//button[@name='QA_Year__c']");
    readonly By ddQAProductionYearValue = By.XPath("//lightning-combobox/label[contains(text(),'QA/Production Year')]/following-sibling::div//lightning-base-combobox-item/span[2]/span");

    readonly By txtWrittenShare = By.XPath("//input[@name='Written_Share__c']");
    readonly By txtPML = By.XPath("//input[@name='PML__c']");
    readonly By txtEstimatedSigning = By.XPath("//input[@name='Estimated_Signed_Share__c']");
    readonly By txt100PercentPolicyLimit = By.XPath("//input[@name='Policy_Limit_Curr__c']");
    readonly By txt100PercentPolicyBookedPremium = By.XPath("//input[@name='Amount']");
    readonly By txt100PercentTIV = By.XPath("//input[@name='TIV__c']");
    readonly By txtForecastPremium = By.XPath("//input[@name='Forecast_Premium__c']");
    readonly By ddPurchasedExternalFACReinsurance = By.XPath("//button[@name='FAC_Reinsurance__c']");
    readonly By ddPurchasedExternalFACReinsuranceValue = By.XPath("//button[@name='FAC_Reinsurance__c']/following::div[2]//span[2]/span");
    readonly By txt100PercentDSU = By.XPath("//input[@name='Delay_in_Startup_Period__c']");
    readonly By ddUnderwritingCreditBranch = By.XPath("//button[@name='Underwriting_Credit_Branch__c']");
    readonly By ddUnderwritingCreditBranchValue = By.XPath("//button[@name='Underwriting_Credit_Branch__c']/following::div[2]//span[2]/span");
    readonly By txtUCBShare = By.XPath("//input[@name='UCB_Share_percentage__c']");
    readonly By ddPurchaseExternalFAC = By.XPath("//button[@name='Purchase_External_FAC_UCB__c']");
    readonly By ddPurchasedExternalFACValue = By.XPath("//button[@name='Purchase_External_FAC_UCB__c']/following::div[2]//span[2]/span");


    // Cumulative Calculations
    readonly By lbl100PercentPolicyBookedPremiumValue = By.XPath("((//span[contains(text(),'100 Percent Policy Booked Premium')])[1]/following::div/span)[1]/span");
    readonly By lblProducerCommissionPercentValue = By.XPath("(((//span[contains(text(),'Producer Commission %')]))[1]/following::div/span/span)[1]");
    readonly By lblProducerCommissionDollarValue = By.XPath("(((//span[contains(text(),'Producer Commission $')]))[1]/following::div/span/span)[1]");

    readonly By lblProducerCommissionStarDollarValue = By.XPath("(((//span[contains(text(),'Producer Commission Starr $')]))[1]/following::div/span/span)[1]");
    readonly By lblStarrEPIGWPValue = By.XPath("//span[contains(text(),'Policy Premium Written Share %')]");
    readonly By lblSGSContent = By.XPath("//span[contains(text(),'Local Client Name')]");

    readonly By lblCumulativePolicyProducerComStarrDollarValue = By.XPath("(((//span[contains(text(),'Cumulative Policy Producer Com Starr $')]))[1]/following::div/span/span)[1]");
    readonly By lblCumulativePolicyProdComStarrDollarChildValue = By.XPath("(((//span[contains(text(),'Cumulative Policy Prod Com Starr $ Child')]))[1]/following::div/span/span)[1]");
    readonly By lblProducerCommissionDollarUSDValue = By.XPath("(((//span[contains(text(),'Producer Commission $ (USD)')]))[1]/following::div/span/span)[1]");

    readonly By lblDailyConversationRateValue = By.XPath("(((//span[contains(text(),'Daily Conversion Rate')]))[1]/following::div/span/span)[1]");

    readonly By lblProducerCommissionUSDLloydsValue = By.XPath("(//span[contains(text(),\"Producer Commission USD (Lloyd's)\")])[1]/following::div/span/span");

    readonly By lblCurencyConversationRateValue = By.XPath("((//span[contains(text(),'Currency Conversion Rate')])[1]/following::div/span/span)[1]");

    readonly By lblProducerCommissionUSDLLCValue = By.XPath("((//span[contains(text(),'Producer Commission USD (LLC)')])[1]/following::div/span/span)[1]");

    readonly By lblLLCConversationRateValue = By.XPath("((//span[contains(text(),'LLC Conversion Rate')])[1]/following::div/span/span)[1]");

    readonly By lblStarrProducerCommissionUSDLloydsValue = By.XPath("((//span[contains(text(),\"Starr Producer Commission USD (Lloyd's)\")])[1]/following::div/span/span)[1]");

    readonly By lblStarrProducerCommissionUSDLLCValue = By.XPath("((//span[contains(text(),'Starr Producer Commission USD (LLC)')])[1]/following::div/span/span)[1]");

    readonly By lblLLCConversationRate = By.XPath("((//span[contains(text(),'LLC Conversion Rate')])[1]/following::div/span/span)[1]");

    // Submission Tabs

    readonly By tabUWProducer = By.XPath("//li/a[contains(text(),'UW/Producer')]");
    readonly By tabPremiumLimits = By.XPath("//li/a[contains(text(),'Prm/Limits')]");
    readonly By tabCurrency = By.XPath("//li/a[contains(text(),'Currency')]");
    readonly By tabSGS = By.XPath("//li/a[contains(text(),'SGS')]");


    // Navigate Back to Parent submisison
    readonly By lblParentSubmission = By.XPath("(//span[contains(text(),'Parent Opportunity')])[1]");
    readonly By linkParentSubmissionValue = By.XPath("((//span[contains(text(),'Parent Opportunity')])[1]/following::div)[1]/span/a");
    readonly By tabDetails = By.XPath("//li/a[@id='detailTab__item']");
    readonly By lblSubmissionName = By.XPath("(//slot[@name='primaryField'])[2]/lightning-formatted-text");



    // Policy Numbers Web Element
    readonly By PolicyNumberSelection = By.XPath("//a[@id='Policy_Number__c']");
    readonly By lstApplicableRecordTypesValues = By.XPath("(//div[contains(text(),'Applicable Record Types')]/following::ul)[1]/li/div/span/span");
    readonly By btnMoveToChoosenRecordTypes = By.XPath("(//button[@title='Move selection to Chosen'])[1]");

    readonly By lstApplicableIssuingOfficeValues = By.XPath("(//div[contains(text(),'Applicable Issuing Office')]/following::ul)[1]/li/div/span/span");
    readonly By btnMoveToChoosenIssuingOffice = By.XPath("(//button[@title='Move selection to Chosen'])[2]");

    readonly By lstApplicableIssuingCompany = By.XPath("(//div[contains(text(),'Applicable Issuing Company')]/following::ul)[1]/li/div/span/span");
    readonly By btnMoveToChoosenIssuingCompany = By.XPath("(//button[@title='Move selection to Chosen'])[3]");

    readonly By lstApplicableProductionOffice = By.XPath("(//div[contains(text(),'Applicable Production Office')]/following::ul)[1]/li/div/span/span");
    readonly By btnMoveToChoosenProductionOffice = By.XPath("(//button[@title='Move selection to Chosen'])[4]");


    readonly By lstApplicableProducers = By.XPath("(//div[contains(text(),'Applicable Producer')]/following::ul)[1]/li/div/span/span");
    readonly By btnMoveToChoosenProducers = By.XPath("(//button[@title='Move selection to Chosen'])[3]");

    //readonly By btnMoveToChoosenProductionOffice = By.XPath("(//button[@title='Move selection to Chosen'])[4]");

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

    // Forecast Tab values
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


    public EndorsementPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
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

    public void NavigateToEndrosementCreationPage()
    {
        driver.MoveToTheElement(btnNewEndrosement);
        Assert.IsTrue(driver.WaitAndClick(btnNewEndrosement), "Could not click on New Endorsement button");
        Assert.IsTrue(driver.WaitForElementToPresent(pageEndorsement), "Endorsement creation page is not displayed");
        Log("Clicked on New Endorsement");
    }

    public void ThenUserNavigatedToEndorsementRecord()
    {
        System.Threading.Thread.Sleep(2000);
        driver.Navigate().GoToUrl(_scenarioContext["Endorsement_URL" + loggingStep.rowNo].ToString());
        Console.WriteLine("Endorsement URL - " + _scenarioContext["Endorsement_URL" + loggingStep.rowNo]);
        Log("Endorsement URL - " + _scenarioContext["Endorsement_URL" + loggingStep.rowNo]);
    }

    public void SaveEndorsement()
    {
        driver.MoveToTheElement(btnSave);
        Assert.IsTrue(driver.WaitAndClick(btnSave), "COULD NOT CLICK ON SAVE BUTTON");
        driver.CaptureScreen(_scenarioContext);
        driver.MoveToTheElement(pageEndorsement);
        for (int waitTIteration = 0; waitTIteration < 3; waitTIteration++)
        {
            if (driver.IsDisplayed(lblErrorMessageInStageProgression))
            {
                if (driver.IsDisplayed(lblErrorMessages))
                {
                    driver.ScrollToCenter(lblErrorMessages);
                    driver.CaptureScreen(_scenarioContext);
                    Console.WriteLine("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessages));
                    Log("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessages));
                    Assert.Fail("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessages));
                }
                else
                {
                    Console.WriteLine("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessageInStageProgression));
                    Log("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessageInStageProgression));
                    Assert.Fail("ERROR MESSAGE IS DISPLAYED" + driver.GetTextFromElement(lblErrorMessageInStageProgression));
                }
            }
            else
            {
                System.Threading.Thread.Sleep(2000);
            }
        }
        driver.WaitTillOverlayDisappears(btnSave);
        driver.Refresh();
        Assert.IsTrue(driver.WaitForElementToPresent(lblCreatedEndorsement), "COULD NOT CREATE ENDORSEMENT RECORD");
        _scenarioContext["Endorsement_URL" + loggingStep.rowNo] = driver.Url;
        Console.WriteLine("Endorsement URL - " + _scenarioContext["Endorsement_URL" + loggingStep.rowNo]);
        Log(" ENDORSEMENT RECORD IS CREATED SUCCESSFULLY");
        Console.WriteLine(" ENDORSEMENT RECORD IS CREATED SUCCESSFULLY");
    }

    public void sendDDValue(By ddFieldWebElement, By Listelement, string inputValue, string FieldName)
    {
        driver.ScrollToCenter(ddFieldWebElement);
        driver.WaitAndClick(ddFieldWebElement);
        Assert.IsTrue(driver.SelectFromList(Listelement, inputValue), FieldName + " was not inputed");
        Log(FieldName + " Field value is inputed");
    }


    public void selectLookUpValue(By element1, string inputValue, By element2, string FieldName)
    {
        driver.ScrollToCenter(element1);
        driver.WaitAndClick(element1);
        driver.ClearAndSend(element1, inputValue);
        driver.WaitForElementToPresent(element2);
        Assert.IsTrue(driver.WaitAndClick(element2), FieldName + "  was not inputed");
        Log(FieldName + "Was Inputed");
    }

    public void sendTextToField(By txtFieldWebElement, string txtFieldValue, string txtFieldName)
    {
        driver.ScrollToCenter(txtFieldWebElement);
        Assert.IsTrue(driver.ClearAndSend(txtFieldWebElement, txtFieldValue), txtFieldName + " field Was NOT Inputed");
        Log(txtFieldName + " field value is inputed");
    }

    public string GenerateDecimalNumber()
    {
        return GenerateRandomNumbers() + "." + Generate2DigitRandomNumber();
    }



    public Boolean SelectRandomValueFromDropdown(By ddField, By ListElements, string FieldName)
    {
        driver.ScrollToCenter(ddField);
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
                System.Threading.Thread.Sleep(3000);
                driver.MoveToTheElementAndClick(elements[i]);
                Log(FieldName + " Is inputed with value " + elements[i].ToString());
                return true;
            }
        }
        Log(FieldName + " Could not Inputed");
        return false;
    }

    public static string Generate2DigitRandomNumber()
    {
        Random random = new Random();
        return random.Next(99).ToString();
    }

    public string removeSpecialCharacterFromString(string inputValue)
    {
        return inputValue.Replace("%", string.Empty);

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


    public void NavigateToTabs(By TabElement, By FieldElement)
    {
        driver.WaitForElementToPresent(TabElement);
        driver.ScrollToCenter(TabElement);
        driver.WaitAndClick(TabElement);
        driver.WaitForElementToPresent(FieldElement);
        //driver.ScrollToCenter(FieldElement);
    }

    public void CalculateAndVerifyProducerCommissionDollarValue(string CalculationFieldName)
    {
        string Currency;

        if (flag == false)
        {
            Assert.IsTrue(driver.WaitForElementToPresent(tabDetails), "COULD NOT NAVIGATE TO SUBMISSION PAGE");
            Currency = GetValueUsingFieldName("Submission Currency", lblDetailsTabFieldsName, lblDetailsFieldValue);
            if (Currency.Trim().Contains("USD"))
            {
                DisplayDecimalDigit = 2;
            }
            else
            {
                DisplayDecimalDigit = 2;
            }
            flag = true;
        }

        switch (CalculationFieldName)
        {
            case "Producer Commission $":

                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lbl100PercentPolicyBookedPremiumValue);
                decimal HundredPercentPolicyBookedPremiumValue = RemoveUnwantedFromString(GetValueUsingFieldName("100 Percent Policy Booked Premium", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                NavigateToTabs(tabUWProducer, lblProducerCommissionPercentValue);

                decimal ProducerCommissionPercentValue = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Producer Commission %", lblUWProducerTabFieldsName, lblUWProducerFieldsValue)));

                decimal ProducerCommissionDollarValue = RemoveUnwantedFromString(GetValueUsingFieldName("Producer Commission $", lblUWProducerTabFieldsName, lblUWProducerFieldsValue));

                decimal CalculateProducerCommissionDollarValue = (ProducerCommissionPercentValue * HundredPercentPolicyBookedPremiumValue) / 100;

                calculations(
                    "Percentage",
                    HundredPercentPolicyBookedPremiumValue,
                    ProducerCommissionPercentValue,
                    ProducerCommissionDollarValue,
                     Math.Round(CalculateProducerCommissionDollarValue, DisplayDecimalDigit),
                    "Producer Commission %",
                    "100 Percent Policy Booked Premium",
                    "Producer Commission $"
                    );

                break;

            case "Producer Commission Starr $":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                decimal StarrEPIGWPValue = RemoveUnwantedFromString(GetValueUsingFieldName("Starr EPI/GWP", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                NavigateToTabs(tabUWProducer, lblProducerCommissionPercentValue);
                ProducerCommissionPercentValue = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Producer Commission %", lblUWProducerTabFieldsName, lblUWProducerFieldsValue)));
                decimal ProducerCommissionStarrDollarValue = RemoveUnwantedFromString(GetValueUsingFieldName("Producer Commission Starr $", lblUWProducerTabFieldsName, lblUWProducerFieldsValue));
                decimal CalculateProducerCommissionStarrDollarValue = (ProducerCommissionPercentValue * StarrEPIGWPValue) / 100;

                calculations(
                    "Percentage",
                    ProducerCommissionPercentValue,
                    StarrEPIGWPValue,
                    ProducerCommissionStarrDollarValue,
                     Math.Round(CalculateProducerCommissionStarrDollarValue, DisplayDecimalDigit),
                    "Producer Commission %",
                    "Starr EPI/GWP",
                    "Producer Commission Starr $"
                    );
                break;

            case "Cumulative Policy Producer Com Starr $":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                decimal CumulativePolicyProdComStarrDollarChildValue = decimal.Parse(GetValueUsingFieldName("Cumulative Policy Prod Com Starr $ Child", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                decimal ProducerCommissionDollarUSDValue = decimal.Parse(GetValueUsingFieldName("Producer Commission $ (USD)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                decimal CumulativePolicyProducerComStarrDollarValue = decimal.Parse(GetValueUsingFieldName("Cumulative Policy Producer Com Starr $", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                decimal CalculateCumulativePolicyProducerComStarrDollarValue = CumulativePolicyProdComStarrDollarChildValue + ProducerCommissionDollarUSDValue;

                calculations(
                    "add",
                    CumulativePolicyProdComStarrDollarChildValue,
                    ProducerCommissionDollarUSDValue,
                    CumulativePolicyProducerComStarrDollarValue,
                     Math.Round(CalculateCumulativePolicyProducerComStarrDollarValue, DisplayDecimalDigit),
                    "Cumulative Policy Producer Com Starr $",
                    "Producer Commission $ (USD)",
                    "Cumulative Policy Producer Com Starr $"
                    );

                break;

            case "Producer Commission $ (USD)":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabUWProducer, lblProducerCommissionStarDollarValue);
                decimal ProducerCommissionStarDollarValue = RemoveUnwantedFromString(GetValueUsingFieldName("Producer Commission Starr $", lblUWProducerTabFieldsName, lblUWProducerFieldsValue));

                NavigateToTabs(tabPremiumLimits, lblDailyConversationRateValue);
                decimal DailyConversationRateValue = decimal.Parse(GetValueUsingFieldName("Daily Conversion Rate", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                ProducerCommissionDollarUSDValue = decimal.Parse(GetValueUsingFieldName("Producer Commission $ (USD)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                decimal CalculateProducerCommissionDollarUSDValue = ProducerCommissionStarDollarValue / DailyConversationRateValue;

                calculations(
                    "Divide",
                    ProducerCommissionStarDollarValue,
                    DailyConversationRateValue,
                    ProducerCommissionDollarUSDValue,
                     Math.Round(CalculateProducerCommissionDollarUSDValue, DisplayDecimalDigit),
                    "Producer Commission Starr $",
                    "Daily Conversion Rate",
                    "Producer Commission $ (USD)"
                    );

                break;

            case "Producer Commission USD (Lloyd's)":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabUWProducer, lblProducerCommissionStarDollarValue);

                decimal ProducerCommissionDollar = RemoveUnwantedFromString(GetValueUsingFieldName("Producer Commission $", lblUWProducerTabFieldsName, lblUWProducerFieldsValue));

                NavigateToTabs(tabCurrency, lblCurencyConversationRateValue);
                decimal CurrencyConversationRate = decimal.Parse(GetValueUsingFieldName("Currency Conversion Rate", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));

                decimal ProducerCommissionUSDLloyds = decimal.Parse(GetValueUsingFieldName("Producer Commission USD (Lloyd's)", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));

                decimal CalculateProducerCommissionUSDLloyds = ProducerCommissionDollar / CurrencyConversationRate;

                calculations(
                   "Divide",
                   ProducerCommissionDollar,
                   CurrencyConversationRate,
                   ProducerCommissionUSDLloyds,
                    Math.Round(CalculateProducerCommissionUSDLloyds, DisplayDecimalDigit),
                   "Producer Commission $",
                   "Currency Conversion Rate",
                   "Producer Commission USD (Lloyd's)"
                   );

                break;

            case "Producer Commission USD (LLC)":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");
                NavigateToTabs(tabUWProducer, lblProducerCommissionStarDollarValue);

                decimal ProductionCommissionDollar = RemoveUnwantedFromString(GetValueUsingFieldName("Producer Commission $", lblUWProducerTabFieldsName, lblUWProducerFieldsValue));

                NavigateToTabs(tabCurrency, lblLLCConversationRateValue);
                decimal LLCConversationRateValue = decimal.Parse(GetValueUsingFieldName("LLC Conversion Rate", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));

                decimal ProducerCommissionUSDLLC = decimal.Parse(GetValueUsingFieldName("Producer Commission USD (LLC)", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));

                decimal CalculateProducerCommissionUSDLLC = ProductionCommissionDollar / LLCConversationRateValue;

                calculations(
                  "Divide",
                  ProductionCommissionDollar,
                  LLCConversationRateValue,
                  ProducerCommissionUSDLLC,
                   Math.Round(CalculateProducerCommissionUSDLLC, DisplayDecimalDigit),
                  "Producer Commission $",
                  "LLC Conversion Rate",
                  "Producer Commission USD (LLC)"
                  );
                break;

            case "Starr Producer Commission USD (Lloyd's)":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabUWProducer, lblProducerCommissionStarDollarValue);

                ProducerCommissionStarrDollarValue = RemoveUnwantedFromString(GetValueUsingFieldName("Producer Commission Starr $", lblUWProducerTabFieldsName, lblUWProducerFieldsValue));

                NavigateToTabs(tabCurrency, lblCurencyConversationRateValue);
                decimal CurencyConversationRateValue = decimal.Parse(GetValueUsingFieldName("Currency Conversion Rate", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));

                decimal StarrProducerCommissionUSDLloydsValue = decimal.Parse(GetValueUsingFieldName("Starr Producer Commission USD (Lloyd's)", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));

                decimal CalculateStarrProducerCommissionUSDLloyds = ProducerCommissionStarrDollarValue / CurencyConversationRateValue;

                calculations(
                  "Divide",
                  ProducerCommissionStarrDollarValue,
                  CurencyConversationRateValue,
                  StarrProducerCommissionUSDLloydsValue,
                   Math.Round(CalculateStarrProducerCommissionUSDLloyds, DisplayDecimalDigit),
                  "Producer Commission Starr $",
                  "Currency Conversion Rate",
                  "Starr Producer Commission USD (Lloyd's)"
                  );
                break;

            case "Starr Producer Commission USD (LLC)":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabUWProducer, lblProducerCommissionStarDollarValue);

                ProducerCommissionStarrDollarValue = RemoveUnwantedFromString(GetValueUsingFieldName("Producer Commission Starr $", lblUWProducerTabFieldsName, lblUWProducerFieldsValue));

                NavigateToTabs(tabCurrency, lblLLCConversationRateValue);
                LLCConversationRateValue = decimal.Parse(GetValueUsingFieldName("LLC Conversion Rate", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));

                decimal StarrProducerCommissionUSDLLCValue = decimal.Parse(GetValueUsingFieldName("Starr Producer Commission USD (LLC)", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));

                decimal CalculateStarrProducerCommissionUSDLL = ProducerCommissionStarrDollarValue / LLCConversationRateValue;

                calculations(
                 "divide",
                 ProducerCommissionStarrDollarValue,
                 LLCConversationRateValue,
                 StarrProducerCommissionUSDLLCValue,
                  Math.Round(CalculateStarrProducerCommissionUSDLL, DisplayDecimalDigit),
                 "Producer Commission Starr $",
                 "LLC Conversion Rate",
                 "Starr Producer Commission USD (LLC)"
                 );
                break;

            case "Starr PML":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                decimal PMLfactor = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("PML Factor", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue)));
                decimal StarrLimitCurrency = RemoveUnwantedFromString(GetValueUsingFieldName("Starr Limit (Currency)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal StarrPML = RemoveUnwantedFromString(GetValueUsingFieldName("Starr PML", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal CalculateStarrPMLValue = (PMLfactor * StarrLimitCurrency) / 100;

                calculations(
                 "Percentage",
                 PMLfactor,
                 StarrLimitCurrency,
                 StarrPML,
                  Math.Round(CalculateStarrPMLValue, DisplayDecimalDigit),
                 "PML Factor",
                 "Starr Limit(Currency)",
                 "Starr PML"
                 );
                break;

            case "Starr Limit (Currency)":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                decimal SignedShare = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Signed Share", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue)));
                decimal HundredPercentPolicyLimitCurrency = RemoveUnwantedFromString(GetValueUsingFieldName("100% Policy Limit Amount (Currency)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                StarrLimitCurrency = RemoveUnwantedFromString(GetValueUsingFieldName("Starr Limit (Currency)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal CalculateStarrLimitCurrency = (SignedShare * HundredPercentPolicyLimitCurrency) / 100;

                calculations(
                 "Percentage",
                 SignedShare,
                 HundredPercentPolicyLimitCurrency,
                 StarrLimitCurrency,
                  Math.Round(CalculateStarrLimitCurrency, DisplayDecimalDigit),
                 "Signed Share",
                 "100% Policy Limit Amount (Currency)",
                 "Starr Limit (Currency)"
                 );
                break;


            case "Starr EPI/GWP (USD)":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                decimal StarrEPIGWP = RemoveUnwantedFromString(GetValueUsingFieldName("Starr EPI/GWP", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal DailyConversionRate = decimal.Parse(GetValueUsingFieldName("Daily Conversion Rate", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal StarrEPIGWPUSD = decimal.Parse(GetValueUsingFieldName("Starr EPI/GWP (USD)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal CalculateStarrEPIGWPUSD = (StarrEPIGWP / DailyConversionRate);

                calculations(
                 "Divide",
                 StarrEPIGWP,
                 DailyConversionRate,
                 StarrEPIGWPUSD,
                  Math.Round(CalculateStarrEPIGWPUSD, DisplayDecimalDigit),
                 "Starr EPI/GWP",
                 "Daily Conversion Rate",
                 "Starr EPI/GWP (USD)"
                 ); ;
                break;

            case "Starr Limit (USD)":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                StarrLimitCurrency = RemoveUnwantedFromString(GetValueUsingFieldName("Starr Limit (Currency)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                DailyConversionRate = decimal.Parse(GetValueUsingFieldName("Daily Conversion Rate", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal starrLimitUSD = decimal.Parse(GetValueUsingFieldName("Starr Limit (USD)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal CalculatestarrLimitUSD = (StarrLimitCurrency / DailyConversionRate);

                calculations(
                 "Divide",
                 StarrLimitCurrency,
                 DailyConversionRate,
                 starrLimitUSD,
                 Math.Round(CalculatestarrLimitUSD, DisplayDecimalDigit),
                 "Starr Limit (Currency)",
                 "Daily Conversion Rate",
                 "Starr Limit (USD)"
                 );
                break;


            case "Starr EPI/GWP USD (Lloyd's)":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                StarrEPIGWP = RemoveUnwantedFromString(GetValueUsingFieldName("Starr EPI/GWP", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                NavigateToTabs(tabCurrency, lblLLCConversationRateValue);
                decimal CurrencyConversionRate = decimal.Parse(GetValueUsingFieldName("Currency Conversion Rate", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));
                decimal StarrEPIGWPUSDLloyds = decimal.Parse(GetValueUsingFieldName("Starr EPI/GWP USD (Lloyd's)", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));
                decimal CalculateStarrEPIGWPUSDLloyds = (StarrEPIGWP / CurrencyConversionRate);

                calculations(
                 "Divide",
                 StarrEPIGWP,
                 CurrencyConversionRate,
                 StarrEPIGWPUSDLloyds,
                 Math.Round(CalculateStarrEPIGWPUSDLloyds, DisplayDecimalDigit),
                 "Starr EPI/GWP",
                 "Currency Conversion Rate",
                 "Starr EPI/GWP USD (Lloyd's)"
                 );
                break;

            case "Starr PML USD (Lloyd's)":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                StarrPML = RemoveUnwantedFromString(GetValueUsingFieldName("Starr PML", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                NavigateToTabs(tabCurrency, lblLLCConversationRateValue);
                CurrencyConversionRate = decimal.Parse(GetValueUsingFieldName("Currency Conversion Rate", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));
                decimal StarrPMLUSDLloyds = decimal.Parse(GetValueUsingFieldName("Starr PML USD (Lloyd's)", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));
                decimal CalculateStarrPMLUSDLloyds = (StarrPML / CurrencyConversionRate);

                calculations(
                 "Divide",
                 StarrPML,
                 CurrencyConversionRate,
                 StarrPMLUSDLloyds,
                 Math.Round(CalculateStarrPMLUSDLloyds, DisplayDecimalDigit),
                 "Starr PML",
                 "Currency Conversion Rate",
                 "Starr PML USD (Lloyd's)"
                 );
                break;

            case "Starr EPI/GWP USD (LLC)":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                StarrEPIGWP = RemoveUnwantedFromString(GetValueUsingFieldName("Starr EPI/GWP", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                NavigateToTabs(tabCurrency, lblLLCConversationRateValue);
                decimal LLCConversionRate = decimal.Parse(GetValueUsingFieldName("LLC Conversion Rate", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));
                decimal StarrEPIGWPUSDLLC = decimal.Parse(GetValueUsingFieldName("Starr EPI/GWP USD (LLC)", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));
                decimal CalculateStarrEPIGWPUSDLLC = (StarrEPIGWP / LLCConversionRate);

                calculations(
                 "Divide",
                 StarrEPIGWP,
                 LLCConversionRate,
                 StarrEPIGWPUSDLLC,
                 Math.Round(CalculateStarrEPIGWPUSDLLC, DisplayDecimalDigit),
                 "Starr EPI/GWP",
                 "LLC Conversion Rate",
                 "Starr EPI/GWP USD (LLC)"
                 );
                break;

            case "Cumulative Starr PRM USD":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                StarrEPIGWPUSD = decimal.Parse(GetValueUsingFieldName("Starr EPI/GWP (USD)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                decimal CumulativeStarrEPIGWPPremiumChild = decimal.Parse(GetValueUsingFieldName("Cumulative Starr EPI/GWP Premium Child", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                decimal CumulativeStarrPRMUSD = decimal.Parse(GetValueUsingFieldName("Cumulative Starr PRM USD", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                decimal CalculateCumulativeStarrPRMUSD = StarrEPIGWPUSD + CumulativeStarrEPIGWPPremiumChild;

                calculations(
                    "add",
                    StarrEPIGWPUSD,
                    CumulativeStarrEPIGWPPremiumChild,
                    CumulativeStarrPRMUSD,
                    Math.Round(CalculateCumulativeStarrPRMUSD, DisplayDecimalDigit),
                    "Cumulative Policy Producer Com Starr $",
                    "Producer Commission $ (USD)",
                    "Cumulative Starr PRM USD"
                    );

                break;

            case "Cumulative Pol Starr EPI/GWP Prm Lloyd's":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                decimal CumulativePolStarrEPIGWPChildLloyds = decimal.Parse(GetValueUsingFieldName("Cumulative Pol Starr EPI/GWP ChildLloyds", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                decimal CumulativePolStarrEPIGWPPrmLloyds = decimal.Parse(GetValueUsingFieldName("Cumulative Pol Starr EPI/GWP Prm Lloyd's", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                NavigateToTabs(tabCurrency, lblLLCConversationRateValue);
                StarrEPIGWPUSDLloyds = decimal.Parse(GetValueUsingFieldName("Starr EPI/GWP USD (Lloyd's)", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));

                decimal CalculateCumulativePolStarrEPIGWPPrmLloyds = CumulativePolStarrEPIGWPChildLloyds + StarrEPIGWPUSDLloyds;

                calculations(
                    "add",
                    CumulativePolStarrEPIGWPChildLloyds,
                    StarrEPIGWPUSDLloyds,
                    CumulativePolStarrEPIGWPPrmLloyds,
                    Math.Round(CalculateCumulativePolStarrEPIGWPPrmLloyds, DisplayDecimalDigit),
                    "Cumulative Pol Starr EPI/GWP ChildLloyds",
                    "Starr EPI/GWP USD (Lloyd's)",
                    "Cumulative Pol Starr EPI/GWP Prm Lloyd's"
                    );
                break;

            case "Cumulative Starr Limit":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                decimal ChildCumulativePolicyLimitStarr = decimal.Parse(GetValueUsingFieldName("Child Cumulative Policy Limit (Starr)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                decimal StarrLimitUSD = decimal.Parse(GetValueUsingFieldName("Starr Limit (USD)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                decimal CumulativeStarrLimit = decimal.Parse(GetValueUsingFieldName("Cumulative Starr Limit", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));

                decimal CalculateCumulativeStarrLimit = ChildCumulativePolicyLimitStarr + StarrLimitUSD;

                calculations(
                    "add",
                    ChildCumulativePolicyLimitStarr,
                    StarrLimitUSD,
                    CumulativeStarrLimit,
                    Math.Round(CalculateCumulativeStarrLimit, DisplayDecimalDigit),
                    "Child Cumulative Policy Limit (Starr)",
                    "Starr Limit (USD)",
                    "Cumulative Starr Limit"
                    );
                break;

            case "Starr EPI/GWP":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                decimal HundredPercentPolicyBookedPremium = RemoveUnwantedFromString(GetValueUsingFieldName("100 Percent Policy Booked Premium", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal Signedshare = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Signed Share", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue)));
                decimal StarrEPIGWP1 = RemoveUnwantedFromString(GetValueUsingFieldName("Starr EPI/GWP", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal CalculateStarrEPIGWP1 = (HundredPercentPolicyBookedPremium * Signedshare) / 100;

                calculations(
                    "Percentage",
                    HundredPercentPolicyBookedPremium,
                    Signedshare,
                    StarrEPIGWP1,
                    Math.Round(CalculateStarrEPIGWP1, DisplayDecimalDigit),
                    "100 Percent Policy Booked Premium",
                    "Signed Share",
                    "Starr EPI/GWP"
                    );
                break;

            case "Production Signed Share":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                SignedShare = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Signed Share", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue)));
                NavigateToTabs(tabSGS, lblSGSContent);
                decimal ProductionOfficeSharePercentage = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Production Office Share %", lblSGSTabFieldsName, lblSGSFieldsValue)));
                decimal ProductionSignedShare = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Production Signed Share", lblSGSTabFieldsName, lblSGSFieldsValue)));
                decimal CalculateProductionSignedShare = (SignedShare * ProductionOfficeSharePercentage) / 100;

                calculations(
                    "Percentage",
                    SignedShare,
                    ProductionOfficeSharePercentage,
                    ProductionSignedShare,
                     Math.Round(CalculateProductionSignedShare, DisplayDecimalDigit),
                    "Signed Share",
                    "Production Office Share %",
                    "Production Signed Share"
                    );
                break;

            case "Production Total 100 EPI/GWP":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                decimal HundredPercentpolicybookedpremium = RemoveUnwantedFromString(GetValueUsingFieldName("100 Percent Policy Booked Premium", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                NavigateToTabs(tabSGS, lblSGSContent);
                decimal ProductionOfficeSharePercent = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Production Office Share %", lblSGSTabFieldsName, lblSGSFieldsValue)));
                decimal ProductionTotal100EPIGWP = RemoveUnwantedFromString(GetValueUsingFieldName("Production Total 100 EPI/GWP", lblSGSTabFieldsName, lblSGSFieldsValue));
                decimal CalculateProductionTotal100EPIGWP = (HundredPercentpolicybookedpremium * ProductionOfficeSharePercent) / 100;

                calculations(
                    "Percentage",
                    HundredPercentpolicybookedpremium,
                    ProductionOfficeSharePercent,
                    ProductionTotal100EPIGWP,
                    Math.Round(CalculateProductionTotal100EPIGWP, DisplayDecimalDigit),
                    "100 Percent Policy Booked Premium",
                    "Production Office Share %",
                    "Production Total 100 EPI/GWP"
                    );
                break;

            case "Production Total 100 EPI/GWP USD":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                DailyConversionRate = decimal.Parse(GetValueUsingFieldName("Daily Conversion Rate", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                NavigateToTabs(tabSGS, lblSGSContent);
                ProductionTotal100EPIGWP = RemoveUnwantedFromString(GetValueUsingFieldName("Production Total 100 EPI/GWP", lblSGSTabFieldsName, lblSGSFieldsValue));
                decimal ProductionTotal100EPIGWPUSD = decimal.Parse(GetValueUsingFieldName("Production Total 100 EPI/GWP USD", lblSGSTabFieldsName, lblSGSFieldsValue));
                decimal CalculateProductionTotal100EPIGWPUSD = (ProductionTotal100EPIGWP / DailyConversionRate);

                calculations(
                    "Divide",
                    ProductionTotal100EPIGWP,
                    DailyConversionRate,
                    ProductionTotal100EPIGWPUSD,
                    Math.Round(CalculateProductionTotal100EPIGWPUSD, DisplayDecimalDigit),
                    "Production Total 100 EPI/GWP",
                    "Daily Conversion Rate",
                    "Production Total 100 EPI/GWP USD"
                    );
                break;

            case "Production Starr EPI/GWP":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                HundredPercentPolicyBookedPremiumValue = RemoveUnwantedFromString(GetValueUsingFieldName("100 Percent Policy Booked Premium", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                NavigateToTabs(tabSGS, lblSGSContent);
                ProductionTotal100EPIGWP = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Production Signed Share", lblSGSTabFieldsName, lblSGSFieldsValue)));
                decimal ProductionStarrEPIGWP = RemoveUnwantedFromString(GetValueUsingFieldName("Production Starr EPI/GWP", lblSGSTabFieldsName, lblSGSFieldsValue));
                decimal CalculateProductionStarrEPIGWP = (HundredPercentPolicyBookedPremiumValue * ProductionTotal100EPIGWP) / 100;

                calculations(
                    "Percentage",
                    HundredPercentPolicyBookedPremiumValue,
                    ProductionTotal100EPIGWP,
                    ProductionStarrEPIGWP,
                    Math.Round(CalculateProductionStarrEPIGWP, DisplayDecimalDigit),
                    "100 Percent Policy Booked Premium",
                    "Production Signed Share",
                    "Production Starr EPI/GWP"
                    );
                break;

            case "Production Policy Limit":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                HundredPercentPolicyLimitCurrency = RemoveUnwantedFromString(GetValueUsingFieldName("100% Policy Limit Amount (Currency)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                NavigateToTabs(tabSGS, lblSGSContent);
                ProductionOfficeSharePercent = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Production Office Share %", lblSGSTabFieldsName, lblSGSFieldsValue)));
                decimal ProductionPolicyLimit = RemoveUnwantedFromString(GetValueUsingFieldName("Production Policy Limit", lblSGSTabFieldsName, lblSGSFieldsValue));
                decimal CalculateProductionPolicyLimit = (HundredPercentPolicyLimitCurrency * ProductionOfficeSharePercent) / 100;

                calculations(
                    "Percentage",
                    HundredPercentPolicyLimitCurrency,
                    ProductionOfficeSharePercent,
                    ProductionPolicyLimit,
                    Math.Round(CalculateProductionPolicyLimit, DisplayDecimalDigit),
                    "100% Policy Limit Amount (Currency)",
                    "Production Office Share %",
                    "Production Policy Limit"
                    );
                break;

            case "Production Policy Limit USD":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                decimal Dailyconversionrate = decimal.Parse(GetValueUsingFieldName("Daily Conversion Rate", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                NavigateToTabs(tabSGS, lblSGSContent);
                ProductionPolicyLimit = RemoveUnwantedFromString(GetValueUsingFieldName("Production Policy Limit", lblSGSTabFieldsName, lblSGSFieldsValue));
                decimal ProductionPolicyLimitUSD = decimal.Parse(GetValueUsingFieldName("Production Policy Limit USD", lblSGSTabFieldsName, lblSGSFieldsValue));
                decimal CalculateProductionPolicyLimitUSD = (ProductionPolicyLimit / Dailyconversionrate);

                calculations(
                    "Divide",
                    ProductionPolicyLimit,
                    Dailyconversionrate,
                    ProductionPolicyLimitUSD,
                    Math.Round(CalculateProductionPolicyLimitUSD, DisplayDecimalDigit),
                    "Production Policy Limit",
                    "Daily Conversion Rate",
                    "Production Policy Limit USD"
                    );
                break;

            case "Production Starr Limit USD":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                Dailyconversionrate = decimal.Parse(GetValueUsingFieldName("Daily Conversion Rate", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                NavigateToTabs(tabSGS, lblSGSContent);
                decimal ProductionStarrLimit = RemoveUnwantedFromString(GetValueUsingFieldName("Production Starr Limit", lblSGSTabFieldsName, lblSGSFieldsValue));
                decimal ProductionStarrLimitUSD = decimal.Parse(GetValueUsingFieldName("Production Starr Limit USD", lblSGSTabFieldsName, lblSGSFieldsValue));
                decimal CalculateProductionStarrLimitUSD = (ProductionStarrLimit / Dailyconversionrate);

                calculations(
                    "Divide",
                    ProductionStarrLimit,
                    Dailyconversionrate,
                    ProductionStarrLimitUSD,
                    Math.Round(CalculateProductionStarrLimitUSD, DisplayDecimalDigit),
                    "Production Starr Limit",
                    "Daily Conversion Rate",
                    "Production Starr Limit USD"
                    );
                break;

            case "Cumulative Policy Starr EPI/GWP Prm LLC":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabCurrency, lblCurencyConversationRateValue);
                StarrEPIGWPUSDLLC = decimal.Parse(GetValueUsingFieldName("Starr EPI/GWP USD (LLC)", lblCurrencyTabFieldsName, lblCurrencyFieldsValue));

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                decimal CumulativePolStarrEPIGWPPrmCldLLC = decimal.Parse(GetValueUsingFieldName("Cumulative Pol Starr EPI/GWP Prm Cld LLC", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal CumulativePolicyStarrEPIGWPPrmLLC = decimal.Parse(GetValueUsingFieldName("Cumulative Policy Starr EPI/GWP Prm LLC", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                decimal CalculateCumulativePolicyStarrEPIGWPPrmLLC = (CumulativePolStarrEPIGWPPrmCldLLC + StarrEPIGWPUSDLLC);

                calculations(
                    "Add",
                    CumulativePolStarrEPIGWPPrmCldLLC,
                    StarrEPIGWPUSDLLC,
                    CumulativePolicyStarrEPIGWPPrmLLC,
                    Math.Round(CalculateCumulativePolicyStarrEPIGWPPrmLLC, DisplayDecimalDigit),
                    "Cumulative Pol Starr EPI/GWP Prm Cld LLC",
                    "Starr EPI/GWP USD (LLC)",
                    "Cumulative Policy Starr EPI/GWP Prm LLC"
                    );
                break;

            case "Production Starr Limit":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                HundredPercentPolicyLimitCurrency = RemoveUnwantedFromString(GetValueUsingFieldName("100% Policy Limit Amount (Currency)", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                SignedShare = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Signed Share", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue)));
                NavigateToTabs(tabSGS, lblSGSContent);
                ProductionOfficeSharePercentage = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Production Office Share %", lblSGSTabFieldsName, lblSGSFieldsValue)));
                ProductionStarrLimit = RemoveUnwantedFromString(GetValueUsingFieldName("Production Starr Limit", lblSGSTabFieldsName, lblSGSFieldsValue));
                decimal CalculateProductionStarrLimit = (HundredPercentPolicyLimitCurrency * SignedShare * ProductionOfficeSharePercentage) / 100;

                calculations(
                    "Percentage",
                    HundredPercentPolicyLimitCurrency,
                    SignedShare,
                    ProductionOfficeSharePercentage,
                    ProductionStarrLimit,
                    Math.Round(CalculateProductionStarrLimit, DisplayDecimalDigit),
                    "100% Policy Limit Amount (Currency)",
                    "Signed Share",
                    "Production Office Share %",
                    "Production Starr Limit"
                    );
                break;

            case "Production Starr PML":
                Log("------- Verifying Formula Field Name = " + CalculationFieldName + "--------------------------");

                NavigateToTabs(tabPremiumLimits, lblStarrEPIGWPValue);
                StarrPML = RemoveUnwantedFromString(GetValueUsingFieldName("Starr PML", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue));
                SignedShare = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("Signed Share", lblPrmLimitsTabFieldsName, lblPrmLimitsFieldsValue)));
                NavigateToTabs(tabSGS, lblSGSContent);
                ProductionOfficeSharePercentage = decimal.Parse(removeSpecialCharacterFromString(GetValueUsingFieldName("PProduction Office Share %", lblSGSTabFieldsName, lblSGSFieldsValue)));
                decimal ProductionStarrPML = RemoveUnwantedFromString(GetValueUsingFieldName("Production Starr PML", lblSGSTabFieldsName, lblSGSFieldsValue));
                decimal CalculateProductionStarrPML = (StarrPML * SignedShare * ProductionOfficeSharePercentage) / 100;

                calculations(
                    "Percentage",
                    StarrPML,
                    SignedShare,
                    ProductionOfficeSharePercentage,
                    ProductionStarrPML,
                    Math.Round(CalculateProductionStarrPML, DisplayDecimalDigit),
                    "Starr PML",
                    "Signed Share",
                    "Production Office Share %",
                    "Production Starr PML"
                    );
                break;

            default:
                Console.WriteLine("Field Name = " + CalculationFieldName);
                Console.WriteLine("PROVIDED FIELD NAME IS PRESENT");
                break;



        }
    }


    public void calculations(string type, decimal InputValue1, decimal InputValue2, decimal Output, decimal CalculatedOutput, string FieldName1, string FieldName2, string OutputField)
    {

        if (type.ToLower().Trim().Equals("percentage"))
        {
            Log("Calculated " + OutputField + " = (( " + FieldName1 + " ) * ( " + FieldName2 + ") / (100) ");
            //Log(Math.Round(CalculatedOutput, 2) + "=  (( " + InputValue1 + " ) * ( " + InputValue2 + " ) / 100)");
            Log(CalculatedOutput + "=  (( " + InputValue1 + " ) * ( " + InputValue2 + " ) / 100)");
            Console.WriteLine("Calculated " + OutputField + " = (( " + FieldName1 + " ) * ( " + FieldName2 + ") / (100) ");
            //Console.WriteLine(CalculatedOutput + "=  (( " + InputValue1 + " ) * ( " + InputValue2 + " ) / 100)");
            Console.WriteLine(CalculatedOutput + "=  (( " + InputValue1 + " ) * ( " + InputValue2 + " ) / 100)");
        }

        if (type.ToLower().Trim().Equals("add"))
        {
            Log("Calculated " + OutputField + " = (( " + FieldName1 + " ) + ( " + FieldName2 + ")  ");
            //Log(Math.Round(CalculatedOutput, 2) + "=  ( " + InputValue1 + " ) + ( " + InputValue2 + " ) ");
            Log(CalculatedOutput + "=  ( " + InputValue1 + " ) + ( " + InputValue2 + " ) ");

            Console.WriteLine("Calculated " + OutputField + " = (( " + FieldName1 + " ) + ( " + FieldName2 + ")  ");
            //Console.WriteLine(Math.Round(CalculatedOutput, 2) + "=  ( " + InputValue1 + " ) + ( " + InputValue2 + " ) ");
            Console.WriteLine(CalculatedOutput + "=  ( " + InputValue1 + " ) + ( " + InputValue2 + " ) ");
        }

        if (type.ToLower().Trim().Equals("divide"))
        {
            Log("Calculated " + OutputField + " = (( " + FieldName1 + " ) / ( " + FieldName2 + ")  ");
            //Log(Math.Round(CalculatedOutput, 2) + "=  ( " + InputValue1 + " ) / ( " + InputValue2 + " ) ");
            Log(CalculatedOutput + "=  ( " + InputValue1 + " ) / ( " + InputValue2 + " ) ");

            Console.WriteLine("Calculated " + OutputField + " = (( " + FieldName1 + " ) / ( " + FieldName2 + ")  ");
            //Console.WriteLine(Math.Round(CalculatedOutput, 2) + "=  ( " + InputValue1 + " ) / ( " + InputValue2 + " ) ");
            Console.WriteLine(CalculatedOutput + "=  ( " + InputValue1 + " ) / ( " + InputValue2 + " ) ");

        }

        Log("Displayed " + OutputField + "Value == Calculated" + OutputField + " Value ");
        //Log(Math.Round(Output, 2) + "  == " + Math.Round(CalculatedOutput, 2));
        Log(Output + "  == " + CalculatedOutput);

        Console.WriteLine("Displayed " + OutputField + " Value == Calculated " + OutputField + " Value ");
        //Console.WriteLine(Math.Round(Output, 2) + "  == " + Math.Round(CalculatedOutput, 2));
        Console.WriteLine(Output + "  == " + CalculatedOutput);

        try
        {
            //Assert.AreEqual(Math.Round(Output, 2), Math.Round(CalculatedOutput, 2), OutputField + " VALUE CALCULATION IS NOT ACCURATE");
            Assert.AreEqual(Output, CalculatedOutput, OutputField + " VALUE CALCULATION IS NOT ACCURATE");

            Log("---------------------" + OutputField + " CALCULATION IS ACCURATE ---------------------");
            Console.WriteLine("---------------------" + OutputField + " CALCULATION IS ACCURATE ---------------------");
        }
        catch (Exception e)
        {
            Log("---------------------" + OutputField + " CALCULATION IS NOT ACCURATE ---------------------");
            Console.WriteLine("---------------------" + OutputField + " CALCULATION IS NOT ACCURATE ---------------------");
        }

    }

    public void calculations(string type, decimal InputValue1, decimal InputValue2, decimal InputValue3, decimal Output, decimal CalculatedOutput, string FieldName1, string FieldName2, string FieldName3, string OutputField)
    {

        if (type.ToLower().Trim().Equals("percentage"))
        {
            Log("Calculated " + OutputField + " = (( " + FieldName1 + " ) * ( " + FieldName2 + ") * ( " + FieldName3 + ") / (100) ");
            //Log(Math.Round(CalculatedOutput, 2) + "=  (( " + InputValue1 + " ) * ( " + InputValue2 + " ) * (" + InputValue3 +") / 100)");
            Log(CalculatedOutput + "=  (( " + InputValue1 + " ) * ( " + InputValue2 + " ) * (" + InputValue3 + ") / 100)");
            Console.WriteLine("Calculated " + OutputField + " = (( " + FieldName1 + " ) * ( " + FieldName2 + ") * ( " + FieldName3 + ") / (100) ");
            //Console.WriteLine(Math.Round(CalculatedOutput, 2) + "=  (( " + InputValue1 + " ) * ( " + InputValue2 + " ) * (" + InputValue3 + ") / 100)");
            Console.WriteLine(CalculatedOutput + "=  (( " + InputValue1 + " ) * ( " + InputValue2 + " ) * (" + InputValue3 + ") / 100)");
        }

        Log("Displayed " + OutputField + "Value == Calculated" + OutputField + " Value ");
        //Log(Math.Round(Output, 2) + "  == " + Math.Round(CalculatedOutput, 2));
        Log(Output + "  == " + CalculatedOutput);

        Console.WriteLine("Displayed " + OutputField + " Value == Calculated " + OutputField + " Value ");
        //Console.WriteLine(Math.Round(Output, 2) + "  == " + Math.Round(CalculatedOutput, 2));
        Console.WriteLine(Output + "  == " + CalculatedOutput);

        try
        {
            //Assert.AreEqual(Math.Round(Output, 2), Math.Round(CalculatedOutput, 2), OutputField + " VALUE CALCULATION IS NOT ACCURATE");
            Assert.AreEqual(Output, CalculatedOutput, OutputField + " VALUE CALCULATION IS NOT ACCURATE");
            Log("---------------------" + OutputField + " CALCULATION IS ACCURATE ---------------------");
            Console.WriteLine("---------------------" + OutputField + " CALCULATION IS ACCURATE ---------------------");
        }
        catch (Exception e)
        {
            Log("---------------------" + OutputField + " CALCULATION IS NOT ACCURATE ---------------------");
            Console.WriteLine("---------------------" + OutputField + " CALCULATION IS NOT ACCURATE ---------------------");
        }

    }



    public void NavigateBackToParentRecordFromChild()
    {
        driver.ScrollToCenter(tabDetails);
        driver.WaitAndClick(tabDetails);
        driver.WaitForElementToPresent(lblParentSubmission);
        ExpectedParentSubmissionNumber = driver.GetTextFromElement(linkParentSubmissionValue);
        Assert.IsTrue(driver.WaitAndClick(linkParentSubmissionValue), "COULD NOT CLICK ON THE PARENT SUBMISSION LINK");
        Log("CLICKED ON PARENT SUBMISSION LINK");
    }

    public void VerifyParentSubmissionPage()
    {
        driver.WaitForElementToEnable(lblSubmissionName);
        string ActualSubmission = driver.GetTextFromElement(lblSubmissionName);
        Assert.AreEqual(ExpectedParentSubmissionNumber, ActualSubmission, "CHILD AND PARENT SUBMISSION IS NOT SAME");
        Log("NAVIGATED TO PARENT SUBMISSION");
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
}
