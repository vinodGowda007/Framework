Feature: A99_Require_Santiago_Fields


@Sanity @Regression
Scenario: Require LATAM Risk code, Client's and Producer's C.U.I.T Number if Issuing or Production Office is Buenos Aires. 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

#  Validation : A01_Policy_No_Assignmnt_Type_ReqOn_Bound
	#Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	Then User Updated 'Licensed Producer' field with values 'Aon Risk Solutions (München)'
	Then User clicked on Save button in submission page
	Then User Refresh the page

	# Licence Check
	Then User Clicked on "License Check" button
	Then Clicked on Close button during Licence Check
	Then Verify Licence Check status is updated for 'Submission1_0'
	Then User Refresh the page

#  Validation : A01_Policy_No_Assignmnt_Type_ReqOn_Bound
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Issuing Office' field with values 'Santiago'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "LATAM Risk Code, Producers Broker RUT and the Client's RUT Number are required for Santiago Issuing Office and Production Office before the Submission may be Bound. LATAM Risk Code : Policy Info Tab // RUT Number : Client/Producer"
	Then User Clicked on Cancel button
	Then User Refresh the page

#  Validation : A01_Policy_No_Assignmnt_Type_ReqOn_Bound
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Client Name,Issuing Office' field with values 'Test_190,Santiago'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "LATAM Risk Code, Producers Broker RUT and the Client's RUT Number are required for Santiago Issuing Office and Production Office before the Submission may be Bound. LATAM Risk Code : Policy Info Tab // RUT Number : Client/Producer"

Examples:
	| Scenarios  | Record Type                  |
	#| Scenario1  | STNA - Domestic Onshore      | This Validation is not reporducable for the remaining record type due to the active validation 'A175_Chile_trackingValidation'
	#| Scenario2  | Global Construction          |
	#| Scenario3  | Global Offshore              |
	#| Scenario4  | Inland Marine - SMA          |
	#| Scenario5  | International Onshore        |
	#| Scenario6  | International Property       |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	#| Scenario9  | SGS - Inland Marine          | Santiago IS NOT PRESENT IN THE PICKLIST *Issuing Office
	| Scenario10 | SGS - Onshore                |
	#| Scenario11 | STNA - Starr Specialty Lines |
	#| Scenario12 | SCI - Crisis Management      |
	#| Scenario13 | SCI - Cyber                  |
	#| Scenario14 | SCI - Environmental          |
	#| Scenario15 | SCI - Financial Lines        |
	#| Scenario16 | SCI - GL Misc                |
	#| Scenario17 | SCI - Healthcare             |