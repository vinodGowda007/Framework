Feature: A170_Broker_email


@Sanity @Regression
Scenario: For "Global" Implementation: Check fields if null before going to Quoted or Bound Submission of Type New/Renewal

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# Logout from URL
	Then User log out from User and Login as Administrator
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

	Then User Reset the Record stage to 'Prospect'

# Edit record 
	Then User Clicked on "Edit" button
	Then User Updated 'Lynx Date' field with values 'Blank'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Login as User
	Given UW Navigate To Salesforce Application
	Then User enter the "Under Writer" Username

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

# Stage Progression to Bound Validation : A170_Broker_email
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Issuing Office,Placing Producer Contact' field with values 'Middle Market,Vincent Flemming'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "If Issuing or Production Office is in Middle Market ,Broker email are required. (UW/Producer : Producer Contact )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
	| Scenario2  | Global Construction          |
	| Scenario3  | Global Offshore              |
	| Scenario5  | International Onshore        |
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