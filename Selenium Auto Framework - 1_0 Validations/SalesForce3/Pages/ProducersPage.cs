using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SalesForce3.Pages;
public class ProducersPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    private string LastName;
    private string Title;

    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

    // Producers Objects
    readonly By tabProducers = By.XPath("(//a/span[contains(text(),'Producers')])[1]");
    readonly By pageProducers = By.XPath("//lst-breadcrumbs//span[contains(text(),'Producers')]");
    readonly By lnkFirstRecord = By.XPath("(//tr/th/span/a)[1]");
    readonly By tabRelated = By.XPath("//a[@id='relatedListsTab__item']");
    readonly By tabSummary = By.XPath("//a[@id='customTab__item']");
    readonly By btnNew = By.XPath("(//ul/li/a[@title='New'])[2]");
    readonly By btnNewProcedure = By.XPath("(//ul/li/a[@title='New'])[1]");
    readonly By rbProducerContact = By.XPath("(//div[@class='changeRecordTypeOptionLeftColumn']/input)[1]");
    readonly By btnNext = By.XPath("//button/span[contains(text(),'Next')]");
    readonly By lblNewContactPage = By.XPath("//div/h2[contains(text(),'New Contact: Producer Contact')]");

    // Contacts page
    readonly By lblSalutation = By.XPath("//label[contains(text(),'Salutation')]");
    readonly By txtLastName = By.XPath("//div/input[@name='lastName']");
    readonly By txtEmail = By.XPath("//div/input[@name='Email']");
    readonly By ddValue = By.XPath("//div/lightning-base-combobox-item/span/span");
    readonly By lblRole = By.XPath("//label[contains(text(),'Role')]");
    readonly By lnkViewAllContact = By.XPath("(//a/div/span[contains(text(),'View All')])[1]");
    readonly By lnkViewAll = By.XPath("(//a/div/span[contains(text(),'View All')])[2]");

    readonly By allRecords = By.XPath("//table[@aria-label='Contacts']/tbody/tr/th/span/a");
    readonly By btnAllActionsList = By.XPath("//div[@class='slds-dropdown slds-dropdown_right']/div[@role='menu']//runtime_platform_actions-action-renderer//a/span");
    readonly By btnShowMoreActions = By.XPath("(//li/lightning-button-menu/button)[2]");
    readonly By lblEditPage = By.XPath("//div[@class=\"slds-modal__header\"]/h2[starts-with(text(),'Edit')]");

    // Producer Page
    readonly By txtProducerName = By.XPath("//div/input[@name='Name']");
    readonly By txtProducerOneId = By.XPath("//div/input[@name='ProducerOne_ID__c']");
    readonly By txtTitle = By.XPath("//input[@name='Title']");
    readonly By lblTitleText = By.XPath("(//div[@class='secondaryFields']/slot/records-highlights-details-item)[2]//lightning-formatted-text");
    readonly By btnEdit = By.XPath("//button[@name='Edit']");
    public ProducersPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
    }

    public static int GenerateRandomNumbers()
    {
        Random random = new Random();
        return random.Next();
    }
    public void NavigateToProducerObject()
    {
        driver.JSClick(tabProducers);
        Assert.IsTrue(driver.WaitForElementToPresent(pageProducers), "Could not navigate to Producer's page");
        Log("Navigated to Producers Object");
        ;
    }

    public void VerifyEditButtonIsNotAvailable()
    {
        Assert.IsFalse(driver.IsDisplayed(btnEdit), "Edit button is exist");
        Log("User don't have Edit button/ Edit permision to edit the Producer record");
    }
    public void OpenExistingRecord()
    {
        driver.WaitAndClick(lnkFirstRecord);
        Assert.IsTrue(driver.WaitForElementToPresent(tabRelated), "Existing Producer's record page is not displayed");
        Log("Existing Producer's record page is displayed");
    }

    public void NavigateToRelatedList()
    {
        driver.WaitAndClick(tabRelated);
        Assert.IsTrue(driver.WaitForElementToPresent(lnkViewAllContact), "Related list page is not displayed");
        Log("Clicked on Related Tab and Naviaged to Related List page");
    }

    public void NavigateToCreateProducerContactPage()
    {
        driver.WaitAndClick(btnNew);
        Assert.IsTrue(driver.WaitForElementToPresent(rbProducerContact), "New Contact Tab is not displayed");
        Log("New Contact Pop Up is displayed");
        //driver.WaitAndClick(rbProducerContact);
        //Log("Selected Producer Contact Radio button");
        driver.WaitAndClick(btnNext);
        Assert.IsTrue(driver.WaitForElementToPresent(lblNewContactPage), "New contact creationpage is not displayed");
        Log("New Contact creation page is displayed");
    }

    public void CreateNewProcedure(string[] ProducerData)
    {
        driver.WaitForNextPage();
        driver.WaitAndClick(btnNewProcedure);
        Assert.IsTrue(driver.WaitForElementToPresent(txtProducerName));
        string ProducerName = ProducerData[0] + GenerateRandomNumbers();
        string ProducerOneId = ProducerData[1] + GenerateRandomNumbers();

        if (ProducerName != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtProducerName, ProducerName), "Producer Name Was NOT Inputed");
        if (ProducerOneId != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtProducerOneId, ProducerOneId), "Producer One Id Was NOT Inputed");
        Log("Filled details in New Producer creation page");
    }

    public void EditTitle(string[] ProducerUpdateData)
    {
        Title = ProducerUpdateData[0] + GenerateRandomNumbers();

        if (Title != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtTitle, Title), "Title Was NOT Inputed");
        Log("Title was inputted");
    }
    //public void FillValueInContactPage(string[] data)
    //{
    //    int recordValue = GenerateRandomNumbers();
    //    string Salutation = data[0] ;
    //    LastName = data[1] + recordValue;
    //    string Email = data[2] + recordValue + "@gmail.com";
    //    string Role = data[3];

    //    driver.WaitAndClick(lblSalutation);
    //    SelectValueFromDropdown(ddValue, Salutation);

    //    if (LastName != "NA")
    //        Assert.IsTrue(driver.ClearAndSend(txtLastName, LastName), "LastName Was NOT Inputed");

    //    if (Email != "NA")
    //        Assert.IsTrue(driver.ClearAndSend(txtEmail, Email), "Email Was NOT Inputed");

    //    driver.WaitAndClick(lblRole);
    //    SelectValueFromDropdown(ddValue, Role);
    //    Log("Values are inputed in Contact page");
    //}

    public void VerifyContactAssociation()
    {
        driver.WaitForElementToPresent(lnkViewAll);
        driver.WaitForElementToPresent(lnkViewAllContact);
        driver.JSClick(lnkViewAllContact);
        Assert.IsTrue(VerifyValueFromList(allRecords, LastName), "Could not associate Contacts");
        Log("Contact is Associated");
    }

    public void ClickOnContactRecord()
    {
        SelectValueFromDropdown(allRecords, LastName);
        Assert.IsTrue(driver.WaitForElementToPresent(btnShowMoreActions), "Could not click on Contact record");
        Log("Clicked on Contact record");
    }

    public void ClickOnEditButton()
    {
        driver.WaitAndClick(btnShowMoreActions);
        SelectValueFromDropdown(btnAllActionsList, "Edit");
        Assert.IsTrue(driver.WaitForElementToPresent(lblEditPage), "Could not clicked on Edit");
        Log("Clicked on Edit button and Edit Producer page is displayed");
    }

    public void VerifyUpdatedData()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(lblTitleText), "Contact details page is not displayed");
        Assert.AreEqual(Title.ToString(), driver.GetTextFromElement(lblTitleText).ToString(), "Data is updated in the Producer page");
        Log("Data is updated successfully");
    }

    public Boolean VerifyValueFromList(By Elements, String Value)
    {
        IList<IWebElement> elements = driver.ListOfElements(Elements);
        foreach (IWebElement element in elements)
        {
            if (element.GetElementText().ToString() == Value.ToString())
            {
                return true;
            }
        }
        return false;
    }

    public void SelectValueFromDropdown(By Elements, String Value)
    {
        IList<IWebElement> elements = driver.ListOfElements(Elements);
        foreach (IWebElement element in elements)
        {
            if (element.GetElementText() == Value.ToString())
            {
                driver.WaitAndClick(element);
                break;
            }
        }
    }

}