Feature: 03_Contacts_1_0


@Sanity
Scenario: 1 : As a UW/UWA I want to Create/Update Producer Contact 
#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
	Then User Refresh the page

#Client Page
	Then User Navigated to 'Contacts' Page
	Then User Clicked on "New" button
	Then User selected the contact record type as "Contact Record Type" 
	Then User Filled the details in Contacts creation page in '1.0'
	Then User clicked on Save button and verify contact record is created

	Examples:
	| Scenarios | 
	| Scenario2 |
	



