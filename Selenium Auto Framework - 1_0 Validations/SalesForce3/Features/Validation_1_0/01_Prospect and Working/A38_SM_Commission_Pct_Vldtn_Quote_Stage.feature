Feature: A38_SM_Commission_Pct_Vldtn_Quote_Stage


@Sanity @Regression
Scenario: For "Global" Implementation: Submission Type New/Renewal Check fields if null before going to Quoted or Bound  

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule - A38_SM_Commission_Pct_Vldtn_Quote_Stage
	Then User Clicked on "Edit" button	
	When User performed Stage progression from "Prospect" to "Working"
	Then User Updated 'Producer Commission %' field with values 'blank'
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Working"
	Then User Refresh the page

# Validation : A38_SM_Commission_Pct_Vldtn_Quote_Stage
	Then User Selected the Stage as 'Quoted' from View Mode and Clicked on Mark as current stage button
	Then Verify the Error messages for Backtracking as "Producer Commission % is required and must be between +/- 100% ( Premium and Limits Tab ) Commission Percentage must be entered when the stage is equal to Quoted or Bound - Field ( Producer Commission % - UW and Producer Info Tab ) True"
	Then User Refresh the page

# Validation Rule - A38_SM_Commission_Pct_Vldtn_Quote_Stage
	Then User Clicked on "Edit" button	
	Then User Updated 'Producer Commission %' field with values 'Random'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

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