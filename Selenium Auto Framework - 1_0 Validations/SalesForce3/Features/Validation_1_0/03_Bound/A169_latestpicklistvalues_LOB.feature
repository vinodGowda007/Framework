Feature: A169_latestpicklistvalues_LOB

@Sanity @Regression
Scenario: New picklist values has to be updated for LOB for old records . Record Type = Global Construction and SGS Construction.

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

## Logout from URL
	Then User log out from User and Login as Administrator

# Login to Existing record
	Then User Navigated to existing Record 'https://starrcompanies--staging.sandbox.lightning.force.com/lightning/r/Opportunity/0060L00000iK2PHQA0/view'
	Then User Refresh the page
	
# Validation : A169_latestpicklistvalues_LOB
	Then User Clicked on "Edit" button
	Then User Updated 'Policy Number - Current,Subclass 1' field with values 'Blank'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Please choose pick latest value on Line of Business and Occupancy fields, not just current record but also related Parent Submission record as well."


Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
