Feature: A143_SCI_Subclass1_required

@Sanity @Regression
Scenario: SCI - Crisis Management - Please choose proper Subclass1 for the selected Line of Business, Check Parent Submission if it is an Endorsement or Check Expiring Submission if it is a Renewal. ( Subclass1 - Details Tab )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# Adding Camilion Created functionality here since previous Testcases related to camilion date is failed then following TC will fail to avoid adding these peace of code
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# User update Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Created Date' field with values 'Blank'
	Then User clicked on Save button in submission page

# Login as Underwriter
	Given UW Navigate To Salesforce Application
	Then User enter the "Under Writer" Username

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule : A143_SCI_Subclass1_required
	Then User Clicked on "Edit" button
	Then User Updated 'Line of Business,Issuing Office,Subclass 1' field with values 'Crisis Management,Random,NON-MAJOR'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Please choose proper Subclass1 for the selected Line of Business, Check Parent Submission if it is an Endorsement or Check Expiring Submission if it is a Renewal. ( Subclass1 - Details Tab )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario12 | SCI - Crisis Management      |

	# -----------------------------------------------------------------------------------------------------------------
	@Sanity @Regression
Scenario:  SCI - Cyber  - Please choose proper Subclass1 for the selected Line of Business, Check Parent Submission if it is an Endorsement or Check Expiring Submission if it is a Renewal. ( Subclass1 - Details Tab )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# Adding Camilion Created functionality here since previous Testcases related to camilion date is failed then following TC will fail to avoid adding these peace of code
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# User update Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Created Date' field with values 'Blank'
	Then User clicked on Save button in submission page

# Login as Underwriter
	Given UW Navigate To Salesforce Application
	Then User enter the "Under Writer" Username

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule : A143_SCI_Subclass1_required
	Then User Clicked on "Edit" button
		Then User Updated 'Line of Business,Issuing Office,Subclass 1' field with values 'Cyber,Random,NON-MAJOR'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Please choose proper Subclass1 for the selected Line of Business, Check Parent Submission if it is an Endorsement or Check Expiring Submission if it is a Renewal. ( Subclass1 - Details Tab )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario13 | SCI - Cyber                  |
# -------------------------------------------------------------------------------------------------------------------------------------------------
	@Sanity @Regression
Scenario: SCI - Environmental  - Please choose proper Subclass1 for the selected Line of Business, Check Parent Submission if it is an Endorsement or Check Expiring Submission if it is a Renewal. ( Subclass1 - Details Tab )

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# Adding Camilion Created functionality here since previous Testcases related to camilion date is failed then following TC will fail to avoid adding these peace of code
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# User update Camilion Created Date
	Then User Clicked on "Edit" button
	Then User Updated 'Camilion Created Date' field with values 'Blank'
	Then User clicked on Save button in submission page

# Login as Underwriter
	Given UW Navigate To Salesforce Application
	Then User enter the "Under Writer" Username

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule : A143_SCI_Subclass1_required
	Then User Clicked on "Edit" button
	Then User Updated 'Line of Business,Issuing Office,Subclass 1' field with values 'Environmental Liability,Atlanta,MAJOR'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Please choose proper Subclass1 for the selected Line of Business, Check Parent Submission if it is an Endorsement or Check Expiring Submission if it is a Renewal. ( Subclass1 - Details Tab )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario14 | SCI - Environmental          |
# -----------------------------------------------------------------------------------------------------------------