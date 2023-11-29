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
    [NUnit.Framework.DescriptionAttribute("A126_TotalSharePercentage")]
    public partial class A126_TotalSharePercentageFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "A126_TotalSharePercentage.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Validation_1_0/01_Prospect and Working", "A126_TotalSharePercentage", null, ProgrammingLanguage.CSharp, featureTags);
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
        [NUnit.Framework.DescriptionAttribute("The total share % is sum of UCB Share % + Production Office Share %. The value sh" +
            "ould be always equal to 100% 1.0")]
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
        public void TheTotalShareIsSumOfUCBShareProductionOfficeShare_TheValueShouldBeAlwaysEqualTo1001_0(string scenarios, string recordType, string[] exampleTags)
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
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The total share % is sum of UCB Share % + Production Office Share %. The value sh" +
                    "ould be always equal to 100% 1.0", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
#line 11
 testRunner.Then("User Navigated to the created submission 1.0 record for \'Validation\' for the stag" +
                        "e \'Prospect\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 14
 testRunner.Then("User Reset the Record stage to \'Prospect\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 17
 testRunner.Then("User Clicked on \"Edit\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 18
 testRunner.Then("User Updated \'Purchase External FAC (UCB),FAC RI PRM (UCB),Policy Ceding Commissi" +
                        "on (UCB)\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 19
 testRunner.Then("User Updated \'Underwriting Credit Branch\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 20
 testRunner.Then("User Updated \'UCB Share %\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 21
 testRunner.Then("User clicked on Save button during Edit Mode in Stage Progression for backtrackin" +
                        "g", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 22
 testRunner.Then("Verify the Error messages for Backtracking as \"UCB Share % and Production Office " +
                        "Share % Summation should always be equal to 100%. ( SGS Tab )\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
