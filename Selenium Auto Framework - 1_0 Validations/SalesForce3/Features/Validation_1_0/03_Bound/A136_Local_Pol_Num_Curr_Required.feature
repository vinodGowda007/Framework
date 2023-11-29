Feature: A136_Local_Pol_Num_Curr_Required


@Sanity @Regression
Scenario: For "Global" Implementation: Submission Type New/Renewal Check fields if null before going to Quoted or Bound  

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

#  Validation : A136_Local_Pol_Num_Curr_Required
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Issuing Office,Local Policy Number – Current' field with values 'Santiago,Blank'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Local Policy Number - Current is required in Bound for Santiago and Buenos Aires Offices. ( Submission Details Tab )"


	Examples:
	| Scenarios  | Record Type                  |
	#| Scenario1  | STNA - Domestic Onshore      |NOt applicable for this record type
	| Scenario2  | Global Construction          |
	| Scenario3  | Global Offshore              |
	#| Scenario4  | Inland Marine - SMA          | NOt applicable for this record type
	| Scenario5  | International Onshore        |
	| Scenario6  | International Property       |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	#| Scenario9  | SGS - Inland Marine          | NOt applicable for this record type
	| Scenario10 | SGS - Onshore                |
	#| Scenario11 | STNA - Starr Specialty Lines | NOt applicable for this record type
	| Scenario12 | SCI - Crisis Management      |
	| Scenario13 | SCI - Cyber                  |
	| Scenario14 | SCI - Environmental          |
	| Scenario15 | SCI - Financial Lines        |
	| Scenario16 | SCI - GL Misc                |
	| Scenario17 | SCI - Healthcare             |