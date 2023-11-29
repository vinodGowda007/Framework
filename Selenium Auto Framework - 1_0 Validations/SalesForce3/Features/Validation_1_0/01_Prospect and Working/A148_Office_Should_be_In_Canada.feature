Feature: A148_Office_Should_be_In_Canada

@Sanity @Regression
	Scenario: 01. For the selected Program, either Issuing Office or Production Office should be in Toronto.

	#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

	# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

	# Validation Rule : A148_Office_Should_be_In_Canada
	Then User Clicked on "Edit" button
	Then User Updated 'Program' field with values 'International Property Canada Fronted'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "For the selected Program, either Issuing Office or Production Office should be in Toronto."

Examples:
	| Scenarios  | Record Type                  |
	| Scenario6  | International Property       |


# ------------------------------------------------------------------------------------------------------------------------------------------------
	@Sanity @Regression
	Scenario: 02. For the selected Program, either Issuing Office or Production Office should be in Toronto.

	#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

	# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

	# Validation Rule : A148_Office_Should_be_In_Canada
	Then User Clicked on "Edit" button
	Then User Updated 'Division,Subdivision,Program' field with values 'International Property,International Property,International Property Canada'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "For the selected Program, either Issuing Office or Production Office should be in Toronto."

Examples:
	| Scenarios  | Record Type                  |
	| Scenario8  | SGS - General Property       |
