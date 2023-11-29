Feature: A48_ST_Clearance_Indicator_Check

@Sanity @Regression
Scenario: Must be checked before proceed to working, bound or quoted

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Working'
	Then User Refresh the page

# Validation : A48_ST_Clearance_Indicator_Check
	Then User Clicked on "Edit" button
	Then User Updated 'Clearance Indicator' field
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Clearance Indicator must be checked before proceeding. ( Submission Details Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Stage progression Quoted
	Then User Selected the Stage as 'Quoted' from View Mode and Clicked on Mark as current stage button

# Verify Submission Status Quoted
	Then User Verified submission record status changed to "Quoted"

# Validation : A48_ST_Clearance_Indicator_Check
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	Then User Updated 'Clearance Indicator' field
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Clearance Indicator must be checked before proceeding. ( Submission Details Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

#  Validation : A46_ST_Bound_Stage_Valid_part3
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Clearance Indicator' field
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Clearance Indicator must be checked before proceeding. ( Submission Details Tab )"


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