Feature: A33_Proj_Name_Check_On_Bound


@Sanity @Regression
Scenario: For "Global" Implementation: Submission Type New/Renewal Check fields if null before going to Quoted or Bound  

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Bound'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

#  Validation : A33_Proj_Name_Check_On_Bound
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Project Name' field with values 'Blank'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "When Stage is moved to Bound, either Project Name has to be selected or the checkbox Check if no Project Name has to be checked. ( Submission Details Tab )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario2  | Global Construction          |
	| Scenario4  | Inland Marine - SMA          |
	| Scenario7  | SGS - Construction           |
	| Scenario9  | SGS - Inland Marine          |

