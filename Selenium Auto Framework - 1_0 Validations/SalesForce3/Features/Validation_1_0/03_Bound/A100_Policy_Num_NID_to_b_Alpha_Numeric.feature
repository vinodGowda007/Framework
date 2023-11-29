Feature: A100_Policy_Num_NID_to_b_Alpha_Numeric


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

#  Validation : A100_Policy_Num_NID_to_b_Alpha_Numeric
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Network Identification (NID)' field with values '&@#$%'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Policy Number and Network Identification (NID) has to be alpha numeric, required in Bound and cannot contain special characters. - Field ( Policy Number - Current : Submission Details Tab // Network Identification(NID) - SGS Tab )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	| Scenario9  | SGS - Inland Marine          |
	| Scenario10 | SGS - Onshore                |
