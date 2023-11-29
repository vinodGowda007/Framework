Feature: B_A23_License_Number_is_Required


@Sanity @Regression
Scenario: 	It is required at Bound when the Issuing Company is Starr Surplus Lines Insurance Company

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

	Then User Clicked on "Edit" button
	Then User Updated '*Client Name' field with values 'Validation Client'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Stage progression Working
	Then User Selected the Stage as 'Working' from View Mode and Clicked on Mark as current stage button

# Verify Submission Working
	Then User Verified submission record status changed to "Working"

#  Validation : A23_License_Number_is_Required
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Company,License Number' field with values 'Starr Surplus Lines Insurance Company,Blank'
	Then User clicked on Save button in submission page
	Then User Refresh the page


# Reset Submission Record Stage 
	Then User Reset the Record stage to 'Quoted'
#	
#  Validation : A23_License_Number_is_Required
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "License Number is Required at Bound Stage ( UW and Producer Info Tab )"
#	Then User Clicked on Cancel button
#	Then User Refresh the page
#
## User Navigated to Created submission record
#	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'
#
## Reset Submission Record Stage 
#	Then User Reset the Record stage to 'Prospect'
#
## Reset the client 
#	Then User Clicked on "Edit" button
#	Then User Updated 'Client Name' field with values 'Created Client'
#	Then User clicked on Save button in submission page


Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
	| Scenario2  | Global Construction          |
	#| Scenario3  | Global Offshore              |
	| Scenario4  | Inland Marine - SMA          |
	| Scenario5  | International Onshore        |
	#| Scenario6  | International Property       |Starr Surplus Lines Insurance Company IS NOT PRESENT IN THE PICKLIST hence these 2 are not automatable / out of scope
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