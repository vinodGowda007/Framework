Feature: A115_LOB_Occ_NID_Camilion_Crt_date

@Sanity @Regression
Scenario: 01 SCI record types cannot be update the LOB or Occupancy if the Camilion Created Date is not Populated. It does not depend on bound or Renewal 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# User update Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Created Date' field with values 'Date'
	Then User clicked on Save button in submission page

# Login as Underwriter
	Given UW Navigate To Salesforce Application
	Then User enter the "Under Writer" Username

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule : A115_LOB_Occ_NID_Camilion_Crt_date
	Then User Clicked on "Edit" button
	Then User Updated 'Line of Business,Occupancy,Subclass 1' field with values 'Random,Random,MAJOR'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "LOB and Occupancy, Subclass1 and Network Identification, Local Policy Number Current cannot be updated as the Submission has already been imported into Camilion. If this is a valid change, please open a Remedy Ticket."
	Then User Clicked on Cancel button
	Then User Refresh the page

# Logout from admin
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# RESET Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Created Date' field with values 'Blank'
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




	@Sanity @Regression
	Scenario: 02 SCI record types cannot be update the LOB or Occupancy if the Camilion Created Date is not Populated. It does not depend on bound or Renewal 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# User update Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Created Date' field with values 'Date'
	Then User clicked on Save button in submission page

# Login as Underwriter
	Given UW Navigate To Salesforce Application
	Then User enter the "Under Writer" Username

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'	

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule : A115_LOB_Occ_NID_Camilion_Crt_date
	Then User Clicked on "Edit" button
	Then User Updated 'Network Identification (NID)' field with values 'Random1234'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "LOB and Occupancy, Subclass1 and Network Identification, Local Policy Number Current cannot be updated as the Submission has already been imported into Camilion. If this is a valid change, please open a Remedy Ticket."
	Then User Clicked on Cancel button
	Then User Refresh the page

# Logout from admin
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# RESET Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Created Date' field with values 'Blank'
	Then User clicked on Save button in submission page
	Then User Refresh the page


Examples:
	| Scenarios  | Record Type                  |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	| Scenario9  | SGS - Inland Marine          |
	| Scenario10 | SGS - Onshore                |