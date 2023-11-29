using Microsoft.VisualBasic;
using SalesForce3.Pages;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using TechTalk.SpecFlow;

namespace SalesForce3.Steps;

[Binding]
public class SubmissionSteps : BaseStep
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
    private readonly SubmissionPage submissionpage;
    public static int Version;
    readonly FeatureContext _featureContext;

    //DEPENDENCY INJECTION OR CONTEXT INJECTION 
    public SubmissionSteps(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenairoContext, FeatureContext featureContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        excelHelper = loggingStep.excelHelper;

        dataCollection = excelHelper.LoadExcelToDataTable(_loggingStep.fileName);

        _dtColSfaPg = excelHelper.StoreExcelValuesToCollection(_loggingStep.fileName, "Submission Data", dataCollection);

        submissionpage = new(parallelConfig, loggingStep, scenairoContext, featureContext);

        _featureContext = featureContext;
    }

    [Then(@"User navigated to submission Page")]
    public void ThenUserNavigatedToSubmissionPage()
    {
        submissionpage.NavigateToSubmissionTab();
    }

    [Then(@"User Navigated to '([^']*)' Page")]
    public void ThenUserNavigatedToPage(string TabName)
    {
        submissionpage.ThenUserNavigatedToPage(TabName);
    }


    [Then(@"User select an existing submission record")]
    public void ThenUserSelectAnExistingSubmissionRecord()
    {
        submissionpage.SelectFirstRecord();
    }

    [Then(@"User navigated to submission edit details page")]
    public void ThenUserNavigatedToSubmissionEditDetailsPage()
    {
        submissionpage.EditSubmission();
    }

    //[Then(@"User verify the business unit and perform respective udpate on the fields")]
    //public void ThenUserVerifyTheBusinessUnitAndPerformRespectiveUdpateOnTheFields()
    //{
    //    submissionpage.VerifyBusinessUnitValueAndPerformAutoClosureAction();
    //}

    [Then(@"User Changed the app to ""([^""]*)""")]
    public void ThenUserChangedTheAppTo(string AppName)
    {
        submissionpage.NavigateToApp(AppName);
    }


    [Then(@"User clicked on New button in submission Page")]
    public void ThenUserClickedOnNewButtonInSubmissionPage()
    {
        submissionpage.ClickOnNewButton();
    }



    [Then(@"User selected the record type as ""([^""]*)""")]
    public void ThenUserSelectedTheRecordTypeAs(string recordTypeValue)
    {
        switch (recordTypeValue)
        {
            case "Record Type 1.0":
                _featureContext["App_Version" + loggingStep.rowNo] = "1";
                break;

            case "Record Type 2.0":
                _featureContext["App_Version" + loggingStep.rowNo] = "2";
                break;

            default:
                Console.WriteLine("PROVIDED APP VERSION IS INVALID");
                break;
        }
        submissionpage.SelectRecordType(ExcelValue(recordTypeValue));
    }


    [Then(@"User selected the record type as (.*) for ""([^""]*)""")]
    public void ThenUserSelectedTheRecordTypeAsRecordTypeFor(string RecordType,string Version)
    {
        switch(Version)
        {
            case "1.0": _featureContext["App_Version"+loggingStep.rowNo] = "1";
                break;

            case "2.0":
                _featureContext["App_Version" + loggingStep.rowNo] = "2";
                break;

            default: Console.WriteLine("PROVIDED APP VERSION IS INVALID");
                break;
        }
        submissionpage.SelectRecordType(RecordType);
    }


    [Then(@"User fill the data in Submission version two creation record for '([^']*)'")]
    public void ThenUserFillTheDataInSubmissionVersionTwoCreationRecordFor(string InsuranceType)
    {
        string[] Data = {
         ExcelValue("Stage 2.0"),
         ExcelValue("Date Format"),
         ExcelValue("Licensed Producer 2.0"),
         ExcelValue("Placing Producer Contact"),
         ExcelValue("Profit Center"),
         ExcelValue("Product Profit Center"),
         ExcelValue("Business Unit"),
         ExcelValue("Issuing Company"),
         ExcelValue("Assigned Underwriter"),
         ExcelValue("Submission Currency"),
         ExcelValue("Placing Broaker")
        };
        submissionpage.FillValuesInSubmissionPagefor_Submission2_0(Data, InsuranceType);
    }

    // This can be deleted Later Stage
    //[Then(@"User fill the data in Submission version one creation record for '([^']*)'")]
    //public void ThenUserFillTheDataInSubmissionVersionOneCreationRecordFor(string InsuranceType)
    //{

    //    string[] Data = {
    //     ExcelValue("Stage"),
    //     ExcelValue("Production Office"),
    //     ExcelValue("Insuring Company"),
    //     ExcelValue("Line of Business"),
    //     ExcelValue("Subclass1"),
    //     ExcelValue("Issuing Office"),
    //     ExcelValue("UW Region"),
    //     ExcelValue("100% Policy Limit Amount (Currency)"),
    //     ExcelValue("Date Format"),
    //     ExcelValue("Occupancy"),
    //     ExcelValue("Shell Policy"),
    //     ExcelValue("Affiliated"),
    //     ExcelValue("Policy Number"),
    //     ExcelValue("Submission Currency"),
    //     ExcelValue("Licensed Producer"),
    //     ExcelValue("Program")
    //    };
    //    submissionpage.FillValuesInSubmissionPagefor_Submission1_0(Data, InsuranceType);
    //}

    [Then(@"User fill the data in Submission version one creation record for '([^']*)'")]
    public void ThenUserFillTheDataInSubmissionVersionOneCreationRecordFor(string InsuranceType)
    {

        string[] Data = {
         ExcelValue("Stage"),
         ExcelValue("Production Office"),
         ExcelValue("Issuing Company"),
         ExcelValue("Line of Business"),
         ExcelValue("Subclass1"),
         ExcelValue("Issuing Office"),
         ExcelValue("UW Region"),
         ExcelValue("Date Format"),
         ExcelValue("Occupancy"),
         ExcelValue("Shell Policy"),
         ExcelValue("Affiliated"),
         ExcelValue("Policy Number"),
         ExcelValue("Submission Currency"),
         ExcelValue("Licensed Producer"),
         ExcelValue("Program"),
         ExcelValue("Carrier Branch"),
         ExcelValue("Underwriting Credit Branch"),
         ExcelValue("Profit Center")
        };
        submissionpage.FillValuesInSubmissionPagefor_Submission1_0(Data, InsuranceType);
    }

 

    [Then(@"User Fill the values in Renewal submission creation page")]
    public void ThenUserFillTheValuesInRenewalSubmissionCreationPage()
    {
        string[] Data =
        {
            ExcelValue("Date Format")
        };
        submissionpage.ThenUserFillTheValuesInRenewalSubmissionCreationPage(Data);
    }

    [Then(@"User Fill the values in Endorsement creation page for ""([^""]*)""")]
    public void ThenUserFillTheValuesInEndorsementCreationPageFor(string EndorsementType)
    {
        string[] Data =
        {

            ExcelValue("EProduction Month"),
            ExcelValue("EProduction Year"),
            ExcelValue("EUnderwriting Credit Branch"),
            ExcelValue("EUCB Share"),
            ExcelValue("EPurchase External FAC"),
            ExcelValue("E100 Percent Policy Booked Premium"),
            ExcelValue("EPurchased External FAC Reinsurance"),
            ExcelValue("EFAC Basis of Acceptance"),
            ExcelValue("EFAC RI PRM"),
            ExcelValue("Date Format")
       };
        submissionpage.ThenUserFillTheValuesInEndorsementCreationPageFor(Data, EndorsementType);
    }



    [Then(@"User Fill the values in Section creation page")]
    public void ThenUserFillTheValuesInSectionCreationPage()
    {
        string[] Data = {
         ExcelValue("Date Format"),
         ExcelValue("SProduction Month"),
         ExcelValue("SProduction Year"),
         ExcelValue("SUnderwriting Credit Branch"),
         ExcelValue("SUCB Share"),
         ExcelValue("SPurchase External FAC"),
         ExcelValue("Saffiliated"),
         ExcelValue("SPurchased External FAC Reinsurance"),
         ExcelValue("SFAC Basis of Acceptance"),
         ExcelValue("SFAC RI PRM"),
    };
        submissionpage.FillValuesInSection1_0(Data);
    }

    [Then(@"User Fill the values in Clone submission page")]
    public void ThenUserFillTheValuesInCloneSubmissionPage()
    {
        submissionpage.ThenUserFillTheValuesInCloneSubmissionPage();
    }


    [Then(@"User Fill the values in Terrorism creation page")]
    public void ThenUserFillTheValuesInTerrorismCreationPage()
    {
        string[] Data = {
         ExcelValue("Date Format"),
         ExcelValue("TProduction Month"),
         ExcelValue("TProduction Year"),
         ExcelValue("TUnderwriting Credit Branch"),
         ExcelValue("TUCB Share"),
         ExcelValue("TPurchase External FAC"),
         ExcelValue("Taffiliated"),
         ExcelValue("TPurchased External FAC Reinsurance"),
         ExcelValue("TFAC Basis of Acceptance"),
         ExcelValue("TFAC RI PRM"),
    };
        submissionpage.FillValuesInTerrorism1_0(Data);
    }


    [Then(@"User clicked on Save button in submission page")]
    public void ThenUserClickedOnSaveButtonInSubmissionPage()
    {
        submissionpage.ClickOnSaveButton();
    }

 
    [Then(@"User verify the created submission record")]
    public void ThenUserVerifyTheCreatedSubmissionRecord()
    {
        submissionpage.VerifySubmissionCreation();
    }

    [Then(@"User verify the created submission record for '([^']*)'")]
    public void ThenUserVerifyTheCreatedSubmissionRecordFor(string Validation)
    {
        submissionpage.VerifySubmissionCreation(Validation);
    }

    [Then(@"User verify the created submission record for '([^']*)' for '([^']*)' Stage")]
    public void ThenUserVerifyTheCreatedSubmissionRecordForForStage(string validation, string stageName)
    {
        submissionpage.VerifySubmissionCreationforValidation(validation,stageName);
    }



    [Then(@"User perform Ice Check in Submission Page")]
    public void ThenUserPerformIceCheckInSubmissionPage()
    {
        submissionpage.ClickOnIceCheckButton();
    }


    [Then(@"User Updated the fields required for Snapshot informaiton")]
    public void ThenUserUpdatedTheFieldsRequiredForSnapshotInformaiton()
    {
        string ProjectValue = ExcelValue("Update Client");
        submissionpage.ThenUserUpdatedTheFieldsRequiredForSnapshotInformaiton(ProjectValue);
    }

    [Then(@"User clicked on Refresh button in Submission page")]
    public void ThenUserClickedOnRefreshButtonInSubmissionPage()
    {
        submissionpage.IceCheckRefresh();
    }

    [Then(@"User verify the Ice Check status '([^']*)' performing Ice Check action")]
    public void ThenUserVerifyTheIceCheckStatusPerformingIceCheckAction(string IceCheckAction)
    {
        submissionpage.VerifyIceCheckUpdate(IceCheckAction);
    }



     [Then(@"Clicked on Close button during Licence Check")]
    public void ThenClickedOnCloseButtonDuringLicenceCheck()
    {
        submissionpage.ThenClickedOnCloseButtonDuringLicenceCheck();
    }

    [Then(@"Verify Licence Check status is updated for '([^']*)'")]
    public void ThenVerifyLicenceCheckStatusIsUpdatedFor(string SubmissionVersion)
    {
        submissionpage.ThenUserLicenceCheckStatusIsUpdatedInTheSubmissionDetailsPage(SubmissionVersion);
    }




    //[Then(@"User Navigated to the created submission record")]
    //public void ThenUserNavigatedToTheCreatedSubmissionRecord()
    //{
    //    string Data = ExcelValue("Scenario Count");
    //    submissionpage.NavigateToCreatedSubmissionRecord(Data);
    //}

    [Then(@"User Navigated to the created submission (.*) record")]
    public void ThenUserNavigatedToTheCreatedSubmissionRecord(string SubVersion)
    {
        submissionpage.NavigateToCreatedSubmissionRecord(SubVersion);
    }

    [Then(@"User Navigated to the created submission (.*) record for '([^']*)'")]
    public void ThenUserNavigatedToTheCreatedSubmissionRecordFor(string SubVersion, string ExecutionType)
    {
        submissionpage.NavigateToCreatedSubmissionRecord(SubVersion, ExecutionType);
    }

    [Then(@"User Navigated to the created submission (.*) record for '([^']*)' for the stage '([^']*)'")]
    public void ThenUserNavigatedToTheCreatedSubmissionRecordForForTheStage(string SubVersion, string ExecutionType, string stageName)
    {
        submissionpage.NavigateToCreatedSubmissionRecordforthestageName(SubVersion, ExecutionType, stageName);
    }




    [Then(@"User Navigated to existing Record '([^']*)'")]
    public void ThenUserNavigatedToExistingRecord(string ExistingURL)
    {
        submissionpage.ThenUserNavigatedToExistingRecord(ExistingURL);
    }




    [When(@"User performed Stage progression from ""([^""]*)"" to ""([^""]*)""")]
    public void WhenUserPerformedStageProgressionFromTo(string FromStageValue, string ToStageValue)
    {
        submissionpage.StageProgression(FromStageValue, ToStageValue);
    }

    [When(@"User performed Stage progression from ""([^""]*)"" to ""([^""]*)"" in (.*)")]
    public void WhenUserPerformedStageProgressionFromToIn(string FromStageValue, string ToStageValue, string p2)
    {
        submissionpage.StageProgression2_0(FromStageValue, ToStageValue);
    }

    [Then(@"User capture the value of '([^']*)'")]
    public void ThenUserCaptureTheValueOf(string p0)
    {
        submissionpage.ThenUserCaptureTheValueOfField();
    }




    [Then(@"User Update the Declined Reason in the submission")]
    public void ThenUserUpdateTheDeclinedReasonInTheSubmission()
    {
        submissionpage.ProvideDeclainedReason();
    }

    [Then(@"User Fill the value in '([^']*)' field")]
    public void ThenUserFillTheValueInField(string FieldName)
    {
        submissionpage.ThenUserFillTheValueInField(FieldName);
    }

    [Then(@"User Fill the value in '([^']*)' field as '([^']*)'")]
    public void ThenUserFillTheValueInFieldAs(string FieldName, string FieldValue)
    {
        submissionpage.ThenUserFillTheValueInField(FieldName, FieldValue);
    }


    [Then(@"User Fill the value in submission required for Bound stage")]
    public void ThenUserFillTheValueInSubmissionRequiredForBoundStage()
    {
        string Data = ExcelValue("Date Format");
        submissionpage.ThenUserFillTheValueInSubmissionRequiredForBoundStage(Data);
    }

    [Then(@"User Fill the value in submission required for Cancelled stage")]
    public void ThenUserFillTheValueInSubmissionRequiredForCancelledStage()
    {
        string Data = ExcelValue("Date Format");
        submissionpage.ThenUserFillTheValueInSubmissionRequiredForCancelledStage(Data);
    }



    [Then(@"User Verified submission record status changed to ""([^""]*)""")]
    public void ThenUserVerifiedSubmissionRecordStatusChangedTo(string ChangedStageValue)
    {
        submissionpage.VerifySubmissionStatus(ChangedStageValue);
    }

    [Then(@"User Verified submission record status changed to ""([^""]*)"" in (.*)")]
    public void ThenUserVerifiedSubmissionRecordStatusChangedToIn(string working, string p1)
    {
        submissionpage.ThenUserVerifiedSubmissionRecordStatusChangedToIn(working);
    }

    [Then(@"Verify the Error messages for Backtracking as ""([^""]*)""")]
    public void ThenVerifyTheErrorMessagesForBacktrackingAs(string errorMessage)
    {
        submissionpage.VerifyErrormessage(errorMessage);
    }

    [Then(@"Verify the Error message ""([^""]*)""")]
    public void ThenVerifyTheErrorMessage(string errorMessage)
    {
        submissionpage.VerifyErrormessage(errorMessage);
    }


    [Then(@"Verify Error message is displayed ""([^""]*)""")]
    public void ThenVerifyErrorMessageIsDisplayed(string errorMsg2_0)
    {
        submissionpage.ThenVerifyErrorMessageIsDisplayed(errorMsg2_0);
    }

    [Then(@"Verify Error message is displayed under the field ""([^""]*)""")]
    public void ThenVerifyErrorMessageIsDisplayedUnderTheField(string errorMsg2_0)
    {
        submissionpage.ThenVerifyErrorMessageIsDisplayedUnderField(errorMsg2_0);
    }


    [Then(@"Verify Error message is display '([^']*)'")]
    public void ThenVerifyErrorMessageIsDisplay(string errorMsg2_0)
    {
        submissionpage.ThenVerifyErrorMessageIsDisplayed(errorMsg2_0);
    }


    [Then(@"User clicked on Save button in Submission Page for Verification")]
    public void ThenUserClickedOnSaveButtonInSubmissionPageForVerification()
    {
        submissionpage.ClickOnSaveButtonDuringVerificationOfLicenceCheck();
    }







    [Then(@"User perfom ""([^""]*)""")]
    public void ThenUserPerfom(string ReservationCheckName)
    {
        submissionpage.ReservationChecks(ReservationCheckName);
    }

    [Then(@"User verify the ""([^""]*)"" Page")]
    public void ThenUserVerifyThePage(string ReservationCheckName)
    {
        submissionpage.VerifyReservationCheckPages(ReservationCheckName);
    }

    [Then(@"Check for the Potential Fuzzy matches")]
    public void ThenCheckForThePotentialFuzzyMatches()
    {
        submissionpage.VerifyFuzzyMatches();
    }

    [Then(@"Navigate Back to Parent submission")]
    public void ThenNavigateBackToParentSubmission()
    {
        submissionpage.ThenNavigateBackToParentSubmission();
    }

    [Then(@"User Clicked on ""([^""]*)"" button")]
    public void ThenUserClickedOnButton(string ButtonName)
    {
        submissionpage.ThenUserClickedOnButton(ButtonName);
    }

    [Then(@"User Verify ""([^""]*)"" button from the View Layout")]
    public void ThenUserVerifyButtonFromTheViewLayout(string ButtonName)
    {
        submissionpage.ThenUserVerifyButtonFromTheViewLayout(ButtonName);
    }


    [Then(@"Clone Submission window is displayed and User clicked on Punitive Damages button")]
    public void ThenCloneSubmissionWindowIsDisplayedAndUserClickedOnPunitiveDamagesButton()
    {
        submissionpage.ThenCloneSubmissionWindowIsDisplayedAndUserClickedOnPunitiveDamagesButton();
    }

    [Then(@"Cloned Submission Record is created successfully")]
    public void ThenClonedSubmissionRecordIsCreatedSuccessfully()
    {
        submissionpage.ThenClonedSubmissionRecordIsCreatedSuccessfully();
    }

    [Then(@"User verify cloned submission data")]
    public void ThenUserVerifyClonedSubmissionData()
    {
        submissionpage.ThenUserVerifyClonedSubmissionData();
    }

    [Then(@"User perform Ice Check in Submission (.*) Page")]
    public void ThenUserPerformIceCheckInSubmissionPage(string p0)
    {
        submissionpage.ThenUserPerformIceCheckInSubmissionPage();
    }

    [Then(@"User Updated the Client Information")]
    public void ThenUserUpdatedTheClientInformation()
    {
        string clientName = ExcelValue("Update Client");
        submissionpage.ThenUserUpdatedTheClientInformation(clientName);
    }

    [Then(@"User Verify updated Client is updated")]
    public void ThenUserVerifyUpdatedClientIsUpdated()
    {
        submissionpage.ThenUserVerifyUpdatedClientIsUpdated();
    }

    [Then(@"Verify Added Incumbent Insurer in Submission record in '([^']*)'")]
    public void ThenVerifyAddedIncumbentInsurerInSubmissionRecordIn(string Version)
    {
        submissionpage.VerifyIncumbentInsurerInSubmission(Version);
    }

    [Then(@"User Fill the details in Policy tab")]
    public void ThenUserFillTheDetailsInPolicyTab()
    {
        string DateFormate = ExcelValue("Date Format");
        submissionpage.ThenUserFillTheDetailsInPolicyTab(DateFormate);
    }

    [Then(@"User Capture the Starr submission Number in '([^']*)'")]
    public void ThenUserCaptureTheStarrSubmissionNumberIn(string p0)
    {
        submissionpage.ThenUserCaptureTheStarrSubmissionNumberIn();
    }

    [When(@"User Selected the Submission which is in Block stage")]
    public void WhenUserSelectedTheSubmissionWhichIsInBlockStage()
    {
        submissionpage.WhenUserSelectedTheSubmissionWhichIsInBlockStage();
    }

    [Then(@"User Clicked on Release Block button in Release block page")]
    public void ThenUserClickedOnReleaseBlockButtonInReleaseBlockPage()
    {
        submissionpage.ThenUserClickedOnReleaseBlockButtonInReleaseBlockPage();
    }

    [Then(@"User Navigated to submission which is released")]
    public void ThenUserNavigatedToSubmissionWhichIsReleased()
    {
        submissionpage.ThenUserNavigatedToSubmissionWhichIsReleased();
    }

    [Then(@"User Fill the details required for MidTerm Broker By passing new Producer information")]
    public void ThenUserFillTheDetailsRequiredForMidTermBrokerByPassingNewProducerInformation()
    {
        string[] data =
        {
            ExcelValue("New Licenced Producer"),
            ExcelValue("New Producer Contact"),
            ExcelValue("Date Format")
        };
        submissionpage.ThenUserFillTheDetailsRequiredForMidTermBrokerByPassingNewProducerInformation(data);
    }

    [Then(@"User verify the fields which are updated during Midterm broker")]
    public void ThenUserVerifyTheFieldsWhichAreUpdatedDuringMidtermBroker()
    {
        string[] data =
       {
            ExcelValue("New Licenced Producer"),
            ExcelValue("New Producer Contact"),
            ExcelValue("Date Format")
        };
        submissionpage.ThenUserVerifyTheFieldsWhichAreUpdatedDuringMidtermBroker(data);
    }

    [Then(@"User Verify Policy number is Assigned in the submission")]
    public void ThenUserVerifyPolicyNumberIsAssignedInTheSubmission()
    {
        submissionpage.ThenUserVerifyPolicyNumberIsAssignedInTheSubmission();
    }


    [Then(@"User added Pre Defined Item")]
    public void ThenUserAddedPreDefinedItem()
    {
        submissionpage.ThenUserAddedPreDefinedItem();
    }

    [Then(@"User Deleted the Pre Defined Item")]
    public void ThenUserDeletedThePreDefinedItem()
    {
        submissionpage.ThenUserDeletedThePreDefinedItem();
    }


    [Then(@"User Updated the Client to check the Validaiton - '([^']*)'")]
    public void ThenUserUpdatedTheClientToCheckTheValidaiton_(string p0)
    {
        //string data = ExcelValue("Update Client");
        submissionpage.UpdateClientFieldValue(p0);
    }

    [Then(@"User Updated '([^']*)' field")]
    public void ThenUserUpdatedField(string FieldName)
    {
        submissionpage.ThenUserUpdatedRequiredField(FieldName);
    }

     [Then(@"User Updated '([^']*)' field with values '([^']*)'")]
    public void ThenUserUpdatedFieldWithValues(string FieldName, string FieldValue)
    {
        submissionpage.ThenUserUpdatedRequiredField(FieldName, FieldValue);
    }

    [Then(@"User Updated the value for the field ""([^""]*)""")]
    public void ThenUserUpdatedTheValueForTheField(string FieldName)
    {
        string data = ExcelValue("Line of Business Update");
        submissionpage.ThenUserUpdatedTheValueForTheField(FieldName,data);
    }


    [Then(@"User clicked on Save button in Update submission to Renewal")]
    public void ThenUserClickedOnSaveButtonInUpdateSubmissionToRenewal()
    {
        submissionpage.ThenUserClickedOnSaveButtonInUpdateSubmissionToRenewal();
    }

    [Then(@"User Verify the error message for Submission Renewal as ""([^""]*)""")]
    public void ThenUserVerifyTheErrorMessageForSubmissionRenewalAs(string errorMsg)
    {
        submissionpage.VerifyErrorMessageUpdateRenewalSubmissionTypeToRenewal(errorMsg);
    }    


    [Then(@"User get the value of the field '([^']*)'")]
    public void ThenUserGetTheValueOfTheField(string FieldName)
    {
        submissionpage.ThenUserGetTheValueOfTheField(FieldName);
    }


    [Then(@"User Updated the value for '([^']*)' and '([^']*)'")]
    public void ThenUserUpdatedTheValueForAnd(string Field1, string Field2)
    {
        string data = ExcelValue("Production Office Carrier");
        submissionpage.ThenUserUpdatedTheValueForAnd(Field1, Field2, data);
    }


    [Then(@"User Selected the Stage as '([^']*)' from View Mode and Clicked on Mark as current stage button")]
    public void ThenUserSelectedTheStageAsFromViewModeAndClickedOnMarkAsCurrentStageButton(string StaeName)
    {
        submissionpage.ThenUserSelectedTheStageAsFromViewModeAndClickedOnMarkAsCurrentStageButton(StaeName);
    }

    [Then(@"User Move the record stage from '([^']*)' To '([^']*)' from View Mode")]
    public void ThenUserMoveTheRecordStageFromToFromViewMode(string OldStage, string NewStage)
    {
        submissionpage.ThenUserSelectedTheStageAsFromViewModeAndClickedOnMarkAsCurrentStageButton(NewStage);
    }

    [Then(@"User Reset the Record stage to '([^']*)'")]
    public void ThenUserResetTheRecordStageTo(string stageName)
    {
        submissionpage.ResetSubmissionStageForExecution(stageName);
    }















}
