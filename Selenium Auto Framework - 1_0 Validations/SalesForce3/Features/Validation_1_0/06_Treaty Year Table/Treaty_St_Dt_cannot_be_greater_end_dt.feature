Feature: Treaty_St_Dt_cannot_be_greater_end_dt

@Sanity
Scenario Outline: Validates that the account Billing Zip/Postal Code is in 99999 or 99999-9999 format if Billing Country is USA or US.

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
## Logout from URL
	Then User log out from User and Login as Administrator
	#Then User search for "Custom Metadata Types" and Navigate to "Custom Metadata Types"

# Login to Existing record
	Then User Navigated to existing Record 'https://starrcompanies--sf20standb.sandbox.lightning.force.com/lightning/setup/CustomMetadata/page?address=%2Fm0z%3Fsetupid%3DCustomMetadata'

# Then user upated the value of Treate start date
	Then User Clicked on New button in Treaty Year Tables
	Then user fill the values in treaty table fields
	Then user clicked on save button in treat table fields
	Then user verify the error message in treaty table as "Treaty Start Date cannot be greater than Treaty end date."
	
	


	Examples:
	| Scenarios | 
	| Scenario1 | 


	
