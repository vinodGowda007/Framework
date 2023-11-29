Feature: A110_Forecast_FAC_RI_PRM_Is_Required

@Sanity @Regression
Scenario:if “Forecast FAC RI PRM” OR “FAC RI PRM” OR “Policy Ceding Commission Amount” field is populated the Purchased External FAC Reinsurance field should be Yes 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation : A110_Forecast_FAC_RI_PRM_Is_Required
	Then User Clicked on "Edit" button
	Then User Updated 'Policy Ceding Commission Amount,FAC RI PRM,Purchased External FAC Reinsurance,Forecast FAC RI PRM' field with values 'Random,Random,--None--,Random'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "“FAC Premium, Ceding Commission, and/or Forecast FAC RI PRM has been populated, but the Purchased External FAC Reinsurance indicator has not been selected, please review”"
	Then User Clicked on Cancel button

Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
	| Scenario2  | Global Construction          |
	| Scenario3  | Global Offshore              |
	| Scenario4  | Inland Marine - SMA          |
	| Scenario5  | International Onshore        |
	| Scenario6  | International Property       |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	| Scenario9  | SGS - Inland Marine          |
	| Scenario10 | SGS - Onshore                |
	| Scenario11 | STNA - Starr Specialty Lines |
	| Scenario12 | SCI - Crisis Management      |
	| Scenario13 | SCI - Cyber                  |
	| Scenario14 | SCI - Environmental          |
	| Scenario15 | SCI - Financial Lines        |
	| Scenario16 | SCI - GL Misc                |
	| Scenario17 | SCI - Healthcare             |