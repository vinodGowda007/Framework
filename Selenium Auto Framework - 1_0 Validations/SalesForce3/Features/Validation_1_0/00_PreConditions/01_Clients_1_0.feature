Feature: 01_Clients_1_0

@Sanity
Scenario Outline: Verify whether UnderWriter is able to create  and Update New client in 1.0
#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

#Home Page-SF
	Then User Navigated to 'Clients' Page

#CLIENT PAGE - CREATE CLIENT RECORD
	Then User Clicked on "New" button
	Then User Fill the data in Clients creation page using 'Submission1_0'
	Then User clicked on Save button
	Then New Client record is created successfully
	Then User Refresh the page

# Update Client Record 
	Then User Clicked on "Edit" button
	Then User will update the fields in the client detail page
	And  User clicked on Save button
	Then Client record Updated successfully.
	Then User Refresh the page

# Perform Ice Check
	Then User Clicked on "ICE Check" button
	Then User clicked on Refresh button in Submission page
	Then User Refresh the page
	Then User Verify Ice check status is updated in the clients details page.


	Examples:
	| Scenarios | 
	| Scenario1 | 
	| Scenario2 | 
	| Scenario3 | 


	
