Feature: A132_Purchase_Ext_UCCB_No

@Sanity @Regression
Scenario: 01  If Purchase External UCB is No, FAC RI PRM UCB and Pol Ced Comm UCB should be blank 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule : A132_Purchase_Ext_UCCB_No
	Then User Clicked on "Edit" button
	Then User Updated 'Purchase External FAC (UCB),FAC RI PRM (UCB),Policy Ceding Commission (UCB)' field
	Then User Updated 'Underwriting Credit Branch' field with values 'Reset Picklist'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Underwriting Credit Branch is required if UCB Share % OR Purchase External FAC UCB is Yes/No OR if the production office or issuing office is Middle Market ( SGS Tab )"


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