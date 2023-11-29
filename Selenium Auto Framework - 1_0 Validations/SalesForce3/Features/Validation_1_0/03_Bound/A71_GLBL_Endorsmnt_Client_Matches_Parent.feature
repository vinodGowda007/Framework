Feature: A71_GLBL_Endorsmnt_Client_Matches_Parent

@Sanity @Regression
Scenario: Global: Parent Submission must be specified for an Endorsement Submission. An Endorsement Submission must be attached to the same Client as its Parent Submission.

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

# Validaiton : A26_Match_Policy_Number_for_Endorsements
	Then User Clicked on "New Endorsements" button
	And User Fill the values in Endorsement creation page for "Endorsement"
	Then User Updated 'FAC RI PRM In Child Records' field
	Then User Updated 'Client Name' field with values 'Updated Client Value'
	Then User clicked on save button for validation
	Then Verify the Error messages for Backtracking as "Parent Submission must be specified for an Endorsement Submission. An Endorsement Submission must be attached to the same Client as its Parent Submission. ( Submission Details Tab )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
	| Scenario4  | Inland Marine - SMA          |
	| Scenario9  | SGS - Inland Marine          |
	| Scenario11 | STNA - Starr Specialty Lines |
