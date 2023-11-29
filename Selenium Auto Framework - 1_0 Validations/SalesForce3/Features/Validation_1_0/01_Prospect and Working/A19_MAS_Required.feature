Feature: A19_MAS_Required

@Sanity @Regression
Scenario: Required if Carrier Branch = Singapore OR if Issuing Company = Starr International Insurance (Singapore) Pte. Ltd.1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'
	Then User Refresh the page

# Updated Issuing Company
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Company' field with values 'CVS 1919'
	Then User clicked on Save button in submission page

# Licence Check
	Then User Clicked on "License Check" button
	Then Clicked on Close button during Licence Check
	Then Verify Licence Check status is updated for 'Submission1_0'
	Then User Refresh the page
	Then User Refresh the page

# Stage progression Working
	Then User Selected the Stage as 'Working' from View Mode and Clicked on Mark as current stage button

# Verify Submission Working
	Then User Verified submission record status changed to "Working"
	Then User Refresh the page

# Validation : A19_MAS_Required
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Working" to "Quoted"
	Then User Updated 'Carrier Branch,MAS' field with values 'Singapore,--None--'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Required when Carrier Branch is Singapore Or Issuing Company is Starr International Insurance (Singapore) Pte. Ltd. - Field ( MAS - Submission Details Tab)"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Updated Issuing Company
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Company' field with values 'Starr Indemnity & Liability Company'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Licence Check
	Then User Clicked on "License Check" button
	Then Clicked on Close button during Licence Check
	Then Verify Licence Check status is updated for 'Submission1_0'


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


