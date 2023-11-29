using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class RenewalSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColRenewalPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColRenewalPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly RenewalPage renewalpage;
    readonly FeatureContext _featureContext;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public RenewalSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;

        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);

        _dtColRenewalPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Renewal Data", dataCollection);

        renewalpage = new(parallelConfig, loggingStep, scenairoContext, featureContext);
        _featureContext = featureContext;
    }

    [Then(@"User clicked on Save button in Renewal Submission creation page")]
    public void ThenUserClickedOnSaveButtonInRenewalSubmissionCreationPage()
    {
        renewalpage.SaveRenewalRecord();
    }

    [Then(@"User clicked on save button in child record to verify the error messages")]
    public void ThenUserClickedOnSaveButtonInChildRecordToVerifyTheErrorMessages()
    {
        renewalpage.SaveRecordToVerifyErrorMessageForChildRecords();
    }


    [Then(@"User verified Renewal submission creation")]
    public void ThenUserVerifiedRenewalSubmissionCreation()
    {
        renewalpage.ThenUserVerifiedRenewalSubmissionCreation();
    }



    [Then(@"User clicked on Submission Renewal button to verify Already renewed submission error message")]
    public void ThenUserClickedOnSubmissionRenewalButtonToVerifyAlreadyRenewedSubmissionErrorMessage()
    {
        renewalpage.VerifyAlreadyRenewedSubmissionCreatedErrorMessage();
    }

 

    [Then(@"User Verified parent information in  created Renewal record")]
    public void ThenUserVerifiedParentInformationInCreatedRenewalRecord()
    {
        renewalpage.ThenUserVerifiedParentInformationInCreatedRenewalRecord();
    }


    [Then(@"User Verified the Created Child Records in the Renewal record")]
    public void ThenUserVerifiedTheCreatedChildRecordsInTheRenewalRecord()
    {
        renewalpage.ThenUserVerifiedTheCreatedChildRecordsInTheRenewalRecord();
    }

    [Then(@"User clicked on Save button in Clone creation page for '([^']*)'")]
    public void ThenUserClickedOnSaveButtonInCloneCreationPageFor(string SubmissionVersion)
    {
        renewalpage.ThenUserClickedOnSaveButtonInCloneCreationPage(SubmissionVersion);
    }

    [Then(@"User verified created clone record")]
    public void ThenUserVerifiedCreatedCloneRecord()
    {
        renewalpage.ThenUserVerifiedCreatedCloneRecord();
    }


    [Then(@"User Verify Renewed Submission creation")]
    public void ThenUserVerifyRenewedSubmissionCreation()
    {
        renewalpage.ThenUserVerifyRenewedSubmissionCreation();
    }

    [When(@"User Clicked on Regular Clone button")]
    public void WhenUserClickedOnRegularCloneButton()
    {
        renewalpage.WhenUserClickedOnRegularCloneButton();
    }

    [Then(@"User Verify Clone submission is created successfully")]
    public void ThenUserVerifyCloneSubmissionIsCreatedSuccessfully()
    {
        renewalpage.ThenUserVerifyCloneSubmissionIsCreatedSuccessfully();
    }






    [Then(@"User Verify Parent Submisison Number under Renewed submission")]
    public void ThenUserVerifyParentSubmisisonNumberUnderRenewedSubmission()
    {
        renewalpage.ThenUserVerifyParentSubmisisonNumberUnderRenewedSubmission();
    }

    [Then(@"User verify renewed submission number under parent submission")]
    public void ThenUserVerifyRenewedSubmissionNumberUnderParentSubmission()
    {

    }





}
