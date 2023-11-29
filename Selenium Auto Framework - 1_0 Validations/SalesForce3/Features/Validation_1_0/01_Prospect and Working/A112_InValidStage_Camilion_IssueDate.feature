Feature: A112_InValidStage_Camilion_IssueDate

@Sanity @Regression
Scenario: If the Camilion Issue Date is Populated, Stage cannot be Void, Lost/NTU, Declined.1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# User update Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Issued Date' field with values 'Date'
	Then User clicked on Save button in submission page

# Login as Underwriter
	Given UW Navigate To Salesforce Application
	Then User enter the "Under Writer" Username

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'	

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule : A112_InValidStage_Camilion_IssueDate
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Prospect" to "Void"
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Not a valid stage as this policy appears to have already been issued in Camilion. If this is an error, please open a Remedy Ticket"
	Then User Clicked on Cancel button
	Then User Refresh the page

# Logout from admin
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# RESET Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Issued Date' field with values 'Blank'
	Then User clicked on Save button in submission page
	Then User Refresh the page


Examples:
	| Scenarios  | Record Type                  |
	| Scenario12 | SCI - Crisis Management      |
	| Scenario13 | SCI - Cyber                  |
	| Scenario14 | SCI - Environmental          |
	| Scenario15 | SCI - Financial Lines        |
	| Scenario16 | SCI - GL Misc                |
	| Scenario17 | SCI - Healthcare             |