Feature: Valid_Canada_Billing_Zip_Code

@Sanity
Scenario Outline: Validates that the account Billing Zip/Postal Code is in the correct format if Billing Country is Canada.

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

#Home Page-SF
	Then User Navigated to 'Clients' Page

#CLIENT PAGE - CREATE CLIENT RECORD
	Then User Clicked on "New" button
	Then User Fill the data in Clients creation page using 'Submission1_0'
	Then User clicked on Save button to verify Error Message
	Then user verify the error message for client as "Canadian ZIP/Postal code must be in A9A 9A9 format."


	Examples:
	| Scenarios | 
	| Scenario1 | 


	
