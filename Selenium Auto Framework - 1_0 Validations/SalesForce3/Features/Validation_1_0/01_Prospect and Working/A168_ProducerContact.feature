Feature: A168_ProducerContact

@Sanity @Regression
Scenario: Any Submissions associated with Japan should be using the generic Producer Contact “Japan Contact,” (UW/Producer Tab) please update accordingly. Open a Remedy ticket if you are still experiencing issues.

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Attach Assumed Insurer into Submission
	Then User Navigated to "Policy Info" Tab
	Then User Clicked on Click Here link to Add Assumed Insurers
	Then User Selected the Assumed Insurer
	Then User Clicked on Save button in Add Assumed Insurer
	Then User selected the value in the dropdown
	Then User Refresh the page

# Validation Rule : A168_ProducerContact
	Then User Clicked on "Edit" button
	Then User Updated 'Licensed Producer,Placing Producer Contact' field with values 'JLT Japan Limited,Starr Japan'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Any Submissions associated with Japan should be using the generic Producer Contact “Japan Contact,” (UW/Producer Tab) please update accordingly. Open a Remedy ticket if you are still experiencing issues."

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