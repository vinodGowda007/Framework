using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class SF_CloneSubmissionSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;
    private static List<DataCollection> _dtColSfPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSfPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly SF_CloneSubmissionPage SF;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public SF_CloneSubmissionSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;
        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);
        _dtColSfPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "SFPage", dataCollection);
        SF = new(parallelConfig, loggingStep, scenairoContext);
    }

    //ENTERING THE SUBMISSION NUMBER TO CLONE IN SEARCH BOX
    [Then(@"UW enter the submisson number in search box")]
    public void ThenUWEnterTheSubmissonNumberInSearchBox()
    {
        SF.EnterSubmissionNumberInSearchBox(ExcelValue("Submission Number"));
    }

    //SELECTING THE NAME OF THE SUBMISSION FROM SEARCH RESULT
    [Then(@"UW Click The Submission Name In Submissions Section")]
    public void ThenUWClickTheSubmissionNameInSubmissions()
    {
        SF.ClickOnSubmissionNameOnSearchedResult();
    }

    //CLICKING THE CLONE SUBMISSION OPTION
    [Then(@"UW should be able to Clone Sumbission")]
    public void ThenUWShouldAbleToCloneSumbission()
    {
        SF.CloneTheSubmission();
    }

    //AFTER CLONING , DOING ICE CHECK
    [Then(@"UW should be able to Do ICE Check")]
    public void ThenUWShouldAbleToDoICECheck()
    {
        SF.ICE_CheckForClonedSubmission();
    }

    //LICENSE CHECK
    [Then(@"UW should be able to Do License Check")]
    public void ThenUWShouldAbleToDoLicenseCheck()
    {
        SF.LicenseCheckForClonedSubmission();

    }

    //RENAMING THE INSURED NAME FOR CLONED SUBMISSION
    [Then(@"UW should be able to Fill Details Cloned Submission")]
    public void ThenUWShouldAbleToFillDetailsInClonedSubmission()
    {
        string[] Data = {
         ExcelValue("Insured Name"),
         ExcelValue("Street"),
         ExcelValue("City"),
         ExcelValue("State"),
         ExcelValue("Zip"),
         ExcelValue("Country"),
         ExcelValue("PolicyEffectiveDate"),
         ExcelValue("PolicyExpiryDate")
        };
        SF.FillDetailsInClonedSubmission(Data);
    }

    //SELECT COVERAGES
    [Then(@"UW should be able to Select Coverages")]
    public void ThenUWShouldAbleToSelectCoverages()
    {
        string coverage = ExcelValue("Coverage");
        string[] coverages = coverage.Split(";");
        SF.SelectCoverages(coverages);
    }

    //SAVING CHANGES IN CLONED SUBMISSION
    [Then(@"UW should be able to Save The Changes")]
    public void ThenUWShouldAbleToSaveTheChanges()
    {
        SF.ClickSaveButon();
    }


    //PRINTING THE SUBMISSION NUMBER IN CONSOLE
    [Then(@"UW should be able to Print The Submission Number Of The Cloned Submission Number")]
    public void ThenUWShouldAbleToPrintTheSubmissionNumberOfTheClonedSubmissionNumber()
    {
        SF.PrintSubmissionNumber();
    }
}
