Feature: A59_GLBL_StageWorking_LockClient

@Sanity @Regression
Scenario: Updates on locked fields are not allowed at this stage.- Client ( Submission Details Tab )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"


# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'
	Then User Refresh the page


# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Stage progression Working
	Then User Selected the Stage as 'Working' from View Mode and Clicked on Mark as current stage button

# Verify Submission Working
	Then User Verified submission record status changed to "Working"

# Validation Rule : A59_GLBL_StageWorking_LockClient
	Then User Clicked on "Edit" button
	Then User Updated 'Client Name' field with values 'New Client Value'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Updates on locked fields are not allowed at this stage. - Client ( Submission Details Tab )$Updates on locked fields are not allowed at this stage. ( Submission Details Tab ) - Client - Parent Submission - LOB"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
	| Scenario4  | Inland Marine - SMA          |
	| Scenario9  | SGS - Inland Marine          |
