Feature: A27_NYFreeZone_Required_StageBound

@Sanity @Regression
Scenario: Please select a value for New York Free Trade Zone. ( Dates/Additional Info Tab )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Quoted'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation : A27_NYFreeZone_Required_StageBound
	Then User Clicked on "Edit" button
	Then User Updated 'Client Name Update' field with values 'Madison Avenue Bridge'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Attach Assumed Insurer into Submission
	Then User Navigated to "Policy Info" Tab
	Then User Clicked on Click Here link to Add Assumed Insurers
	Then User Selected the Assumed Insurer
	Then User Clicked on Save button in Add Assumed Insurer
	Then User selected the value in the dropdown
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

# Validation : A27_NYFreeZone_Required_StageBound
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Issuing Company' field with values 'Starr Indemnity & Liability Company'
	Then User Updated 'New York Free Trade Zone' field with values '--None--'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Please select a value for New York Free Trade Zone. ( Dates/Additional Info Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

	# Validation : A27_NYFreeZone_Required_StageBound
	Then User Clicked on "Edit" button
	Then User Updated 'Client Name' field with values 'Previous Value'
	Then User Updated 'New York Free Trade Zone' field with values 'No'
	Then User clicked on Save button in submission page

Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
	#| Scenario2  | Global Construction          | Not applicable for commented record type
	#| Scenario3  | Global Offshore              |
	#| Scenario4  | Inland Marine - SMA          |
	#| Scenario5  | International Onshore        |
	#| Scenario6  | International Property       |
	#| Scenario7  | SGS - Construction           |
	#| Scenario8  | SGS - General Property       |
	| Scenario9  | SGS - Inland Marine          |
	| Scenario10 | SGS - Onshore                |
	#| Scenario11 | STNA - Starr Specialty Lines |
	#| Scenario13 | SCI - Cyber                  |
	#| Scenario17 | SCI - Healthcare             |
