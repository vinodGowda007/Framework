Feature: 01_CreateSubmissions

@Sanity @Regression
Scenario: 01  Create Submission

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# Creation of Submission Record
	Then User Refresh the page
	Then User Navigated to 'Submissions' Page
	Then User Clicked on "New" button
	Then User selected the record type as "Record Type 1.0"
	Then User fill the data in Submission version one creation record for 'Assumed'
	Then User clicked on Save button in submission page
	Then User verify the created submission record for 'Validation' for 'Bound' Stage
	Then User Refresh the page

# Ice Check 
	Then User verify the Ice Check status 'Before' performing Ice Check action
	Then User Clicked on "Ice Check" button
	Then User clicked on Refresh button in Submission page
	Then User Refresh the page
	Then User verify the Ice Check status 'After' performing Ice Check action

# Licence Check
	Then User Clicked on "License Check" button
	Then Clicked on Close button during Licence Check
	Then Verify Licence Check status is updated for 'Submission1_0'
	Then User Refresh the page


# Attach Assumed Insurer into Submission
    Then User Navigated to "Policy Info" Tab
    Then User Clicked on Click Here link to Add Assumed Insurers
    Then User Selected the Assumed Insurer
    Then User Clicked on Save button in Add Assumed Insurer
    Then User selected the value in the dropdown
    Then User Navigated to "Policy Info" Tab
    Then User Refresh the page
    Then User Navigated to "Policy Info" Tab
    Then User verify the added Assumed Insurer in Submission

# Logout from User
	Then User log out from User and Login as Administrator
#Home Page-SF
	When User Navigated to "Incumbent Insurers"

#Incumbent Insurer PAGE - CREATE Incumbent Insurer RECORD
	Then User clicked on New button in Incumbent Insurers Page
	Then User Fill the value Incumbent Insurers creation page for 'New' submission for "Bound"
	Then User clicked on Save button in Incumbent Insurers Page
	Then Verify New Incumbent Insurer record is created successfully
	Then Verify Added Incumbent Insurer in Submission record in '1.0'



Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
	| Scenario2  | Global Construction          |
	| Scenario3  | Global Offshore              |
	| Scenario4  | Inland Marine - SMA          |
	| Scenario5  | International Onshore        |
	| Scenario6  | International Property       |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	| Scenario9  | SGS - Inland Marine          |
	| Scenario10 | SGS - Onshore                |
	| Scenario11 | STNA - Starr Specialty Lines |
	| Scenario12 | SCI - Crisis Management      |
	| Scenario13 | SCI - Cyber                  |
	| Scenario14 | SCI - Environmental          |
	| Scenario15 | SCI - Financial Lines        |
	| Scenario16 | SCI - GL Misc                |
	| Scenario17 | SCI - Healthcare             |




# ------------------------------------------------------------------------------------------------------------------------------------------------------------------------
#
#
## -------------------------------------------------------------------------------------------------------------------------------------------------------------
#
#@Sanity @Regression
#	Scenario: 09 Validations Rule - 1) A130_Shell_policy_Affliated_SGS -  For Bound
#
##LOGIN PAGE-SF
#	Given UW Navigate To Salesforce Application
#	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
#
## User Navigated to Created submission record
#	Then User Navigated to the created submission 1.0 record
#
## Verify Submission Status before perfming the validaiton
#	Then User Verified submission record status changed to "Quoted"
#
## Validation : A130_Shell_policy_Affliated_SGS
#	Then User Clicked on "Edit" button
#	When User performed Stage progression from "Quoted" to "Bound"
#	Then User Updated 'Shell Policy,Affiliated ?' field
#	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
#	Then Verify the Error messages for Backtracking as "Shell Policy, Affiliated are required in Quoted, Bound and Cancelled stages. ( SGS Tab )"
#
#	Examples:
#	| Scenarios  |
#	| Scenario7  |
#	| Scenario8  |
#	| Scenario9  |
#	| Scenario10 |
#
# #------------------------------------------------------------------------------------------------------------------------------------------------------------------------
#
#
#
#

