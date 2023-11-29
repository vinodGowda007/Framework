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
public class ContactsPage : BasePage
{
    private readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver driver;
    public static string FirstName, LastName, Salutation, Title, ProducerClient, ContactRecordType, OrgVersion;
    public static string ContactRecordPath = SubmissionPage.BaseURL + "Contacts/Contact.txt";



    //FOR LOG FILE INPUT
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

    // Producers Objects
    readonly By tabContacts = By.XPath("(//a/span[contains(text(),'Contacts')])[1]");
    readonly By btnNewContacts = By.XPath("//a/div[@title='New']");
    readonly By lblRecordType = By.XPath("//div[@class='changeRecordTypeOptionRightColumn']/span");
    readonly By btnNext = By.XPath("//button/span[contains(text(),'Next')]");

    readonly By ddSalutation = By.XPath("//button[@name='salutation']");
    readonly By ddSalutationValue = By.XPath("//button[@name='salutation']/following::div[2]/lightning-base-combobox-item/span[2]/span");
    readonly By txtFirstName = By.XPath("//input[@name='firstName']");
    readonly By txtLastName = By.XPath("//input[@name='lastName']");
    readonly By ddProducr = By.XPath("(//label[contains(text(),'Producer')]/following::div)[2]//input");
    readonly By ddProducerValue = By.XPath("(((//label[contains(text(),'Producer')]/following::div)[2]//input/following::div)[2]//ul/li//span[@class='slds-media__body']/span)[1]");
    readonly By txtEmail = By.XPath("//input[@name='Email']");
    readonly By ddRole = By.XPath("//label[contains(text(),'Role')]/following::div[1]//button");
    readonly By ddRoleValue = By.XPath("//label[contains(text(),'Role')]/following::div//lightning-base-combobox-item/span[2]/span");
    readonly By btnSave = By.XPath("//button[@name='SaveEdit']");
    readonly By lblCreatedContact = By.XPath("//div/h1/slot[@name='primaryField']//span[@class='custom-truncate uiOutputText']");
    readonly By lblProducerClient = By.XPath("(//slot[@name='secondaryFields']/records-highlights-details-item)[5]/div/p[2]//a//span");
    readonly By btnMoreAction = By.XPath("(//ul[@class='slds-button-group-list']/li//button)[4]");
    readonly By btnActionNames = By.XPath("//div[@class='slds-dropdown slds-dropdown_right']//runtime_platform_actions-action-renderer//a/span");
    readonly By btnClearProducer = By.XPath("(//button[@title='Clear Selection'])[1]");
    readonly By btnClearClient = By.XPath("(//button[@title='Clear Selection'])[1]");
    readonly By txtUpdateProducer = By.XPath("//input[@title='Search Producers']");
    readonly By tabDetails = By.XPath("//a[@id='detailTab__item']");
    readonly By lblStarrUniquValue = By.XPath("//span[contains(text(),'Starr Unique Id')]/following::div[2]/span/slot/lightning-formatted-text");
    readonly By ddClientNameLookup = By.XPath("//label[contains(text(),'Client Name')]/following::div[1]//input");
    readonly By ddClientValue = By.XPath("(((//label[contains(text(),'Client Name')]/following::div)[2]//input/following::div)[2]//ul/li//span[@class='slds-media__body']/span)[1]");
    readonly By btnDeleteInPopup = By.XPath("//span[contains(text(),'Delete')]");
    readonly By lblErrorProducerContactmsg = By.XPath("//div[contains(text(),'Cannot delete because Producer is not null')]");
    readonly By lblErrorClientContactmsg = By.XPath("//div[contains(text(),'Cannot delete because Client Name is not null')]");

    

