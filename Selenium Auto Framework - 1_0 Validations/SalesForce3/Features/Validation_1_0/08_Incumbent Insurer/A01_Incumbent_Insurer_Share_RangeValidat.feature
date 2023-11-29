Feature: A01_Incumbent_Insurer_Share_RangeValidat

@Sanity
Scenario Outline: If the share is Unknown, enable the Share Unknown Checkbox. Share needs to be greater than 0 and less than or equal to 100

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

## Logout from URL
	Then User log out from User and Login as Administrator

#Home Page-SF
	When User Navigated to "Incumbent Insurers"

#Incumbent Insurer PAGE - CREATE Incumbent Insurer RECORD
	Then User clicked on New button in Incumbent Insurers Page
	Then User Fill the value Incumbent Insurers creation page for Validation
	Then User clicked on Save button in Incumbent Insurers Page
	Then user verify the error message for Incumbent Insurer as "If the share is Unknown, enable the Share Unknown Checkbox. Share needs to be greater than 0 and less than or equal to 100"

	
	Examples:
	| Scenarios | 
	| Scenario1 | 

