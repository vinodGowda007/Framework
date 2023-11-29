Feature: Required_Fields_for_Active_Stage

@Sanity
Scenario Outline: Following fields are required for Active Status. 1)Insured 2)RUT Insured 3)LOB 4)Effective Date 5)Expiration Date 6)Representante Legal 7)Correo Electronico Representante Legal

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
	#Then User Fill the value in Chile Manual Policy Number log
	Then User added the email id as "Test@gmail.com"
	Then User clicked on Save button to verify Error Message
	Then user verify the error message for client on Page Level "Following fields are required for Active Status. 1)Insured 2)RUT Insured 3)LOB 4)Effective Date 5)Expiration Date 6)Representante Legal 7)Correo Electronico Representante Legal"

	
	


	Examples:
	| Scenarios | 
	| Scenario1 | 



	
