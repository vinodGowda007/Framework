Feature: A104_Starr_EPIGWP_Equal_FAC_GrossPremium

@Sanity @Regression
Scenario: 1 If “Program” = Onshore Fronted, Construction Fronted, or Property Fronted that Starr EPI/GWP has to equal FAC Gross Premium within $5 range this should cover any rounding issues. 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Quoted'
	Then User Refresh the page
# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

# Validation : A104_Starr_EPIGWP_Equal_FAC_GrossPremium
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Program,Purchased External FAC Reinsurance,FAC Basis of Acceptance,FAC RI PRM,Policy Ceding Commission Amount' field with values 'Onshore Fronted,No,Random,0,12345'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Please check FAC Premium; Submission is marked as a Fronted account. If error persists, open a Remedy Ticket - Field ( FAC Reinsurance Gross Premium - Policy Information Tab )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |
	| Scenario5  | International Onshore        | 


	@Sanity @Regression
Scenario: 2 If “Program” = Onshore Fronted, Construction Fronted, or Property Fronted that Starr EPI/GWP has to equal FAC Gross Premium within $5 range this should cover any rounding issues. 1.0

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Quoted'
	Then User Refresh the page

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Quoted'
	Then User Refresh the page

# Validation : A104_Starr_EPIGWP_Equal_FAC_GrossPremium
	Then User capture the value of 'Starr EPI/GWP'
	Then User Clicked on "Edit" button
	When User performed Stage progression from "Quoted" to "Bound"
	Then User Updated 'Program,Purchased External FAC Reinsurance,FAC Basis of Acceptance,FAC RI PRM,Policy Ceding Commission Amount' field with values 'Property Fronted,No,Random,0,12345'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "Please check FAC Premium; Submission is marked as a Fronted account. If error persists, open a Remedy Ticket - Field ( FAC Reinsurance Gross Premium - Policy Information Tab )$Cannot change Policy Number, Issuing Company, Line of Business, Occupancy, Local Policy Number Current, Local Country and UDX Currency once the Stage has reached Bound. ( Submission Details Tab )"

Examples:
	| Scenarios  | Record Type                  |
	| Scenario6  | International Property       | 



