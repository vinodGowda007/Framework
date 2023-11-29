using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class ResetDataSetSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSfaiPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSfaiPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly ResetDataSetPage resetdatapage;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    readonly FeatureContext _featureContext;

    public ResetDataSetSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;

        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);

        _dtColSfaiPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Reset Data", dataCollection);

        resetdatapage = new(parallelConfig, loggingStep, scenairoContext, featureContext);

        _featureContext = featureContext;
    }

    [Given(@"Delete all folders under Data set")]
    public void GivenDeleteAllFoldersUnderDataSet()
    {
        resetdatapage.DeleteAllFolderUnderDataSet();
    }

    [Then(@"Create All folder under DataSet to store the data")]
    public void ThenCreateAllFolderUnderDataSetToStoreTheData()
    {
        resetdatapage.CreateAllFolderUnderDataSet();
    }


}
