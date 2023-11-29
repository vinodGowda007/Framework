Feature: A138_Local_Country_Required


@Sanity @Regression
Scenario: For "Global" Implementation: Check fields if null before going to Quoted or Bound Submission of Type New/Renewal

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

# Stage Progression to Bound
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Bound"
	Then User Refresh the page

# Validation Rule - A138_Local_Country_Required
	Then User Clicked on "New Section" button
	And User Fill the values in Section creation page
	Then User Updated 'Local Country,Shell Policy' field with values '--None--,Yes'
	Then User clicked on save button for validation
	Then Verify the Error messages for Backtracking as "Local Country is required on Sections for SGS. ( SGS Tab )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	| Scenario9  | SGS - Inland Marine          |
	| Scenario10 | SGS - Onshore                |
