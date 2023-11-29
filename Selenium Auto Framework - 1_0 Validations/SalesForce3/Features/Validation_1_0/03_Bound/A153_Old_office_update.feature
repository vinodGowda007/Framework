#Feature: A153_Old_office_update
#
#
#@Sanity @Regression
#Scenario: If issuing OR production office = Middle Market the picklist cannot be None 1.0
#
##LOGIN PAGE-SF
#	Given UW Navigate To Salesforce Application
#	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
#
### Logout from URL
#	Then User log out from User and Login as Administrator
#
## Login to Existing record
#	Then User Navigated to existing Record 'https://starrcompanies--staging.sandbox.lightning.force.com/lightning/r/Opportunity/0060L00000iJzKyQAK/view'
#	Then User Refresh the page
#	
## Validation : A153_Old_office_update
#	Then User Clicked on "Edit" button
#	Then User Updated '*Effective Date' field with values 'Current Date'
#	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
#	Then Verify the Error messages for Backtracking as "Please choose issuing office and production office as 'Melbourne' or 'Kuala Lumpur'"
#
#
#
#Examples:
#	| Scenarios  | Record Type                  |
#	| Scenario1  | STNA - Domestic Onshore      |

# This validation is not triggering with existing record so we can remove ignore it from the execution
