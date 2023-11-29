Feature: A139_Cancelled_Stage_Cannot_be_Modified

@Sanity @Regression
Scenario: Local Policy Number - Current cannot be modified in Bound or Cancelled Stages. 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'

# User update Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Issued Date' field with values 'Date'
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
	Then User Clicked on "Edit" button
	Then User Updated 'Issuing Office,Local Policy Number – Current' field with values 'Atlanta,Random'
	Then User clicked on Save button in submission page
	Then User Refresh the page

# Stage Progression to Bound
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Bound"
	Then User Refresh the page

# Validation : A139_Cancelled_Stage_Cannot_be_Modified
	Then User Clicked on "Edit" button
	Then User Updated 'Local Policy Number – Current' field with values '12345678'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Local Policy Number - Current cannot be modified in Bound or Cancelled Stages."
	Then User Clicked on Cancel button
	Then User Refresh the page

# Endorsement creation 
	Then User Clicked on "New Endorsements" button
	And User Fill the values in Endorsement creation page for "Endorsement Cancellation"
	Then User Updated 'FAC RI PRM In Child Records' field
	Then User clicked on Save button in Endorsement page


# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Validation : A139_Cancelled_Stage_Cannot_be_Modified
	Then User Clicked on "Edit" button
	Then User Updated 'Local Policy Number – Current' field with values '12345678'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Local Policy Number - Current cannot be modified in Bound or Cancelled Stages."
	Then User Clicked on Cancel button
	Then User Refresh the page

# Logout from admin
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

Examples:
	| Scenarios  |
	| Scenario1  |
	| Scenario2  |
	| Scenario3  |
	| Scenario4  |
	| Scenario5  |
	| Scenario6  |
	| Scenario7  |
	| Scenario8  |
	| Scenario9  |
	| Scenario10 |
	| Scenario11 |
	| Scenario12 |
	| Scenario13 |
	| Scenario14 |
	| Scenario15 |
	| Scenario16 |
	| Scenario17 |