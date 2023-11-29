Feature: A03_Choose_Carrier_Branch

@Sanity @Regression
Scenario: Carrier Branch is Required for this Issuing Office or Production Office ( Submission Details Tab )

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

# Update Issuing Company
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Company' field with values 'Starr Indemnity & Liability Company'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Validation : A03_Choose_Carrier_Branch
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Working" to "Quoted"
	Then User Updated 'Issuing Office,Carrier Branch' field with values 'Lima,--None--'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Carrier Branch is Required for this Issuing Office or Production Office ( Submission Details Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Update Issuing Company
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Company' field with values 'CVS 1919'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Validation : A03_Choose_Carrier_Branch
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Working" to "Quoted"
	Then User Updated 'Issuing Office,Carrier Branch' field with values 'Singapore,--None--'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Carrier Branch is Required for this Issuing Office or Production Office ( Submission Details Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Update Issuing Company
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Company' field with values 'Starr Insurance & Reinsurance Limited'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Validation : A03_Choose_Carrier_Branch
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Working" to "Quoted"
	Then User Updated 'Issuing Office,Carrier Branch' field with values 'Toronto,--None--'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Carrier Branch is Required for this Issuing Office or Production Office ( Submission Details Tab )"


Examples:
	| Scenarios  | Record Type                  |
	| Scenario2  | Global Construction          |
	| Scenario3  | Global Offshore              |
	| Scenario5  | International Onshore        |
	| Scenario6  | International Property       |
	| Scenario12 | SCI - Crisis Management      |
	| Scenario13 | SCI - Cyber                  |
	| Scenario14 | SCI - Environmental          |
	| Scenario15 | SCI - Financial Lines        |
	| Scenario17 | SCI - Healthcare             |


# -----------------------------------------------------------------------------------------------------------------------------------------------
@Sanity @Regression
Scenario: 2 Carrier Branch is Required for this Issuing Office or Production Office ( Submission Details Tab )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Quoted'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Stage progression Working
	Then User Selected the Stage as 'Working' from View Mode and Clicked on Mark as current stage button

# Verify Submission Working
	Then User Verified submission record status changed to "Working"

# Update Issuing Company
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Company' field with values 'Starr Indemnity & Liability Company'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Validation : A03_Choose_Carrier_Branch
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Working" to "Quoted"
	Then User Updated 'Issuing Office,Carrier Branch' field with values 'Atlanta,--None--'
	Then User Updated 'UW Region,Production Office' field with values 'Latin America,Santiago'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Carrier Branch is Required for this Issuing Office or Production Office ( Submission Details Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

	# Update Issuing Company
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Company' field with values 'CVS 1919'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Validation : A03_Choose_Carrier_Branch
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Working" to "Quoted"
	Then User Updated 'Issuing Office,Carrier Branch' field with values 'Atlanta,--None--'
	Then User Updated 'UW Region,Production Office' field with values 'Australasia,Hong Kong'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Carrier Branch is Required for this Issuing Office or Production Office ( Submission Details Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Update Issuing Company
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Company' field with values 'Starr Insurance & Reinsurance Limited'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Validation : A03_Choose_Carrier_Branch
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Working" to "Quoted"
	Then User Updated 'Issuing Office,Carrier Branch' field with values 'Atlanta,--None--'
	Then User Updated 'UW Region,Production Office' field with values 'North America,Toronto'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Carrier Branch is Required for this Issuing Office or Production Office ( Submission Details Tab )"


Examples:
	| Scenarios  | Record Type                  |
	| Scenario2  | Global Construction          |
	| Scenario5  | International Onshore        |	
	| Scenario12 | SCI - Crisis Management      |
	| Scenario13 | SCI - Cyber                  |
	| Scenario14 | SCI - Environmental          |
	| Scenario15 | SCI - Financial Lines        |
	| Scenario16 | SCI - GL Misc                |
	| Scenario17 | SCI - Healthcare             |