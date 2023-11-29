Feature: A46_ST_Bound_Stage_Valid_part3


@Sanity @Regression
Scenario: Required for Bound stage - Field ( Framework ST Risk Code - Policy Information Tab - Framework ST Risk Code )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

#  Validation : A46_ST_Bound_Stage_Valid_part3
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Framework ST Risk Code' field with values '--None--'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Required for Bound stage - Field ( Framework ST Risk Code - Policy Information Tab - Framework ST Risk Code )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario2  | Global Construction          |
	| Scenario3  | Global Offshore              |
	| Scenario5  | International Onshore        |
	| Scenario6  | International Property       |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	| Scenario10 | SGS - Onshore                |
