Feature: A02_External_FAC_Reinsurance_Reqd_Fields

@Sanity @Regression
Scenario: HPQC 454 / 802 - make Basis Of Acceptance, and Policy Ceding Commission Amount, FAC Reinsurance Gross Premium required when FAC Reinsurance is set to Yes.

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule : A02_External_FAC_Reinsurance_Reqd_Fields
	Then User Clicked on "Edit" button
	Then User Updated 'FAC Basis of Acceptance,FAC RI PRM,Policy Ceding Commission Amount' field
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "When Purchased External FAC Reinsurance is Yes, following fields cannot be blank. - Basis Of Acceptance - Policy Ceding Commission Amount - FAC Reinsurance Gross Premium"

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