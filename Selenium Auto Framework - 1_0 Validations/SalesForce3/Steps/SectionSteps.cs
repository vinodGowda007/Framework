using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class SectionSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSectionPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSectionPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly SectionPage sectionpage;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public SectionSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;

        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);

        _dtColSectionPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Section Data", dataCollection);

        sectionpage = new(parallelConfig, loggingStep, scenairoContext);
    }
 
    [Then(@"User clicked on Save button in Section creation page")]
    public void ThenUserClickedOnSaveButtonInSectionCreationPage()
    {
        sectionpage.SaveSectionRecord();
    }

    [Then(@"User clicked on save button for validation")]
    public void ThenUserClickedOnSaveButtonForValidation()
    {
        sectionpage.SaveSection();
    }

}
