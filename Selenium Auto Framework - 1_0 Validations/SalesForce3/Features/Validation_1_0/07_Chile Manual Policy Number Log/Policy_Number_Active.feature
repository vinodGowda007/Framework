Feature: Policy_Number_Active

@Sanity
Scenario Outline: Details of a Policy Number record may only be changed when Status is Available.

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

## Logout from URL
	Then User log out from User and Login as Administrator

#Home Page-SF
	When User Navigated to "Chile Manual Policy Number Logs"
	Then User Refresh the page

# Validation Rule - Provide_Valid_Email_Id
	Then User Clicked on "New" button
	Then User Fill the value in Chile Manual Policy Number log
	Then User added the email id as "Test@gmail.com"
	#Then user fill remainng fields in Chile Manual Policy Number log form
	Then User clicked on Save button
	Then User Refresh the page

	Then User Clicked on "Edit" button
	Then user updated the field "Insured" in Chile Manual Policy record
	Then User clicked on Save button to verify Error Message
	Then user verify the error message for client on Page Level "Details of a Policy Number record may only be changed when Status is Available."

	
	


	Examples:
	| Scenarios | 
	| Scenario1 | 

