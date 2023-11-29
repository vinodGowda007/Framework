#Feature: A63_ST_User_MayNot_Modify_EndorseRenew
#
#
#@Sanity @Regression
#Scenario: You may not modify the Related Submission Name, Parent Submission, (Local) Policy Number - Expiring, Line of Business, Occupancy, or Subclass 1, 2, or 3 (Sub Details Tab), Expired EPI on an Endorsement or Renewal (or Renewal/Section for Global Offshore )
#
##LOGIN PAGE-SF
#	Given UW Navigate To Salesforce Application
#	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
#
## User Navigated to Created submission record
#	Then User Navigated to the created submission 1.0 record for 'Validation'
#	Then User Refresh the page
#
#	Then User Reset the Record stage to 'Prospect'
#	Then User Refresh the page
#
#	Then User Clicked on "Edit" button
#	Then User Updated 'Local Policy Number – Expiring' field with values '1234556'
#	Then User clicked on Save button in submission page
#
## Reset Submission Record Stage to Prospect
#	Then User Reset the Record stage to 'Quoted'
#	Then User Refresh the page
#
## Stage Progression to Bound
#	Then User capture the value of 'Starr EPI/GWP'
#	Then User Clicked on "Edit" button
#	When User performed Stage progression from "Quoted" to "Bound"
#	Then User clicked on Save button in submission page
#	Then User Verified submission record status changed to "Bound"
#	Then User Refresh the page
#
## Create Section under Renewal Submission
#	Then User Clicked on "New Section" button
#	And User Fill the values in Section creation page
#	Then User Updated 'FAC RI PRM In Child Records' field
#	Then User clicked on Save button in Section creation page
#	Then User Refresh the page
#
#	Then User Clicked on "Edit" button
#	Then User Updated 'Local Policy Number – Expiring' field with values '23452345'
#	Then User clicked on Save button in submission page
#	Then User Refresh the page
#
##Validation : A63_ST_User_MayNot_Modify_EndorseRenew
#	Then User Clicked on "Edit" button
#	Then User Updated 'Local Policy Number – Expiring' field with values '123456'
#	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
#	Then Verify the Error messages for Backtracking as "You may not modify the Related Submission Name, Parent Submission, (Local) Policy Number - Expiring, Line of Business, Occupancy, or Subclass 1, 2, or 3 (Sub Details Tab), Expired EPI on an Endorsement or Renewal (or Renewal/Section for Global Offshore )"
#
#	Examples:
#	| Scenarios  | Record Type                  |
#	| Scenario3  | Global Offshore              |
#
#
#
#	@Sanity @Regression
#Scenario: 2 You may not modify the Related Submission Name, Parent Submission, (Local) Policy Number - Expiring, Line of Business, Occupancy, or Subclass 1, 2, or 3 (Sub Details Tab), Expired EPI on an Endorsement or Renewal (or Renewal/Section for Global Offshore )
#
##LOGIN PAGE-SF
#	Given UW Navigate To Salesforce Application
#	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
#
## User Navigated to Created submission record
#	Then User Navigated to the created submission 1.0 record for 'Validation'
#	Then User Refresh the page
#
#	Then User Reset the Record stage to 'Prospect'
#	Then User Refresh the page
#
#	Then User Clicked on "Edit" button
#	Then User Updated 'Local Policy Number – Expiring' field with values '1234556'
#	Then User clicked on Save button in submission page
#
## Reset Submission Record Stage to Prospect
#	Then User Reset the Record stage to 'Quoted'
#	Then User Refresh the page
#
## Stage Progression to Bound
#	Then User capture the value of 'Starr EPI/GWP'
#	Then User Clicked on "Edit" button
#	When User performed Stage progression from "Quoted" to "Bound"
#	Then User clicked on Save button in submission page
#	Then User Verified submission record status changed to "Bound"
#	Then User Refresh the page
#
## Create Section under Renewal Submission
#	Then User Clicked on "New Endorsements" button
#	And User Fill the values in Endorsement creation page for "Endorsement"
#	Then User Updated 'FAC RI PRM In Child Records' field
#	Then User clicked on Save button in Endorsement page
#	Then User Refresh the page
#
##Validation : A63_ST_User_MayNot_Modify_EndorseRenew
#	Then User Clicked on "Edit" button
#	Then User Updated 'Local Policy Number – Expiring' field with values '123456'
#	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
#	Then Verify the Error messages for Backtracking as "You may not modify the Related Submission Name, Parent Submission, (Local) Policy Number - Expiring, Line of Business, Occupancy, or Subclass 1, 2, or 3 (Sub Details Tab), Expired EPI on an Endorsement or Renewal (or Renewal/Section for Global Offshore )"
#
#	Examples:
#	| Scenarios  | Record Type                  |
#	| Scenario1  | STNA - Domestic Onshore      |
#	| Scenario5  | International Onshore        |
#	| Scenario6  | International Property       |
#	| Scenario11 | STNA - Starr Specialty Lines |
#	| Scenario12 | SCI - Crisis Management      |
#	| Scenario13 | SCI - Cyber                  |
#	| Scenario14 | SCI - Environmental          |
#	| Scenario15 | SCI - Financial Lines        |
#	| Scenario16 | SCI - GL Misc                |
#	| Scenario17 | SCI - Healthcare             |