Feature: A50_ST_Create_New_Submission

@Sanity @Regression
Scenario: Stage is not valid during initial creation of the Submission. ( Submission Details Tab )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
	Then User Refresh the page
	Then User Refresh the page

# Creation of Submission Record
	Then User Navigated to 'Submissions' Page
	Then User Clicked on "New" button
	Then User selected the record type as "Record Type 1.0"
	Then User fill the data in Submission version one creation record for 'Assumed'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "ICE Status cannot be Unchecked on the Submission and Client$Stage is not valid during initial creation of the Submission. ( Submission Details Tab )"


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

