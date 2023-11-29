using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class ReservationCheckSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSfaPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSfaPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly ReservationCheckPage reservationcheck;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public ReservationCheckSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;

        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);

        _dtColSfaPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Client Data", dataCollection);

        reservationcheck = new(parallelConfig, loggingStep, scenairoContext);
    }


    [Then(@"User clicked on Reservation Check button using ""([^""]*)""")]
    public void ThenUserClickedOnReservationCheckButtonUsing(string objectName)
    {
        reservationcheck.ClickOnReservationCheck(objectName);
    }

    [Then(@"Reservation Check Page is displayed")]
    public void ThenReservationCheckPageIsDisplayed()
    {
        reservationcheck.NavigatedToReservationCheckPage();
    }

    [Then(@"Verify the Reservation Check page")]
    public void ThenVerifyTheReservationCheckPage()
    {
        reservationcheck.VerifyReservationCheckPage();
    }



}