Feature: A02_Choose_either_Insured_Name_or_Other

@Sanity
Scenario Outline: Choose either Insurer Name or Insurer (if Other) but not Both

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

## Logout from URL
	Then User log out from User and Login as Administrator

#Home Page-SF
	When User Navigated to "Incumbent Insurers"

#Incumbent Insurer PAGE - CREATE Incumbent Insurer RECORD
	Then User clicked on New button in Incumbent Insurers Page
	Then user add value in "Insurer (if Other)" Field
	Then User Fill the value Incumbent Insurers creation page for Validation
	#Then User Fill the value Incumbent Insurers creation page for 'New' submission
	Then User clicked on Save button in Incumbent Insurers Page
	Then user verify the error message for client on Page Level "Choose either Insurer Name or Insurer (if Other) but not Both"


	
	Examples:
	| Scenarios | 
	| Scenario1 | 

	
