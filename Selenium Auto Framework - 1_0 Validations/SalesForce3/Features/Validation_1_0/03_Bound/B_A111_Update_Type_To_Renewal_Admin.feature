Feature: B_A111_Update_Type_To_Renewal_Admin


@Sanity @Regression
Scenario: If issuing OR production office = Middle Market the picklist cannot be None 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"
	Then User Refresh the page

## Logout from URL
	Then User log out from User and Login as Administrator

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'
	Then User Refresh the page

# Validation : A111_Update_Type_To_Renewal_Admin
	Then User Clicked on "Update Submission Type to Renewal" button
	Then User clicked on Save button in Update submission to Renewal
	Then User Verify the error message for Submission Renewal as "Submission Type to Renewal Can only be Updated for New Submissions. If this is an error. Please Open a Remedy Ticket."

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
