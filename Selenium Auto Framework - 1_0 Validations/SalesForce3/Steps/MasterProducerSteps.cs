using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class MasterProducerSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSMPPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSMPPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly MasterProducerPage masterproducerpage;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public MasterProducerSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;
        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);
        _dtColSMPPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Master Producer Data", dataCollection);
        masterproducerpage = new(parallelConfig, loggingStep, scenairoContext);
    }

    [Then(@"User Search for Producer Name")]
    public void ThenUserSearchForProducerName()
    {
        string producerName = ExcelValue("Producer Name");
        masterproducerpage.ThenUserSearchForProducerName(producerName);
    }

    [Then(@"User Select selected Producer Name")]
    public void ThenUserSelectSelectedProducerName()
    {
        string producerName = ExcelValue("Producer Name");
        masterproducerpage.ThenUserSelectSelectedProducerName(producerName);
    }

    [Then(@"User Navigated to Producer Details page")]
    public void ThenUserNavigatedToProducerDetailsPage()
    {
        masterproducerpage.ThenUserNavigatedToProducerDetailsPage();
    }

    [Then(@"User Navigated to Related Tab")]
    public void ThenUserNavigatedToRelatedTab()
    {
        masterproducerpage.ThenUserNavigatedToRelatedTab();
    }

    [Then(@"User Clicked on New Contact Button")]
    public void ThenUserClickedOnNewContactButton()
    {
        masterproducerpage.ThenUserClickedOnNewContactButton();
    }

    [Then(@"User Verify created contact is visible in the Contact related list")]
    public void ThenUserVerifyCreatedContactIsVisibleInTheContactRelatedList()
    {
        masterproducerpage.ThenUserVerifyCreatedContactIsVisibleInTheContactRelatedList();
    }

    [Then(@"User Clicked on New button in Submission section")]
    public void ThenUserClickedOnNewButtonInSubmissionSection()
    {
        masterproducerpage.ThenUserClickedOnNewButtonInSubmissionSection();
    }

    [Then(@"User Verify creaed submission is visible in the Submission related list")]
    public void ThenUserVerifyCreaedSubmissionIsVisibleInTheSubmissionRelatedList()
    {
        masterproducerpage.ThenUserVerifyCreaedSubmissionIsVisibleInTheSubmissionRelatedList();
    }



}
