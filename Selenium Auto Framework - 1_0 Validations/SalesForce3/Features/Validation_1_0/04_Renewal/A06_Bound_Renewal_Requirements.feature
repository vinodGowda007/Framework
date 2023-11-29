Feature: A06_Bound_Renewal_Requirements


@Sanity @Regression
Scenario: 	Please enter a value for the Current and Expiring Policy Number and Expired EPI fields and Ultimate Loss Ratio. ( Submission Details Tab )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# Creation of Submission Record
	Then User Refresh the page
	Then User Navigated to 'Submissions' Page
	Then User Clicked on "New" button
	Then User selected the record type as "Record Type 1.0"
	Then User fill the data in Submission version one creation record for 'Assumed'
	Then User clicked on Save button in submission page
	Then User verify the created submission record for 'Validation' for 'Renewal' Stage
	Then User Refresh the page

# Ice Check 
	Then User verify the Ice Check status 'Before' performing Ice Check action
	Then User Clicked on "Ice Check" button
	Then User clicked on Refresh button in Submission page
	Then User Refresh the page
	Then User verify the Ice Check status 'After' performing Ice Check action

# Licence Check
	Then User Clicked on "License Check" button
	Then Clicked on Close button during Licence Check
	Then Verify Licence Check status is updated for 'Submission1_0'
	Then User Refresh the page
	Then User Refresh the page

# Attach Assumed Insurer into Submission
	Then User Navigated to "Policy Info" Tab
	Then User Clicked on Click Here link to Add Assumed Insurers
	Then User Selected the Assumed Insurer
	Then User Clicked on Save button in Add Assumed Insurer
	Then User selected the value in the dropdown
	Then User Refresh the page
	Then User Navigated to "Policy Info" Tab
	Then User verify the added Assumed Insurer in Submission
	Then User Refresh the page

# Logout from User
	Then User log out from User and Login as Administrator
#Home Page-SF
	When User Navigated to "Incumbent Insurers"

#Incumbent Insurer PAGE - CREATE Incumbent Insurer RECORD
	Then User clicked on New button in Incumbent Insurers Page
	#Then User Fill the value Incumbent Insurers creation page for 'New' submission
	Then User Fill the value Incumbent Insurers creation page for 'New' submission for "Renewal"
	Then User clicked on Save button in Incumbent Insurers Page
	Then Verify New Incumbent Insurer record is created successfully
	Then Verify Added Incumbent Insurer in Submission record in '1.0'
	Then User Refresh the page

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	Then User enter the "Under Writer" Username

# User Navigated to Created submission record
		Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Renewal'
	Then User Refresh the page

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

# Stage Progression to Bound
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Bound"
	Then User Refresh the page

# Create Renewal Submission	
	Then User Clicked on "Submission Renewal" button
	And User Fill the values in Renewal submission creation page
	Then User clicked on Save button in Renewal Submission creation page
	Then User verified Renewal submission creation
	Then User Refresh the page

# Ice Check 
	Then User verify the Ice Check status 'Before' performing Ice Check action
	Then User Clicked on "Ice Check" button
	Then User clicked on Refresh button in Submission page
	Then User Refresh the page
	Then User verify the Ice Check status 'After' performing Ice Check action

# Licence Check
	Then User Clicked on "License Check" button
	Then Clicked on Close button during Licence Check
	Then Verify Licence Check status is updated for 'Submission1_0'
	Then User Refresh the page
	Then User Refresh the page

# Logout from User
	Then User log out from User and Login as Administrator
#Home Page-SF
	When User Navigated to "Incumbent Insurers"

#Incumbent Insurer PAGE - CREATE Incumbent Insurer RECORD
	Then User clicked on New button in Incumbent Insurers Page
	Then User Fill the value Incumbent Insurers creation page for 'Renewal' submission for "Renewal"
	Then User clicked on Save button in Incumbent Insurers Page
	Then Verify New Incumbent Insurer record is created successfully
	Then Verify Added Incumbent Insurer in Submission record in '1.0'

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	Then User enter the "Under Writer" Username

# User Navigated to Created submission record
	Then User Navigated to the created submission Renewal 1.0 record for 'Validation'
	Then User Refresh the page

# Stage progression Working
	Then User Selected the Stage as 'Working' from View Mode and Clicked on Mark as current stage button

# Verify Submission Working
	Then User Verified submission record status changed to "Working"

# Stage progression Quoted
	Then User Selected the Stage as 'Quoted' from View Mode and Clicked on Mark as current stage button

# Verify Submission Status Quoted
	Then User Verified submission record status changed to "Quoted"
	Then User Refresh the page

# Stage Progression to Bound
#  Validation : A06_Bound_Renewal_Requirements
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated '*Policy Number - Expiring' field with values 'Blank'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Please enter a value for the Current and Expiring Policy Number and Expired EPI fields and Ultimate Loss Ratio. ( Submission Details Tab )"
	

Examples:
	| Scenarios  | Record Type                  |
	| Scenario4  | Inland Marine - SMA          |
	| Scenario9  | SGS - Inland Marine          |
