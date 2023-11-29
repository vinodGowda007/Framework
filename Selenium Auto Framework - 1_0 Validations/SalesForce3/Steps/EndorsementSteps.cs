using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class EndorsementSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSendPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSendPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly EndorsementPage endorsementPage;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public EndorsementSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;

        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);

        _dtColSendPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Endorsement Data", dataCollection);

        endorsementPage = new(parallelConfig, loggingStep, scenairoContext);
    }

    [Then(@"User clicked on New Endorsement button")]
    public void ThenUserClickedOnNewEndorsementButton()
    {
        endorsementPage.NavigateToEndrosementCreationPage();
    }


    [Then(@"User clicked on Save button in Endorsement page")]
    public void ThenUserClickedOnSaveButtonInEndorsementPage()
    {
        endorsementPage.SaveEndorsement();
    }

    [Then(@"User Navigated to Endorsement Record")]
    public void ThenUserNavigatedToEndorsementRecord()
    {
        endorsementPage.ThenUserNavigatedToEndorsementRecord();
    }


    [Then(@"User Verify the ""([^""]*)"" Field calculation")]
    public void ThenUserVerifyTheFieldCalculation(string FieldName)
    {
        endorsementPage.CalculateAndVerifyProducerCommissionDollarValue(FieldName);
    }

    [Then(@"User Navigated back to Parent Submission from the child reocrd")]
    public void ThenUserNavigatedBackToParentSubmissionFromTheChildReocrd()
    {
        endorsementPage.NavigateBackToParentRecordFromChild();
    }

    [Then(@"User Verified the Parent submission page")]
    public void ThenUserVerifiedTheParentSubmissionPage()
    {
        endorsementPage.VerifyParentSubmissionPage();
    }




}
