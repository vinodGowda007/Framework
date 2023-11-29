Feature: A04_Submission_Comments_cannot_be_Blank

@Sanity @Regression
Scenario: Submission Comments cannot be Blank when the Reason is one of the following (Coverage, Other, Terms/Conditions)

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

# Validation Rule : A04_Submission_Comments_cannot_be_Blank
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Lost/NTU"
	Then User Updated 'LostNTU or Declined Reason,Submission Comments' field with values 'Coverage (Provide Details in Submission Comments Section),Null'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Submission Comments cannot be Blank when the Reason is either Coverage or Other or Terms/Conditions ( Submission Details Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Validation Rule : A04_Submission_Comments_cannot_be_Blank
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Lost/NTU"
	Then User Updated 'LostNTU or Declined Reason,Submission Comments' field with values 'Other (Provide Details in Submission Comments Section),Null'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Submission Comments cannot be Blank when the Reason is either Coverage or Other or Terms/Conditions ( Submission Details Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Validation Rule : A04_Submission_Comments_cannot_be_Blank
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Lost/NTU"
	Then User Updated 'LostNTU or Declined Reason,Submission Comments' field with values 'Terms/Conditions (Provide details in Submission Comments section),Null'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Submission Comments cannot be Blank when the Reason is either Coverage or Other or Terms/Conditions ( Submission Details Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page
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