Feature: A149_Local_Client_Name_Cannot_Be_Changed

@Sanity @Regression
Scenario: If Type = New/Renewal/Section and the camilion created date should not be blank or If type = Endorsement/Terrorism and stage =bound,then while changing the stage value, we will receive the error message.1.0 changed based on defect 146314

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# User update Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Created Date' field with values 'Date'
	Then User clicked on Save button in submission page

# Login as Underwriter
	Given UW Navigate To Salesforce Application
	Then User enter the "Under Writer" Username

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

# Validation Rule : A149_Local_Client_Name_Cannot_Be_Changed
	Then User Clicked on "Edit" button
	Then User Updated 'Local Client Name' field with values 'Client Name'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Local Client Name Cannot be Updated; Please Open a Remedy Ticket"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Endorsement creation 
	Then User Clicked on "New Endorsements" button
	And User Fill the values in Endorsement creation page for "Endorsement"
	Then User Updated 'FAC RI PRM In Child Records' field
	Then User clicked on Save button in Endorsement page
	Then User Refresh the page
		
#  Validation : A149_Local_Client_Name_Cannot_Be_Changed
	Then User Clicked on "Edit" button
	Then User Updated 'Local Client Name' field with values 'Client Name'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Local Client Name Cannot be Updated; Please Open a Remedy Ticket"
	Then User Clicked on Cancel button
	Then User Refresh the page

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Section creation 
	Then User Clicked on "New Section" button
	And User Fill the values in Section creation page
	Then User Updated 'FAC RI PRM In Child Records' field
	Then User clicked on save button for validation
	Then User Refresh the page
		
#  Validation : A149_Local_Client_Name_Cannot_Be_Changed
	Then User Clicked on "Edit" button
	Then User Updated 'Local Client Name' field with values 'Client Name'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Local Client Name Cannot be Updated; Please Open a Remedy Ticket"
	Then User Clicked on Cancel button
	Then User Refresh the page

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Terrorism creation 
	Then User Clicked on "New Terrorism" button
	And User Fill the values in Terrorism creation page
	Then User Updated 'FAC RI PRM In Child Records' field
	Then User clicked on save button for validation
	Then User Refresh the page
		
#  Validation : A149_Local_Client_Name_Cannot_Be_Changed
	Then User Clicked on "Edit" button
	Then User Updated 'Local Client Name' field with values 'Client Name'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Local Client Name Cannot be Updated; Please Open a Remedy Ticket"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Logout from admin
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'

# RESET Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Created Date' field with values 'Blank'
	Then User clicked on Save button in submission page

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