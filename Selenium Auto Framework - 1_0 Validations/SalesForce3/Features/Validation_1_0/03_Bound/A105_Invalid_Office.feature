Feature: A105_Invalid_Office


@Sanity @Regression
	Scenario: Invalid Issuing/Production Office!

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

## Logout from URL
	#Then User log out from User and Login as Administrator

# Login to Existing record
	Then User Navigated to existing Record 'https://starrcompanies--staging.sandbox.lightning.force.com/lightning/r/Opportunity/0060L00000plSTuQAM/view'
	Then User Refresh the page
	
# Validation : A105_Invalid_Office
	Then User Clicked on "Edit" button
	Then User Updated 'Division,Subdivision,Program' field with values 'Random,Random,Random'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Invalid Issuing/Production Office!"



Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
