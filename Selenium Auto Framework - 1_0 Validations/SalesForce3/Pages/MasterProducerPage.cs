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
public class MasterProducerPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    public static string BaseURL = "C:/SF_Automation/SeleniumAutoFramework/SalesForce3/RecordInfo/";
    public static string ContactFilePath = BaseURL + "Contacts/Contact.txt";

    readonly By txtSearchProducerName = By.XPath("//input[@name='searchBox']");
    readonly By cbUltimateProducer = By.XPath("(//input[@name='searchUltimateProducer']/following::label/span)[1]");
    readonly By btnSearch = By.XPath("//button[@title='Search']");
    readonly By listProducerName = By.XPath("//forcegenerated-flexipageregion_starr_master_producer_lookup_right__js//article//a/div");
    readonly By listProducerlink = By.XPath("//forcegenerated-flexipageregion_starr_master_producer_lookup_right__js//article//a");
    readonly By lblProducerDetailsPage = By.XPath("//h1/slot/lightning-formatted-text[@slot='primaryField']");
    readonly By listProducerTab = By.XPath("//ul[@class='slds-tabs_default__nav']/li/a");
    readonly By lblRelatedContact = By.XPath("//h2/a/span[contains(text(),'Contacts')]");
    readonly By lblRelatedSubmission = By.XPath("//h2/a/span[contains(text(),'Submissions')]");
    readonly By btnNewContacts = By.XPath("(//div[contains(text(),'New')])[1]");
    readonly By btnNewSubmission = By.XPath("(//div[contains(text(),'New')])[2]");

    readonly By listChildContacts = By.XPath("//table[@aria-label='Contacts']/tbody/tr/th//a");
    readonly By listChildSubmission = By.XPath("//table[@aria-label='Submissions']/tbody/tr/th//a");

    readonly By pageViewAllContacts = By.XPath("//span[@title='Name']");
    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);


    public MasterProducerPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
    }

    public void ThenUserSearchForProducerName(string ProducerName)
    {
        driver.WaitForElementToPresent(txtSearchProducerName);
        driver.ClearAndSend(txtSearchProducerName, "Automation 2024");
        driver.WaitAndClick(cbUltimateProducer);
        driver.JSClick(btnSearch);
        Log("CLICKED ON SEARCH BUTTON");
    }

    public void ThenUserSelectSelectedProducerName(string ProducerName)
    {
        driver.MoveToTheElementAndClick(listProducerlink);
        //Assert.IsTrue(ClickOnValueUsingFieldName("Automation 2024", listProducerName, listProducerlink), "COULD NOT SELECT THE PRODUCER INFORMATION"); 
        Log("CLICKED ON PRODUCER INFORMAIOTN");
    }

    public void ThenUserNavigatedToProducerDetailsPage()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(lblProducerDetailsPage), "COULD NOT NAVIGATE TO PRODUCER PAGE");
        Log("NAVIGATE TO PRODUCER PAGE");
    }

    public void ThenUserNavigatedToRelatedTab()
    {
        Assert.IsTrue(driver.SelectFromList(listProducerTab, "Related"), "COULD NOT SELECT THE REQUIRED TAB");
        Log("SELECTED RELATED TAB");
        //driver.WaitForElementToPresent(lblRelatedContact);
        //driver.ScrollToCenter(lblRelatedContact);
    }

    public void ThenUserClickedOnNewContactButton()
    {
        Assert.IsTrue(driver.WaitAndClick(btnNewContacts), "COULD NOT CLICK ON NEW CONTACTS BUTTON");
        Log("CLICKED ON NEW CONTACTS BUTTON");
    }

    public void ThenUserVerifyCreatedContactIsVisibleInTheContactRelatedList()
    {

        string Cratecontact = ReadDataFromFile(ContactFilePath);
        Console.WriteLine("I am searching for the contact - " + Cratecontact);
        driver.WaitForElementToPresent(lblRelatedContact);
        driver.WaitAndClick(lblRelatedContact);
        System.Threading.Thread.Sleep(2000);
        driver.Refresh();
        driver.WaitForElementToPresent(pageViewAllContacts);
        Assert.IsTrue(VerifyValueFromList(listChildContacts, Cratecontact.Trim()), "CREATED CONTACT IS NOT VISIBLE IN THE LIST");
        Log("CREATED CONTACT IS DISPLAYED IN THE LIST");
    }

    public void ThenUserClickedOnNewButtonInSubmissionSection()
    {
        System.Threading.Thread.Sleep(4000);
        driver.WaitForElementToPresent(btnNewSubmission);
        driver.ScrollToCenter(btnNewSubmission);
        Assert.IsTrue(driver.WaitAndClick(btnNewSubmission), "COULD NOT CLICK ON NEW SUBMISSION BUTTON");
        Log("CLICKED ON NEW SUBMISSION BUTTON");
    }

    public void ThenUserVerifyCreaedSubmissionIsVisibleInTheSubmissionRelatedList()
    {
        string CratedSubmission = null;
        driver.WaitForElementToPresent(lblRelatedSubmission);
        driver.WaitAndClick(lblRelatedSubmission);
        driver.WaitForElementToPresent(lblRelatedSubmission);
        Assert.IsTrue(VerifyValueFromList(listChildSubmission, CratedSubmission.Trim()), "CREATED SUBMISSION IS NOT VISIBLE IN THE LIST");
        Log("CREATED SUBMISSION IS DISPLAYED IN THE LIST");

    }

    public Boolean VerifyValueFromList(By Elements, String Value)
    {
        IList<IWebElement> elements = driver.ListOfElements(Elements);
        Console.WriteLine(elements.Count);
        foreach (IWebElement element in elements)
        {
            Console.WriteLine(element.GetElementText());
            if (element.GetElementText().ToString() == Value.ToString())
            {
                return true;
            }
        }
        return false;
    }

    public bool ClickOnValueUsingFieldName(string FieldName, By ListFieldName, By ListFields)
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
                FieldsList[FieldCount].Click();
                return true;
            }
        }
        Console.WriteLine("COULD NOT FIND THE FIELD " + FieldName);
        Log("COULD NOT FIND THE FIELD " + FieldName);
        return false;
    }
    public static string ReadDataFromFile(string Filepath)
    {
        string[] LInes = File.ReadAllLines(Filepath);
        string[] value;
        string returnValue;
        foreach (string str in LInes)
        {
            value = str.Split("=");
            Console.WriteLine("Value is = " + value);
            Console.WriteLine("Value index count = " + value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                Console.WriteLine("out put of " + i + " = ", value[i]);
            }
            returnValue = value[1];
            return returnValue;
        }
        Console.WriteLine("Could not fetch Value from the file");
        return null;
    }
}