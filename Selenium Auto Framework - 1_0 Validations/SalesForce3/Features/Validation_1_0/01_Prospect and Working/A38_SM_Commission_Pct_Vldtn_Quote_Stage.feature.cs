﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SaleForce3.Features.Validation_1_0._01_ProspectAndWorking
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("A38_SM_Commission_Pct_Vldtn_Quote_Stage")]
    public partial class A38_SM_Commission_Pct_Vldtn_Quote_StageFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "A38_SM_Commission_Pct_Vldtn_Quote_Stage.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Validation_1_0/01_Prospect and Working", "A38_SM_Commission_Pct_Vldtn_Quote_Stage", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("For \"Global\" Implementation: Submission Type New/Renewal Check fields if null bef" +
            "ore going to Quoted or Bound")]
        [NUnit.Framework.CategoryAttribute("Sanity")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        [NUnit.Framework.TestCaseAttribute("Scenario1", "STNA - Domestic Onshore", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario2", "Global Construction", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario3", "Global Offshore", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario4", "Inland Marine - SMA", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario5", "International Onshore", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario6", "International Property", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario7", "SGS - Construction", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario8", "SGS - General Property", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario9", "SGS - Inland Marine", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario10", "SGS - Onshore", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario11", "STNA - Starr Specialty Lines", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario12", "SCI - Crisis Management", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario13", "SCI - Cyber", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario14", "SCI - Environmental", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario15", "SCI - Financial Lines", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario16", "SCI - GL Misc", null)]
        [NUnit.Framework.TestCaseAttribute("Scenario17", "SCI - Healthcare", null)]
        public void ForGlobalImplementationSubmissionTypeNewRenewalCheckFieldsIfNullBeforeGoingToQuotedOrBound(string scenarios, string recordType, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "Sanity",
                    "Regression"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Scenarios", scenarios);
            argumentsOfScenario.Add("Record Type", recordType);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("For \"Global\" Implementation: Submission Type New/Renewal Check fields if null bef" +
                    "ore going to Quoted or Bound", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 5
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
 testRunner.Given("UW Navigate To Salesforce Application", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 9
 testRunner.When(string.Format("UW enters the login credentials in single sign in to test {0} using \"Under Writer" +
                            "\"", scenarios), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 12
 testRunner.Then("User Navigated to the created submission 1.0 record for \'Validation\' for the stag" +
                        "e \'Prospect\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 13
 testRunner.Then("User Refresh the page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 16
 testRunner.Then("User Reset the Record stage to \'Prospect\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 19
 testRunner.Then("User Clicked on \"Edit\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 20
 testRunner.When("User performed Stage progression from \"Prospect\" to \"Working\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 21
 testRunner.Then("User Updated \'Producer Commission %\' field with values \'blank\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 22
 testRunner.Then("User clicked on Save button in submission page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 23
 testRunner.Then("User Verified submission record status changed to \"Working\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 24
 testRunner.Then("User Refresh the page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 27
 testRunner.Then("User Selected the Stage as \'Quoted\' from View Mode and Clicked on Mark as current" +
                        " stage button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 28
 testRunner.Then(@"Verify the Error messages for Backtracking as ""Producer Commission % is required and must be between +/- 100% ( Premium and Limits Tab ) Commission Percentage must be entered when the stage is equal to Quoted or Bound - Field ( Producer Commission % - UW and Producer Info Tab ) True""", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 29
 testRunner.Then("User Refresh the page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 32
 testRunner.Then("User Clicked on \"Edit\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 33
 testRunner.Then("User Updated \'Producer Commission %\' field with values \'Random\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 34
 testRunner.Then("User clicked on Save button in submission page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 35
 testRunner.Then("User Refresh the page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 38
 testRunner.Then("User Reset the Record stage to \'Prospect\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
