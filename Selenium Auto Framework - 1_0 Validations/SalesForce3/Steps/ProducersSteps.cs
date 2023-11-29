using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System.Collections.Generic;
using System.Data;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class ProducersSteps : BaseStep
{

    //DEPENDENCY INJECTION OR CONTEXT INJECTION
    private readonly LoggingStep _loggingStep;
    private readonly ExcelHelper excelHelper;
    static DataTableCollection dataCollection;

    private static List<DataCollection> _dtColSProdPg = new();

    //THIS METHOD IS USED TO READ VALUES FROM EXCEL SHEET
    public string ExcelValue(string ColumnName) => excelHelper.ExcelValue(ColumnName, _dtColSProdPg, _loggingStep.rowNo);

    //CREATING OBJECT REFERENCE FOR PAGE CLASS
    private readonly ProducersPage producerpage;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public ProducersSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;
        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);
        _dtColSProdPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Contact Data", dataCollection);
        producerpage = new(parallelConfig, loggingStep, scenairoContext);
    }

    [Then(@"User navigated to Producers Object")]
    public void ThenUserNavigatedToProducersObject()
    {
        producerpage.NavigateToProducerObject();
    }

    [Then(@"User Opened Existing record")]
    public void ThenUserOpenedExistingRecord()
    {
        producerpage.OpenExistingRecord();
    }

    [Then(@"User Navigated to Related List")]
    public void ThenUserNavigatedToRelatedList()
    {
        producerpage.NavigateToRelatedList();
    }

    [Then(@"User Navigated to Create contact page")]
    public void ThenUserNavigatedToCreateContactPage()
    {
        producerpage.NavigateToCreateProducerContactPage();
    }



    [Then(@"User verified the created contact Association")]
    public void ThenUserVerifiedTheCreatedContactAssociation()
    {
        producerpage.VerifyContactAssociation();
    }

    [Then(@"User Created new Procedure record")]
    public void ThenUserCreatedNewProcedureRecord()
    {
        string[] Data =
        {
            ExcelValue("Producer Name"),
            ExcelValue("ProducerOne ID")
        };
        producerpage.CreateNewProcedure(Data);
    }

    [Then(@"User clicked on Contact record")]
    public void ThenUserClickedOnContactRecord()
    {
        producerpage.ClickOnContactRecord();
    }

    [Then(@"User clicked on Edit button in Contact object")]
    public void ThenUserClickedOnEditButtonInContactObject()
    {
        producerpage.ClickOnEditButton();
    }

    [Then(@"User updated the data in Contact fields")]
    public void ThenUserUpdatedTheDataInContactFields()
    {
        string[] Data =
        {
            ExcelValue("Producer Name"),
        };
        producerpage.EditTitle(Data);
    }
    [Then(@"Verify updated data in contact page")]
    public void ThenVerifyUpdatedDataInContactPage()
    {
        producerpage.VerifyUpdatedData();
    }

    [Then(@"Verify user don't have Edit permission for Producer record")]
    public void ThenVerifyUserDontHaveEditPermissionForProducerRecord()
    {
        producerpage.VerifyEditButtonIsNotAvailable();
    }




}
