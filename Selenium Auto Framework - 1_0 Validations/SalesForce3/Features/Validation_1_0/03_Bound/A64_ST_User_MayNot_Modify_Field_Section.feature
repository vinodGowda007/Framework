Feature: A64_ST_User_MayNot_Modify_Field_Section


	@Sanity @Regression
Scenario: Issuing Company on the Renewal Submission should match the Issuing Company on the Related Submission. If Section or Terrorism or Endorsement,should match the Parent Submission.Applicable March 17 Onwards. for section

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
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Bound"
	Then User Refresh the page

# Create Section under Renewal Submission
	Then User Clicked on "New Section" button
	And User Fill the values in Section creation page
	Then User Updated 'FAC RI PRM In Child Records' field
	Then User clicked on Save button in Section creation page
	Then User Refresh the page

#Validation : A64_ST_User_MayNot_Modify_Field_Section
	Then User Clicked on "Edit" button
	Then User Updated 'Policy Number - Expiring' field with values 'Random Values'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "You may not modify the Related Submission Name, Parent Submission, Policy Number - Expiring, Local Policy Number Expiring (Submission Details ), Expired EPI, (Premium and Limits Tab ) on a Section.$You may not modify the Related Submission Name, Parent Submission, (Local) Policy Number - Expiring, Line of Business, Occupancy, or Subclass 1, 2, or 3 (Sub Details Tab), Expired EPI on an Endorsement or Renewal (or Renewal/Section for Global Offshore )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
	| Scenario2  | Global Construction          |
	| Scenario3  | Global Offshore              |
	| Scenario5  | International Onshore        |
	| Scenario6  | International Property       |
	| Scenario11 | STNA - Starr Specialty Lines |
	| Scenario12 | SCI - Crisis Management      |
	| Scenario13 | SCI - Cyber                  |
	| Scenario14 | SCI - Environmental          |
	| Scenario15 | SCI - Financial Lines        |
	| Scenario16 | SCI - GL Misc                |
	| Scenario17 | SCI - Healthcare             |