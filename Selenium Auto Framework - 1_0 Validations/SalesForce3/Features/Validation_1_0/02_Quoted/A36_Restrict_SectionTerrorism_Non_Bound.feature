Feature: A36_Restrict_SectionTerrorism_Non_Bound

@Sanity @Regression
Scenario: Carrier Branch is Required for this Issuing Office or Production Office ( Submission Details Tab )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Quoted'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'
	Then User Refresh the page

	Then User Clicked on "Edit" button
	Then User Updated 'Client Name' field with values 'Previous Value'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Verifying Error mesages for SECTION
	Then User Clicked on "New Section" button
	Then Verify the Error messages "Section can be created on a New or Renewal Type submission, in Bound Stage Only."
	Then User Refresh the page

# Verifying Error mesages for Terrorism
	Then User Clicked on "New Terrorism" button
	Then Verify the Error messages "Terrorism can be created on a New or Renewal Type submission, in Bound Stage Only."
	Then User Refresh the page

# Stage progression Working
	Then User Selected the Stage as 'Working' from View Mode and Clicked on Mark as current stage button

# Verify Submission Working
	Then User Verified submission record status changed to "Working"

# Verifying Error mesages for SECTION
	Then User Clicked on "New Section" button
	Then Verify the Error messages "Section can be created on a New or Renewal Type submission, in Bound Stage Only."
	Then User Refresh the page

# Verifying Error mesages for Terrorism
	Then User Clicked on "New Terrorism" button
	Then Verify the Error messages "Terrorism can be created on a New or Renewal Type submission, in Bound Stage Only."
	Then User Refresh the page

# Stage progression Quoted
	Then User Selected the Stage as 'Quoted' from View Mode and Clicked on Mark as current stage button

# Verify Submission Status Quoted
	Then User Verified submission record status changed to "Quoted"

# Verifying Error mesages for SECTION
	Then User Clicked on "New Section" button
	Then Verify the Error messages "Section can be created on a New or Renewal Type submission, in Bound Stage Only."
	Then User Refresh the page

# Verifying Error mesages for Terrorism
	Then User Clicked on "New Terrorism" button
	Then Verify the Error messages "Terrorism can be created on a New or Renewal Type submission, in Bound Stage Only."

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