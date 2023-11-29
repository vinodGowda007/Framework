using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class IncumbentInsurersSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSIncumbentPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSIncumbentPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly IncumbentInsurersPage incumbent;
    readonly FeatureContext _featureContext;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public IncumbentInsurersSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;
        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);
        _dtColSIncumbentPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Incumbent Data", dataCollection);
        incumbent = new(parallelConfig, loggingStep, scenairoContext, featureContext);
        _featureContext = featureContext;
    }

    [When(@"User Navigated to Incumbent Insurers Page")]
    public void WhenUserNavigatedToIncumbentInsurersPage()
    {
        incumbent.NavigateToIncumbentInsurersPage();
    }

    [Then(@"User clicked on New button in Incumbent Insurers Page")]
    public void ThenUserClickedOnNewButtonInIncumbentInsurersPage()
    {
        incumbent.ClickOnNewButton();
    }

    [Then(@"User clicked on Save button in Incumbent Insurers Page")]
    public void ThenUserClickedOnSaveButtonInIncumbentInsurersPage()
    {
        incumbent.ClickOnSaveButton();
    }

    [Then(@"user add value in ""([^""]*)"" Field")]
    public void ThenUserAddValueInField(string FieldName)
    {
        incumbent.ThenUserAddValueInField();
    }


    [Then(@"Verify the Error messages for ""([^""]*)"" in Insurers Page")]
    public void ThenVerifyTheErrorMessagesForInInsurersPage(string erromsg)
    {
        incumbent.VerifyErrorMessages(erromsg);
    }

    //[Then(@"User Fill the value in submission field")]
    //public void ThenUserFillTheValueInSubmissionField()
    //{
    //    string ScenarioCount = ExcelValue("Scenario Count");
    //    incumbent.InputValueInSubmission(string SubmissionType);
    //}

    [Then(@"User fill the value in Insurer\(if Other\) field")]
    public void ThenUserFillTheValueInInsurerIfOtherField()
    {
        incumbent.InputInsurerIfAny();
    }

    [Then(@"User select the Share Unknown Checkbox")]
    public void ThenUserSelectTheShareUnknownCheckbox()
    {
        incumbent.SelectShareUnknownCheckbox();
    }

    [Then(@"New Incumbent Insurer record is created successfully")]
    public void ThenNewIncumbentInsurerRecordIsCreatedSuccessfully()
    {
        incumbent.VerifyCreatedIncumbentInsurersRecord();
    }

    //[Then(@"User Fill the value Incumbent Insurers creation page for '([^']*)' submission")]
    //public void ThenUserFillTheValueIncumbentInsurersCreationPageForSubmission(string SubmissionType)
    //{
    //    incumbent.FillValueInIncumbentInsurer(SubmissionType);
    //}

    [Then(@"User Fill the value Incumbent Insurers creation page for '([^']*)' submission for ""([^""]*)""")]
    public void ThenUserFillTheValueIncumbentInsurersCreationPageForSubmissionFor(string SubmissionType, string Stage)
    {
        incumbent.FillValueInIncumbentInsurer(SubmissionType, Stage);
    }


    [Then(@"User Fill the value Incumbent Insurers creation page for Validation")]
    public void ThenUserFillTheValueIncumbentInsurersCreationPageForValidation()
    {
        incumbent.ThenUserFillTheValueIncumbentInsurersCreationPageForValidation();
    }

    [Then(@"user verify the error message for Incumbent Insurer as ""([^""]*)""")]
    public void ThenUserVerifyTheErrorMessageForIncumbentInsurerAs(string ErrorMsg)
    {
        incumbent.ThenUserVerifyTheErrorMessageForIncumbentInsurerAs(ErrorMsg);
    }





    [Then(@"User Fill the value Incumbent Insurers creation page for Renewal (.*) submission")]
    public void ThenUserFillTheValueIncumbentInsurersCreationPageForRenewalSubmission(string version)
    {
        string ScenarioCount = ExcelValue("Scenario Count");
        incumbent.ThenUserFillTheValueIncumbentInsurersCreationPageForRenewalSubmission(ScenarioCount, version);
    }


    [Then(@"Verify New Incumbent Insurer record is created successfully")]
    public void ThenVerifyNewIncumbentInsurerRecordIsCreatedSuccessfully()
    {
        incumbent.VerifyIncumbentInsuereRecord();
    }

    //[Then(@"Verify Added Incumbent Insurer in Submission record")]
    //public void ThenVerifyAddedIncumbentInsurerInSubmissionRecord()
    //{
    //    incumbent.VerifyIncumbentInsurerInSubmission();
    //}

    //[Then(@"Verify Added Incumbent Insurer in Submission record in '([^']*)'")]
    //public void ThenVerifyAddedIncumbentInsurerInSubmissionRecordIn(string Version)
    //{
    //    incumbent.VerifyIncumbentInsurerInSubmission(Version);
    //}


















}
