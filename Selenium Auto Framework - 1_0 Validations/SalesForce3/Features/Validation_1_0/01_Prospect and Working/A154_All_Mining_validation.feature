Feature: A154_All_Mining_validation

@Sanity @Regression
Scenario: All Mining policies should be booked under the International Onshore Record Type. If you do not have access please open a Remedy Ticket.

#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

# Validation Rule : A154_All_Mining_validation
	Then User Clicked on "Edit" button
	Then User Updated 'Occupancy,Subclass 1' field with values 'Mining,SPEC/MINING - ABOVE'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "All Mining policies should be booked under the International Onshore Record Type. If you do not have access please open a Remedy Ticket."

Examples:
	| Scenarios  | Record Type                  |
	| Scenario1  | STNA - Domestic Onshore      |