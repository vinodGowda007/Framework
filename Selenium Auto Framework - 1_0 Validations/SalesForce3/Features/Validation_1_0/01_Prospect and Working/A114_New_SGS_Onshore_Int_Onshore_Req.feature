Feature: A114_New_SGS_Onshore_Int_Onshore_Req

@Sanity @Regression
	Scenario: From 06-01-2020 Occupancy on International Property should be filled and If the Production Office is in LATAM, the LOB should be General Property 1.0

	#LOGIN PAGE-SF
	Given UW Navigate To Salesforce Application
	When UW enters the login credentials in single sign in to test <Scenarios> using "Under Writer"

# User Navigated to Created submission record
	Then User Navigated to the created submission 1.0 record for 'Validation' for the stage 'Prospect'

# Reset Submission Record Stage to Prospect
	Then User Reset the Record stage to 'Prospect'

	# Validation Rule : A114_New_SGS_Onshore_Int_Onshore_Req
	Then User Clicked on "Edit" button
	Then User Updated 'Line of Business,Issuing Office,Occupancy' field with values 'General Property,Atlanta,Communications'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "General Property LOB has been selected; please use International Property Record Type. ( Submission Details Tab )"
	Then User Clicked on Cancel button
	Then User Refresh the page

	# Validation Rule : A114_New_SGS_Onshore_Int_Onshore_Req
	Then User Clicked on "Edit" button
	Then User Updated 'Line of Business,Issuing Office,Occupancy,UW Region,Production Office' field with values 'General Property,Bogota,Communications,Asia,Random'
	Then User clicked on Save button during Edit Mode in Stage Progression for backtracking
	Then Verify the Error messages for Backtracking as "General Property LOB has been selected; please use International Property Record Type. ( Submission Details Tab )"


	Examples:
	| Scenarios  | Record Type                  |
	| Scenario5  | International Onshore        |
	#| Scenario10 | SGS - Onshore                |  General Property Value is not available for this record type
