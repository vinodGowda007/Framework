using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class ClientPageSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSfaPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSfaPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    // private readonly SF_CloneSubmissionPage SF;
    private readonly ClientsPage clientsPage;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public ClientPageSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;

        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);

        _dtColSfaPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Client Data", dataCollection);

        clientsPage = new(parallelConfig, loggingStep, scenairoContext);
    }

    [When(@"User Navigated to Clients page")]
    public void WhenUserNavigatedToClientsPage()
    {
        clientsPage.NavigateToClientsPage();
    }

    [Then(@"User clicked on New button")]
    public void ThenUserClickedOnNewButton()
    {
        clientsPage.OpenNewClientDetailsPage();
    }

    [Then(@"User Fill the data in Clients creation page using '([^']*)'")]
    public void WhenUserFillTheDataInClientsCreationPageUsing(string SubmissionVersion)
    {
        string[] Data = {
         ExcelValue("Client Name"),
         ExcelValue("Billing Street"),
         ExcelValue("Billing City"),
         ExcelValue("Billing Province"),
         ExcelValue("Billing Postal Code"),
         ExcelValue("Billing Country"),
         ExcelValue("FEINTaxId"),
         ExcelValue("Scenario Count"),
         ExcelValue("Client Type")
        };
        clientsPage.FillDetailsInClientsCreationPage(Data, SubmissionVersion);
    }


    [Then(@"user verify the error message for client as ""([^""]*)""")]
    public void ThenUserVerifyTheErrorMessageForClientAs(string ErrorMessage)
    {
        clientsPage.ThenUserVerifyTheErrorMessageForClientAs(ErrorMessage);
    }

    [Then(@"user verify the error message for client on Page Level ""([^""]*)""")]
    public void ThenUserVerifyTheErrorMessageForClientOnPageLevel(string ErrorMessage)
    {
        clientsPage.ThenUserVerifyTheErrorMessageForClientInPageLevel(ErrorMessage);
    }

    [Then(@"User clicked on Save button")]
    public void ThenUserClickedOnSaveButton()
    {
        clientsPage.ClickOnSaveButton();
    }

    [Then(@"User clicked on Save button to verify Error Message")]
    public void ThenUserClickedOnSaveButtonToVerifyErrorMessage()
    {
        clientsPage.ClickedOnSaveButtonToVerifyErrorMessage();
    }

  
    [Then(@"User added the email id as ""([^""]*)""")]
    public void ThenUserAddedTheEmailIdAs(string email)
    {
        clientsPage.ThenUserAddedTheEmailIdAs(email);
    }



    [Then(@"User Fill the value in Chile Manual Policy Number log")]
    public void ThenUserFillTheValueInChileManualPolicyNumberLog()
    {
        clientsPage.ThenUserFillTheValueInChileManualPolicyNumberLog();
    }



    [Then(@"User Verified Duplicate Error Message")]
    public void ThenUserVerifiedDuplicateErrorMessage()
    {
        clientsPage.ThenUserVerifiedDuplicateErrorMessage();
    }


    [Then(@"New Client record is created successfully")]
    public void ThenNewClientRecordIsCreatedSuccessfully()
    {
        clientsPage.VerifyCreatedClient();
    }


    [When(@"User select a record and clicked on edit button")]
    public void WhenUserSelectARecordAndClickedOnEditButton()
    {
        clientsPage.ClickAndEditFirstClientRecord();
    }

    [Then(@"Select Client Type as ""([^""]*)""")]
    public void ThenSelectClientTypeAs(string clientType)
    {
        clientsPage.SelectClientType(clientType);
    }

    [Then(@"Error message should be displayed for FEIN field")]
    public void ThenErrorMessageShouldBeDisplayedForFEINField()
    {
        clientsPage.VerifyFEINErrorMessage();
    }

    [When(@"User clear the values in FEIN / Tax ID field and Clicked on Save button")]
    public void WhenUserClearTheValuesInFEINTaxIDFieldAndClickedOnSaveButton()
    {
        clientsPage.ClearValueFromTextbox();
        clientsPage.ClickOnSaveButton();
    }


    [Then(@"User will update the fields in the client detail page")]
    public void ThenUserWillUpdateTheFieldsInTheClientDetailPage()
    {
        string[] DataUpdate = {
         ExcelValue("FEINTaxId")
        };
        clientsPage.UpdateClientsDetails(DataUpdate);

    }

    [Then(@"Client record Updated successfully\.")]
    public void ThenClientRecordUpdatedSuccessfully_()
    {
        clientsPage.VerifyUpdatedClientRecord();
    }


    [Then(@"Verify the Error messages")]
    public void ThenVerifyTheErrorMessages()
    {
        clientsPage.VerifyErrorMessage();
    }

    [Then(@"User Fill the data in Clients creation page Except Mailing State and Mailing Province")]
    public void ThenUserFillTheDataInClientsCreationPageExceptMailingStateAndMailingProvince()
    {
        string[] Data = {
         ExcelValue("Client Name"),
         ExcelValue("Mailing Street"),
         ExcelValue("Billing Street"),
         ExcelValue("Billing City"),
         ExcelValue("Billing Province"),
         ExcelValue("Billing Postal Code"),
         ExcelValue("Billing Country"),
         ExcelValue("Mailing City"),
         ExcelValue("Mailing Postal Code"),
        };
        clientsPage.EnterClientsDetailsToVerifyErrorMsg(Data);
    }

    [Then(@"User Select a Client record")]
    public void ThenUserSelectAClientRecord()
    {
        clientsPage.ClickOnFirstClientRecord();
    }

    [Then(@"Clicked on Address Validation button")]
    public void ThenClickedOnAddressValidationButton()
    {
        clientsPage.ClickOnAddressValidationButton();
    }

    [Then(@"User is navigated to Address validation page")]
    public void ThenUserIsNavigatedToAddressValidationPage()
    {
        clientsPage.ValidateAddressValidationPage();
    }

    [Then(@"User clicked on Edit button")]
    public void ThenUserClickedOnEditButton()
    {
        clientsPage.ClickOnEditButton();
    }


    [Then(@"User search for the client record")]
    public void ThenUserSearchForTheClientRecord()
    {
        clientsPage.SearchForClientRecord();
    }

    [Then(@"User clicked on credit Report")]
    public void ThenUserClickedOnCreditReport()
    {
        clientsPage.ClickOnCreditReport();
    }

    [Then(@"User verify the credit report page")]
    public void ThenUserVerifyTheCreditReportPage()
    {
        clientsPage.VerifyCreditReportPage();
    }

    [Then(@"User verify Source field is not editable")]
    public void ThenUserVerifySourceFieldIsNotEditable()
    {
        clientsPage.ThenUserVerifySourceFieldIsNotEditable();
    }

    [Then(@"User selected all the Checkboxes")]
    public void ThenUserSelectedAllTheCheckboxes()
    {
        clientsPage.ThenUserSelectedAllTheCheckboxes();
    }

    [Then(@"User Clicked on Update button")]
    public void ThenUserClickedOnUpdateButton()
    {
        clientsPage.ThenUserClickedOnUpdateButton();
    }

    [Then(@"User Clicked on Validate Address Button")]
    public void ThenUserClickedOnValidateAddressButton()
    {
        clientsPage.ThenUserClickedOnValidateAddressButton();
    }

    [Then(@"User verify Address Validate Updated Date")]
    public void ThenUserVerifyAddressValidateUpdatedDate()
    {
        clientsPage.ThenUserVerifyAddressValidateUpdatedDate();
    }



    [Then(@"User Check for the Source value in Profile page")]
    public void ThenUserCheckForTheSourceValueInProfilePage()
    {
        clientsPage.ThenUserCheckForTheSourceValueInProfilePage();
    }

    [Then(@"User verify the source value for '([^']*)'")]
    public void ThenUserVerifyTheSourceValueFor(string SFSource)
    {
        clientsPage.ThenUserVerifyTheSourceValueFor(SFSource);
    }

    // For Treat Table using Client page
    [Then(@"User Clicked on New button in Treaty Year Tables")]
    public void ThenUserClickedOnNewButtonInTreatyYearTables()
    {
        clientsPage.ThenUserClickedOnNewButtonInTreatyYearTables();
    }

    [Then(@"User search for ""([^""]*)"" and Navigate to ""([^""]*)""")]
    public void ThenUserSearchForAndNavigateTo(string searchValue, string sisplayedValue)
    {
        clientsPage.ThenUserSearchForAndNavigateTo(searchValue, sisplayedValue);
    }


    [Then(@"user fill the values in treaty table fields")]
    public void ThenUserFillTheValuesInTreatyTableFields()
    {
        clientsPage.ThenUserFillTheValuesInTreatyTableFields();
    }

    [Then(@"user clicked on save button in treat table fields")]
    public void ThenUserClickedOnSaveButtonInTreatTableFields()
    {
        clientsPage.ThenUserClickedOnSaveButtonInTreatTableFields();
    }

    [Then(@"user verify the error message in treaty table as ""([^""]*)""")]
    public void ThenUserVerifyTheErrorMessageInTreatyTableAs(string ErrorMsg)
    {
        clientsPage.ThenUserVerifyTheErrorMessageInTreatyTableAs(ErrorMsg);
    }

    [Then(@"user fill remainng fields in Chile Manual Policy Number log form")]
    public void ThenUserFillRemainngFieldsInChileManualPolicyNumberLogForm()
    {
        clientsPage.ThenUserFillRemainngFieldsInChileManualPolicyNumberLogForm();
    }

    [Then(@"user updated the field ""([^""]*)"" in Chile Manual Policy record")]
    public void ThenUserUpdatedTheFieldInChileManualPolicyRecord(string insured)
    {
        clientsPage.ThenUserUpdatedTheFieldInChileManualPolicyRecord();
    }




}
