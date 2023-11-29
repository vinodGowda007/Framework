Feature: Billing_Address_is_Required

@Sanity
Scenario Outline: Billing Address needs to be Complete

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

#Home Page-SF
	Then User Navigated to 'Clients' Page

#CLIENT PAGE - CREATE CLIENT RECORD
	Then User Clicked on "New" button
	Then User Fill the data in Clients creation page using 'Submission1_0'
	Then User clicked on Save button to verify Error Message
	Then user verify the error message for client on Page Level "Please fill in the complete Billing Address"


	Examples:
	| Scenarios | 
	| Scenario1 | 


	
