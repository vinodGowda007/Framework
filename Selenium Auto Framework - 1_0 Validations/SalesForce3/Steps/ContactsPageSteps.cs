using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class ContactsPageSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSProdPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSProdPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly ContactsPage contactpage;
    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public ContactsPageSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;
        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);
        _dtColSProdPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Contact Data", dataCollection);
        contactpage = new(parallelConfig, loggingStep, scenairoContext);
    }

    [Then(@"User navigated to Contacts Object")]
    public void ThenUserNavigatedToContactsObject()
    {
        contactpage.NavigateToContactObject();
    }

    [Then(@"User clicked on New button in contact object")]
    public void ThenUserClickedOnNewButtonInContactObject()
    {
        contactpage.NewContactThenUserClickedOnNewButtonInContactObject();
    }

    [Then(@"User selected the contact record type as ""([^""]*)""")]
    public void ThenUserSelectedTheContactRecordTypeAs(string ContactRecordType)
    {
        string RecordType = ExcelValue(ContactRecordType);
        contactpage.SelectRecordType(RecordType);
    }


    //[Then(@"User Filled the details in Contacts creation page")]
    //public void ThenUserFilledTheDetailsInContactsCreationPage()
    //{
    //    string[] Data = {
    //         ExcelValue("Salutation"),
    //         ExcelValue("First Name"),
    //         ExcelValue("Email"),
    //         ExcelValue("Role"),
    //         ExcelValue("Producer")
    //        };
    //    contactpage.FillValueInContactPage(Data);
    //}

    [Then(@"User Filled the details in Contacts creation page in '([^']*)'")]
    public void ThenUserFilledTheDetailsInContactsCreationPageIn(string Version)
    {
        string[] Data = {
             ExcelValue("Salutation"),
             ExcelValue("First Name"),
             ExcelValue("Email"),
             ExcelValue("Role"),
             ExcelValue("Producer")
            };
        contactpage.FillValueInContactPage(Data ,Version);
    }



    [Then(@"User clicked on Save button and verify contact record is created")]
    public void ThenUserClickedOnSaveButtonAndVerifyContactRecordIsCreated()
    {
        contactpage.SaveAndVerifyContactRecord();
    }

    [Then(@"User Navigated to Details tab")]
    public void ThenUserNavigatedToDetailsTab()
    {
        contactpage.ThenUserNavigatedToDetailsTab();
    }

    [Then(@"User verified Unique ID is displayed in the screen")]
    public void ThenUserVerifiedUniqueIDIsDisplayedInTheScreen()
    {
        contactpage.ThenUserVerifiedUniqueIDIsDisplayedInTheScreen();
    }


    [Then(@"User Store the Client Value which is related to Producer")]
    public void ThenUserStoreTheClientValueWhichIsRelatedToProducer()
    {
        contactpage.ThenUserStoreTheClientValueWhichIsRelatedToProducer();
    }

    //[Then(@"User Edit the Contact record and Update the Producer Field")]
    //public void ThenUserEditTheContactRecordAndUpdateTheProducerField()
    //{

    //}

    [Then(@"User Edit the Contact record and Update the '([^']*)' Field")]
    public void ThenUserEditTheContactRecordAndUpdateTheField(string ContactType)
    {
        string[] data = {
            ExcelValue("Update Producer"),
            ExcelValue("Update Client")
        };
        contactpage.ThenUserEditTheContactRecordAndUpdateTheProducerField(data, ContactType);
    }

    [Then(@"User clicked on Save button during contact update")]
    public void ThenUserClickedOnSaveButtonDuringContactUpdate()
    {
        contactpage.ThenUserClickedOnSaveButtonDuringContactUpdate();
    }


    [Then(@"User verify '([^']*)' information is updated successfully")]
    public void ThenUserVerifyInformationIsUpdatedSuccessfully(string Type)
    {
        string[] data = {
            ExcelValue("Update Producer"),
            ExcelValue("Update Client")
        };
        contactpage.ThenUserVerifyClientInformationIsUpdatedSuccessfully(data,Type );
    }


    [Then(@"User Verify '([^']*)' contact deletion status")]
    public void ThenUserVerifyContactDeletionStatus(string client)
    {
        contactpage.ThenUserVerifyContactDeletionStatus();
    }











}
