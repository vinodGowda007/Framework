using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class AssumedInsurerSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSfaiPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSfaiPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly AssumedInsurerPage assumed;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    readonly FeatureContext _featureContext;

    public AssumedInsurerSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;

        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);

        _dtColSfaiPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Insurer Data", dataCollection);

        assumed = new(parallelConfig, loggingStep, scenairoContext, featureContext);

        _featureContext = featureContext;
    }


    [When(@"User Navigated to ""([^""]*)""")]
    public void WhenUserNavigatedTo(string AppName)
    {
        assumed.NavigateToAssumedInsurer(AppName);
    }

    [Then(@"User Refresh the page")]
    public void ThenUserRefreshThePage()
    {
        assumed.PageRefresh();
    }


    [Then(@"User Fill the value in Name field")]
    public void ThenUserFillTheValueInNameField()
    {
        string Data = ExcelValue("Insurer Name");
        assumed.InputInsurerName(Data);
    }

    [Then(@"Verify the Error messages for ""([^""]*)""")]
    public void ThenVerifyTheErrorMessagesFor(string ErrorMsg)
    {
        assumed.VerifyErrorMessages(ErrorMsg);
    }

    [Then(@"User select the Active Checkbox")]
    public void ThenUserSelectTheActiveCheckbox()
    {
        assumed.SelectActiveCheckbox();
    }

    [Then(@"User Fill values in Assumed Insurer creation page")]
    public void ThenUserFillValuesInAssumedInsurerCreationPage()
    {
        string[] Data = {
         ExcelValue("Scenario Count"),
         ExcelValue("Insurer Name")
        };
        assumed.FillValuesInAssumedInsurerPage(Data);
    }


    [Then(@"User select the Roles and move it to choosen")]
    public void ThenUserSelectTheRolesAndMoveItToChoosen()
    {
        assumed.SelectRoles();
    }

    [Then(@"New Assumed Insurer record is created successfully")]
    public void ThenNewAssumedInsurerRecordIsCreatedSuccessfully()
    {
        assumed.VerifyCreatedInsurerRecord();
    }

    [Then(@"User clicked on Save button in Insurrer Page")]
    public void ThenUserClickedOnSaveButtonInInsurrerPage()
    {
        assumed.ClickOnSaveButton();
    }

    [Then(@"User clicked on New button in Insurer page")]
    public void ThenUserClickedOnNewButtonInInsurerPage()
    {
        assumed.ClickOnNewButton();
    }

    [Then(@"User log out from User and Login as Administrator")]
    public void ThenUserLogOutFromUserAndLoginAsAdministrator()
    {
        assumed.LogoutFromUser();
    }


    [Then(@"User Logged Out from the application")]
    public void ThenUserLoggedOutFromTheApplication()
    {
        assumed.LogoutFromApplication();
    }

    [Then(@"User Navigated to Policy Info Tab")]
    public void ThenUserNavigatedToPolicyInfoTab()
    {
        assumed.NavigateToPolicyInfoTabInSubmission();
    }

    [Then(@"User Navigated to ""([^""]*)"" Tab")]
    public void ThenUserNavigatedToTab(string TabName)
    {
        assumed.ThenUserNavigatedToTab(TabName);
    }

    


    [Then(@"User Clicked on Click Here link to Add Assumed Insurers")]
    public void ThenUserClickedOnClickHereLinkToAddAssumedInsurers()
    {
        assumed.NavigateToAddAssumedInsurerPage();
    }

    [Then(@"User Selected the Assumed Insurer")]
    public void ThenUserSelectedTheAssumedInsurer()
    {
        if (!_featureContext["Assumed_Require" + loggingStep.rowNo].ToString().Equals("No"))
        {
            assumed.SelectAssumedInsurers();
        }
    }

    [Then(@"User Clicked on Save button in Add Assumed Insurer")]
    public void ThenUserClickedOnSaveButtonInAddAssumedInsurer()
    {
        if (!_featureContext["Assumed_Require" + loggingStep.rowNo].ToString().Equals("No"))
        {
            assumed.SaveAssuredInsurer();
        }
    }

    [Then(@"User selected the value in the dropdown")]
    public void ThenUserSelectedTheValueInTheDropdown()
    {
        if (!_featureContext["Assumed_Require" + loggingStep.rowNo].ToString().Equals("No"))
        {
            assumed.AddCourrier();
        }
    }

    [Then(@"User verify the added Assumed Insurer in Submission")]
    public void ThenUserVerifyTheAddedAssumedInsurerInSubmission()
    {
        if (!_featureContext["Assumed_Require" + loggingStep.rowNo].ToString().Equals("No"))
        {
            assumed.VerifyAddedAssumedInsurer();
        }
    }

}
