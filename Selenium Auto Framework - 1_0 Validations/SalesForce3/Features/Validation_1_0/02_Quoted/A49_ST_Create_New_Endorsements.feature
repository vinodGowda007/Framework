Feature: A49_ST_Create_New_Endorsements


@Sanity @Regression
Scenario: Required for Bound stage - Field ( Framework ST Risk Code - Policy Information Tab - Framework ST Risk Code )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Quoted'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Stage progression Working
	Then User Selected the Stage as 'Working' from View Mode and Clicked on Mark as current stage button

# Verify Submission Working
	Then User Verified submission record status changed to "Working"

# Stage progression Quoted
	Then User Selected the Stage as 'Quoted' from View Mode and Clicked on Mark as current stage button

# Verify Submission Status Quoted
	Then User Verified submission record status changed to "Quoted"

# Quoted to Lost
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Lost/NTU"
	Then User Update the Declined Reason in the submission
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Lost/NTU"
	Then User Refresh the page

# Validation: A49_ST_Create_New_Endorsements
	Then User Clicked on "New Endorsements" button
	Then Verify the Error messages "Endorsement can be created on a Bound Submission only"
	Then User Refresh the page

# Lost to Quoted

	Then User Clicked on "Edit" button
	When User performed Stage progression from "Lost/NTU" to "Quoted"
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Quoted"
	Then User Refresh the page


# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

Examples:
	| Scenarios  | Record Type                  |
	| Scenario2  | Global Construction          |
	| Scenario3  | Global Offshore              |
	| Scenario5  | International Onshore        |
	| Scenario6  | International Property       |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	| Scenario10 | SGS - Onshore                |
