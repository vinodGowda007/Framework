using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class IceCheckSteps : BaseStep
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
    private readonly IceCheckPage icecheckpage;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public IceCheckSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;

        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);

        _dtColSfaPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Client Data", dataCollection);

        icecheckpage = new(parallelConfig, loggingStep, scenairoContext);
    }

    [Then(@"User perform Ice Check")]
    public void ThenUserPerformIceCheck()
    {
        icecheckpage.ClickOnIceCheckButton();
    }

    [Then(@"Ice check status will be displayed in the popup")]
    public void ThenIceCheckStatusWillBeDisplayedInThePopup()
    {
        icecheckpage.VerifyIceCheckStautusCompletion();
    }

    [Then(@"User clicked on Refresh button")]
    public void ThenUserClickedOnRefreshButton()
    {
        icecheckpage.UserClickedOnRefreshButton();
    }


    [Then(@"User Verify Ice check status is updated in the clients details page\.")]
    public void ThenUserVerifyIceCheckStatusIsUpdatedInTheClientsDetailsPage_()
    {
        icecheckpage.VerifyIceStatus();
    }

    [Then(@"User Verify Ice check status is before ICE CHECK Action")]
    public void ThenUserVerifyIceCheckStatusIsBeforeICECHECKAction()
    {
        icecheckpage.ThenUserVerifyIceCheckStatusIsBeforeICECHECKAction();
    }


    [Then(@"User Navigated to Starr UnderWriting Lightning App")]
    public void ThenUserNavigatedToStarrUnderWritingLightningApp()
    {
        icecheckpage.NavigateToStarrUnderwritingLightningApp();
    }

    [Then(@"User clicked on ICE Status dropdown")]
    public void ThenUserClickedOnICEStatusDropdown()
    {
        icecheckpage.ClickedOnIceStatusdropdown();
    }

    [Then(@"User verify the Ice Status dropdown value")]
    public void ThenUserVerifyTheIceStatusDropdownValue()
    {
        icecheckpage.VerifyIceStatusDropdownValue();
    }

    [Then(@"User select Ice Status as Frozen")]
    public void ThenUserSelectIceStatusAsFrozen()
    {
        icecheckpage.SelectValueFromIceCheckDropdown();
    }





}
