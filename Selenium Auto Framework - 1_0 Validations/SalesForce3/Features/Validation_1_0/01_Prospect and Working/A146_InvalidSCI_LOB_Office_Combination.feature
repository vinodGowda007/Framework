Feature: A146_InvalidSCI_LOB_Office_Combination

@Sanity @Regression
Scenario: Invalid Line of Business, Subclass1 combination for the selected office. Check Parent Submission if it is an Endorsement or Check Expiring Submission if it is a Renewal. ( Subclass1 - Details Tab )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule : A146_InvalidSCI_LOB_Office_Combination
	Then User Clicked on "Edit" button
	Then User Updated 'Line of Business,Issuing Office,Subclass 1' field with values 'Crisis Management,Random,MAJOR'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Invalid Line of Business, Subclass1 combination for the selected office. Check Parent Submission if it is an Endorsement or Check Expiring Submission if it is a Renewal. ( Subclass1 - Details Tab )"

Examples:
	| Scenarios  | Record Type             |
	| Scenario12 | SCI - Crisis Management |

#----------------------------------------------------------------------------------------------------------------------------------------------------------