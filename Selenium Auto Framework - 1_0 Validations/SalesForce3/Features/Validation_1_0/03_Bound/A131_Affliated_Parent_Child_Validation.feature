Feature: A131_Affliated_Parent_Child_Validation


@Sanity @Regression
Scenario: 	Total 100pct EPI/GWP must be 0 or negative for a Flat or Endorsement Cancellation 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
	Then User Refresh the page

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Upate the value of the field "Affiliated"
	Then User Clicked on "Edit" button
	Then User Updated 'Affiliated ?' field with values 'No'
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Prospect"
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

# Stage Progression to Bound
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Bound"
	Then User Refresh the page

# Endorsement creation 
	Then User Clicked on "New Endorsements" button
	And User Fill the values in Endorsement creation page for "Endorsement"
	Then User Updated 'Affiliated ?' field 
	#Then User Updated 'Affiliated ?' field with values 'Yes'
	Then User clicked on save button for validation
	Then Verify the Error messages for Backtracking as "Affliated cannot be Yes, if the Parent Submission Affliated Value is not Yes. ( SGS Tab )"
	Then User Refresh the page

# Section creation 
	Then User Clicked on "New Section" button
	And User Fill the values in Section creation page
	Then User Updated 'Affiliated ?' field 
	Then User clicked on save button for validation
	Then Verify the Error messages for Backtracking as "Affliated cannot be Yes, if the Parent Submission Affliated Value is not Yes. ( SGS Tab )"
	Then User Refresh the page

# Terrorism
	Then User Clicked on "New Terrorism" button
	Then User Fill the values in Terrorism creation page
	Then User Updated 'Affiliated ?' field
	Then User clicked on save button for validation
	Then Verify the Error messages for Backtracking as "Affliated cannot be Yes, if the Parent Submission Affliated Value is not Yes. ( SGS Tab )"
	Then User Refresh the page

# Reset Submission Record Stage to Bound
	Then User Reset the Record stage to 'Prospect'

# Upate the value of the field "Affiliated"
	Then User Clicked on "Edit" button
	Then User Updated 'Affiliated ?' field with values 'Yes'
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Prospect"


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