    // Un/Producer Tab values
    readonly By lblUWProducerTabFieldsName = By.XPath("//slot[@name='tabs']/flexipage-tab2[2]//div[@class='test-id__field-label-container slds-form-element__label']/span");
    readonly By lblUWProducerFieldsValue = By.XPath("//slot[@name='tabs']/flexipage-tab2[2]//div[@class='slds-form-element__control slds-grid itemBody']/span/span | //slot[@name='tabs']/flexipage-tab2[2]//div[@class='slds-form-element__control slds-grid itemBody']/span/a");

    public ContactsPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        driver = parallelConfig.Driver;
    }


    public void NavigateToContactObject()
    {
        driver.CaptureScreen(_scenarioContext);
        driver.WaitForElementToPresent(tabContacts);
        driver.JSClick(tabContacts);
        Log("CLICKED ON CONTACT OBJECTS");
    }

    public void NewContactThenUserClickedOnNewButtonInContactObject()
    {
        Assert.IsTrue(driver.WaitForElementToPresent(btnNewContacts), "COULD NOT NAVIGATE TO CONTACT OBJECT PAGE");
        Assert.IsTrue(driver.WaitAndClick(btnNewContacts), "COULD NOT CLICK ON NEW CONTACT BUTTON");
        Assert.IsTrue(driver.WaitForElementToPresent(btnNext), "COULD NOT NAVIGATE TO CONTACT RECORD TYPE PAGE");
        Log("CLICKED ON NEW BUTTON");

    }

    public void SelectRecordType(string RecordTypeValue)
    {
        driver.WaitForElementToPresent(lblRecordType);
        ContactRecordType = RecordTypeValue;
        System.Threading.Thread.Sleep(2000);
        Assert.IsTrue(driver.SelectFromList(lblRecordType, RecordTypeValue), "COULD NOT SELECT THE RECORD TYPE AS " + RecordTypeValue);
        Log("SELECTED RECORD TYPE  " + RecordTypeValue);
        driver.WaitAndClick(btnNext);
        Log("Selected Record Type " + RecordTypeValue + "Navigated to New Submission Page");
    }

    public string GenerateRandomNumbers()
    {
        Random random = new Random();
        return random.Next(99999).ToString();
    }

    public void FillValueInContactPage(string[] data, string version)
    {
        OrgVersion = version;
        string recordValue = GenerateRandomNumbers();
        Salutation = data[0];
        FirstName = data[1];
        LastName = recordValue;
        string Email = data[2] + recordValue + "@gmail.com";
        string Role = data[3];
        string Producer = data[4];

        driver.WaitAndClick(ddSalutation);
        SelectValueFromDropdown(ddSalutationValue, Salutation);

        if (FirstName != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtFirstName, FirstName), "LAST NAME WAS NOT INPUTED");

        if (LastName != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtLastName, LastName), "LAST NAME WAS NOT INPUTED");


        if (ContactRecordType.ToLower().Equals("producer contact"))
        {
           Assert.IsTrue(driver.ClearAndSend(ddProducr, Producer),"PRODUCER FIEDLS IS NOT PRESENT");
           Assert.IsTrue(driver.WaitAndClick(ddProducerValue),"PRODUCER LIST IS NOT DISPLAYED");
        }

        
        if (Email != "NA")
            Assert.IsTrue(driver.ClearAndSend(txtEmail, Email), "EMAIL WAS NOT INPUTED");

        if (ContactRecordType.ToLower().Equals("client contact"))
        {
            if(OrgVersion.Equals("1.0"))
            {
                Assert.IsTrue(driver.ClearAndSend(ddClientNameLookup, ReadDataFromFile(ClientsPage.Clients1_0FilePath + "Client_Create.txt")),"COULD NOT INPUT VALUE IN CLIENT FIELD");
            }
            if (OrgVersion.Equals("2.0"))
            {
                Assert.IsTrue(driver.ClearAndSend(ddClientNameLookup, ReadDataFromFile(ClientsPage.Clients2_0FilePath + "Client_Create.txt")),"COULD NOT INPUT VALUE IN CLIENT FIELD");
            }
            driver.WaitAndClick(ddClientValue);
        }

        SelectRandomValueFromDropdown(ddRole, ddRoleValue, "Role");
        
        //driver.JSClick(ddRole);
        //SelectValueFromDropdown(ddRoleValue, Role);
        Log("VALUES ARE INPUTED IN CONTACT CREATION PAGE");
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

    
    public Boolean SelectRandomValueFromDropdown(By ddField, By ListElements, string FieldName)
    {
        driver.JSClick(ddField);
        System.Threading.Thread.Sleep(1000);
        IList<IWebElement> elements = driver.ListOfElements(ListElements);
        Random num = new Random();
        int IndexNo = num.Next(elements.Count);
        if (IndexNo == 0)
        {
            IndexNo++;
        }
        for (int i = 0; i <= elements.Count; i++)
        {
            if (i == IndexNo)
            {
                //System.Threading.Thread.Sleep(3000);
                driver.WaitAndClick(elements[i]);
                Log(FieldName + " Is inputed with value " + elements[i].ToString());
                return true;
            }
        }
        Log(FieldName + " Could not Inputed");
        return false;
    }

    public void SaveAndVerifyContactRecord()
    {
        driver.CaptureScreen(_scenarioContext);
        string createdContactName = Salutation + " " + FirstName + " " + LastName;
        string saveContactName = FirstName + " " + LastName;
        driver.WaitForElementToPresent(btnSave);
        Assert.IsTrue(driver.WaitAndClick(btnSave), "COULD NOT CLICK ON SAVE BUTTON");
        driver.WaitForElementToPresent(lblCreatedContact);
        Assert.IsTrue(driver.GetTextFromElement(lblCreatedContact).Equals(createdContactName), " COULD NOT CREATE CONTACT RECORD");

        Log("CONTACT RECORD IS CREATED SUCCESSFULLY");
        if (ContactRecordType.ToLower().Equals("producer contact"))
        {
            File.WriteAllText(ContactRecordPath, "Producer Contact Name = " + saveContactName);
        }
            
    }

    public void ThenUserStoreTheClientValueWhichIsRelatedToProducer()
    {
        
        try
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblProducerClient));
            ProducerClient = driver.GetTextFromElement(lblProducerClient);
            Log("PRODUCER CLIENT NAME IS CAPTURED AND THE CLIENT NAME IS " + ProducerClient);
        }
        catch (Exception e)
        {
            Log("PRODUCER CLIENT NAME IS NOT DISPLAYED");
            Assert.Fail("PRODUCER CLIENT NAME IS NOT DISPLAYED");
        }        
    }

    public void ThenUserEditTheContactRecordAndUpdateTheProducerField(string[] ClientData, string ContactType)
    {
        string Producer = ClientData[0];
        string Client = ClientData[1];


        if (ContactType.ToLower().Equals("producer"))
        {
            driver.WaitForElementToPresent(btnClearProducer);
            driver.WaitAndClick(btnClearProducer);
            driver.ClearAndSend(ddProducr, Producer);
            Assert.IsTrue(driver.WaitAndClick(ddProducerValue), "COULD NOT SELECT PRODUCER VALUE FROM THE LIST");
            Log("SUCCESSFULLY UPDATED PRODUCER ");
        }

        if (ContactType.ToLower().Equals("client"))
        {
            driver.WaitForElementToPresent(btnClearClient);
            driver.WaitAndClick(btnClearClient);
            driver.ClearAndSend(ddClientNameLookup, Client);
            Assert.IsTrue(driver.WaitAndClick(ddClientValue), "COULD NOT SELECT CLIENT VALUE FROM THE LIST");
            Log("SUCCESSFULLY UPDATED CLIENT ");
        }
        
    }

    public void ThenUserVerifyClientInformationIsUpdatedSuccessfully(string[] ClientData, String Type)
    {
        string Producer1 = ClientData[0];
        string Client1 = ClientData[1];

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lblProducerClient));

        //if (OrgVersion.Equals("1.0"))
        //{
            if (Type.Equals("Client"))
            {
                Assert.AreEqual(driver.GetTextFromElement(lblProducerClient), Client1, "CLIENT VALUES IS NOT UPDATED SUCCESSFULLY");
            }
            if (Type.Equals("Producer"))
            {
                Assert.AreEqual(driver.GetTextFromElement(lblProducerClient), Producer1, "CLIENT VALUES IS NOT UPDATED SUCCESSFULLY");
            }
        //}

        //if (OrgVersion.Equals("2.0"))
        //{
        //    if (Type.Equals("Client"))
        //    {
        //        Assert.AreEqual(driver.GetTextFromElement(lblProducerClient), Client1, "CLIENT VALUES IS NOT UPDATED SUCCESSFULLY");
        //    }
        //    if (Type.Equals("Producer"))
        //    {
        //        Assert.AreEqual(driver.GetTextFromElement(lblProducerClient), Producer1, "CLIENT VALUES IS NOT UPDATED SUCCESSFULLY");
        //    }
        //}
        Log("CLIENT VALUES ARE UPDATED SUCCESSFULY");
    }



    public void ThenUserClickedOnSaveButtonDuringContactUpdate()
    {
        //string createdContactName = Salutation + " " + FirstName + " " + LastName;
        string saveContactName = FirstName + " " + LastName;
        driver.WaitForElementToPresent(btnSave);
        Assert.IsTrue(driver.WaitAndClick(btnSave), "COULD NOT CLICK ON SAVE BUTTON");
        Log("CLICKED ON SAVE BUTTON");
        if (ContactRecordType.ToLower().Equals("producer contact"))
        {
            File.WriteAllText(ContactRecordPath, "Producer Contact Name = " + saveContactName);
        }
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

    public void ThenUserNavigatedToDetailsTab()
    {
        driver.WaitForElementToPresent(tabDetails);
        Assert.IsTrue(driver.WaitAndClick(tabDetails),"COULD NOT CLICK ON DETAILS TAB");
        Log("CLICKED ON DETAILS TAB");
    }

    public void ThenUserVerifiedUniqueIDIsDisplayedInTheScreen()
    {
        driver.ScrollByPixel(700);
        driver.WaitForElementToPresent(lblStarrUniquValue);
        driver.ScrollToCenter(lblStarrUniquValue);
        String StarrUniqueId = driver.GetTextFromElement(lblStarrUniquValue);
        Assert.IsFalse(string.IsNullOrEmpty(StarrUniqueId),"Starr unique id is null");
        Log("STARR UNIQUE ID IS PRESENT AND THE VALUE IS = " + StarrUniqueId);
        Console.WriteLine("STARR UNIQUE ID IS PRESENT AND THE VALUE IS = " + StarrUniqueId);
    }

    public void ThenUserVerifyContactDeletionStatus()
    {
        driver.WaitForElementToPresent(btnDeleteInPopup);
        driver.CaptureScreen(_scenarioContext);
        Assert.IsTrue(driver.WaitAndClick(btnDeleteInPopup),"COULD NOT CLICK ON THE DELETE BUTOTN IN THE POPUP");
        Log("CLICKED ON DELETE BUTTON");

        if (ContactRecordType.ToLower().Equals("producer contact"))
        {
            Assert.IsTrue(driver.WaitForElementToPresent(lblErrorProducerContactmsg), "EXPECTED ERROR MESSAGE IS NOT DISPLAYED");
        }
        if (ContactRecordType.ToLower().Equals("client contact"))
        {
            Assert.IsTrue(driver.WaitForElementToPresent(lblErrorClientContactmsg), "EXPECTED ERROR MESSAGE IS NOT DISPLAYED");
        }    
        
        Log("EXPECTED ERROR MESSAGE IS DISPLAYED");
    }


}