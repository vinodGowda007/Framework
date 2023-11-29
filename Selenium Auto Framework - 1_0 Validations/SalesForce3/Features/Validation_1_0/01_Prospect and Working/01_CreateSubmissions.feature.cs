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
    [NUnit.Framework.DescriptionAttribute("01_CreateSubmissions")]
    public partial class _01_CreateSubmissionsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "01_CreateSubmissions.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Validation_1_0/01_Prospect and Working", "01_CreateSubmissions", null, ProgrammingLanguage.CSharp, featureTags);
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
        [NUnit.Framework.DescriptionAttribute("01  Create Submission")]
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
        public void _01CreateSubmission(string scenarios, string recordType, string[] exampleTags)
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
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("01  Create Submission", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 4
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
 testRunner.Given("UW Navigate To Salesforce Application", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 8
 testRunner.When(string.Format("UW enters the login credentials in single sign in to test {0} using \"Under Writer" +
                            "\"", scenarios), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 9
 testRunner.Then("User Refresh the page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 12
 testRunner.Then("User Navigated to \'Submissions\' Page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 13
 testRunner.Then("User Clicked on \"New\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 14
 testRunner.Then("User selected the record type as \"Record Type 1.0\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 15
 testRunner.Then("User fill the data in Submission version one creation record for \'Assumed\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 16
 testRunner.Then("User clicked on Save button in submission page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 17
 testRunner.Then("User verify the created submission record for \'Validation\' for \'Prospect\' Stage", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 18
 testRunner.Then("User Refresh the page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 21
 testRunner.Then("User verify the Ice Check status \'Before\' performing Ice Check action", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 22
 testRunner.Then("User Clicked on \"Ice Check\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 23
 testRunner.Then("User clicked on Refresh button in Submission page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 24
 testRunner.Then("User Refresh the page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 25
 testRunner.Then("User verify the Ice Check status \'After\' performing Ice Check action", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 28
 testRunner.Then("User Clicked on \"License Check\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 29
 testRunner.Then("Clicked on Close button during Licence Check", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 30
 testRunner.Then("Verify Licence Check status is updated for \'Submission1_0\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 31
 testRunner.Then("User Refresh the page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 35
    testRunner.Then("User Navigated to \"Policy Info\" Tab", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 36
    testRunner.Then("User Clicked on Click Here link to Add Assumed Insurers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 37
    testRunner.Then("User Selected the Assumed Insurer", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 38
    testRunner.Then("User Clicked on Save button in Add Assumed Insurer", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 39
    testRunner.Then("User selected the value in the dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 40
    testRunner.Then("User Navigated to \"Policy Info\" Tab", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 41
    testRunner.Then("User Refresh the page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 42
    testRunner.Then("User Navigated to \"Policy Info\" Tab", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 43
    testRunner.Then("User verify the added Assumed Insurer in Submission", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
