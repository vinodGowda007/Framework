Feature: A130_Shell_policy_Affliated_SGS

#Cancelled is pending

@Sanity @Regression
Scenario: For "Global" Implementation: Submission Type New/Renewal Check fields if null before going to Quoted or Bound  

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

# Validation : A130_Shell_policy_Affliated_SGS
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	Then User Updated 'Shell Policy,Affiliated ?' field
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Shell Policy, Affiliated are required in Quoted, Bound and Cancelled stages. ( SGS Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Validation : A130_Shell_policy_Affliated_SGS
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Shell Policy,Affiliated ?' field
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Shell Policy, Affiliated are required in Quoted, Bound and Cancelled stages. ( SGS Tab )"


	Examples:
	| Scenarios  | Record Type                  |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	| Scenario9  | SGS - Inland Marine          |
	| Scenario10 | SGS - Onshore                |
