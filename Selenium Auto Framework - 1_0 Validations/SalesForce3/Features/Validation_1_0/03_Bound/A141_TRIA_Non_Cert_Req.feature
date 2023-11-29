Feature: A141_TRIA_Non_Cert_Req


@Sanity @Regression
Scenario: If issuing OR production office = Middle Market the picklist cannot be None 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

#  Validation : A141_TRIA_Non_Cert_Req
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Issuing Office,TRIA/Non Cert Included' field with values 'Middle Market,--None--'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "If Issuing or Production Office is in Middle Market ,TRIA/Non Cert Included are required. ( Policy Info : TRIA/Non Cert Included || UW/Producer : Producer Contact )"
	Then User Clicked on Cancel button
	Then User Refresh the page

#  Validation : A141_TRIA_Non_Cert_Req
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'UW Region,Production Office,TRIA/Non Cert Included' field with values 'North America,Middle Market,--None--'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "If Issuing or Production Office is in Middle Market ,TRIA/Non Cert Included are required. ( Policy Info : TRIA/Non Cert Included || UW/Producer : Producer Contact )"
	Then User Clicked on Cancel button
	Then User Refresh the page

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