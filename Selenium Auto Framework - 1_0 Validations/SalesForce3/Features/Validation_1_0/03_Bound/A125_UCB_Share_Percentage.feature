Feature: A125_UCB_Share_Percentage

@Sanity @Regression
Scenario: UCB Share % is required if the UCB is not blank. It is not required if the production office or issuing office is Middle Market. It has to greater or equal to 0% and less than or equal to 100%. 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
	Then User Refresh the page

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Working'
	Then User Refresh the page

# Validation : A125_UCB_Share_Percentage
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	Then User Updated 'Underwriting Credit Branch,UCB Share %' field with values '--None--,Blank'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "UCB Share % must be between 0% and 100%. ( SGS Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Stage progression Quoted
	Then User Selected the Stage as 'Quoted' from View Mode and Clicked on Mark as current stage button

# Verify Submission Status Quoted
	Then User Verified submission record status changed to "Quoted"
	Then User Refresh the page

# Validation : A125_UCB_Share_Percentage
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	Then User Updated 'Underwriting Credit Branch,UCB Share %' field with values '--None--,Blank'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "UCB Share % must be between 0% and 100%. ( SGS Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

#  Validation : A33_Proj_Name_Check_On_Bound
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Bound"
	Then User Refresh the page

# Validation : A125_UCB_Share_Percentage
	Then User Clicked on "Edit" button
	Then User Updated 'Underwriting Credit Branch,UCB Share %' field with values '--None--,Blank'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "UCB Share % must be between 0% and 100%. ( SGS Tab )"

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