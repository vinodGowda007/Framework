Feature: Valid_US_Billing_Zip_Code

@Sanity
Scenario Outline: Validates that the account Billing Zip/Postal Code is in 99999 or 99999-9999 format if Billing Country is USA or US.

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

#Home Page-SF
	Then User Navigated to 'Clients' Page

#CLIENT PAGE - CREATE CLIENT RECORD
	Then User Clicked on "New" button
	Then User Fill the data in Clients creation page using 'Submission1_0'
	Then User clicked on Save button to verify Error Message
	Then user verify the error message for client as "Zip code must be in 99999 or 99999-9999 format."


	Examples:
	| Scenarios | 
	| Scenario1 | 


	
