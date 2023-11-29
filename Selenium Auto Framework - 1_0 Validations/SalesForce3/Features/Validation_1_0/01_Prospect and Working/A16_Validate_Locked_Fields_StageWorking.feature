Feature: A16_Validate_Locked_Fields_StageWorking

@Sanity @Regression
Scenario: Updates on locked fields are not allowed at "Working" stage

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Stage progression Working
	Then User Selected the Stage as 'Working' from View Mode and Clicked on Mark as current stage button

# Verify Submission Working
	Then User Verified submission record status changed to "Working"

# Validation : A16_Validate_Locked_Fields_StageWorking
	Then User Clicked on "Edit" button
	Then User Updated 'Client Name' field with values 'Update Client'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Updates on locked fields are not allowed at this stage. - Client ( Submission Details Tab )$Updates on locked fields are not allowed at this stage. ( Submission Details Tab ) - Client - Parent Submission - LOB"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
	| Scenario2  | Global Construction          |
	#| Scenario3  | Global Offshore              |
	| Scenario4  | Inland Marine - SMA          |
	#| Scenario5  | International Onshore        |
	#| Scenario6  | International Property       |
	| Scenario7  | SGS - Construction           |
	#| Scenario8  | SGS - General Property       |
	| Scenario9  | SGS - Inland Marine          |
	#| Scenario10 | SGS - Onshore                |
	| Scenario11 | STNA - Starr Specialty Lines |
	| Scenario12 | SCI - Crisis Management      |
	| Scenario13 | SCI - Cyber                  |
	| Scenario14 | SCI - Environmental          |
	| Scenario15 | SCI - Financial Lines        |
	| Scenario16 | SCI - GL Misc                |
	| Scenario17 | SCI - Healthcare             |