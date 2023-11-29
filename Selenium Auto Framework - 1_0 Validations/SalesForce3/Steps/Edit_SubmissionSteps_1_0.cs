using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class Edit_SubmissionSteps_1_0 : BaseStep
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
    private readonly Edit_SubmissionsPage editsubmissionPage1_0;
    readonly FeatureContext _featureContext;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public Edit_SubmissionSteps_1_0(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;
        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);
        _dtColSfaPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Submission Data", dataCollection);
        editsubmissionPage1_0 = new(parallelConfig, loggingStep, scenairoContext, featureContext);
        _featureContext = featureContext;
    }

    [Then(@"User clicked on Edit button to edit the submission record")]
    public void ThenUserClickedOnEditButtonToEditTheSubmissionRecord()
    {
        editsubmissionPage1_0.ClickOnEditButton();
    }

    [Then(@"User updated the Project Name field in submission")]
    public void ThenUserUpdatedTheProjectNameFieldInSubmission()
    {
        editsubmissionPage1_0.EditProjectField();
    }

    [Then(@"User Edit Assumed Carrier Branch field")]
    public void ThenUserEditAssumedCarrierBranchField()
    {
        editsubmissionPage1_0.ThenUserEditAssumedCarrierBranchField();
    }

    [Then(@"User Updated '([^']*)' field with value '([^']*)'")]
    public void ThenUserUpdatedFieldWithValue(string FieldName, string FieldValue)
    {
        editsubmissionPage1_0.ThenUserUpdatedFieldWithValue(FieldName, FieldValue);
    }

    [Then(@"User Clicked on save submission in View edit mode")]
    public void ThenUserClickedOnSaveSubmissionInViewEditMode()
    {
        editsubmissionPage1_0.ThenUserClickedOnSaveSubmissionInViewEditMode();
    }


    [Then(@"User verified the error message displayed as ""([^""]*)""")]
    public void ThenUserVerifiedTheErrorMessageDisplayedAs(string ErrorMessage)
    {
        editsubmissionPage1_0.ThenUserVerifiedTheErrorMessageDisplayed(ErrorMessage);
    }


    [Then(@"User clicked on Edit button to edit the submission record during stage progression")]
    public void ThenUserClickedOnEditButtonToEditTheSubmissionRecordDuringStageProgression()
    {
        editsubmissionPage1_0.ClickOnEditSubmissionButton();
    }

    [Then(@"User updated the fields required")]
    public void ThenUserUpdatedTheFieldsRequired()
    {
        editsubmissionPage1_0.FillValuesInSubmissionEditPagefor_Submission1_0();
        // editsubmissionPage1_0.FillValuesInSubmissionEditPageforExistingRecord();
    }

    [Then(@"User clicked on Save button during Edit Mode")]
    public void ThenUserClickedOnSaveButtonDuringEditMode()
    {
        editsubmissionPage1_0.SaveEditedRecord();
    }

    [Then(@"User updated the fields required during Edit Mode")]
    public void ThenUserUpdatedTheFieldsRequiredDuringEditMode()
    {
        editsubmissionPage1_0.FillValuesInSubmissionEditPageforExistingRecord();
    }



    [Then(@"User clicked on Save button during Edit Mode in Stage Progression")]
    public void ThenUserClickedOnSaveButtonDuringEditModeInStageProgression()
    {
        editsubmissionPage1_0.SaveEditedSubmissionRecord();
    }

    [Then(@"Click On Save button to verify the error message If Ice And Licence Check Fails")]
    public void ThenClickOnSaveButtonToVerifyTheErrorMessageIfIceAndLicenceCheckFails()
    {
        editsubmissionPage1_0.ThenClickOnSaveButtonToVerifyTheErrorMessageIfIceAndLicenceCheckFails();
    }



    [Then(@"User Clicked on Cancel button")]
    public void ThenUserClickedOnCancelButton()
    {
        editsubmissionPage1_0.CancelSubmissionCreateOrEdit();
    }


    [Then(@"User clicked on Save button during Edit Mode in Stage Progression for backtracking")]
    public void ThenUserClickedOnSaveButtonDuringEditModeInStageProgressionForBacktracking()
    {
        editsubmissionPage1_0.SaveEditedSubmissionRecordForStageProgressionBackTracking();
    }

    [Then(@"User click on Save button")]
    public void ThenUserClickOnSaveButton()
    {
        editsubmissionPage1_0.SaveEditedSubmissionRecordForStageProgressionBackTracking();
    }


    [Then(@"User submission record is updated successfully")]
    public void ThenUserSubmissionRecordIsUpdatedSuccessfully()
    {
        editsubmissionPage1_0.VerifySavedSubmissionRecord();
    }

    [Then(@"User clicked on First submission record")]
    public void ThenUserClickedOnFirstSubmissionRecord()
    {
        editsubmissionPage1_0.FirstSubmissionRecord();
    }

    //[Then(@"User verify the Updated Project Name")]
    //public void ThenUserVerifyTheUpdatedProjectName()
    //{

    //}

    //[Then(@"User Navigated to Snapshot Information")]
    //public void ThenUserNavigatedToSnapshotInformation()
    //{
    //    editsubmissionPage1_0.NavigateToSnapshotInfoTabInSubmission();
    //}

    [Then(@"User verify the Snapshot Information")]
    public void ThenUserVerifyTheSnapshotInformation()
    {
        editsubmissionPage1_0.UserStoreThenUserVerifyTheSnapshotInformation();
    }

    [Then(@"User verify the Snapshot Information after Update")]
    public void ThenUserVerifyTheSnapshotInformationAfterUpdate()
    {
        editsubmissionPage1_0.ThenUserVerifyTheSnapshotInformationAfterUpdate();
    }



}
