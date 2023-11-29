Feature: A145_SCIFinanciaLines_Subclass1_required

@Sanity @Regression
Scenario: Please choose proper Subclass1 for the selected Line of Business. Check Parent Submission if it is an Endorsement or Check Expiring Submission if it is a Renewal. ( Subclass1 - Details Tab )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule : A145_SCIFinanciaLines_Subclass1_required
	Then User Clicked on "Edit" button
	Then User Updated 'Line of Business,Issuing Office,Subclass 1' field with values 'Financial Lines,Tokyo,NON-MAJOR'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Please choose proper Subclass1 for the selected Line of Business. Check Parent Submission if it is an Endorsement or Check Expiring Submission if it is a Renewal. ( Subclass1 - Details Tab )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario15 | SCI - Financial Lines        |
