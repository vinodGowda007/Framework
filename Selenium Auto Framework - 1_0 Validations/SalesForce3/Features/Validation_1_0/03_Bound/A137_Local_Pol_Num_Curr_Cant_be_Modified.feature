Feature: A137_Local_Pol_Num_Curr_Cant_be_Modified


@Sanity @Regression
Scenario: For "Global" Implementation: Check fields if null before going to Quoted or Bound Submission of Type New/Renewal

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

# Stage Progression to Bound
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Office,Local Policy Number – Current' field with values 'Atlanta,Random'
	Then User clicked on Save button in submission page
	Then User Refresh the page

	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Bound"
	Then User Refresh the page

# Endorsement creation 
	Then User Clicked on "New Endorsements" button
	And User Fill the values in Endorsement creation page for "Endorsement"
	Then User Updated 'FAC RI PRM In Child Records' field
	Then User clicked on Save button in Endorsement page
	Then User Refresh the page
	
#  Validation : A137_Local_Pol_Num_Curr_Cant_be_Modified
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Office,Local Policy Number – Current' field with values 'London,Blank'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Local Policy Number - Current cannot be modified if the issuing or production office is in London and also if the type is endorsement or terrorism"


Examples:
	| Scenarios  | Record Type                  |
	#| Scenario1  | STNA - Domestic Onshore      | Not applicable for this record type
	| Scenario2  | Global Construction          |
	| Scenario3  | Global Offshore              |
	#| Scenario4  | Inland Marine - SMA          | Not applicable for this record type
	| Scenario5  | International Onshore        |
	| Scenario6  | International Property       |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	#| Scenario9  | SGS - Inland Marine          |  Not applicable for this record type
	| Scenario10 | SGS - Onshore                |
	#| Scenario11 | STNA - Starr Specialty Lines | Not applicable for this record type
	| Scenario12 | SCI - Crisis Management      |
	| Scenario13 | SCI - Cyber                  |
	| Scenario14 | SCI - Environmental          |
	| Scenario15 | SCI - Financial Lines        |
	| Scenario16 | SCI - GL Misc                |
	| Scenario17 | SCI - Healthcare             |