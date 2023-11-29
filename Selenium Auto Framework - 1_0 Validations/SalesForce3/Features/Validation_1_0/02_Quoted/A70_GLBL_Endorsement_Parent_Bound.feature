Feature: A70_GLBL_Endorsement_Parent_Bound

@Sanity @Regression
Scenario:  Parent Submission must be "bound" for an Endorsement Submission.

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

# Validation : A70_GLBL_Endorsement_Parent_Bound
	Then User Clicked on "New Endorsements" button
	Then Verify the Error messages "Endorsement can be created on a Bound Submission only"


Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
	| Scenario4  | Inland Marine - SMA          |
	| Scenario9  | SGS - Inland Marine          |
	| Scenario11 | STNA - Starr Specialty Lines |
