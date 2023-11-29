using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace SalesForce3.Pages;
public class TerrorismPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    public static string ScenarioCount, PolicyLimitId;
    public static string TerrorismFilepath = SubmissionPage.BaseURL + "Terrorism/Terrorism1_0.txt";

    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

    // Terrorism Webelements
    readonly By btnNewTerrorism = By.XPath("//button[@name='Opportunity.STARR_New_Terrorism']");
    readonly By pageTerrorism = By.XPath("//h2/span[contains(text(),'Terrorism')]");
    readonly By btnSave = By.XPath("//button[@name='cancel']/parent::div/button[1]");
    readonly By btnCancel = By.XPath("//button[@name='cancel']");
    readonly By lblCreatedTerrorism = By.XPath("//span[contains(text(),'Submission Name')]/following::div[1]/span/span");

    readonly By lblErrorMessages = By.XPath("//div[@class='slds-notify__content']/h2/following::p[1]");
    readonly By lblErrorMessageInStageProgression = By.XPath("//div[@class='slds-notify__content']/h2");

    readonly By txtParentOpportunity = By.XPath("(//label[contains(text(),'Parent Opportunity')]/following::div)[1]//input");
    readonly By lkupParentOpportunityValue = By.XPath("((//label[contains(text(),'Parent Opportunity')]/following::div)[1]/div//div)[4]//ul/li//span[2]/span");

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

    readonly By ddOcupancyValue = By.XPath("//lightning-combobox/label[contains(text(),'Occupancy')]/following-sibling::div//lightning-base-combobox-item/span[2]/span");
    readonly By ddOcupancy = By.XPath("//button[@name='Occupancy__c']");

    readonly By ddSubclass1Value = By.XPath("//lightning-combobox/label[contains(text(),'Subclass 1')]/following-sibling::div//lightning-base-combobox-item/span[2]/span");
    readonly By ddSubclass1 = By.XPath("//button[@name='Subclass_1__c']");

    readonly By ddProgramValue = By.XPath("//lightning-combobox/label[contains(text(),'Program')]/following-sibling::div//lightning-base-combobox-item/span[2]/span");
    readonly By ddProgram = By.XPath("//button[@name='Program__c']");

    // Error message 
    readonly By lblErrorMessage = By.XPath("//span[@class='toastMessage forceActionsText'] | //div[@class='slds-notify__content']/h2/following::p[1]");



    public TerrorismPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
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

    public void SaveTerrorismRecord()
    {
        driver.MoveToTheElement(btnSave);
        Assert.IsTrue(driver.WaitAndClick(btnSave), "COULD NOT CLICK ON SAVE BUTTON");
        driver.CaptureScreen(_scenarioContext);
        driver.MoveToTheElement(pageTerrorism);
        for (int waitIteration = 0; waitIteration < 3; waitIteration++)
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
                Log("Terrorism record creaed successfully");
                System.Threading.Thread.Sleep(2000);
            }
        }
    }

    public void sendDDValue(By ddFieldWebElement, By Listelement, string inputValue, string FieldName)
    {
        driver.ScrollToCenter(ddFieldWebElement);
        driver.WaitAndClick(ddFieldWebElement);
        Assert.IsTrue(driver.SelectFromList(Listelement, inputValue), FieldName + " WAS NOT INPUTED");
        Log(FieldName + " FIELD VALUE IS INPUTED");
    }


    public void selectLookUpValue(By element1, string inputValue, By element2, string FieldName)
    {
        driver.ScrollToCenter(element1);
        driver.WaitAndClick(element1);
        driver.ClearAndSend(element1, inputValue);
        driver.WaitForElementToPresent(element2);
        Assert.IsTrue(driver.WaitAndClick(element2), FieldName + "  WAS NOT INPUTED");
        Log(FieldName + "WAS INPUTED");
    }

    public void sendTextToField(By txtFieldWebElement, string txtFieldValue, string txtFieldName)
    {
        driver.ScrollToCenter(txtFieldWebElement);
        Assert.IsTrue(driver.ClearAndSend(txtFieldWebElement, txtFieldValue), txtFieldName + " FIELD WAS NOT INPUTED");
        Log(txtFieldName + " FIELD VALUE IS INPUTED");
    }

    public string GenerateDecimalNumber()
    {
        return GenerateRandomNumbers() + "." + Generate2DigitRandomNumber();
    }



    public Boolean SelectValueFromDropdownByIndex(By Elements, int IndexNo)
    {
        IList<IWebElement> elements = driver.ListOfElements(Elements);
        Random num = new Random();
        for (int i = 0; i <= elements.Count - 1; i++)
        {
            if (i == IndexNo)
            {
                System.Threading.Thread.Sleep(2000);
                elements[i].Click();
                return true;
            }
        }
        return false;
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
                Log(FieldName + " IS INPUTED WITH VALUE " + elements[i].ToString());
                return true;
            }
        }
        Log(FieldName + " COULD NOT INPUTED");
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
        decimal returnvalue = decimal.Parse(inputValue[1]);
        Log("Split the string and value is " + returnvalue);
        return returnvalue;
    }

    public void ThenVerifyTheErrorMessages(string ErrorMessage)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblErrorMessage));
        string displayedErrorMessage = driver.GetTextFromElement(lblErrorMessage);
        Assert.AreEqual(displayedErrorMessage, ErrorMessage, "DISPLAYED ERROR MESSAGE IS NOT EXPECTED");
        Log("EXPECTED ERROR MESSAGE IS DISPLAYED  AND THE ERROR MESSAGE IS = " + displayedErrorMessage);
        Console.WriteLine("EXPECTED ERROR MESSAGE IS DISPLAYED  AND THE ERROR MESSAGE IS = " + displayedErrorMessage);
    }


}
