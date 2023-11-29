Feature: A102_Require_Buenos_Aires_Fields


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

# Stage Progression to Bound
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User clicked on Save button in submission page
	Then User Verified submission record status changed to "Bound"

#  Validation : A102_Require_Buenos_Aires_Fields
	Then User Clicked on "Edit" button
	Then User Updated 'Client Name,Issuing Office,Licensed Producer' field with values 'Test_190,Buenos Aires,Aon Risk Solutions (München)'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Client's and Producer's C.U.I.T Numbers are required for Buenos Aires Issuing Office and Production Office before the Submission may be Bound."

Examples:
	| Scenarios  | Record Type                  |
	| Scenario2  | Global Construction          |
	| Scenario3  | Global Offshore              |
	| Scenario5  | International Onshore        |
	| Scenario6  | International Property       |
	| Scenario7  | SGS - Construction           |
	| Scenario8  | SGS - General Property       |
	| Scenario10 | SGS - Onshore                |

