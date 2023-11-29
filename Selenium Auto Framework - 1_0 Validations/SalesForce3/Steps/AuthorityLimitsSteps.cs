using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class AuthorityLimitsSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSfaiPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSfaiPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly AuthorityLimitsPage authoritylimitpage;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public AuthorityLimitsSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;

        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);

        _dtColSfaiPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Authority Limit Data", dataCollection);

        authoritylimitpage = new(parallelConfig, loggingStep, scenairoContext);
    }


    [Then(@"User clicked on New button in Authority Limits Page")]
    public void ThenUserClickedOnNewButtonInAuthorityLimitsPage()
    {
        authoritylimitpage.NewAuthorityLimitCreationPage();
    }

    //[Then(@"User Fill values in Authority Limits Page")]
    //public void ThenUserFillValuesInAuthorityLimitsPage()
    //{
    //    string[] Data = {
    //     ExcelValue("Policy Limit ID"),
    //     ExcelValue("Limit Type"),
    //     ExcelValue("Authority Limit Amount"),
    //     ExcelValue("Scenario Count")
    //    };
    //    authoritylimitpage.FillValuesInAuthorityLimit(Data);

    //}

    [Then(@"User clicked on Save button in Authority Limits Page")]
    public void ThenUserClickedOnSaveButtonInAuthorityLimitsPage()
    {
        authoritylimitpage.SaveAuthorityLimiteRecord();
    }

    [Then(@"New Authority Limit is created successfully")]
    public void ThenNewAuthorityLimitIsCreatedSuccessfully()
    {
        authoritylimitpage.VerifyCreatedAuthorityLimitRecord();
    }

    [Then(@"User Update required fields in submission for authority limit")]
    public void ThenUserUpdateRequiredFieldsInSubmissionForAuthorityLimit()
    {
        string[] data =
        {
            ExcelValue("Submission Currency")
        };
        authoritylimitpage.updateSubmissionForAuthorityLimitCheck(data);
    }

    [Then(@"User Navigated to created Authority Limit Record")]
    public void ThenUserNavigatedToCreatedAuthorityLimitRecord()
    {
        string data = ExcelValue("Scenario Count");
        authoritylimitpage.NavigateToAuthorityLimitRecord(data);
    }


    [Then(@"User Edited the Authority Limit record")]
    public void ThenUserEditedTheAuthorityLimitRecord()
    {
        authoritylimitpage.UpdateAuthorityLimit();
    }

    [Then(@"Submission record saved successfully and verified the Starr Limit USD \(Lloyd's\) value in submission")]
    public void ThenSubmissionRecordSavedSuccessfullyAndVerifiedTheStarrLimitUSDLloydsValueInSubmission()
    {
        authoritylimitpage.VerifyStarLimitUSDLoydsvalue();
    }



    [Then(@"User clicked on Edit button to edit the submission record during Authority limit")]
    public void ThenUserClickedOnEditButtonToEditTheSubmissionRecordDuringAuthorityLimit()
    {
        authoritylimitpage.ClickOnEditSubmissionButton();
    }

    [Then(@"User clicked on Save button during Edit Mode in Authority Limit")]
    public void ThenUserClickedOnSaveButtonDuringEditModeInAuthorityLimit()
    {
        authoritylimitpage.SaveAuthorityLimit();
    }

    [Then(@"User Deleted the Authority Limit record")]
    public void ThenUserDeletedTheAuthorityLimitRecord()
    {
        authoritylimitpage.DeleteAuthorityLimitRecord();
    }

}
