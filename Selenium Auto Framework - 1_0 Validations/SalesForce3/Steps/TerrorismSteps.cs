using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class TerrorismSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSendPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSendPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly TerrorismPage terrorismpage;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public TerrorismSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;

        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);

        _dtColSendPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Terrorism Data", dataCollection);

        terrorismpage = new(parallelConfig, loggingStep, scenairoContext);
    }

    [Then(@"User clicked on Save button in Terrosism creation page")]
    public void ThenUserClickedOnSaveButtonInTerrosismCreationPage()
    {
        terrorismpage.SaveTerrorismRecord();
    }

    [Then(@"Verify the Error messages ""([^""]*)""")]
    public void ThenVerifyTheErrorMessages(string ErrorMessage)
    {
        terrorismpage.ThenVerifyTheErrorMessages(ErrorMessage);
    }




}
