Feature: A01_Policy_No_Assignmnt_Type_ReqOn_Bound


@Sanity @Regression
Scenario: For "Global" Implementation: Check fields if null before going to Quoted or Bound Submission of Type New/Renewal

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'

# Stage Progression to Bound
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Bound"

#  Validation : A01_Policy_No_Assignmnt_Type_ReqOn_Bound
	Then User Clicked on "Edit" button
	Then User Updated 'Policy Number - Assignment Type,Policy Number - Current' field with values '--None--,Blank'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Please ensure Policy Number and Policy Assignment Type have been populated; if they are and you're still experiencing issues; open a Remedy Ticket - ( Submission Details Tab )$Cannot change Policy Number, Issuing Company, Line of Business, Occupancy, Local Policy Number Current, Local Country and UDX Currency once the Stage has reached Bound. ( Submission Details Tab )"

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